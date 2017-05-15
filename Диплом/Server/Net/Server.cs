using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Share;

namespace Server
{

    public static class SocketServer
    {
        const string LOGTYPE = "SOCK";

        public static IPHostEntry ipHost;
        public static IPAddress ipAddr;
        public static IPEndPoint ipEndPoint;
        public static string status = "Отключен";
        public static Socket sListener;
        public static bool enabled = false;
        public static string data;


        public static void Initialize(int port)
        {

            Log.write(LOGTYPE, "Создаю сервер на порте: " + port);

            if (sListener != null)
            {
                try
                {
                    sListener.Shutdown(SocketShutdown.Both);
                }
                catch (Exception) { }
            }


            ipHost = Dns.GetHostEntryAsync("localhost").Result;
            ipAddr = IPAddress.Any;
            ipEndPoint = new IPEndPoint(ipAddr, port);

            Activate();
        }

        private static void Connected(Socket _handler)
        {
            IPAddress address = ((IPEndPoint)_handler.RemoteEndPoint).Address;
            if (Builder.Global.isInList(Builder.Global.blacklist, address))
            {
                Log.write(LOGTYPE, address, "Входящее подключение отклонено. Адрес находится в черном списке", ConsoleColor.Yellow);
                SendResponse(_handler, "blacklist");
                return;
            }
            Log.write(LOGTYPE, address, "Входящее подключение", ConsoleColor.Magenta);


            data = null;
            byte[] bytes = new byte[Int32.MaxValue / 100];
            int bytesRec = _handler.Receive(bytes);
            data += Encoding.UTF8.GetString(bytes, 0, bytesRec);
            Command recived;
            try
            {
                recived = JsonConvert.DeserializeObject<Command>(data);
            }
            catch (Exception)
            {
                Log.write(LOGTYPE, "Не удаеться разобрать запрос", ConsoleColor.Red);
                return;
            }

            recived.qualify(address);
            SendResponse(_handler, recived.toAnswer);


        }

        private static void SendResponse(Socket _handler, string toSend)
        {

            byte[] msg = Encoding.UTF8.GetBytes(toSend);
            _handler.Send(msg);
        }

        public static void Activate()
        {

            sListener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            sListener.Bind(ipEndPoint);

            enabled = true;
            status = "Включен";

            Thread potok = new Thread(Listen);
            potok.Start();

            Log.write(LOGTYPE, "Сервер успешно запущен", ConsoleColor.Green);
        }

        public static void DeActivate()
        {
            Log.write(LOGTYPE, "Останавливаю сервер", ConsoleColor.Blue);
            enabled = false;
            status = "Отключен";
            sListener.Dispose();

        }
        public static void Listen()
        {


            while (enabled)
            {
                Socket handler;
                sListener.Listen(10);
                try
                {
                    handler = sListener.Accept();
                }
                catch (SocketException) { break; }
                Task.Run(() => Connected(handler));

                //   text();



            }

        }


        private static void text(string write)
        {
            Console.WriteLine("[NET]ПОЛУЧЕН ОТВЕТ. СОДЕРЖАНИЕ:");
            Log.write(LOGTYPE, "Клиент ответил: " + write);

        }
    }
}
