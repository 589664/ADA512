using System;
using System.Net;
//using System.Drawing;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Wpf;


namespace Assignment2_network_communications
{
    public partial class GraphForm : Form
    {

        // UDP/TCP communication settings
        string serverIP = "127.0.0.1";          // Server IP address
        int serverPort = 12345;                 // Server port number
        UdpClient udpClient;                    // UDP client for communication
        IPEndPoint serverEndPoint;              // Server endpoint
        TcpClient tcpClient;                    // TCP client for communication
        NetworkStream networkStream;            // Network stream for TCP communication


        // PID controller settings
        int time = 50;                          // Timer interval in milliseconds
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

        Random randomNR = new Random();         // Random number generator

        // LiveCharts setup
        private SeriesCollection seriesCollection;              // Collection of chart series
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


        public GraphForm()
        {
            InitializeComponent();

            // GUI setup
            setKP.Text = kp.ToString();
            setKI.Text = ki.ToString();
            setKD.Text = kd.ToString();

            // LiveCharts configuration
            seriesCollection = new SeriesCollection(setpointSeries);
            cartesianChart1.Series = seriesCollection;

            // Initialize chart series
            seriesCollection.Add(setpointSeries);
            seriesCollection.Add(temperatLocSeries);
            seriesCollection.Add(temperatPIDSeries);

            // Timer setup for periodic updates
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = time;
            timer.Tick += Activity;
            timer.Start();
        }

        private void Activity(object sender, EventArgs e)
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
            textBox1.Text=countLOC.ToString();
            textBox2.Text=countPID.ToString();
        }

        private void UDProtocol()
        {
            while (true)
            {
                try
                {
                    // Send data to the server
                    string text = kp.ToString() + " " + ki.ToString() + " " + kd.ToString() + " " + setpoint.ToString() + " " + temperatPID.ToString();
                    string messageToSend = text;
                    byte[] messageBytes = Encoding.ASCII.GetBytes(messageToSend);
                    udpClient.Send(messageBytes, messageBytes.Length, serverEndPoint);

                    // Receive data from the server
                    IPEndPoint serverResponseEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    byte[] receivedBytes = udpClient.Receive(ref serverResponseEndPoint);
                    string receivedMessage = Encoding.ASCII.GetString(receivedBytes);
                    
                    temperatPID = Double.Parse(receivedMessage);
                    countPID++;
                }
                catch (Exception ee)
                {
                    // Handle exceptions related to UDP communication
                    udpClient = new UdpClient();
                    serverEndPoint = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);
                }
                // Sleep for a while before the next iteration
                Thread.Sleep(time*2);
            }
        }

        void TCPProtocol()
        {
            try
            {
                // Connect to the server
                tcpClient = new TcpClient(serverIP, serverPort);
                networkStream = tcpClient.GetStream();

                // Send data to the server
                string text = kp.ToString() + " " + ki.ToString() + " " + kd.ToString() + " " + setpoint.ToString() + " " + temperatPID.ToString();
                byte[] messageBytes = Encoding.ASCII.GetBytes(text);
                networkStream.Write(messageBytes, 0, messageBytes.Length);

                // Receive data from the server
                byte[] receivedBytes = new byte[1024];
                int bytesRead = networkStream.Read(receivedBytes, 0, receivedBytes.Length);
                string receivedMessage = Encoding.ASCII.GetString(receivedBytes, 0, bytesRead);

                temperatPID = Double.Parse(receivedMessage);
                countPID++;

                // Sleep for a while before the next iteration
                Thread.Sleep(time * 2);
            }
            catch (Exception ee)
            {
                // Handle exceptions related to TCP communication
                tcpClient.Close();
            }
        }

        private void startTransmission(object sender, EventArgs e)
        {
            // Start a separate task for communication
            Task runUDP = Task.Run(() =>
            {
                // UDP setup
                udpClient = new UdpClient();
                serverEndPoint = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);
                UDProtocol();
            });

            Task runTCP = Task.Run(() =>
            {
                // TCP setup
                tcpClient = new TcpClient();
                networkStream = null;
                TCPProtocol();
            });
        }

        private void randomValueButton(object sender, EventArgs e)
        {
            // Add a random value to the setpoint
            setpointSeries.Values.Add(randomNR.NextDouble()*100);
        }

        private void chooseProtocol(object sender, EventArgs e)
        {

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

        //LABELS
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void label3_Click(object sender, EventArgs e)
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

