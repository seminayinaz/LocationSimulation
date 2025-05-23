using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Entities.Concrete;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DataAccess.Concrete
{
    public class SocketManager
    {
        CoordinateManager _coordinateManager;
        ILogger<SocketManager> _logger;

        public SocketManager(CoordinateManager coordinateManager, ILogger<SocketManager> logger)
        {
            _coordinateManager = coordinateManager;
            _logger = logger;
        }

        public string StartClient(string hostname, int port, Location[] location)
        {
            byte[] bytes = new byte[1024];
            List<string> result = new List<string>();

            try
            {
                IPHostEntry host = Dns.GetHostEntry(hostname);
                IPAddress ipAddress = host.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

                Socket sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                sender.Connect(remoteEP);

                var message = _coordinateManager.MessageFormat(location).Result;
                var last = message.LastOrDefault();

                string strMessage = JsonConvert.SerializeObject(message, Formatting.Indented);
                byte[] msg = Encoding.ASCII.GetBytes(strMessage + "<EOF>");

                int bytesSent = sender.Send(msg, msg.Length, 0);
                int bytesRec = sender.Receive(bytes);

                string rec = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                return rec;

                //foreach (var item in message)
                //{
                //    string strMessage = JsonConvert.SerializeObject(item, Formatting.Indented);

                //    if (item.Equals(last) && item.AnchorCode == 103)
                //    {
                //        byte[] msg = Encoding.ASCII.GetBytes(strMessage + "<EOF>");
                //        int bytesSent = sender.Send(msg, msg.Length, 0);
                //    }
                //    else
                //    {
                //        byte[] msg = Encoding.ASCII.GetBytes(strMessage);
                //        int bytesSent = sender.Send(msg, msg.Length, 0);
                //    }
                //}

            }
            catch (Exception ex)
            {
                _logger.LogError("Exception!" + ex.Message);
                throw;
            }
            return null;
        }
    }
}
