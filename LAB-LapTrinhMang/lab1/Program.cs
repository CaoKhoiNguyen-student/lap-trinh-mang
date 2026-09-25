using System;
using System.Net;
using System.Net.Sockets;  

class Program
{
    static void GetHostInfo(string host)
    {
        try
        {
            // Yêu cầu 2: Sử dụng Dns.GetHostEntry()
            IPHostEntry hostInfo = Dns.GetHostEntry(host);

            // Yêu cầu 3: Hiển thị HostName
            Console.WriteLine("\n--- KET QUA PHAN GIAI ---");
            Console.WriteLine("HostName: " + hostInfo.HostName);
            Console.WriteLine("Danh sach dia chi IP:");

            // Yêu cầu 3: Duyệt AddressList và phân loại IPv4 / IPv6
            int countV4 = 0, countV6 = 0;

            foreach (IPAddress ipaddr in hostInfo.AddressList)
            {
                // Dựa vào thuộc tính AddressFamily để phân loại
                if (ipaddr.AddressFamily == AddressFamily.InterNetwork)
                {
                    countV4++;
                    Console.WriteLine("  [IPv4] " + ipaddr.ToString());
                }
                else if (ipaddr.AddressFamily == AddressFamily.InterNetworkV6)
                {
                    countV6++;
                    Console.WriteLine("  [IPv6] " + ipaddr.ToString());
                }
                else
                {
                    Console.WriteLine("  [Khac] " + ipaddr.ToString());
                }
            }

            Console.WriteLine($"\n=> Tong cong: {countV4} IPv4, {countV6} IPv6");
        }
        // Yêu cầu 4: Bắt ngoại lệ SocketException và hiển thị mã lỗi cụ thể
        catch (SocketException ex)
        {
            Console.WriteLine("\n[LOI] Khong the phan giai ten mien: " + host);
            Console.WriteLine("  - Ma loi (ErrorCode)     : " + ex.ErrorCode);
            Console.WriteLine("  - Ma SocketError         : " + ex.SocketErrorCode);
            Console.WriteLine("  - Thong bao              : " + ex.Message);

            // Giải thích chi tiết mã lỗi
            switch (ex.SocketErrorCode)
            {
                case SocketError.HostNotFound:
                    Console.WriteLine("  => Khong tim thay host nay trong he thong DNS.");
                    break;
                case SocketError.NoData:
                    Console.WriteLine("  => Host ton tai nhung khong co ban ghi IP nao.");
                    break;
                case SocketError.TryAgain:
                    Console.WriteLine("  => DNS tam thoi khong phan hoi, vui long thu lai.");
                    break;
                case SocketError.NoRecovery:
                    Console.WriteLine("  => Loi khong the phuc hoi tu DNS.");
                    break;
                default:
                    Console.WriteLine("  => Loi khong xac dinh.");
                    break;
            }
        }
        // Bắt thêm các lỗi khác (ví dụ: chuỗi rỗng, ký tự không hợp lệ)
        catch (ArgumentException ex)
        {
            Console.WriteLine("\n[LOI] Ten mien khong hop le: " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n[LOI KHAC] {ex.GetType().Name}: {ex.Message}");
        }
    }

    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Cho phép nhập nhiều lần cho đến khi gõ 'exit'
        while (true)
        {
            Console.WriteLine("\n==================================================");
            Console.Write("Nhap Domain hoac IP (go 'exit' de thoat): ");
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Ban chua nhap gi ca!");
                continue;
            }

            if (input.Trim().ToLower() == "exit")
                break;

            GetHostInfo(input.Trim());
        }

        Console.WriteLine("\nNhan phim bat ky de thoat...");
        Console.ReadKey();
    }
}