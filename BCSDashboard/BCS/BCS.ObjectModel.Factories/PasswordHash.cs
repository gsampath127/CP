using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BCS.ObjectModel.Factories
{
    public class PasswordHash
    {
       

        public static string ComputeHash(string password)
        {
            string siteWideSalt = "4c553b40-ac4f-4ab5-b95f-0f559a8caef8";
            string encryptedPassword;
            encryptedPassword = HashSHA512Managed(siteWideSalt + password);                  
            return encryptedPassword;
        }
        private static string HashSHA512Managed(string saltedPassword)
        {
            UnicodeEncoding uniEncode = new UnicodeEncoding();
            SHA512Managed sha = new SHA512Managed();
            byte[] bytePassword = uniEncode.GetBytes(saltedPassword);
            byte[] hash = sha.ComputeHash(bytePassword);
            return Convert.ToBase64String(hash);
        }

    }
}
