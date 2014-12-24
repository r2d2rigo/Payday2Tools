using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PD2Bundle
{
    class Hash64
    {
        public static unsafe ulong HashString(string input, ulong level = 0)
        {
            fixed (byte* data = UTF8Encoding.UTF8.GetBytes(input))
            {
                return Hash64Managed.Hash64.Hash(data, (ulong)UTF8Encoding.UTF8.GetByteCount(input), level);
            }
        }
    }

}
