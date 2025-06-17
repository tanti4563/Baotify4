using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Service.Helper
{
    public class EncryptHelper
    {
        public static string ComputeMd5Hash(string input)
        {
            // Sử dụng MD5 để tạo băm
            using (MD5 md5 = MD5.Create())
            {
                // Chuyển chuỗi đầu vào thành mảng byte
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                // Tạo hash từ byte array
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Chuyển byte array thành chuỗi hex
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}