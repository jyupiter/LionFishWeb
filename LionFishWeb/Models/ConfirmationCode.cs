using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.ComponentModel.DataAnnotations;

namespace LionFishWeb.Models
{
    public class ConfirmationCode
    {
        [Required]
        public string Code { get; set; }
        [Required, EmailAddress, RegularExpression(@"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
        public bool IsPasswordReset { get; set; }
        public DateTime Date { get; set; }

        public ConfirmationCode() { }

        public ConfirmationCode(string email, bool ispr)
        {
            Code = Connect();
            Email = email;
            IsPasswordReset = ispr;
        }

        private string GenerateSegment()
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            string result = "";
            var byteArray = new byte[2];
            provider.GetBytes(byteArray);
            var randomInteger = BitConverter.ToUInt16(byteArray, 0);
            return result + randomInteger;
        }

        private string Connect()
        {
            string result = "";
            string temp = "";
            for (int i = 0; i < 4; i++)
            {
                if (i == 3)
                    temp = GenerateSegment();
                else
                    temp = GenerateSegment() + "-";
                result += temp;
            }
            return result;
        }
    }
}