using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class IDPServers
{
    static async Task Main()
    {
        int udpPort = 12345;
        int tcpPort = 54321;

        // Start UDP server as a task
        Task udpServerTask = StartUDPServer(udpPort);

        // Start TCP server as a task
        Task tcpServerTask = StartTCPServer(tcpPort);

        Console.WriteLine("UDP Server on port: " + udpPort);
        Console.WriteLine("TCP Server on port: " + tcpPort);

        // Wait for both tasks to complete
        await Task.WhenAll(udpServerTask, tcpServerTask);
    }

    static async Task StartUDPServer(int udpPort)
    {
        UdpClient udpServer = new(udpPort);

        Console.WriteLine("UDP Server \nLocal IP addresses:");

        foreach (var ipAddress in GetLocalIPAddresses())
        {
            Console.WriteLine(ipAddress);
        }

        Console.WriteLine("Public IP addresses:");
        string publicIpAddress = await GetPublicIPAddress();
        Console.WriteLine(publicIpAddress);

        while (true)
        {
            IPEndPoint udpClientEndPoint = new(IPAddress.Any, udpPort);
            byte[] udpReceivedBytes = udpServer.Receive(ref udpClientEndPoint);
            string udpReceivedData = Encoding.ASCII.GetString(udpReceivedBytes);

            Console.WriteLine("Received UDP data from " + udpClientEndPoint + ": " + udpReceivedData);

            byte[] udpSendData = Encoding.ASCII.GetBytes("UDP Server received: " + udpReceivedData);
            udpServer.Send(udpSendData, udpSendData.Length, udpClientEndPoint);
        }
    }

    static async Task StartTCPServer(int tcpPort)
    {
        TcpListener tcpListener = new(IPAddress.Any, tcpPort);
        tcpListener.Start();

        while (true)
        {
            TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();
            NetworkStream tcpStream = tcpClient.GetStream();
            byte[] tcpReceivedBytes = new byte[1024];
            int bytesRead = await tcpStream.ReadAsync(tcpReceivedBytes, 0, tcpReceivedBytes.Length);
            string tcpReceivedData = Encoding.ASCII.GetString(tcpReceivedBytes, 0, bytesRead);

            Console.WriteLine("Received TCP data from " + ((IPEndPoint?)tcpClient.Client.RemoteEndPoint)?.ToString() + ": " + tcpReceivedData);

            byte[] tcpSendData = Encoding.ASCII.GetBytes("TCP Server received: " + tcpReceivedData);
            await tcpStream.WriteAsync(tcpSendData, 0, tcpSendData.Length);
            tcpClient.Close();
        }
    }

    // Helper method to retrieve local IP addresses
    static string[] GetLocalIPAddresses()
    {
        string[] localIPAddresses =
            Dns.GetHostAddresses(Dns.GetHostName())
            .Where(ip => ip.AddressFamily == AddressFamily.InterNetwork)
            .Select(ip => ip.ToString())
            .ToArray();

        return localIPAddresses;
    }

    // Helper method to retrieve the public IP address
    static async Task<string> GetPublicIPAddress()
    {
        using HttpClient httpClient = new();
        try
        {
            string publicIpAddress = await httpClient.GetStringAsync("https://api.ipify.org");
            return publicIpAddress;
        }
        catch (Exception ex)
        {
            return "Error: " + ex.Message;
        }
    }
}
