using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Thiết lập Server EndPoint: IP bất kỳ và Port 5000
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Any, 5000);

            // Khởi tạo socket lắng nghe sử dụng giao thức TCP
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // Gắn socket với EndPoint và bắt đầu lắng nghe kết nối
            serverSocket.Bind(serverEndPoint);
            serverSocket.Listen(10);
            Console.WriteLine("Server dang cho ket noi o cong 5000...");

            // Chờ và chấp nhận kết nối từ Client
            Socket clientSocket = serverSocket.Accept();
            EndPoint clientEndPoint = clientSocket.RemoteEndPoint;
            Console.WriteLine("Client ket noi: " + clientEndPoint.ToString());

            // Gửi câu chào khi client vừa kết nối thành công
            string hello = "Hello Client";
            byte[] buff = Encoding.ASCII.GetBytes(hello);
            clientSocket.Send(buff, 0, buff.Length, SocketFlags.None);

            // Vòng lặp nhận dữ liệu từ Client và phản hồi (Echo)
            while (true)
            {
                try
                {
                    buff = new byte[1024];
                    int byteReceive = clientSocket.Receive(buff, 0, buff.Length, SocketFlags.None);

                    // Client ngắt kết nối an toàn (gửi FIN gói 0 byte)
                    if (byteReceive == 0)
                    {
                        Console.WriteLine("Client da ngat ket noi.");
                        break;
                    }

                    string str = Encoding.ASCII.GetString(buff, 0, byteReceive);
                    Console.WriteLine("Nhan tu client: " + str);

                    // Gửi trả dữ liệu ngược lại cho Client
                    clientSocket.Send(buff, 0, byteReceive, SocketFlags.None);
                }
                catch (SocketException)
                {
                    // Bắt ngoại lệ khi Client tắt đột ngột
                    Console.WriteLine("Client da ngat ket noi dot ngot.");
                    break;
                }
            }

            // Đóng các kết nối
            clientSocket.Close();
            serverSocket.Close();
        }
    }
    
}
