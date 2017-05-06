﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{

   public static class Server
    {
        public static IPHostEntry ipHost;
        public static IPAddress ipAddr;
        public static IPEndPoint ipEndPoint;
        public static string status = "Отключен";
        public static Socket sListener;
        public static bool enabled = false;
        public static string data;


        public static  void Initialize(int port)
        {
            Console.WriteLine("Создаю сервер на порте: " + port);

           if(sListener != null)
            {
                sListener.Shutdown(SocketShutdown.Both);
            }
           
           
            ipHost = Dns.GetHostEntryAsync("localhost").Result;
            ipAddr = IPAddress.Any;
            ipEndPoint = new IPEndPoint(ipAddr, port);

            Activate();
        }

        private static string Connected(Socket _handler)
        {
            Console.WriteLine("Входящее подключение от  " + _handler.RemoteEndPoint);
            data = null;
            byte[] bytes = new byte[Int32.MaxValue/100];
            int bytesRec = _handler.Receive(bytes);

            data += Encoding.UTF8.GetString(bytes, 0, bytesRec);
            return data;
        }

        private static void SendResponse(Socket _handler, string toSend)
        {
            Console.WriteLine("Создают ответ для " + _handler.RemoteEndPoint);
            byte[] msg = Encoding.UTF8.GetBytes(toSend);
            _handler.Send(msg);
            Console.WriteLine("Отправил ответ для " + _handler.RemoteEndPoint);
        }

        public static void Activate()
        {
            Console.WriteLine("Активирую сервер...");
            sListener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            sListener.Bind(ipEndPoint);

            enabled = true;
            status = "Включен";
            
            Thread potok = new Thread(Listen);
            potok.Start();
        }

        public static void Listen()
        {
           

            while (enabled) {

                sListener.Listen(10);
                Socket handler = sListener.Accept();

                text(Connected(handler));

                SendResponse(handler,"все ништяк");

            }

        }


        private static void text(string write)
        {
            Console.WriteLine("ПОЛУЧЕН ОТВЕТ. СОДЕРЖАНИЕ:");
            Console.WriteLine(write);
        }
    }
}
