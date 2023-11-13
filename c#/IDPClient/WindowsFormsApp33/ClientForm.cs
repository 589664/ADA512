using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Wpf;


namespace IDPClient
{
    public partial class ClientForm : Form
    {

        // UDP/TCP communication settings
        string serverIP = "84.215.100.38";      // Server IP address
        int UDPport = 12345;                    // UDP Server port number
        int TCPport = 54321;                    // TCP Server port number
        UdpClient udpClient;                    // UDP client for communication
        IPEndPoint serverEndPoint;              // Server endpoint
        private TcpClient tcpClient;            // TCP client for communication
        private NetworkStream networkStream;    // Network stream for TCP communication

        // PID controller settings
        // PID controller settings
        int time = 50;                          // Timer interval in milliseconds
        int delay = 1;                          // Delay in milliseconds
        double setpoint = 40.0;                 // Desired temperature setpoint
        double outputLoc, outputPID;            // Control outputs
        double temperatLoc = 40.0;              // Local temperature
        double temperatPID;                     // PID-controlled temperature
        double kp = 0.1;                        // Proportional constant
        double ki = 0.01;                       // Integral constant
        double kd = 0.01;                       // Derivative constant
        double integral;                        // Integral sum
        double prevError;                       // Previous error
        int countLOC = 0, countPID = 0;         // Iteration counters

        // Variables to track transmission speed
        double udpTransmissionSpeed = 0.0;                                  // Speed in bytes per second for UDP
        double tcpTransmissionSpeed = 0.0;                                  // Speed in bytes per second for TCP
        DateTime udpLastTransmissionTime = DateTime.UtcNow;
        DateTime tcpLastTransmissionTime = DateTime.UtcNow;

        private Random randomNR = new Random();                             // Random number generator

        // LiveCharts setup
        private SeriesCollection seriesCollection;                          // Collection of chart series
        private SeriesCollection transmissionSeriesCollection;              // Collection of transmissionRate series
        private readonly LineSeries setpointSeries = new LineSeries
        {
            Title = "Setpoint",
            Values = new ChartValues<double> { 40,40 },
            Fill = Brushes.Transparent,
            StrokeThickness = 1,
            PointGeometry = null,
        };
        private LineSeries temperatLocSeries = new LineSeries
        {
            Title = "TemperatLoc",
            Values = new ChartValues<double> { 40,40 },
            Fill = Brushes.Transparent,
            StrokeThickness = 1,
            PointGeometry = null,
        };
        private LineSeries temperatPIDSeries = new LineSeries
        {
            Title = "TemperatPID",
            Values = new ChartValues<double> { 40, 40 },
            Fill = Brushes.Transparent,
            StrokeThickness = 1,
            PointGeometry = null,
        };

        private LineSeries udpTransmissionSpeedSeries = new LineSeries
        {
            Title = "UDP Transmission Speed",
            Values = new ChartValues<double>(),
            Fill = Brushes.Transparent,
            StrokeThickness = 1,
            PointGeometry = null,
        };

        private LineSeries tcpTransmissionSpeedSeries = new LineSeries
        {
            Title = "TCP Transmission Speed",
            Values = new ChartValues<double>(),
            Fill = Brushes.Transparent,
            StrokeThickness = 1,
            PointGeometry = null,
        };



        public ClientForm()
        {
            InitializeComponent();

            // GUI setup
            setKP.Text = kp.ToString();
            setKI.Text = ki.ToString();
            setKD.Text = kd.ToString();
            ipAdress.Text = serverIP;

            // LiveCharts configuration
            seriesCollection = new SeriesCollection(setpointSeries);
            pidChart.Series = seriesCollection;
            transmissionSeriesCollection = new SeriesCollection(setpointSeries);
            transmissionChart.Series = transmissionSeriesCollection;

            // Initialize chart series
            seriesCollection.Add(setpointSeries);
            seriesCollection.Add(temperatLocSeries);
            seriesCollection.Add(temperatPIDSeries);
            transmissionSeriesCollection.Add(udpTransmissionSpeedSeries);
            transmissionSeriesCollection.Add(tcpTransmissionSpeedSeries);

            // Timer setup for periodic updates
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = time;
            timer.Tick += Activity;
            timer.Start();

        }

        private void Activity(object sender, EventArgs e)
        {
            // Run the Activity method on a separate thread
            Task.Run(() =>
            {
                // PID control logic
                double error = setpoint - temperatLoc;
                integral += error;
                double derivative = error - prevError;

                double differ = kp * error;
                double integ = ki * integral;
                double deriv = kd * derivative;

                deriv = deriv / 0.05;  // Check this value

                prevError = error;

                outputLoc = differ + integ + deriv;

                temperatLoc = temperatLoc + outputLoc;
                countLOC++;

                // Use BeginInvoke to update UI elements on the UI thread
                BeginInvoke((Action)(() =>
                {
                    // Update LiveCharts series
                    setpointSeries.Values.Add(setpoint);
                    temperatLocSeries.Values.Add(temperatLoc);
                    temperatPIDSeries.Values.Add(temperatPID);

                    // Remove old data to keep the chart clean
                    if (setpointSeries.Values.Count > 100)
                    {
                        setpointSeries.Values.RemoveAt(0);
                        temperatLocSeries.Values.RemoveAt(0);
                        temperatPIDSeries.Values.RemoveAt(0);
                    }

                    // Update the displayed iteration counts
                    textBox1.Text = countLOC.ToString();
                    textBox2.Text = countPID.ToString();
                }));
            });
        }


    private void UDProtocol()
    {
        while (true)
        {
            try
            {
                    //Send data to the server
                    string messageToSend = kp.ToString() + " " + ki.ToString() + " " + kd.ToString() + " " + setpoint.ToString() + " " + temperatPID.ToString();
                    byte[] messageBytes = Encoding.ASCII.GetBytes(messageToSend);
                    udpClient.Send(messageBytes, messageBytes.Length, serverEndPoint);

                    // Calculate UDP transmission speed
                    DateTime currentTime = DateTime.UtcNow;
                    double elapsedTimeSeconds = (currentTime - udpLastTransmissionTime).TotalSeconds;
                    udpTransmissionSpeed = messageBytes.Length / elapsedTimeSeconds;
                    udpLastTransmissionTime = currentTime;

                    // Display UDP transmission speed
                    BeginInvoke((Action)(() =>
                    {
                        udpTransmissionSpeedSeries.Values.Add(Math.Floor(udpTransmissionSpeed));
                    }));
                
                    // Receive data from the server
                    IPEndPoint serverResponseEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    byte[] receivedBytes = udpClient.Receive(ref serverResponseEndPoint);
                    string receivedMessage = Encoding.ASCII.GetString(receivedBytes);

                    temperatPID = Double.Parse(receivedMessage);
                    countPID++;

                    // Sleep for a while before the next iteration
                    Thread.Sleep(time * delay);
            }
            catch (Exception ee)
            {
                // Handle exceptions related to UDP communication
                udpClient = new UdpClient();
                serverEndPoint = new IPEndPoint(IPAddress.Parse(serverIP), UDPport);
            }
        }
    }

        private async Task TCPProtocol()
        {
            TcpClient tcpClient = null;
            NetworkStream networkStream = null;
            StreamReader reader = null;
            StreamWriter writer = null;

            try
            {
                // Connect to the server
                tcpClient = new TcpClient();
                await tcpClient.ConnectAsync(serverIP, TCPport);
                networkStream = tcpClient.GetStream();
                reader = new StreamReader(networkStream);
                writer = new StreamWriter(networkStream);

                // Continuously send data to the server
                while (true)
                {
                    string messageToSend = $"{kp} {ki} {kd} {setpoint} {temperatPID}";

                    // Send the message to the server
                    await writer.WriteLineAsync(messageToSend);
                    await writer.FlushAsync(); // Ensure the message is sent immediately

                    // Calculate TCP transmission speed
                    DateTime currentTime = DateTime.UtcNow;
                    double elapsedTimeSeconds = (currentTime - tcpLastTransmissionTime).TotalSeconds;
                    tcpTransmissionSpeed = messageToSend.Length / elapsedTimeSeconds;
                    tcpLastTransmissionTime = currentTime;

                    // Display TCP transmission speed on the UI thread
                    BeginInvoke((Action)(() =>
                    {
                        tcpTransmissionSpeedSeries.Values.Add(Math.Floor(tcpTransmissionSpeed));
                    }));

                    // Read data from the server
                    string receivedMessage = await reader.ReadLineAsync();

                    // Parse and process the received data
                    if (!string.IsNullOrEmpty(receivedMessage))
                    {
                        Console.WriteLine(receivedMessage);
                        temperatPID = double.Parse(receivedMessage);
                        countPID++;
                    }

                    // Sleep for a while before the next iteration
                    await Task.Delay(time * delay);
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
                // Handle exceptions related to TCP communication
            }
            finally
            {
                reader?.Close();
                writer?.Close();
                networkStream?.Close();
                tcpClient?.Close();
            }
        }


        private void startTransmission(object sender, EventArgs e)
        {
            if (radioButtonUDP.Checked)
            {
                // Start a separate task for UDP communication
                Task runUDP = Task.Run(() =>
                {
                    // UDP setup
                    udpClient = new UdpClient();
                    serverEndPoint = new IPEndPoint(IPAddress.Parse(serverIP), UDPport);
                    UDProtocol();
                });
            }
            else if (radioButtonTCP.Checked)
            {
                // Start a separate task for TCP communication
                Task runTCP = Task.Run(async () =>
                {
                    // TCP setup
                    tcpClient = new TcpClient();
                    networkStream = null;
                    await TCPProtocol();
                });
            }
        }

        private void randomValueButton(object sender, EventArgs e)
        {
            // Add a random value to the setpoint
            setpointSeries.Values.Add(randomNR.NextDouble()*100);
        }

        private void plus_Click(object sender, EventArgs e)
        {
            // Increase the setpoint value by 1
            setpoint += 1;
        }


        private void setKP_TextChanged(object sender, EventArgs e)
        {
            // Update the kp value when the text box changes
            kp = double.Parse(setKP.Text);
        }

        private void setKI_TextChanged(object sender, EventArgs e)
        {
            // Update the ki value when the text box changes
            ki = double.Parse(setKI.Text);
        }

        private void setKD_TextChanged(object sender, EventArgs e)
        {
            // Update the kd value when the text box changes
            kd = double.Parse(setKD.Text);
        }

        private void minus_Click(object sender, EventArgs e)
        {
            setpoint -= 1;
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void transmissionDelay_ValueChanged(object sender, EventArgs e)
        {
            delay = (int)transmissionDelay.Value;
        }

        private void ipAdress_TextChanged(object sender, EventArgs e)
        {
            serverIP = ipAdress.Text;
        }

        private void cartesianChart1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void transmissionChart_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}

