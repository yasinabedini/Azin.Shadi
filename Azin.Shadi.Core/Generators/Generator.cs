using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azin.Shadi.Core.Generators
{
    public class Generator
    {
        public static string CreateUniqueText()
        {
            return Guid.NewGuid().ToString().Replace("-","");
        }
    }
}
