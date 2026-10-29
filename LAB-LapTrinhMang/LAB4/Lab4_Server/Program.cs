using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

class TCPServer
{
    static void Main(string[] args)
    {
        TcpListener server = new TcpListener(IPAddress.Any, 9050);
        server.Start();
        Console.WriteLine("[SERVER] Dang lang nghe ket noi tai cong 9050...");

        TcpClient client = server.AcceptTcpClient();
        Console.WriteLine("[SERVER] Client da ket noi thanh cong!");
        NetworkStream ns = client.GetStream();

        // Ghi thong tin nhan vien ra file .txt theo yeu cau nang cao
        using (StreamWriter sw = new StreamWriter("EmployeeData.txt", true))
        {
            while (true)
            {
                try
                {
                    byte[] sizeBytes = new byte[2];
                    int recv = ns.Read(sizeBytes, 0, 2);
                    if (recv == 0) break;

                    int packsize = BitConverter.ToInt16(sizeBytes, 0);
                    byte[] data = new byte[packsize];
                    ns.Read(data, 0, packsize);

                    Employee emp = new Employee(data);
                    string info = $"ID: {emp.EmployeeID} | Ho Ten: {emp.FirstName} {emp.LastName} | Nam lam viec: {emp.YearsService} | Luong: {emp.Salary}";

                    Console.WriteLine("[NHAN DUOC] " + info);
                    sw.WriteLine(info);
                    sw.Flush();
                }
                catch
                {
                    break;
                }
            }
        }

        ns.Close();
        client.Close();
        server.Stop();
        Console.WriteLine("[SERVER] Da dong ket noi.");
    }
}