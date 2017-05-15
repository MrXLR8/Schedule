using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Builder
{
    public static class Net
    {
        public static IPHostEntry ipHost;
        public static IPAddress ipAddr;
        public static IPEndPoint ipEndPoint;

        public static Socket socket;
        public static void Initialize(string ipv4, int port)
        {
            FileInteraction.saveNetSettings();
            ipHost = Dns.GetHostEntry("localhost");
            ipAddr = IPAddress.Parse(ipv4); //Куда мы будем отправлять
            ipEndPoint = new IPEndPoint(ipAddr, port); // Обєкт-цель

        }
        public static bool connect()
        {
            socket = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);  // Сокет
            socket.Connect(ipEndPoint);
            System.Diagnostics.Debug.Write("================\nСокет соединяется с  " + socket.RemoteEndPoint.ToString());
            return socket.Connected;
        }

        public static string Send(string mess)
        {


            byte[] responseBytes = new byte[Int32.MaxValue / 100]; // Буфер для получения данных
            int responseBytesCount;  // Количество байтов которые получим

            if (socket == null || socket.Connected == false) connect();// Соединяем сокет с удаленной точкой



            byte[] msg = Encoding.UTF8.GetBytes(mess); // сообщение для отправки в байтомов виде
            socket.Send(msg); // Отправляем данные через сокет

            System.Diagnostics.Debug.Write("\nОтправил");

            responseBytesCount = socket.Receive(responseBytes); // Получаем ответ от сервера и кол-во байтов

            Stop();

            string result = Encoding.UTF8.GetString(responseBytes, 0, responseBytesCount);

            if (result == "blacklist") { MessageBox.Show("Ваш IP-адресс находиться в черном списке на сервере"); Global.RefreshTimer.IsEnabled = false; }
            if (result == "whitelist") MessageBox.Show("Для внесение изменений в расписаний необходимо находиться в белом списке на сервере");

            return result; // ответ в виде стринга






        }

        public static void Stop()
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }

    }
}
