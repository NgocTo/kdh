using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace kdh.Utils
{
    public static class Hasher
    {
        // Takes string as an argument and return hashed string
        public static string ToHashedStr(string str)
        {
            // encode val to utf-8 and store as an array of byte
            byte[] byteVal = Encoding.UTF8.GetBytes(str);

            // calculate hashed value
            SHA256 sha256 = new SHA256CryptoServiceProvider();
            byte[] hash256Val = sha256.ComputeHash(byteVal);

            // get result of the calculation in utf8 string format
            StringBuilder hashedText = new StringBuilder();
            for (int i = 0; i < hash256Val.Length; i++)
            {
                // hexadecimal to string
                hashedText.AppendFormat("{0:X2}", hash256Val[i]);
            }
            return hashedText.ToString();
        }

        // Check if user provided hased value and stored hash matches or not
        public static bool IsCorrespond(string str, string hash)
        {
            return ToHashedStr(str) == hash;
        }


    }

}