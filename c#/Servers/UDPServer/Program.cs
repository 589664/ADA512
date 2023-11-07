using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

class IDPServers
{
    static void Main()
    {
        int port = 12345;
        UdpClient udpServer = new(port);

        // Print the IP addresses that the server is listening on
        Console.WriteLine("UDP Server \nLocal IP addresses:");

        foreach (var ipAddress in GetLocalIPAddresses())
        {
            Console.WriteLine(ipAddress);
        }

        // Retrieve and print the public IP address
        Console.WriteLine("Public IP addresses:");
        string publicIpAddress = GetPublicIPAddress().GetAwaiter().GetResult();
        Console.WriteLine(publicIpAddress);

        Console.WriteLine("On port/ports:\n" + port);

        while (true)
        {
            IPEndPoint clientEndPoint = new(IPAddress.Any, port);
            byte[] receivedBytes = udpServer.Receive(ref clientEndPoint);
            string receivedData = Encoding.ASCII.GetString(receivedBytes);

            Console.WriteLine("Received data from " + clientEndPoint + ": " + receivedData);

            byte[] sendData = Encoding.ASCII.GetBytes("Server received: " + receivedData);
            udpServer.Send(sendData, sendData.Length, clientEndPoint);
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
