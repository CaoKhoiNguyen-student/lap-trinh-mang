using System;
using System.Net.Sockets;

class TCPClient
{
    static void Main(string[] args)
    {
        Console.WriteLine("[CLIENT] Dang ket noi toi Server...");
        TcpClient client = new TcpClient("127.0.0.1", 9050);
        NetworkStream ns = client.GetStream();
        Console.WriteLine("[CLIENT] Da ket noi!");

        while (true)
        {
            Employee emp = new Employee();

            Console.WriteLine("\n--- NHAP THONG TIN NHAN VIEN ---");
            Console.Write("Nhap EmployeeID (so nguyen): ");
            emp.EmployeeID = int.Parse(Console.ReadLine());

            Console.Write("Nhap First Name: ");
            emp.FirstName = Console.ReadLine();

            Console.Write("Nhap Last Name: ");
            emp.LastName = Console.ReadLine();

            Console.Write("Nhap Years of Service: ");
            emp.YearsService = int.Parse(Console.ReadLine());

            Console.Write("Nhap Salary: ");
            emp.Salary = double.Parse(Console.ReadLine());

            // Dong goi byte va gui
            byte[] empData = emp.GetBytes();
            byte[] size = BitConverter.GetBytes((short)emp.size);

            ns.Write(size, 0, 2);
            ns.Write(empData, 0, emp.size);
            ns.Flush();
            Console.WriteLine(">> Da gui du lieu len Server thanh cong!");

            Console.Write("\nBan co muon tiep tuc nhap khong? (Nhap 'Khong' de thoat): ");
            string choice = Console.ReadLine();
            if (choice != null && choice.Trim().Equals("Khong", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }
        }

        ns.Close();
        client.Close();
        Console.WriteLine("[CLIENT] Da ngat ket noi.");
    }
}