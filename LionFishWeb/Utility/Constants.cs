using Jint;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace LionFishWeb.Utility
{
    public static class Constants
    {
        public static string Conn => ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        public const string captchaSecret = "6LcPQ30UAAAAABFqqSAazpaMGWObvP6lCuSZggbR";

        private static readonly int SaltLengthLimit = 16;
        private static byte[] GetSalt()
        {
            return GetSalt(SaltLengthLimit);
        }
        private static byte[] GetSalt(int maximumSaltLength)
        {
            var salt = new byte[maximumSaltLength];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }
            return salt;
        }
        public static string SaltPass(string pass)
        {
            byte[] salt = GetSalt();
            var pbkdf2 = new Rfc2898DeriveBytes(pass, salt, 250);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hbyt = new byte[36];
            Array.Copy(salt, 0, hbyt, 0, 16);
            Array.Copy(hash, 0, hbyt, 16, 20);
            return Convert.ToBase64String(hbyt);
        }
        public static int ZXCVBN(string p)
        {
            var s = System.IO.File.ReadAllText("C:/Users/Jupiter/source/repos/LionFishWeb/LionFishWeb/Scripts/zxcvbn.js");
            int x = 10;

            var engine = new Engine();
            var al = new System.Collections.ArrayList(2)
            {
                x,
                p
            };
            engine.SetValue("al", al);
            engine.Execute(s);
            engine.Execute(@"
                var z = zxcvbn(al[1]);
                al[0] = z.score;
            ");

            return Convert.ToInt16(al[0]);
        }
    }
}