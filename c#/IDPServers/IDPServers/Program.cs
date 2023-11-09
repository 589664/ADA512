using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class IDPServers
{
    // Define PID-related variables
    int TID = 50;
    double setpoint = 40.0;
    double outputLoc, outputPID;

    double temperatLoc = 40.0;
    double temperatPID = 40.0;

    double kp = 0.1;
    double ki = 0.01;
    double kd = 0.01;

    double integral = 0;
    double prevError = 0;
    int countLOC = 0, countPID = 0;

    // Define UDP server-related variables
    int udpPort = 12345;
    UdpClient udpServer = new();

    // Define TCP server-related variables
    int tcpPort = 54321;

    static async Task Main()
    {
        Console.WriteLine("### IDP SERVERS ###");

        // Create an instance of the IDPServers class
        var idpServer = new IDPServers();

        // Initialize the UDP server
        idpServer.udpServer = new UdpClient(idpServer.udpPort);

        // Start UDP server as a task
        Task udpServerTask = idpServer.StartUDPServer();

        // Start TCP server as a task
        Task tcpServerTask = idpServer.StartTCPServer();

        Console.WriteLine("UDP Server on port: " + idpServer.udpPort);
        Console.WriteLine("TCP Server on port: " + idpServer.tcpPort);
        idpServer.PrintLocalIPAddresses();
        await idpServer.PrintPublicIPAddress();

        // Wait for both tasks to complete
        await Task.WhenAll(udpServerTask, tcpServerTask);
    }

    async Task StartUDPServer()
    {
        try
        {
            while (true)
            {
                UdpReceiveResult udpReceived = await Task.Run(() => udpServer.ReceiveAsync());

                IPEndPoint udpClientEndPoint = udpReceived.RemoteEndPoint;
                byte[] udpReceivedBytes = udpReceived.Buffer;

                string receivedMessage = Encoding.ASCII.GetString(udpReceivedBytes);

                string[] data = receivedMessage.Split(' ');

                Console.WriteLine("Received UDP data from " + udpClientEndPoint + ": " + receivedMessage);

                kp = double.Parse(data[0]);
                ki = double.Parse(data[1]);
                kd = double.Parse(data[2]);
                setpoint = double.Parse(data[3]);
                temperatPID = double.Parse(data[4]);

                // PID
                double error = setpoint - temperatPID;
                integral += error;
                double derivative = error - prevError;

                double differ = kp * error;
                double integ = ki * integral;
                double deriv = kd * derivative;

                prevError = error;

                outputPID = differ + integ + deriv;

                temperatPID += outputPID;

                string messageToSend = temperatPID.ToString();
                byte[] messageBytes = Encoding.ASCII.GetBytes(messageToSend);
                udpServer.Send(messageBytes, messageBytes.Length, udpClientEndPoint);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("UDP Server error: " + ex.Message);
        }
    }

    async Task StartTCPServer()
    {
        TcpListener tcpListener = new TcpListener(IPAddress.Any, tcpPort);
        tcpListener.Start();

        try
        {
            while (true)
            {
                TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();
                using NetworkStream networkStream = tcpClient.GetStream();
                StreamReader reader = new StreamReader(networkStream);
                StreamWriter writer = new StreamWriter(networkStream);
                writer.AutoFlush = true; // Enable auto-flushing

                while (true)
                {
                    string receivedMessage = await reader.ReadLineAsync();

                    if (string.IsNullOrEmpty(receivedMessage))
                    {
                        // The client closed the connection
                        break;
                    }

                    string[] data = receivedMessage.Split(' ');

                    Console.WriteLine("Received TCP data from " + tcpClient.Client.RemoteEndPoint + ": " + receivedMessage);

                    // Perform the same PID calculations as in the UDP server (update variables, calculate PID)

                    kp = double.Parse(data[0]);
                    ki = double.Parse(data[1]);
                    kd = double.Parse(data[2]);
                    setpoint = double.Parse(data[3]);
                    temperatPID = double.Parse(data[4]);

                    // Perform PID calculations here...
                    double error = setpoint - temperatPID;
                    integral += error;
                    double derivative = error - prevError;

                    double differ = kp * error;
                    double integ = ki * integral;
                    double deriv = kd * derivative;

                    prevError = error;

                    outputPID = differ + integ + deriv;

                    temperatPID += outputPID;

                    // Send the response back to the client
                    string messageToSend = temperatPID.ToString();
                    await writer.WriteLineAsync(messageToSend);
                }

                // Close the TCP client when done, but keep listening for new connections
                tcpClient.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("TCP Client error: " + ex.Message);
        }
    }


    // Helper method to print local IP addresses
    void PrintLocalIPAddresses()
    {
        string[] localIPAddresses =
            Dns.GetHostAddresses(Dns.GetHostName())
            .Where(ip => ip.AddressFamily == AddressFamily.InterNetwork)
            .Select(ip => ip.ToString())
            .ToArray();

        Console.WriteLine("Local IP Addresses:");
        foreach (string ipAddress in localIPAddresses)
        {
            Console.WriteLine(ipAddress);
        }
    }

    // Helper method to print the public IP address
    async Task PrintPublicIPAddress()
    {
        using HttpClient httpClient = new();
        try
        {
            string publicIpAddress = await httpClient.GetStringAsync("https://api.ipify.org");
            Console.WriteLine("Public IP Address: " + publicIpAddress);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
