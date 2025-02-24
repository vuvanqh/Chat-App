using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace Client.NewFolder;

public class NetworkException : Exception
{
    public NetworkException(string? message) : base(message) { }
}



public class Server
{
    TcpClient client;

    public Server() 
    {
        client = new TcpClient();
    }

    public IPAddress? GetServer()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip;
            }
        }
        return null;
    }

    public void ConnectToServer()
    {
        if (!client.Connected)
        {
            try
            {
                var ip = GetServer();
                if (ip == null)
                {
                    throw new NetworkException("can't find the LAN IP");
                }
                client.Connect(ip, 3145);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Invalid lan ip retreival logic:\n{e.Message}");
                client.Connect("127.0.0.1", 3145);
            }
        }
    }
}

