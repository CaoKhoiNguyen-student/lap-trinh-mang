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
        Console.WriteLine("   TINH TOAN DIA CHI MANG (NETWORK ADDRESS)");
        Console.WriteLine("==================================================\n");

        // Test tự động
        Console.WriteLine("=== TEST TU DONG ===");
        TinhToan("192.168.1.130", "255.255.255.0");
        TinhToan("192.168.1.130", "255.255.255.192");
        TinhToan("10.0.0.55", "255.0.0.0");
        TinhToan("172.16.5.200", "255.255.240.0");

        // Nhập từ bàn phím
        Console.WriteLine("\n\n=== NHAP TU BAN PHIM ===");
        while (true)
        {
            Console.Write("\nNhap dia chi IPv4 (go 'exit' de thoat): ");
            string ipInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(ipInput)) continue;
            if (ipInput.Trim().ToLower() == "exit") break;

            Console.Write("Nhap Subnet Mask: ");
            string maskInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(maskInput)) continue;

            Console.WriteLine();
            TinhToan(ipInput.Trim(), maskInput.Trim());
        }

        Console.ReadKey();
    }

    // ================================================================
    // HÀM CHÍNH: Tính địa chỉ mạng
    // ================================================================
    static void TinhToan(string ipStr, string maskStr)
    {
        try
        {
            // --- Bước 1: Parse IP và Mask ---
            IPAddress ip, mask;
            if (!IPAddress.TryParse(ipStr, out ip))
                throw new FormatException($"Dia chi IP '{ipStr}' khong hop le!");

            if (!IPAddress.TryParse(maskStr, out mask))
                throw new FormatException($"Subnet Mask '{maskStr}' khong hop le!");

            // Kiểm tra cả hai đều là IPv4
            if (ip.AddressFamily != AddressFamily.InterNetwork)
                throw new ArgumentException($"'{ipStr}' khong phai dia chi IPv4!");

            if (mask.AddressFamily != AddressFamily.InterNetwork)
                throw new ArgumentException($"'{maskStr}' khong phai Subnet Mask IPv4!");

            // --- Bước 2: Trích xuất mảng byte bằng GetAddressBytes() ---
            byte[] ipBytes = ip.GetAddressBytes();
            byte[] maskBytes = mask.GetAddressBytes();

            if (ipBytes.Length != 4 || maskBytes.Length != 4)
                throw new Exception("Du lieu khong phai IPv4 (khong phai 4 byte)!");

            // --- Bước 3: Áp dụng toán tử bitwise AND trên từng byte ---
            byte[] networkBytes = new byte[4];
            byte[] broadcastBytes = new byte[4];
            byte[] hostBytes = new byte[4];

            for (int i = 0; i < 4; i++)
            {
                // Network Address = IP AND Mask
                networkBytes[i] = (byte)(ipBytes[i] & maskBytes[i]);

                // Broadcast Address = Network OR (NOT Mask)
                broadcastBytes[i] = (byte)(networkBytes[i] | (byte)~maskBytes[i]);

                // Host Address = IP AND (NOT Mask)
                hostBytes[i] = (byte)(ipBytes[i] & (byte)~maskBytes[i]);
            }

            // --- Bước 4: Tạo IPAddress từ mảng byte ---
            IPAddress network = new IPAddress(networkBytes);
            IPAddress broadcast = new IPAddress(broadcastBytes);
            IPAddress host = new IPAddress(hostBytes);

            // --- Hiển thị kết quả ---
            Console.WriteLine("┌─────────────────────────────────────────────");
            Console.WriteLine($"│ IP Address       : {ip}");
            Console.WriteLine($"│ Subnet Mask      : {mask}");
            Console.WriteLine("├─────────────────────────────────────────────");
            Console.WriteLine($"│ Network Address  : {network}");
            Console.WriteLine($"│ Broadcast Address: {broadcast}");
            Console.WriteLine($"│ Host Address     : {host}");
            Console.WriteLine($"│ Prefix Length    : /{DemBit1(maskBytes)}");
            Console.WriteLine("├─────────────────────────────────────────────");
            Console.WriteLine("│ CHI TIET TUNG BYTE (IP & Mask):");
            Console.WriteLine("│   Byte |     IP     |    Mask    |    AND");
            Console.WriteLine("│  ------+------------+------------+------------");
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine($"│     {i}  |  {ipBytes[i],3} ({ipBytes[i]:X2})  |  {maskBytes[i],3} ({maskBytes[i]:X2})  |  {networkBytes[i],3} ({networkBytes[i]:X2})");
            }
            Console.WriteLine("└─────────────────────────────────────────────\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✘ LOI: {ex.Message}\n");
        }
    }

    // ================================================================
    // Đếm số bit 1 trong mảng byte (để tính prefix length /24, /25...)
    // ================================================================
    static int DemBit1(byte[] bytes)
    {
        int count = 0;
        foreach (byte b in bytes)
        {
            byte temp = b;
            while (temp != 0)
            {
                count += temp & 1;
                temp >>= 1;
            }
        }
        return count;
    }
}