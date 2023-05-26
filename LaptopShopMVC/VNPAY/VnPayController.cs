using LaptopShopMVC.Email;
using LaptopShopMVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LaptopShopMVC.VNPAY
{
    public class VnPayController : Controller
    {
        // GET: VnPay
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Payment(decimal tongTien)
        {
            string tongTienThanhToan = (tongTien * 100).ToString();
            string url = ConfigurationManager.AppSettings["Url"];
            string returnUrl = ConfigurationManager.AppSettings["ReturnUrl"];
            string tmnCode = ConfigurationManager.AppSettings["TmnCode"];
            string hashSecret = ConfigurationManager.AppSettings["HashSecret"];

            PayLib pay = new PayLib();

            pay.AddRequestData("vnp_Version", "2.1.0"); //Phiên bản api mà merchant kết nối. Phiên bản hiện tại là 2.1.0
            pay.AddRequestData("vnp_Command", "pay"); //Mã API sử dụng, mã cho giao dịch thanh toán là 'pay'
            pay.AddRequestData("vnp_TmnCode", tmnCode); //Mã website của merchant trên hệ thống của VNPAY (khi đăng ký tài khoản sẽ có trong mail VNPAY gửi về)
            pay.AddRequestData("vnp_Amount", tongTienThanhToan); //số tiền cần thanh toán, công thức: số tiền * 100 - ví dụ 10.000 (mười nghìn đồng) --> 1000000
            pay.AddRequestData("vnp_BankCode", ""); //Mã Ngân hàng thanh toán (tham khảo: https://sandbox.vnpayment.vn/apis/danh-sach-ngan-hang/), có thể để trống, người dùng có thể chọn trên cổng thanh toán VNPAY
            pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss")); //ngày thanh toán theo định dạng yyyyMMddHHmmss
            pay.AddRequestData("vnp_CurrCode", "VND"); //Đơn vị tiền tệ sử dụng thanh toán. Hiện tại chỉ hỗ trợ VND
            pay.AddRequestData("vnp_IpAddr", Util.GetIpAddress()); //Địa chỉ IP của khách hàng thực hiện giao dịch
            pay.AddRequestData("vnp_Locale", "vn"); //Ngôn ngữ giao diện hiển thị - Tiếng Việt (vn), Tiếng Anh (en)
            pay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang"); //Thông tin mô tả nội dung thanh toán
            pay.AddRequestData("vnp_OrderType", "other"); //topup: Nạp tiền điện thoại - billpayment: Thanh toán hóa đơn - fashion: Thời trang - other: Thanh toán trực tuyến
            pay.AddRequestData("vnp_ReturnUrl", returnUrl); //URL thông báo kết quả giao dịch khi Khách hàng kết thúc thanh toán
            pay.AddRequestData("vnp_TxnRef", DateTime.Now.Ticks.ToString()); //mã hóa đơn

            string paymentUrl = pay.CreateRequestUrl(url, hashSecret);

            return Redirect(paymentUrl);
        }
        LaptopDBContext context = new LaptopDBContext();

        public void sendingEMail()
        {
            TAIKHOANKHACHHANG taiKhoanKH = context.TAIKHOANKHACHHANGs.FirstOrDefault(p => p.TENDANGNHAP.Contains(User.Identity.Name));

            DONHANG donhang_KH = context.DONHANGs.Where(m => m.MAKHACHHANG == taiKhoanKH.KHACHHANG.MAKHACHHANG).ToArray().Last();
            EmailViewModels emailVm = new EmailViewModels();

            emailVm.EmailBody = @"<h2>Hello " + taiKhoanKH.KHACHHANG.TENKHACHHANG + "! </h2> <br />" +
                            "<h3>THÔNG TIN ĐƠN HÀNG</h3>" +
                            "Mã Đơn Hàng: " + donhang_KH.MADONHANG + "<br/>" +
                            "Ngày Đặt: " + donhang_KH.NGAYTHANHTOAN + "<br/>" +
                            "Tổng Tiền: " + donhang_KH.TONGTIEN + "<br/>" +
                            "Ngày gửi: " + DateTime.Now.ToString() + "<br/>" +
                            "<br/>Thanks for shopping with FRICA!";

            emailVm.SenderEmailAddress = System.Configuration.ConfigurationManager.AppSettings["SenderEmail"];
            emailVm.SenderPassword = System.Configuration.ConfigurationManager.AppSettings["SenderPassword"];
            emailVm.SmtpHostServer = System.Configuration.ConfigurationManager.AppSettings["smtpHostServer"];
            emailVm.SmtpPort = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["smtpPort"]);
            emailVm.ReceiverEmailAddress = taiKhoanKH.KHACHHANG.EMAIL;
            emailVm.EmailSubject = "FRICA - COMFIRM";
            try
            {
                var client = new SmtpClient(emailVm.SmtpHostServer, emailVm.SmtpPort)
                {
                    EnableSsl = true,
                    Timeout = 100000,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(emailVm.SenderEmailAddress, emailVm.SenderPassword)
                };
                var mailMessage = new MailMessage
                {
                    IsBodyHtml = true,
                    BodyEncoding = Encoding.UTF8,
                    From = new MailAddress(emailVm.SenderEmailAddress)
                };
                mailMessage.To.Add(emailVm.ReceiverEmailAddress);
                mailMessage.Subject = emailVm.EmailSubject;
                mailMessage.Body = emailVm.EmailBody;
                client.Send(mailMessage);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult PaymentConfirm()
        {
            if (Request.QueryString.Count > 0)
            {
                string hashSecret = ConfigurationManager.AppSettings["HashSecret"]; //Chuỗi bí mật
                var vnpayData = Request.QueryString;
                PayLib pay = new PayLib();

                //lấy toàn bộ dữ liệu được trả về
                foreach (string s in vnpayData)
                {
                    if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                    {
                        pay.AddResponseData(s, vnpayData[s]);
                    }
                }

                long orderId = Convert.ToInt64(pay.GetResponseData("vnp_TxnRef")); //mã hóa đơn
                long vnpayTranId = Convert.ToInt64(pay.GetResponseData("vnp_TransactionNo")); //mã giao dịch tại hệ thống VNPAY
                string vnp_ResponseCode = pay.GetResponseData("vnp_ResponseCode"); //response code: 00 - thành công, khác 00 - xem thêm https://sandbox.vnpayment.vn/apis/docs/bang-ma-loi/
                string vnp_SecureHash = Request.QueryString["vnp_SecureHash"]; //hash của dữ liệu trả về

                bool checkSignature = pay.ValidateSignature(vnp_SecureHash, hashSecret); //check chữ ký đúng hay không?

                if (checkSignature)
                {
                    if (vnp_ResponseCode == "00")
                    {
                        ViewBag.Message = "Thanh toán thành công hóa đơn: " + orderId;
                        ViewBag.MaGiaoDich = "Mã giao dịch: " + vnpayTranId;
                        sendingEMail();
                        return RedirectToAction("paymentSuccessful", "Home");
                    }
                    else
                    {
                        ViewBag.Message = "Có lỗi xảy ra trong quá trình xử lý hóa đơn: " + orderId;
                        ViewBag.MaGiaoDich = "Mã giao dịch: " + vnpayTranId;
                        ViewBag.MaLoi = "Mã lỗi: " + vnp_ResponseCode;
                        return RedirectToAction("errorPayment", "Home");
                    }
                }
                else
                {
                    ViewBag.Message = "Có lỗi xảy ra trong quá trình xử lý";
                }
            }

            return RedirectToAction("errorResult");
        }
    }
}