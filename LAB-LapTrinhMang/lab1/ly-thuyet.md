# II. Lý thuyết

## 1) Kiến trúc Giao thức & Mô hình Mạng

### Câu 1.1: Trong mô hình TCP/IP, địa chỉ IP và số hiệu Port lần lượt hoạt động tại tầng nào? Giải thích lý do tại sao tầng Transport bắt buộc phải sử dụng Port để định tuyến dữ liệu đến đúng tiến trình ứng dụng.

Trong mô hình TCP/IP, địa chỉ IP và số hiệu Port hoạt động tại các tầng và giữ vai trò như sau:

**1. Tầng hoạt động của Địa chỉ IP và Số hiệu Port**

- **Địa chỉ IP (Internet Address):** Hoạt động tại **Tầng Network** (Tầng mạng / Network Layer).
- **Số hiệu Port (Port Number):** Hoạt động tại **Tầng Transport** (Tầng vận chuyển / Transport Layer).

**2. Lý do Tầng Transport bắt buộc phải sử dụng Port**

Tầng Transport phải sử dụng số hiệu Port vì các lý do sau:

- **Tầng Network (IP) chỉ định vị giữa máy với máy (Host-to-Host):** Giao thức IP ở tầng Network chịu trách nhiệm chuyển các gói tin qua mạng đến đúng máy host đích dựa vào địa chỉ IP.
- **Một máy host chạy nhiều tiến trình cùng lúc:** Trên một máy host có thể có rất nhiều ứng dụng/tiến trình mạng đang chạy đồng thời (như trình duyệt Web, phần mềm FTP, ứng dụng chat). Do đó, địa chỉ IP đơn thuần không đủ chi tiết để xác định gói tin thuộc về ứng dụng nào.
- **Định địa chỉ mức đầu-cuối (End-to-End Addressing):** Để đưa dữ liệu đến chính xác ứng dụng, tầng Transport (TCP và UDP) yêu cầu độ mịn địa chỉ cao hơn (finer granularity of addressing). Số hiệu Port đóng vai trò định danh duy nhất cho một socket/tiến trình ứng dụng cụ thể trên máy host đó.
- **Phân phát dữ liệu (Demultiplexing):** Khi dữ liệu từ tầng Network chuyển lên, tầng Transport căn cứ vào số hiệu Port để phân phát (demultiplex) dữ liệu vào đúng bộ đệm/socket của tiến trình ứng dụng tương ứng.

> Địa chỉ IP tương tự như địa chỉ tòa nhà/số nhà (giúp bưu điện giao thư đến đúng tòa nhà), còn số hiệu Port tương tự như số phòng hoặc số máy lẻ nội bộ (giúp người nhận trong tòa nhà chuyển thư đến đúng người/tiến trình cụ thể).

---

### Câu 1.2: Gói tin DNS (Domain Name System) khi phân giải tên miền thông thường sử dụng giao thức truyền vận nào (TCP hay UDP) và số hiệu cổng mặc định là bao nhiêu?

Khi phân giải tên miền, dịch vụ DNS (Domain Name System) sử dụng giao thức truyền vận và số cổng mặc định như sau:

- **Giao thức truyền vận thông thường:** **UDP** (User Datagram Protocol).
  - **Lý do:** Các truy vấn phân giải tên miền thông thường có kích thước nhỏ (gồm 1 gói câu hỏi và 1 gói trả lời). Việc dùng UDP giúp tối ưu tốc độ, giảm độ trễ và tránh chi phí bắt tay thiết lập kết nối (handshake) như TCP.
- **Lưu ý:** DNS vẫn hỗ trợ TCP[1] trong một số trường hợp đặc biệt như khi dữ liệu phản hồi vượt quá kích thước giới hạn của UDP (truy vấn nâng cao) hoặc khi thực hiện đồng bộ dữ liệu giữa các máy chủ DNS (Zone Transfer).
- **Số hiệu cổng mặc định (Port number):** **Cổng 53 (Port 53)**[1][2].

---

### Câu 1.3: Trình bày vai trò của Subnet Mask và Default Gateway khi một Host cần gửi dữ liệu ra ngoài mạng cục bộ (LAN).

**1. Vai trò của Subnet Mask (Mặt nạ mạng con)**

- **Phân chia địa chỉ IP:** Subnet Mask giúp máy tính xác định ranh giới giữa Network ID (mã định danh mạng) và Host ID (mã định danh máy host) trong một địa chỉ IP.
- **Xác định vị trí thiết bị đích (Local hay Remote):** Khi gửi dữ liệu, Host nguồn lấy địa chỉ IP đích và thực hiện phép toán nhị phân AND với Subnet Mask của chính nó, đồng thời thực hiện phép AND giữa địa chỉ IP nguồn với Subnet Mask.
  - **Nếu 2 kết quả trùng nhau:** Địa chỉ đích nằm trong cùng mạng LAN. Host nguồn sẽ gửi trực tiếp gói tin đến máy đích qua hạ tầng mạng nội bộ (dùng giao thức ARP để tìm địa chỉ MAC của máy đích).
  - **Nếu 2 kết quả khác nhau:** Địa chỉ đích nằm ngoài mạng LAN. Host nguồn hiểu rằng gói tin không thể giao trực tiếp trong nội bộ mạng mà bắt buộc phải chuyển cho Default Gateway xử lý.

**2. Vai trò của Default Gateway (Cổng mặc định)**

- **Làm điểm thoát (Cửa ngõ) ra khỏi mạng cục bộ:** Default Gateway thường là địa chỉ IP giao diện nội bộ của một Router (hoặc thiết bị định tuyến) kết nối mạng LAN với các mạng khác hoặc với Internet[1].
- **Chuyển tiếp gói tin qua các mạng (Forwarding/Routing):**
  - Sau khi Subnet Mask xác định thiết bị đích ở mạng ngoài, Host nguồn sẽ đóng gói tin IP (vẫn giữ nguyên IP nguồn và IP đích thực tế)[2] và gửi gói tin này đến địa chỉ MAC của Default Gateway.
  - Default Gateway (Router) nhận được gói tin, kiểm tra bảng định tuyến (Routing Table) của nó để tìm đường đi tối ưu[3], sau đó chuyển tiếp (forward) gói tin qua các router trung gian khác trên mạng cho đến khi tới được mạng đích[1].

---

## 2) Lớp IPAddress

### Câu 2.1: So sánh phương thức IPAddress.Parse() và IPAddress.TryParse(). Tại sao trong các ứng dụng thực tế có tiếp nhận dữ liệu từ người dùng, TryParse() lại được ưu tiên sử dụng?

**1. So sánh IPAddress.Parse() và IPAddress.TryParse()**

| Tiêu chí | IPAddress.Parse() | IPAddress.TryParse() |
|---|---|---|
| Kiểu trả về | Đối tượng IPAddress[1] | Kiểu bool (true nếu thành công, false nếu thất bại) |
| Cách lấy kết quả | Trả về trực tiếp[1] | Thông qua tham số đầu ra `out IPAddress address` |
| Xử lý khi chuỗi sai/null | Bắn ra ngoại lệ (Exception) như ArgumentNullException hoặc FormatException[1] | Không bắn ngoại lệ, chỉ trả về false và gán address = null |
| Cấu trúc mã nguồn | Phải bọc trong khối try-catch để bắt lỗi | Dùng trực tiếp trong cấu trúc điều kiện if-else |

**2. Tại sao IPAddress.TryParse() được ưu tiên khi tiếp nhận dữ liệu người dùng?**

Trong các ứng dụng thực tế tiếp nhận đầu vào từ người dùng (qua giao diện UI, ô nhập liệu, hoặc dòng lệnh), việc người dùng gõ sai định dạng IP là tình huống xảy ra thường xuyên. IPAddress.TryParse() được ưu tiên sử dụng vì các lý do sau:

- **Tối ưu hiệu năng (Performance):** Trong .NET Framework, việc tạo, ném và bắt ngoại lệ (Exception) tốn rất nhiều chi phí tài nguyên (CPU và bộ nhớ) do phải thực hiện unwinding stack trace. Nếu người dùng nhập sai IP liên tục, việc Parse() bắn ra ngoại lệ thường xuyên sẽ làm giảm hiệu năng ứng dụng.
- **Mã nguồn gọn gàng, dễ kiểm soát (Cleaner Code):** Với TryParse(), lập trình viên có thể kiểm tra trực tiếp trong câu lệnh điều kiện `if (IPAddress.TryParse(userInput, out IPAddress ip)) { ... } else { // Báo lỗi người dùng }`. Điều này giúp luồng xử lý rõ ràng hơn so với việc phải bọc khối try-catch cồng kềnh.
- **Tăng tính ổn định của ứng dụng (Robustness):** Giúp tránh nguy cơ ứng dụng bị dừng đột ngột (crash) do thiếu khối catch xử lý ngoại lệ khi gặp dữ liệu sai.

---

### Câu 2.2: Thuộc tính AddressFamily của IPAddress dùng để làm gì? Làm thế nào để phân biệt một địa chỉ là IPv4 (AddressFamily.InterNetwork) hay IPv6 (AddressFamily.InterNetworkV6) bằng mã lệnh C#?

**1. Vai trò của thuộc tính AddressFamily**

- **Xác định họ địa chỉ mạng (Address Family):** Thuộc tính AddressFamily (trả về giá trị enum `System.Net.Sockets.AddressFamily`) cho biết đối tượng IPAddress thuộc về kiến trúc / họ giao thức mạng nào[1].
- **Cấu hình Socket phù hợp:** Trong .NET Framework, việc phân biệt họ địa chỉ rất quan trọng khi khởi tạo Socket[1][2]. Thuộc tính này giúp chương trình biết chính xác địa chỉ đang xử lý là IPv4 hay IPv6 để truyền đúng tham số AddressFamily vào constructor của lớp Socket (ví dụ: `new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)`)[1].

**2. Cách phân biệt IPv4 và IPv6 bằng mã lệnh C#**

Để kiểm tra một đối tượng IPAddress là IPv4 hay IPv6, ta kiểm tra giá trị của `ip.AddressFamily` so với hai hằng số enum:

- `AddressFamily.InterNetwork`: Đại diện cho địa chỉ IPv4 (32-bit)[1][3].
- `AddressFamily.InterNetworkV6`: Đại diện cho địa chỉ IPv6 (128-bit)[2].

**Ví dụ mã C#:**

```csharp
using System;
using System.Net;
using System.Net.Sockets;

class Program
{
    static void CheckIPType(string ipString)
    {
        // Sử dụng TryParse để chuyển đổi chuỗi an toàn
        if (IPAddress.TryParse(ipString, out IPAddress ip))
        {
            // Kiểm tra thuộc tính AddressFamily
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                Console.WriteLine($"{ipString} là địa chỉ IPv4 (InterNetwork).");
            }
            else if (ip.AddressFamily == AddressFamily.InterNetworkV6)
            {
                Console.WriteLine($"{ipString} là địa chỉ IPv6 (InterNetworkV6).");
            }
            else
            {
                Console.WriteLine($"{ipString} thuộc họ địa chỉ khác: {ip.AddressFamily}");
            }
        }
        else
        {
            Console.WriteLine($"'{ipString}' không phải là địa chỉ IP hợp lệ.");
        }
    }

    static void Main()
    {
        CheckIPType("192.168.1.1");       // IPv4
        CheckIPType("fe80::1%11");        // IPv6
        CheckIPType("256.300.1.1");       // Không hợp lệ
    }
}
```
Câu 2.3: Nêu ý nghĩa và trường hợp sử dụng của hai địa chỉ IP đặc biệt:
a. IPAddress.Loopback (127.0.0.1).

Ý nghĩa: Đại diện cho địa chỉ vòng ngược (loopback address) của chính máy cục bộ (local host)[1]. Khi gửi dữ liệu đến địa chỉ này, gói tin mạng không được phát ra ngoài qua card mạng vật lý mà được quay ngược ngay tại tầng mạng của hệ điều hành trên chính máy đó[1].

Trường hợp sử dụng:

Kiểm thử và gỡ lỗi cục bộ (Local Testing & Debugging): Sử dụng khi bạn chạy cả chương trình Client và Server trên cùng một máy tính[2]. Client sẽ kết nối tới IPAddress.Loopback (hoặc 127.0.0.1) để kiểm tra logic truyền nhận dữ liệu mà không cần phải kết nối mạng thật hay phụ thuộc vào địa chỉ IP do router cấp[2][3].

Giao tiếp giữa các tiến trình (Inter-Process Communication - IPC): Dùng khi các ứng dụng hoặc dịch vụ chạy trên cùng một máy muốn trao đổi dữ liệu với nhau qua giao thức TCP/UDP một cách an toàn, tránh việc các thiết bị ngoài mạng nội bộ có thể truy cập vào.

b. IPAddress.Any (0.0.0.0)

Ý nghĩa: Đại diện cho địa chỉ mặt nạ đại diện (wildcard address) 0.0.0.0[1][4]. Địa chỉ này chỉ định rằng ứng dụng sẽ chấp nhận lưu lượng mạng đến từ bất kỳ giao diện mạng cục bộ nào (any local network interface) trên máy host[1][5].

Trường hợp sử dụng:

Lắng nghe kết nối phía Server (TcpListener / Socket.Bind): Thường được sử dụng khi khởi tạo ứng dụng Server (ví dụ: new TcpListener(IPAddress.Any, port) hoặc socket.Bind(new IPEndPoint(IPAddress.Any, port)))[6].

Hỗ trợ máy có nhiều card mạng (Multihomed host): Khi máy tính làm Server sở hữu nhiều giao diện mạng (ví dụ: vừa có IP Wi-Fi, vừa có IP mạng dây LAN, vừa có kết nối VPN), việc dùng IPAddress.Any cho phép Server lắng nghe và tiếp nhận yêu cầu gửi tới cổng đó từ tất cả các địa chỉ IP mà máy đang sở hữu[4][5].

Cấu hình điểm cuối nhận dữ liệu UDP: Khi lập trình UDP Client/Server, IPAddress.Any thường được truyền vào đối tượng IPEndPoint tham chiếu trong hàm Receive()/ReceiveFrom() để chuẩn bị tiếp nhận gói tin từ bất kỳ IP nguồn nào gửi tới[9][10].

Câu 2.4: Phương thức ip.GetAddressBytes() trả về mảng kích thước bao nhiêu byte đối với IPv4? Giải thích sự khác biệt giữa Network Byte Order (Big-Endian) và Host Byte Order (Little-Endian).
1. Kích thước mảng byte của ip.GetAddressBytes() đối với IPv4

Địa chỉ IPv4 là một số nhị phân 32-bit[1][2].

Khi gọi phương thức ip.GetAddressBytes(), kết quả trả về là một mảng byte[] có kích thước 4 byte[3]. Mảng này chứa 4 byte tương ứng với 4 octet của địa chỉ IP theo thứ tự mạng (Network Byte Order)[3][4]. (Ví dụ: địa chỉ IP 192.168.1.1 sẽ trả về mảng 4 phần tử lần lượt chứa các giá trị 192, 168, 1, 1).

2. So sánh Network Byte Order (Big-Endian) và Host Byte Order (Little-Endian)

Khi truyền hoặc lưu trữ các số nhị phân gồm nhiều byte (multibyte binary numbers), thứ tự sắp xếp các byte có thể theo hai dạng chính[5][6]:

Network Byte Order (Big-Endian):

Định nghĩa: Byte có trọng số lớn nhất (Most Significant Byte - MSB) sẽ được ghi/truyền đi đầu tiên (từ trái sang phải) và byte có trọng số nhỏ nhất được truyền cuối cùng[6].

Quy chuẩn: Hầu hết các giao thức mạng chuẩn (như TCP/IP) đều quy định bắt buộc dùng thứ tự này để trao đổi dữ liệu, nên nó mới được gọi là Network Byte Order[4].

Host Byte Order (Little-Endian):

Định nghĩa: Byte có trọng số nhỏ nhất (Least Significant Byte - LSB) sẽ được ghi/truyền đi đầu tiên, ngược lại hoàn toàn với Big-Endian[6].

Đặc điểm kiến trúc: Các kiến trúc máy tính phổ biến như Intel, AMD hay Alpha (chạy trên hệ điều hành Windows) mặc định lưu trữ dữ liệu trong bộ nhớ theo định dạng Little-Endian[4].

Tầm quan trọng của việc chuyển đổi:

Nếu máy gửi gửi số theo kiểu Big-Endian nhưng máy nhận lại đọc số theo Little-Endian, giá trị số nhận được sẽ bị sai lệch hoàn toàn[4][7]. Để đảm bảo tính tương thích giữa các ứng dụng chạy trên các kiến trúc phần cứng khác nhau, lập trình viên .NET nên chuyển đổi dữ liệu số nhiều byte sang Big-Endian khi gửi và chuyển lại dạng cục bộ khi nhận bằng các hàm của lớp IPAddress[7][8]:

IPAddress.HostToNetworkOrder(): Chuyển số từ thứ tự máy cục bộ sang thứ tự chuẩn mạng[8].

IPAddress.NetworkToHostOrder(): Chuyển số từ thứ tự mạng về lại thứ tự máy cục bộ[8].

3) Lớp IPHostEntry & Cơ chế Phân giải DNS
Câu 3.1: Đối tượng IPHostEntry lưu trữ những thông tin nào? Tại sao thuộc tính AddressList lại là một mảng IPAddress[] thay vì một giá trị đơn lẻ?
1. Thông tin được lưu trữ trong IPHostEntry

Đối tượng IPHostEntry lưu trữ 3 thuộc tính chính[2][3]:

HostName (string): Chuỗi chứa tên máy chủ chính thức (canonical host name) của host[2].

AddressList (IPAddress[]): Một mảng các đối tượng IPAddress chứa danh sách tất cả các địa chỉ IP liên kết với host đó[2].

Aliases (string[]): Một mảng các chuỗi đại diện cho danh sách các tên biệt danh (DNS alias names) đại diện cho host[2].

2. Lý do thuộc tính AddressList là mảng IPAddress[] thay vì một giá trị đơn lẻ

Trong kiến trúc mạng và DNS hiện đại, việc một máy host hoặc tên miền liên kết với nhiều địa chỉ IP là rất phổ biến[1][5]. Thuộc tính AddressList phải là một mảng vì các lý do sau:

Máy host có nhiều card/giao diện mạng (Multihomed host): Một máy tính hoặc máy chủ có thể gắn nhiều card mạng vật lý hoặc giao diện mạng ảo (ví dụ: vừa dùng Wi-Fi, vừa dùng mạng dây LAN, vừa nối VPN)[6][7]. Mỗi giao diện mạng này sở hữu một địa chỉ IP riêng, do đó tên host sẽ phân giải ra danh sách gồm nhiều địa chỉ IP[1].

Cân bằng tải và dự phòng (Load Balancing & Redundancy): Các máy chủ web hoặc dịch vụ lớn (như www.mkp.com) thường áp dụng kỹ thuật phân giải tên miền ra nhiều địa chỉ IP khác nhau[5][8]. Điều này cho phép phân tán lưu lượng truy cập của các client đến nhiều máy chủ thực tế khác nhau để tránh quá tải[1][5].

Hỗ trợ song song cả IPv4 và IPv6 (Dual-Stack): Ngày nay, một tên miền khi truy vấn DNS có thể trả về đồng thời cả địa chỉ IPv4 (bản ghi A) và địa chỉ IPv6 (bản ghi AAAA). Do đó, AddressList cần chứa tất cả các địa chỉ đại diện cho cả hai giao thức để client lựa chọn kết nối phù hợp.

Câu 3.2: Phân biệt cơ chế xử lý của phương thức Dns.GetHostEntry(host) trong hai trường hợp:
a. Khi tham số truyền vào là chuỗi Domain (ví dụ: "dlu.edu.vn")

Cơ chế xử lý: Thực hiện Phân giải thuận (Forward DNS Lookup)[3].

Quy trình:

.NET gửi truy vấn DNS (bản ghi A cho IPv4 hoặc AAAA cho IPv6) tới DNS Server để tra cứu danh sách địa chỉ IP tương ứng với tên miền.

DNS Server tìm kiếm và trả về thông tin danh sách IP của máy chủ lưu trữ tên miền đó.

Thông tin trong IPHostEntry trả về:

HostName: Tên miền chính thức (Canonical Name) của máy chủ[4][5].

AddressList: Mảng các đối tượng IPAddress chứa toàn bộ các địa chỉ IP (IPv4/IPv6) được liên kết với tên miền đó[4][5].

b. Khi tham số truyền vào là chuỗi IP (ví dụ: "8.8.8.8")

Cơ chế xử lý: Thực hiện Phân giải ngược (Reverse DNS Lookup / PTR Query)[2].

Quy trình:

.NET phân tích chuỗi nhập vào và nhận diện đây là một địa chỉ IP hợp lệ.

Dns.GetHostEntry() gửi truy vấn phân giải ngược (bản ghi PTR) tới DNS Server để tìm kiếm tên máy chủ/tên miền đại diện cho IP này.

Thông tin trong IPHostEntry trả về:

HostName: Tên miền/tên máy chủ thu được từ phân giải ngược (ví dụ: IP "8.8.8.8" sẽ trả về HostName là "dns.google").

AddressList: Mảng chứa địa chỉ IP tương ứng.

Lưu ý xử lý ngoại lệ: Nếu địa chỉ IP đầu vào không có bản ghi PTR trên hệ thống DNS (không có tên miền trỏ ngược về), phương thức sẽ ném ra ngoại lệ SocketException (Host not found)[2][6].

Câu 3.3: Ngoại lệ nào thường phát sinh (SocketException) khi gọi Dns.GetHostEntry() với một tên miền không tồn tại hoặc khi máy trạm mất kết nối Internet? Thuộc tính SocketErrorCode có vai trò gì?
1. Ngoại lệ phát sinh

Ngoại lệ chính được ném ra là SocketException[1].

Do Dns.GetHostEntry() phải truy vấn dịch vụ DNS để phân giải tên miền thành địa chỉ IP, nên nếu tên miền không tồn tại hoặc máy tính không thể kết nối tới DNS Server do mất Internet, quá trình tra cứu sẽ thất bại và bắn ra SocketException[1].

2. Vai trò của thuộc tính SocketErrorCode

Thuộc tính SocketErrorCode (trả về kiểu enum SocketError, tương ứng với mã số lỗi WinSock ở thuộc tính ErrorCode) đóng vai trò quan trọng trong việc gỡ lỗi và xử lý ngoại lệ[3]:

Phân loại chính xác nguyên nhân gây lỗi: Do SocketException là ngoại lệ chung được dùng cho rất nhiều tình huống lỗi mạng khác nhau, SocketErrorCode giúp ứng dụng xác định chính xác sự cố cụ thể vừa xảy ra[3][7].

Nhận diện các mã lỗi DNS / Mạng thường gặp:

SocketError.HostNotFound (Mã WinSock 11001 - WSAHOST_NOT_FOUND): Xuất hiện khi tên miền cung cấp không tồn tại trên hệ thống DNS[5].

SocketError.TryAgain (Mã WinSock 11002 - WSATRY_AGAIN): Xuất hiện khi không thể kết nối tới máy chủ DNS hoặc DNS server không phản hồi[5].

SocketError.NoData (Mã WinSock 11004 - WSANO_DATA): Tên miền có tồn tại nhưng không có bản ghi địa chỉ IP[5].

SocketError.NetworkDown (Mã WinSock 10050 - WSAENETDOWN): Xuất hiện khi hệ thống mạng hoặc card mạng của máy trạm bị ngắt kết nối[5].

Hỗ trợ viết mã xử lý lỗi linh hoạt (Error Handling): Cho phép lập trình viên kiểm tra mã lỗi qua câu lệnh điều kiện (như switch (ex.SocketErrorCode)) để đưa ra xử lý phù hợp cho từng trường hợp, chẳng hạn như nhắc người dùng kiểm tra lại kết nối mạng hoặc báo tên miền không hợp lệ.

4) IPEndPoint & Giao diện Mạng
Câu 4.1: Đối tượng IPEndPoint được tạo nên từ sự kết hợp của hai thành phần dữ liệu nào? Vì sao các phương thức Bind(), Connect(), hay SendTo() trong Socket đều đòi hỏi tham số này?
Đối tượng IPEndPoint trong lớp System.Net của .NET biểu diễn một điểm kết nối mạng (Network Endpoint) trong giao thức TCP/IP[1].

1. Hai thành phần dữ liệu tạo nên IPEndPoint

Đối tượng IPEndPoint được kết hợp từ hai thành phần dữ liệu chính[1][2]:

Địa chỉ IP (IPAddress): Xác định máy host (thiết bị hoặc giao diện mạng) trên mạng Internet/LAN[2][3].

Số hiệu cổng (Port - kiểu int): Xác định socket hoặc tiến trình ứng dụng cụ thể chạy trên máy host đó[2].

2. Lý do các phương thức Bind(), Connect(), và SendTo() đòi hỏi tham số này

Trong giao tiếp mạng TCP/IP, địa chỉ IP đơn thuần chỉ định vị được máy tính đích, còn số hiệu port mới giúp phân phát dữ liệu đến đúng ứng dụng[3]. Do đó, mọi thao tác định tuyến, thiết lập hoặc truyền nhận dữ liệu qua Socket đều yêu cầu đầy đủ bộ thông tin điểm cuối (IP + Port)[1][3]:

Đối với Bind() (Ràng buộc socket cục bộ):

Mục đích: Gán một địa chỉ IP cục bộ và một số hiệu port cụ thể cho socket hiện tại trên máy local[5][6].

Lý do cần IPEndPoint: Hệ điều hành cần biết socket này sẽ mở và lắng nghe/nhận dữ liệu trên giao diện mạng nào (IP cục bộ hoặc IPAddress.Any) và cổng dịch vụ nào (Port number)[5][7].

Đối với Connect() (Kết nối tới máy từ xa):

Mục đích: Thiết lập kết nối (đặc biệt trong giao thức TCP) tới Server[8][9].

Lý do cần IPEndPoint: Client cần chỉ định rõ IP của máy chủ đích và Port dịch vụ của máy chủ (ví dụ: Port 80 cho HTTP, Port 7 cho Echo) để gửi gói tin bắt tay thiết lập kết nối (TCP 3-way handshake)[8].

Đối với SendTo() (Gửi dữ liệu UDP không kết nối):

Mục đích: Truyền một gói tin datagram đến một điểm cuối xác định qua giao thức UDP[12][13].

Lý do cần IPEndPoint: Do UDP là giao thức không hướng kết nối (connectionless), từng gói tin gửi đi độc lập bắt buộc phải mang đầy đủ thông tin địa chỉ đích (IP + Port) để hạ tầng mạng và máy nhận phân phát dữ liệu chính xác vào ứng dụng[12].

Câu 4.2: Namespace System.Net.NetworkInformation (đặc biệt là lớp NetworkInterface) cung cấp những thông tin phần cứng mạng nào mà lớp Dns không thể hỗ trợ?
Lớp Dns trong .NET chỉ phục vụ mục đích phân giải tên miền và địa chỉ IP, trả về thông tin qua đối tượng IPHostEntry (chứa HostName, AddressList và Aliases)[1]. Lớp này hoạt động ở mức dịch vụ tên miền nên hoàn toàn không cung cấp thông tin về phần cứng mạng hay cấu hình card mạng cục bộ[1][4].

Ngược lại, namespace System.Net.NetworkInformation (đặc biệt là lớp NetworkInterface) cho phép ứng dụng truy vấn sâu vào các giao diện/card mạng vật lý và ảo (NIC - Network Interface Card) trên máy host[5][6], cung cấp các thông tin phần cứng mà Dns không hỗ trợ:

Địa chỉ MAC vật lý (Physical/MAC Address): Cung cấp địa chỉ MAC phần cứng độc nhất của card mạng thông qua phương thức GetPhysicalAddress().

Mô tả thiết bị phần cứng (Description & Name): Tên chi tiết và model phần cứng do nhà sản xuất cung cấp (ví dụ: "Intel(R) Wi-Fi 6 AX201 160MHz" hay "Realtek PCIe GbE Family Controller").

Trạng thái hoạt động thực tế (Operational Status): Cho biết card mạng vật lý đang kết nối hay bị rút dây/tắt (Up, Down, Testing, v.v.).

Phân loại giao diện mạng (Network Interface Type): Phân biệt kiểu kết nối vật lý như Ethernet, Wireless (Wi-Fi 802.11), Loopback, hay Tunnel/VPN.

Tốc độ đường truyền phần cứng (Speed): Cho biết tốc độ băng thông kết nối tối đa của card mạng tại thời điểm hiện tại (tính bằng bit/giây - bps).

Thống kê lưu lượng mạng (Network Statistics): Cung cấp số liệu chi tiết về số byte/gói tin đã truyền và nhận, số gói tin bị lỗi (errors) hoặc bị hủy (discards) trên card mạng thông qua phương thức GetIPStatistics().

Cấu hình IP chi tiết gắn với từng giao diện (IP Properties): Thông qua GetIPProperties(), lớp này cung cấp Subnet Mask, Default Gateway, địa chỉ Server DHCP, Server DNS được cấu hình riêng cho card mạng đó.