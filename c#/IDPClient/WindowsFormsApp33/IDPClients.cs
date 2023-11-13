using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IDPClients
{
    public class IDPClients
    {
        public UdpClient Client { get; private set; }
        public IPEndPoint EndPoint { get; private set; }
        public DateTime LastTransmissionTime { get; private set; }
        public double TransmissionSpeed { get; private set; }

        public IDPClients(string serverIP, int port)
        {
            Client = new UdpClient();
            EndPoint = new IPEndPoint(IPAddress.Parse(serverIP), port);
            LastTransmissionTime = DateTime.UtcNow;
            TransmissionSpeed = 0.0;
        }

        public void SendData(string data)
        {
            byte[] messageBytes = Encoding.ASCII.GetBytes(data);
            Client.Send(messageBytes, messageBytes.Length, EndPoint);

            // Calculate transmission speed
            DateTime currentTime = DateTime.UtcNow;
            double elapsedTimeSeconds = (currentTime - LastTransmissionTime).TotalSeconds;
            TransmissionSpeed = messageBytes.Length / elapsedTimeSeconds;
            LastTransmissionTime = currentTime;
        }

        public string ReceiveData()
        {
            IPEndPoint senderEndPoint = new IPEndPoint(IPAddress.Any, 0);
            byte[] receivedBytes = Client.Receive(ref senderEndPoint);
            return Encoding.ASCII.GetString(receivedBytes);
        }

        public void Close()
        {
            Client.Close();
        }
    }

}
