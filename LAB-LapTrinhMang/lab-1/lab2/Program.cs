using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        Console.WriteLine("==================================================");
        Console.WriteLine("   CAU HINH CARD MANG CUC BO (NETWORK INTERFACES)");
        Console.WriteLine("==================================================\n");

        // Yeu cau 1: Lay tat ca giao tiep mang
        NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();

        int stt = 0;
        foreach (NetworkInterface ni in interfaces)
        {
            // Yeu cau 2: Bo qua card Down hoac Loopback
            if (ni.OperationalStatus != OperationalStatus.Up) continue;
            if (ni.NetworkInterfaceType == NetworkInterfaceType.Loopback) continue;

            stt++;
            Console.WriteLine($"┌─── CARD MANG #{stt} ───────────────────────────────────");
            Console.WriteLine($"│ Ten giao tiep    : {ni.Name}");
            Console.WriteLine($"│ Mo ta            : {ni.Description}");
            Console.WriteLine($"│ Loai ket noi     : {ni.NetworkInterfaceType}");
            Console.WriteLine($"│ Trang thai       : {ni.OperationalStatus}");
            Console.WriteLine($"│ Toc do           : {ni.Speed / 1_000_000} Mbps");

            // Yeu cau 3: Dia chi vat ly (MAC Address)
            string mac = ni.GetPhysicalAddress().ToString();
            // Dinh dang lai MAC thanh dang XX-XX-XX-XX-XX-XX
            if (mac.Length == 12)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < mac.Length; i += 2)
                {
                    if (i > 0) sb.Append("-");
                    sb.Append(mac.Substring(i, 2));
                }
                mac = sb.ToString();
            }
            Console.WriteLine($"│ MAC Address      : {(string.IsNullOrEmpty(mac) ? "(khong co)" : mac)}");

            // Lay thong tin cau hinh IP
            IPInterfaceProperties ipProps = ni.GetIPProperties();

            // Yeu cau 3: Danh sach IPv4 kem Subnet Mask
            Console.WriteLine("│");
            Console.WriteLine("│ --- DANH SACH IPv4 & SUBNET MASK ---");
            bool hasIPv4 = false;
            foreach (UnicastIPAddressInformation ip in ipProps.UnicastAddresses)
            {
                if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                {
                    hasIPv4 = true;
                    Console.WriteLine($"│   + IP          : {ip.Address}");
                    Console.WriteLine($"│     Subnet Mask : {ip.IPv4Mask}");
                }
            }
            if (!hasIPv4) Console.WriteLine("│   (khong co dia chi IPv4)");

            // Yeu cau 3: Default Gateway
            Console.WriteLine("│");
            Console.WriteLine("│ --- DEFAULT GATEWAY ---");
            bool hasGateway = false;
            foreach (GatewayIPAddressInformation gw in ipProps.GatewayAddresses)
            {
                if (gw.Address.AddressFamily == AddressFamily.InterNetwork)
                {
                    hasGateway = true;
                    Console.WriteLine($"│   + Gateway     : {gw.Address}");
                }
            }
            if (!hasGateway) Console.WriteLine("│   (khong co Gateway)");

            // (Bonus) DNS Servers
            Console.WriteLine("│");
            Console.WriteLine("│ --- DNS SERVERS ---");
            bool hasDns = false;
            foreach (IPAddress dns in ipProps.DnsAddresses)
            {
                if (dns.AddressFamily == AddressFamily.InterNetwork)
                {
                    hasDns = true;
                    Console.WriteLine($"│   + DNS         : {dns}");
                }
            }
            if (!hasDns) Console.WriteLine("│   (khong co DNS)");

            Console.WriteLine("└────────────────────────────────────────────────\n");
        }

        Console.WriteLine($"==> Tong cong: {stt} card mang dang hoat dong.");
        Console.WriteLine("\nNhan phim bat ky de thoat...");
        Console.ReadKey();
    }
}