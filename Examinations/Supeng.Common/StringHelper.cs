using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supeng.Common
{
    public static class StringHelper
    {
        public static List<string> Decompress(this string str, char c)
        {
            return str.Split(c).ToList();
        }
    }
}
