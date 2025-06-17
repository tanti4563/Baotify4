using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helper
{
    public class VNPayHelper
    {
        private const string SECRET_KEY = "0KASCDVFSAQY6BNYJHUWKBKJXM6";

        public static string GenerateSecureHash(string rspRaw)
        {
            try
            {
                // Hash generation: HmacSHA512(secretKey + rspRaw, secretKey)
                var dataToHash = SECRET_KEY + rspRaw;
                var keyBytes = Encoding.UTF8.GetBytes(SECRET_KEY);
                var dataBytes = Encoding.UTF8.GetBytes(dataToHash);

                using (var hmac = new HMACSHA512(keyBytes))
                {
                    var hashBytes = hmac.ComputeHash(dataBytes);
                    return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                }
            }
            catch (Exception ex)
            {
                new LoggingHelper().WriteLog("VNPay Hash Generation Error: " + ex.Message);
                return string.Empty;
            }
        }

        public static bool ValidateSecureHash(string data, string hash)
        {
            try
            {
                var generatedHash = GenerateSecureHash(data);
                return string.Equals(generatedHash, hash, StringComparison.OrdinalIgnoreCase);
            }
            catch (Exception ex)
            {
                new LoggingHelper().WriteLog("VNPay Hash Validation Error: " + ex.Message);
                return false;
            }
        }

        public static string BuildQueryString(Dictionary<string, string> parameters)
        {
            var sortedParams = parameters.OrderBy(x => x.Key);
            var queryString = string.Join("&", sortedParams.Select(x => $"{x.Key}={x.Value}"));
            return queryString;
        }

        public static Dictionary<string, string> ParseVNPayResponse(string queryString)
        {
            var parameters = new Dictionary<string, string>();

            if (string.IsNullOrEmpty(queryString))
                return parameters;

            var pairs = queryString.Split('&');
            foreach (var pair in pairs)
            {
                var keyValue = pair.Split('=');
                if (keyValue.Length == 2)
                {
                    parameters[keyValue[0]] = keyValue[1];
                }
            }

            return parameters;
        }

        public static string GetResponseMessage(string responseCode)
        {
            switch (responseCode)
            {
                case "00": return "Thanh toán thành công";
                case "01": return "Không tồn tại mã đặt chỗ";
                case "02": return "Đơn hàng đã tồn tại";
                case "04": return "Số tiền thanh toán không chính xác";
                case "97": return "Chữ ký không hợp lệ";
                case "99": return "Các lỗi khác";
                default: return "Lỗi không xác định";
            }
        }


    }
}
