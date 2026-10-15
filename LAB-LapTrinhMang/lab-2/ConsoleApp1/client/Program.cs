using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Thiết lập địa chỉ đích đến Server (127.0.0.1 và Port 5000)
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Loopback, 5000);

            // Khởi tạo socket TCP của Client
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            Console.WriteLine("Dang ket noi voi server...");

            try
            {
                serverSocket.Connect(serverEndPoint);
            }
            catch (SocketException se)
            {
                Console.WriteLine("Khong the ket noi den server: " + se.Message);
                return;
            }

            if (serverSocket.Connected)
            {
                Console.WriteLine("Ket noi thanh cong voi server...");

                // Nhận câu chào từ Server
                byte[] buff = new byte[1024];
                int byteReceive = serverSocket.Receive(buff, 0, buff.Length, SocketFlags.None);
                string str = Encoding.ASCII.GetString(buff, 0, byteReceive);
                Console.WriteLine(str);

                // Vòng lặp gửi dữ liệu lên Server và nhận lại
                while (true)
                {
                    Console.Write("> ");
                    str = Console.ReadLine();

                    if (string.IsNullOrEmpty(str))
                        continue;

                    // Thoát khi nhập 'exit'
                    if (str.Trim().ToLower() == "exit")
                    {
                        break;
                    }

                    // Gửi chuỗi lên server
                    buff = Encoding.ASCII.GetBytes(str);
                    serverSocket.Send(buff, 0, buff.Length, SocketFlags.None);

                    // Nhận chuỗi phản hồi từ server
                    buff = new byte[1024];
                    byteReceive = serverSocket.Receive(buff, 0, buff.Length, SocketFlags.None);
                    str = Encoding.ASCII.GetString(buff, 0, byteReceive);
                    Console.WriteLine("Server tra ve: " + str);
                }

                serverSocket.Shutdown(SocketShutdown.Both);
                serverSocket.Close();
            }
        }
    }
    
}
