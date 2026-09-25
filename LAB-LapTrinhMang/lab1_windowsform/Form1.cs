using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace lab1_windowsform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // ============================================================
        // NÚT PHÂN GIẢI
        // ============================================================
        private void btnResolve_Click(object sender, EventArgs e)
        {
            string input = txtInput.Text.Trim();

            if (string.IsNullOrEmpty(input))
            {
                MessageBox.Show("Vui lòng nhập Domain hoặc địa chỉ IP!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtInput.Focus();
                return;
            }

            ResetKetQua();

            try
            {
                IPHostEntry hostInfo = Dns.GetHostEntry(input);

                lblHostNameValue.Text = hostInfo.HostName;

                int countV4 = 0, countV6 = 0;

                foreach (IPAddress ipaddr in hostInfo.AddressList)
                {
                    if (ipaddr.AddressFamily == AddressFamily.InterNetwork)
                    {
                        lstIPv4.Items.Add(ipaddr.ToString());
                        countV4++;
                    }
                    else if (ipaddr.AddressFamily == AddressFamily.InterNetworkV6)
                    {
                        lstIPv6.Items.Add(ipaddr.ToString());
                        countV6++;
                    }
                }

                grpIPv4.Text = $"IPv4 Addresses ({countV4})";
                grpIPv6.Text = $"IPv6 Addresses ({countV6})";

                lblStatus.ForeColor = Color.Green;
                lblStatus.Text = $"✔ Phân giải thành công! Tìm thấy {countV4} IPv4 và {countV6} IPv6.";
            }
            catch (SocketException ex)
            {
                lblStatus.ForeColor = Color.Red;
                lblStatus.Text = $"✘ Lỗi SocketException: {ex.SocketErrorCode} (Code: {ex.ErrorCode})";

                string chiTiet = $"Không thể phân giải '{input}'.\n\n" +
                                 $"• SocketErrorCode : {ex.SocketErrorCode}\n" +
                                 $"• ErrorCode       : {ex.ErrorCode}\n" +
                                 $"• Message         : {ex.Message}\n";

                switch (ex.SocketErrorCode)
                {
                    case SocketError.HostNotFound:
                        chiTiet += "\n➜ Không tìm thấy host này trong hệ thống DNS.";
                        break;
                    case SocketError.NoData:
                        chiTiet += "\n➜ Host tồn tại nhưng không có bản ghi IP nào.";
                        break;
                    case SocketError.TryAgain:
                        chiTiet += "\n➜ DNS tạm thời không phản hồi, vui lòng thử lại.";
                        break;
                    case SocketError.NoRecovery:
                        chiTiet += "\n➜ Lỗi không thể phục hồi từ DNS.";
                        break;
                    default:
                        chiTiet += "\n➜ Lỗi không xác định.";
                        break;
                }

                MessageBox.Show(chiTiet, "Lỗi phân giải DNS",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ArgumentException ex)
            {
                lblStatus.ForeColor = Color.Red;
                lblStatus.Text = "✘ Tên miền không hợp lệ!";
                MessageBox.Show(ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                lblStatus.ForeColor = Color.Red;
                lblStatus.Text = "✘ Có lỗi xảy ra!";
                MessageBox.Show($"{ex.GetType().Name}: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ============================================================
        // Xóa kết quả cũ
        // ============================================================
        private void ResetKetQua()
        {
            lblHostNameValue.Text = "";
            lstIPv4.Items.Clear();
            lstIPv6.Items.Clear();
            lblStatus.Text = "";
            grpIPv4.Text = "IPv4 Addresses";
            grpIPv6.Text = "IPv6 Addresses";
        }

        // ============================================================
        // NÚT XÓA
        // ============================================================
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtInput.Clear();
            ResetKetQua();
            txtInput.Focus();
        }

        // ============================================================
        // NÚT THOÁT
        // ============================================================
        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc muốn thoát chương trình?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        // ============================================================
        // Nhấn Enter trong txtInput = nhấn nút Phân giải
        // ============================================================
        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnResolve_Click(sender, e);
                e.SuppressKeyPress = true;
            }
        }

        // ============================================================
        // Nhấp đúp vào ListBox IPv4 → Copy vào clipboard
        // ============================================================
        private void lstIPv4_DoubleClick(object sender, EventArgs e)
        {
            if (lstIPv4.SelectedItem != null)
            {
                Clipboard.SetText(lstIPv4.SelectedItem.ToString());
                lblStatus.ForeColor = Color.Blue;
                lblStatus.Text = "📋 Đã copy: " + lstIPv4.SelectedItem.ToString();
            }
        }

        // ============================================================
        // Nhấp đúp vào ListBox IPv6 → Copy vào clipboard
        // ============================================================
        private void lstIPv6_DoubleClick(object sender, EventArgs e)
        {
            if (lstIPv6.SelectedItem != null)
            {
                Clipboard.SetText(lstIPv6.SelectedItem.ToString());
                lblStatus.ForeColor = Color.Blue;
                lblStatus.Text = "📋 Đã copy: " + lstIPv6.SelectedItem.ToString();
            }
        }
    }
}