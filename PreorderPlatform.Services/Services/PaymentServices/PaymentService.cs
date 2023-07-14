using AutoMapper;
using PreorderPlatform.Entity.Models;
using PreorderPlatform.Entity.Repositories.PaymentRepositories;
using PreorderPlatform.Service.ViewModels.Payment;
using PreorderPlatform.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PreorderPlatform.Service.Utility;
using Microsoft.EntityFrameworkCore;
using PreorderPlatform.Service.Utility.Pagination;
using PreorderPlatform.Service.Enum;
using MoMo;
using Newtonsoft.Json.Linq;

namespace PreorderPlatform.Service.Services.PaymentServices
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;

        public PaymentService(IPaymentRepository paymentRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }

        public async Task<JObject> TestMomo()
        {
            try
            {
                //request params need to request to MoMo system
                string endpoint = "https://test-payment.momo.vn/v2/gateway/api/create";
                string partnerCode = "MOMO5RGX20191128";
                string accessKey = "M8brj9K6E22vXoDB";
                string serectkey = "nqQiVSgDMy809JoPF6OzP5OdBUB550Y4";
                string orderInfo = "cac";
                string redirectUrl = "localhost://7070";
                string ipnUrl = "localhost://7070";
                string requestType = "captureWallet";

                string amount = "55000";
                string orderId = Guid.NewGuid().ToString();
                string requestId = Guid.NewGuid().ToString();
                string extraData = "";

                //Before sign HMAC SHA256 signature
                string rawHash = "accessKey=" + accessKey +
                    "&amount=" + amount +
                    "&extraData=" + extraData +
                    "&ipnUrl=" + ipnUrl +
                    "&orderId=" + orderId +
                    "&orderInfo=" + orderInfo +
                    "&partnerCode=" + partnerCode +
                    "&redirectUrl=" + redirectUrl +
                    "&requestId=" + requestId +
                    "&requestType=" + requestType
                    ;

                //log.Debug("rawHash = " + rawHash);

                MoMoSecurity crypto = new MoMoSecurity();
                //sign signature SHA256
                string signature = crypto.signSHA256(rawHash, serectkey);
                //log.Debug("Signature = " + signature);

                //build body json request
                JObject message = new JObject
            {
                { "partnerCode", partnerCode },
                { "partnerName", "Test" },
                { "storeId", "MomoTestStore" },
                { "requestId", requestId },
                { "amount", amount },
                { "orderId", orderId },
                { "orderInfo", orderInfo },
                { "redirectUrl", redirectUrl },
                { "ipnUrl", ipnUrl },
                { "lang", "en" },
                { "extraData", extraData },
                { "requestType", requestType },
                { "signature", signature }

            };
                //log.Debug("Json request to MoMo: " + message.ToString());
                string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());

                JObject jmessage = JObject.Parse(responseFromMomo);
                //log.Debug("Return from MoMo: " + jmessage.ToString());
                //DialogResult result = MessageBox.Show(responseFromMomo, "Open in browser", MessageBoxButtons.OKCancel);
           
                jmessage.GetValue("payUrl").ToString();

                Console.WriteLine($"Momo {jmessage}");

                return jmessage;

            }
            catch (Exception ex)
            {
                throw new ServiceException("An error occurred while fetching payments.", ex);
            }
        }

        public async Task<String> TestVNPay()
        {
            try
            {
                string url = "http://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
                string returnUrl = "https://localhost:44393/Home/PaymentConfirm";

                string tmnCode = "B3GJ4EAH";
                string hashSecret = "RAOFLVMYIXMFIPSSRIFYIAWLBOSIJTPQ";

                PayLib pay = new PayLib();

                pay.AddRequestData("vnp_Version", "2.1.0"); //Phiên bản api mà merchant kết nối. Phiên bản hiện tại là 2.1.0
                pay.AddRequestData("vnp_Command", "pay"); //Mã API sử dụng, mã cho giao dịch thanh toán là 'pay'
                pay.AddRequestData("vnp_TmnCode", tmnCode); //Mã website của merchant trên hệ thống của VNPAY (khi đăng ký tài khoản sẽ có trong mail VNPAY gửi về)
                pay.AddRequestData("vnp_Amount", "1000000"); //số tiền cần thanh toán, công thức: số tiền * 100 - ví dụ 10.000 (mười nghìn đồng) --> 1000000
                pay.AddRequestData("vnp_BankCode", ""); //Mã Ngân hàng thanh toán (tham khảo: https://sandbox.vnpayment.vn/apis/danh-sach-ngan-hang/), có thể để trống, người dùng có thể chọn trên cổng thanh toán VNPAY
                pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss")); //ngày thanh toán theo định dạng yyyyMMddHHmmss
                pay.AddRequestData("vnp_CurrCode", "VND"); //Đơn vị tiền tệ sử dụng thanh toán. Hiện tại chỉ hỗ trợ VND
                pay.AddRequestData("vnp_IpAddr", "192.168.1.1"); //Địa chỉ IP của khách hàng thực hiện giao dịch
                pay.AddRequestData("vnp_Locale", "vn"); //Ngôn ngữ giao diện hiển thị - Tiếng Việt (vn), Tiếng Anh (en)
                pay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang"); //Thông tin mô tả nội dung thanh toán
                pay.AddRequestData("vnp_OrderType", "other"); //topup: Nạp tiền điện thoại - billpayment: Thanh toán hóa đơn - fashion: Thời trang - other: Thanh toán trực tuyến
                pay.AddRequestData("vnp_ReturnUrl", returnUrl); //URL thông báo kết quả giao dịch khi Khách hàng kết thúc thanh toán
                pay.AddRequestData("vnp_TxnRef", DateTime.Now.Ticks.ToString()); //mã hóa đơn

                string paymentUrl = pay.CreateRequestUrl(url, hashSecret);

                return (paymentUrl);

            }
            catch (Exception ex)
            {
                throw new ServiceException("An error occurred while fetching payments.", ex);
            }
        }

        public async Task<List<PaymentViewModel>> GetPaymentsAsync()
        {
            try
            {
                var payments = await _paymentRepository.GetAllAsync();
                return _mapper.Map<List<PaymentViewModel>>(payments);
            }
            catch (Exception ex)
            {
                throw new ServiceException("An error occurred while fetching payments.", ex);
            }
        }

        public async Task<PaymentViewModel> GetPaymentByIdAsync(int id)
        {
            try
            {
                var payment = await _paymentRepository.GetByIdAsync(id);

                if (payment == null)
                {
                    throw new NotFoundException($"Payment with ID {id} was not found.");
                }

                return _mapper.Map<PaymentViewModel>(payment);
            }
            catch (NotFoundException)
            {
                // Rethrow NotFoundException to be handled by the caller
                throw;
            }
            catch (Exception ex)
            {
                throw new ServiceException($"An error occurred while fetching payment with ID {id}.", ex);
            }
        }

        public async Task<PaymentViewModel> CreatePaymentAsync(PaymentCreateViewModel model)
        {
            try
            {
                var payment = _mapper.Map<Payment>(model);
                await _paymentRepository.CreateAsync(payment);
                return _mapper.Map<PaymentViewModel>(payment);
            }
            catch (Exception ex)
            {
                throw new ServiceException("An error occurred while creating the payment.", ex);
            }
        }

        public async Task UpdatePaymentAsync(PaymentUpdateViewModel model)
        {
            try
            {
                var payment = await _paymentRepository.GetByIdAsync(model.Id);
                payment = _mapper.Map(model, payment);
                await _paymentRepository.UpdateAsync(payment);
            }
            catch (Exception ex)
            {
                throw new ServiceException($"An error occurred while updating payment with ID {model.Id}.", ex);
            }
        }

        public async Task DeletePaymentAsync(int id)
        {
            try
            {
                var payment = await _paymentRepository.GetByIdAsync(id);
                await _paymentRepository.DeleteAsync(payment);
            }
            catch (Exception ex)
            {
                throw new ServiceException($"An error occurred while deleting payment with ID {id}.", ex);
            }
        }

        public async Task<(IList<PaymentViewModel> payments, int totalItems)> GetAsync(PaginationParam<PaymentEnum.PaymentSort> paginationModel, PaymentSearchRequest filterModel)
        {
            try
            {
                var query = _paymentRepository.Table;

                query = query.GetWithSearch(filterModel); //search

                // Calculate the total number of items before applying pagination
                int totalItems = await query.CountAsync();

                query = query.GetWithSorting(paginationModel.SortKey.ToString(), paginationModel.SortOrder) //sort
                            .GetWithPaging(paginationModel.Page, paginationModel.PageSize);  // pagination

                var paymentList = await query.ToListAsync(); // Call ToListAsync here

                // Map the paymentList to a list of PaymentViewModel objects
                var result = _mapper.Map<List<PaymentViewModel>>(paymentList);

                return (result, totalItems);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception " + ex.Message);
                throw new ServiceException("An error occurred while fetching payments.", ex);
            }
        }
        
    }
}