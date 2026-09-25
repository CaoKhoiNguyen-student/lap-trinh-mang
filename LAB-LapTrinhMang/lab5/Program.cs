using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        Console.WriteLine("==================================================");
        Console.WriteLine("   QUET MAY TINH TRONG MANG CUC BO (LAN SCAN)");
        Console.WriteLine("==================================================\n");

        // ============================================================
        // Yêu cầu 1: Hiển thị cấu hình IP và địa chỉ mạng của máy trạm
        // ============================================================
        Console.WriteLine("--- CAU HINH MANG CUA MAY TRAM ---");
        string baseNetwork = HienThiCauHinhMayTram();

        if (string.IsNullOrEmpty(baseNetwork))
        {
            Console.WriteLine("\n✘ Khong tim thay card mang IPv4 dang hoat dong!");
            Console.ReadKey();
            return;
        }

        Console.WriteLine($"\n=> Dia chi mang cua ban: {baseNetwork}.x");

        // ============================================================
        // Yêu cầu 2: Nhập dải IP và quét
        // ============================================================
        Console.WriteLine("\n--- QUET MAY TRAM TRONG MANG ---");
        Console.Write($"Nhap day IP (Enter de dung mac dinh '{baseNetwork}.1-{baseNetwork}.254'): ");
        string input = Console.ReadLine();

        string startIP, endIP;
        if (string.IsNullOrWhiteSpace(input))
        {
            startIP = baseNetwork + ".1";
            endIP = baseNetwork + ".254";
        }
        else
        {
            // Cho phép nhập dạng: "192.168.1.1-192.168.1.50" hoặc "192.168.1.1 192.168.1.50"
            input = input.Replace(" ", "-");
            string[] parts = input.Split('-');
            if (parts.Length != 2)
            {
                Console.WriteLine("✘ Dinh dang sai! Vui long nhap 'IP_bat_dau-IP_ket_thuc'.");
                Console.ReadKey();
                return;
            }
            startIP = parts[0].Trim();
            endIP = parts[1].Trim();
        }

        Console.WriteLine($"\n=> Bat dau quet tu {startIP} den {endIP} ...");
        Console.WriteLine("(Qua trinh co the mat 1-2 phut, vui long cho...)\n");

        List<HostInfo> ketQua = QuetMang(startIP, endIP);

        // Hiển thị kết quả
        Console.WriteLine("\n==================================================");
        Console.WriteLine($"KET QUA: Tim thay {ketQua.Count} may tinh trong mang");
        Console.WriteLine("==================================================");

        if (ketQua.Count == 0)
        {
            Console.WriteLine("(Khong tim thay may nao. Co the do Firewall chan ping.)");
        }
        else
        {
            Console.WriteLine($"{"STT",-5} {"IP",-18} {"HostName",-30} {"RoundTrip",-12}");
            Console.WriteLine(new string('-', 70));
            int stt = 1;
            foreach (var h in ketQua)
            {
                Console.WriteLine($"{stt,-5} {h.IP,-18} {h.HostName,-30} {h.RoundTrip + " ms",-12}");
                stt++;
            }
        }

        Console.WriteLine("\nNhan phim bat ky de thoat...");
        Console.ReadKey();
    }

    // ================================================================
    // YÊU CẦU 1: Hiển thị cấu hình máy trạm, trả về địa chỉ mạng
    // ================================================================
    static string HienThiCauHinhMayTram()
    {
        NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
        string networkBase = null;

        foreach (NetworkInterface ni in interfaces)
        {
            if (ni.OperationalStatus != OperationalStatus.Up) continue;
            if (ni.NetworkInterfaceType == NetworkInterfaceType.Loopback) continue;

            IPInterfaceProperties ipProps = ni.GetIPProperties();
            foreach (UnicastIPAddressInformation ip in ipProps.UnicastAddresses)
            {
                if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                {
                    // Bỏ qua địa chỉ APIPA (169.254.x.x) - thường là khi không có mạng
                    if (ip.Address.ToString().StartsWith("169.254")) continue;

                    Console.WriteLine($"\n  Card mang     : {ni.Name} ({ni.NetworkInterfaceType})");
                    Console.WriteLine($"  IP            : {ip.Address}");
                    Console.WriteLine($"  Subnet Mask   : {ip.IPv4Mask}");

                    // Tính địa chỉ mạng: IP AND Mask
                    byte[] ipBytes = ip.Address.GetAddressBytes();
                    byte[] maskBytes = ip.IPv4Mask.GetAddressBytes();
                    byte[] netBytes = new byte[4];
                    for (int i = 0; i < 4; i++) netBytes[i] = (byte)(ipBytes[i] & maskBytes[i]);
                    IPAddress network = new IPAddress(netBytes);

                    // Hiển thị Gateway
                    foreach (GatewayIPAddressInformation gw in ipProps.GatewayAddresses)
                    {
                        if (gw.Address.AddressFamily == AddressFamily.InterNetwork)
                            Console.WriteLine($"  Default Gateway: {gw.Address}");
                    }

                    Console.WriteLine($"  Dia chi mang  : {network}");

                    // Chỉ lấy card mạng đầu tiên (thường là card đang dùng)
                    if (networkBase == null)
                        networkBase = network.ToString().TrimEnd('.', '0').TrimEnd('.');
                    // Ví dụ: 192.168.1.0 → 192.168.1
                }
            }
        }
        return networkBase;
    }

    // ================================================================
    // YÊU CẦU 2: Quét dải IP song song, phân giải hostname
    // ================================================================
    static List<HostInfo> QuetMang(string startIP, string endIP)
    {
        List<HostInfo> ketQua = new List<HostInfo>();
        object lockObj = new object();

        // Đổi IP thành số để dễ duyệt
        long start = IPToLong(IPAddress.Parse(startIP));
        long end = IPToLong(IPAddress.Parse(endIP));

        if (start > end)
        {
            Console.WriteLine("✘ IP bat dau phai nho hon IP ket thuc!");
            return ketQua;
        }

        long total = end - start + 1;
        Console.WriteLine($"Tong so IP can quet: {total}");

        // Dùng Parallel để quét song song → nhanh hơn
        Parallel.For(start, end + 1, new ParallelOptions { MaxDegreeOfParallelism = 50 },
            i =>
            {
                string ipStr = LongToIP(i).ToString();
                HostInfo info = KiemTraHost(ipStr);
                if (info != null)
                {
                    lock (lockObj)
                    {
                        ketQua.Add(info);
                        Console.WriteLine($"  ✔ Tim thay: {ipStr}  →  {info.HostName}");
                    }
                }
            });

        // Sắp xếp theo IP
        ketQua.Sort((a, b) => IPToLong(IPAddress.Parse(a.IP)).CompareTo(IPToLong(IPAddress.Parse(b.IP))));
        return ketQua;
    }

    // ================================================================
    // Kiểm tra 1 host có "sống" không và lấy hostname
    // ================================================================
    static HostInfo KiemTraHost(string ipStr)
    {
        try
        {
            Ping ping = new Ping();
            PingReply reply = ping.Send(ipStr, 800); // timeout 800ms

            if (reply.Status == IPStatus.Success)
            {
                string hostname = "(khong ro)";
                try
                {
                    // Phân giải ngược IP → HostName
                    IPHostEntry entry = Dns.GetHostEntry(ipStr);
                    if (!string.IsNullOrEmpty(entry.HostName))
                        hostname = entry.HostName;
                }
                catch
                {
                    // Không phân giải được hostname thì bỏ qua
                }

                return new HostInfo
                {
                    IP = ipStr,
                    HostName = hostname,
                    RoundTrip = reply.RoundtripTime
                };
            }
        }
        catch
        {
            // Bỏ qua lỗi khi ping
        }
        return null;
    }

    // ================================================================
    // Hàm phụ: Chuyển IP ↔ long
    // ================================================================
    static long IPToLong(IPAddress ip)
    {
        byte[] b = ip.GetAddressBytes();
        return ((long)b[0] << 24) | ((long)b[1] << 16) | ((long)b[2] << 8) | b[3];
    }

    static IPAddress LongToIP(long value)
    {
        return new IPAddress(new byte[]
        {
            (byte)((value >> 24) & 0xFF),
            (byte)((value >> 16) & 0xFF),
            (byte)((value >> 8) & 0xFF),
            (byte)(value & 0xFF)
        });
    }

    // Class chứa thông tin host
    class HostInfo
    {
        public string IP { get; set; }
        public string HostName { get; set; }
        public long RoundTrip { get; set; }
    }
}