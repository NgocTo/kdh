using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kdh.Utils
{
    public static class TokenGenerator
    {
        public static string GenerateEmailToken()
        {
            string now = DateTime.Now.ToString();
            string emailTk = Guid.NewGuid().ToString();
            string emailToken = Hasher.ToHashedStr(now + emailTk);

            return emailToken;
        }
    }
}