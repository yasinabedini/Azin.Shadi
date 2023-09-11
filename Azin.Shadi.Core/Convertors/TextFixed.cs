using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azin.Shadi.Core.Convertors
{
    public class TextFixed
    {
        public static string EmailFix(string email)
        {
            return email.Trim().ToLower();
        }


    }
}
