using System;
using System.Net;

class Program
{
    static void GetHostInfo(string host)
    {
        try
        {
            IPHostEntry hostInfo = Dns.GetHostEntry(host);
            // Hiển thị tên miền
            Console.WriteLine("Ten mien: " + hostInfo.HostName);
            // Hiển thị danh sách địa chỉ IP
            Console.Write("Dia chi IP: ");
            foreach (IPAddress ipaddr in hostInfo.AddressList)
            {
                Console.Write(ipaddr.ToString() + " ");
            }
            Console.WriteLine(); // Xuống dòng sau khi in hết IP
        }
        catch (Exception)
        {
            Console.WriteLine("Khong phan giai duoc ten mien: " + host + "\n");
        }
    }

    static void Main(string[] args)
    {
        // Yêu cầu người dùng nhập tên miền
        Console.Write("Nhap ten mien can phan giai (vi du: google.com): ");
        string input = Console.ReadLine();

        // Kiểm tra xem người dùng có nhập gì không
        if (!string.IsNullOrEmpty(input))
        {
            GetHostInfo(input);
        }
        else
        {
            Console.WriteLine("Ban chua nhap ten mien!");
        }

        // Dừng màn hình để xem kết quả
        Console.WriteLine("\nNhan phim bat ky de thoat...");
        Console.ReadKey();
    }
}