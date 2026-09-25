using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        Console.WriteLine("==================================================");
        Console.WriteLine("        MODULE PARSE IPENDPOINT (Host:Port)");
        Console.WriteLine("==================================================\n");

        // Danh sách test tự động
        string[] tests =
        {
            "127.0.0.1:8080",
            "dlu.edu.vn:443",
            "google.com:80",
            "8.8.8.8:53",
            "192.168.1.1:99999",     // Port sai
            "192.168.1.1:0",         // Port sai
            "abcxyz123.com:80",      // Host không tồn tại
            "khongcoformat",         // Sai format
            "1.1.1.1:abc",           // Port không phải số
        };

        Console.WriteLine("=== TEST TU DONG ===");
        foreach (string t in tests)
        {
            Console.WriteLine($"\nInput: \"{t}\"");
            try
            {
                IPEndPoint ep = ParseEndPoint(t);
                Console.WriteLine($"  ✔ Ket qua : {ep}");
                Console.WriteLine($"    Address : {ep.Address} ({ep.AddressFamily})");
                Console.WriteLine($"    Port    : {ep.Port}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  ✘ Loi     : {ex.Message}");
            }
        }

        Console.WriteLine("\n\n=== NHAP TU BAN PHIM ===");
        while (true)
        {
            Console.Write("\nNhap 'Host:Port' (go 'exit' de thoat): ");
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input)) continue;
            if (input.Trim().ToLower() == "exit") break;

            try
            {
                IPEndPoint ep = ParseEndPoint(input);
                Console.WriteLine($"  ✔ IPEndPoint: {ep}");
                Console.WriteLine($"    Address   : {ep.Address}");
                Console.WriteLine($"    Port      : {ep.Port}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  ✘ Loi: {ex.Message}");
            }
        }

        Console.ReadKey();
    }

    // ================================================================
    // YÊU CẦU CHÍNH: ParseEndPoint
    // ================================================================
    static IPEndPoint ParseEndPoint(string input)
    {
        // --- Bước 1: Kiểm tra đầu vào ---
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("Chuỗi đầu vào rỗng!");

        input = input.Trim();

        // --- Bước 2: Tách chuỗi theo dấu ':' ---
        // Lưu ý: dùng LastIndexOf để hỗ trợ IPv6 dạng [::1]:8080
        string hostPart;
        string portPart;

        // Trường hợp IPv6 trong ngoặc vuông: [::1]:8080
        if (input.StartsWith("["))
        {
            int closeBracket = input.IndexOf(']');
            if (closeBracket < 0)
                throw new FormatException("Thieu dau ']' cho dia chi IPv6!");

            hostPart = input.Substring(1, closeBracket - 1);
            string rest = input.Substring(closeBracket + 1);

            if (!rest.StartsWith(":"))
                throw new FormatException("Thieu dau ':' sau dia chi IPv6!");

            portPart = rest.Substring(1);
        }
        else
        {
            // Trường hợp thông thường: host:port
            int colonIndex = input.LastIndexOf(':');
            if (colonIndex < 0)
                throw new FormatException("Chuoi phai co dinh dang 'Host:Port'!");

            hostPart = input.Substring(0, colonIndex).Trim();
            portPart = input.Substring(colonIndex + 1).Trim();
        }

        // --- Bước 3: Kiểm tra Host không rỗng ---
        if (string.IsNullOrEmpty(hostPart))
            throw new FormatException("Phan Host khong duoc rong!");

        // --- Bước 4: Kiểm tra Port hợp lệ (1 <= Port <= 65535) ---
        int port;
        if (!int.TryParse(portPart, out port))
            throw new FormatException($"Port '{portPart}' khong phai la so nguyen!");

        if (port < 1 || port > 65535)
            throw new ArgumentOutOfRangeException(
                $"Port {port} khong hop le! Phai nam trong khoang 1 - 65535.");

        // --- Bước 5: Kiểm tra Host có phải IP trực tiếp không ---
        IPAddress ipAddress;
        if (IPAddress.TryParse(hostPart, out ipAddress))
        {
            // Host là địa chỉ IP trực tiếp
            return new IPEndPoint(ipAddress, port);
        }

        // --- Bước 6: Nếu không phải IP, dùng DNS để phân giải ---
        try
        {
            IPHostEntry hostEntry = Dns.GetHostEntry(hostPart);

            // Lấy địa chỉ IPv4 đầu tiên
            foreach (IPAddress addr in hostEntry.AddressList)
            {
                if (addr.AddressFamily == AddressFamily.InterNetwork)
                {
                    return new IPEndPoint(addr, port);
                }
            }

            // Nếu không có IPv4, thử lấy IPv6
            if (hostEntry.AddressList.Length > 0)
            {
                return new IPEndPoint(hostEntry.AddressList[0], port);
            }

            throw new Exception($"Khong tim thay dia chi IP cho host '{hostPart}'.");
        }
        catch (SocketException ex)
        {
            throw new Exception(
                $"Khong the phan giai host '{hostPart}'. " +
                $"SocketError: {ex.SocketErrorCode} (Code: {ex.ErrorCode})", ex);
        }
    }
}