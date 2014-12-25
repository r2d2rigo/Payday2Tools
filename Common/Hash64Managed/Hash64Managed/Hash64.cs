using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hash64Managed
{
    public static class Hash64
    {
        public static UInt64 HashString(string input, UInt64 level = 0)
        {
            Byte[] data = UTF8Encoding.UTF8.GetBytes(input);

            return HashByteArray(data, level);
        }

        public static unsafe UInt64 HashByteArray(Byte[] data, UInt64 level = 0)
        {
            if (data == null || data.Length == 0)
            {
                return HashData(null, 0, level);
            }

            fixed (Byte* dataPointer = &data[0])
            {
                return HashData(dataPointer, (UInt64)data.Length, level);
            }
        }

        private static unsafe UInt64 HashData(Byte* data, UInt64 dataSize, UInt64 level)
        {
            UInt64 length = dataSize;
            UInt64 a = level;
            UInt64 b = level;
            UInt64 c = 0x9e3779b97f4a7c13UL;

            while (length >= 24)
            {
                a += (data[0] + ((UInt64)data[1] << 8) + ((UInt64)data[2] << 16) + ((UInt64)data[3] << 24)
                    + ((UInt64)data[4] << 32) + ((UInt64)data[5] << 40) + ((UInt64)data[6] << 48) + ((UInt64)data[7] << 56));
                b += (data[8] + ((UInt64)data[9] << 8) + ((UInt64)data[10] << 16) + ((UInt64)data[11] << 24)
                 + ((UInt64)data[12] << 32) + ((UInt64)data[13] << 40) + ((UInt64)data[14] << 48) + ((UInt64)data[15] << 56));
                c += (data[16] + ((UInt64)data[17] << 8) + ((UInt64)data[18] << 16) + ((UInt64)data[19] << 24)
                 + ((UInt64)data[20] << 32) + ((UInt64)data[21] << 40) + ((UInt64)data[22] << 48) + ((UInt64)data[23] << 56));

                Mix64(ref a, ref b, ref c);

                data += 24; length -= 24;
            }

            c += dataSize;
            switch (length)
            {
                case 23:
                    c += ((UInt64)data[22] << 56);
                    c += ((UInt64)data[21] << 48);
                    c += ((UInt64)data[20] << 40);
                    c += ((UInt64)data[19] << 32);
                    c += ((UInt64)data[18] << 24);
                    c += ((UInt64)data[17] << 16);
                    c += ((UInt64)data[16] << 8);
                    b += ((UInt64)data[15] << 56);
                    b += ((UInt64)data[14] << 48);
                    b += ((UInt64)data[13] << 40);
                    b += ((UInt64)data[12] << 32);
                    b += ((UInt64)data[11] << 24);
                    b += ((UInt64)data[10] << 16);
                    b += ((UInt64)data[9] << 8);
                    b += ((UInt64)data[8]);
                    a += ((UInt64)data[7] << 56);
                    a += ((UInt64)data[6] << 48);
                    a += ((UInt64)data[5] << 40);
                    a += ((UInt64)data[4] << 32);
                    a += ((UInt64)data[3] << 24);
                    a += ((UInt64)data[2] << 16);
                    a += ((UInt64)data[1] << 8);
                    a += ((UInt64)data[0]);
                    break;
                case 22:
                    c += ((UInt64)data[21] << 48);
                    c += ((UInt64)data[20] << 40);
                    c += ((UInt64)data[19] << 32);
                    c += ((UInt64)data[18] << 24);
                    c += ((UInt64)data[17] << 16);
                    c += ((UInt64)data[16] << 8);
                    b += ((UInt64)data[15] << 56);
                    b += ((UInt64)data[14] << 48);
                    b += ((UInt64)data[13] << 40);
                    b += ((UInt64)data[12] << 32);
                    b += ((UInt64)data[11] << 24);
                    b += ((UInt64)data[10] << 16);
                    b += ((UInt64)data[9] << 8);
                    b += ((UInt64)data[8]);
                    a += ((UInt64)data[7] << 56);
                    a += ((UInt64)data[6] << 48);
                    a += ((UInt64)data[5] << 40);
                    a += ((UInt64)data[4] << 32);
                    a += ((UInt64)data[3] << 24);
                    a += ((UInt64)data[2] << 16);
                    a += ((UInt64)data[1] << 8);
                    a += ((UInt64)data[0]);
                    break;
                case 21:
                    c += ((UInt64)data[20] << 40);
                    c += ((UInt64)data[19] << 32);
                    c += ((UInt64)data[18] << 24);
                    c += ((UInt64)data[17] << 16);
                    c += ((UInt64)data[16] << 8);
                    b += ((UInt64)data[15] << 56);
                    b += ((UInt64)data[14] << 48);
                    b += ((UInt64)data[13] << 40);
                    b += ((UInt64)data[12] << 32);
                    b += ((UInt64)data[11] << 24);
                    b += ((UInt64)data[10] << 16);
                    b += ((UInt64)data[9] << 8);
                    b += ((UInt64)data[8]);
                    a += ((UInt64)data[7] << 56);
                    a += ((UInt64)data[6] << 48);
                    a += ((UInt64)data[5] << 40);
                    a += ((UInt64)data[4] << 32);
                    a += ((UInt64)data[3] << 24);
                    a += ((UInt64)data[2] << 16);
                    a += ((UInt64)data[1] << 8);
                    a += ((UInt64)data[0]);
                    break;
                case 20:
                    c += ((UInt64)data[19] << 32);
                    c += ((UInt64)data[18] << 24);
                    c += ((UInt64)data[17] << 16);
                    c += ((UInt64)data[16] << 8);
                    b += ((UInt64)data[15] << 56);
                    b += ((UInt64)data[14] << 48);
                    b += ((UInt64)data[13] << 40);
                    b += ((UInt64)data[12] << 32);
                    b += ((UInt64)data[11] << 24);
                    b += ((UInt64)data[10] << 16);
                    b += ((UInt64)data[9] << 8);
                    b += ((UInt64)data[8]);
                    a += ((UInt64)data[7] << 56);
                    a += ((UInt64)data[6] << 48);
                    a += ((UInt64)data[5] << 40);
                    a += ((UInt64)data[4] << 32);
                    a += ((UInt64)data[3] << 24);
                    a += ((UInt64)data[2] << 16);
                    a += ((UInt64)data[1] << 8);
                    a += ((UInt64)data[0]);
                    break;

                case 19:
                    c += ((UInt64)data[18] << 24);
                    c += ((UInt64)data[17] << 16);
                    c += ((UInt64)data[16] << 8);
                    b += ((UInt64)data[15] << 56);
                    b += ((UInt64)data[14] << 48);
                    b += ((UInt64)data[13] << 40);
                    b += ((UInt64)data[12] << 32);
                    b += ((UInt64)data[11] << 24);
                    b += ((UInt64)data[10] << 16);
                    b += ((UInt64)data[9] << 8);
                    b += ((UInt64)data[8]);
                    a += ((UInt64)data[7] << 56);
                    a += ((UInt64)data[6] << 48);
                    a += ((UInt64)data[5] << 40);
                    a += ((UInt64)data[4] << 32);
                    a += ((UInt64)data[3] << 24);
                    a += ((UInt64)data[2] << 16);
                    a += ((UInt64)data[1] << 8);
                    a += ((UInt64)data[0]);
                    break;

                case 18:
                    c += ((UInt64)data[17] << 16);
                    c += ((UInt64)data[16] << 8);
                    b += ((UInt64)data[15] << 56);
                    b += ((UInt64)data[14] << 48);
                    b += ((UInt64)data[13] << 40);
                    b += ((UInt64)data[12] << 32);
                    b += ((UInt64)data[11] << 24);
                    b += ((UInt64)data[10] << 16);
                    b += ((UInt64)data[9] << 8);
                    b += ((UInt64)data[8]);
                    a += ((UInt64)data[7] << 56);
                    a += ((UInt64)data[6] << 48);
                    a += ((UInt64)data[5] << 40);
                    a += ((UInt64)data[4] << 32);
                    a += ((UInt64)data[3] << 24);
                    a += ((UInt64)data[2] << 16);
                    a += ((UInt64)data[1] << 8);
                    a += ((UInt64)data[0]);
                    break;

                case 17:
                    c += ((UInt64)data[16] << 8);
                    b += ((UInt64)data[15] << 56);
                    b += ((UInt64)data[14] << 48);
                    b += ((UInt64)data[13] << 40);
                    b += ((UInt64)data[12] << 32);
                    b += ((UInt64)data[11] << 24);
                    b += ((UInt64)data[10] << 16);
                    b += ((UInt64)data[9] << 8);
                    b += ((UInt64)data[8]);
                    a += ((UInt64)data[7] << 56);
                    a += ((UInt64)data[6] << 48);
                    a += ((UInt64)data[5] << 40);
                    a += ((UInt64)data[4] << 32);
                    a += ((UInt64)data[3] << 24);
                    a += ((UInt64)data[2] << 16);
                    a += ((UInt64)data[1] << 8);
                    a += ((UInt64)data[0]);
                    break;
                case 16:
                    b += ((UInt64)data[15] << 56);
                    b += ((UInt64)data[14] << 48);
                    b += ((UInt64)data[13] << 40);
                    b += ((UInt64)data[12] << 32);
                    b += ((UInt64)data[11] << 24);
                    b += ((UInt64)data[10] << 16);
                    b += ((UInt64)data[9] << 8);
                    b += ((UInt64)data[8]);
                    a += ((UInt64)data[7] << 56);
                    a += ((UInt64)data[6] << 48);
                    a += ((UInt64)data[5] << 40);
                    a += ((UInt64)data[4] << 32);
                    a += ((UInt64)data[3] << 24);
                    a += ((UInt64)data[2] << 16);
                    a += ((UInt64)data[1] << 8);
                    a += ((UInt64)data[0]);
                    break;

                case 15:
                    b += ((UInt64)data[14] << 48);
                    b += ((UInt64)data[13] << 40);
                    b += ((UInt64)data[12] << 32);
                    b += ((UInt64)data[11] << 24);
                    b += ((UInt64)data[10] << 16);
                    b += ((UInt64)data[9] << 8);
                    b += ((UInt64)data[8]);
                    a += ((UInt64)data[7] << 56);
                    a += ((UInt64)data[6] << 48);
                    a += ((UInt64)data[5] << 40);
                    a += ((UInt64)data[4] << 32);
                    a += ((UInt64)data[3] << 24);
                    a += ((UInt64)data[2] << 16);
                    a += ((UInt64)data[1] << 8);
                    a += ((UInt64)data[0]);
                    break;

                case 14:
                    b += ((UInt64)data[13] << 40);
                    b += ((UInt64)data[12] << 32);
                    b += ((UInt64)data[11] << 24);
                    b += ((UInt64)data[10] << 16);
                    b += ((UInt64)data[9] << 8);
                    b += ((UInt64)data[8]);
                    a += ((UInt64)data[7] << 56);
                    a += ((UInt64)data[6] << 48);
                    a += ((UInt64)data[5] << 40);
                    a += ((UInt64)data[4] << 32);
                    a += ((UInt64)data[3] << 24);
                    a += ((UInt64)data[2] << 16);
                    a += ((UInt64)data[1] << 8);
                    a += ((UInt64)data[0]);
                    break;

                case 13:
                    b += ((UInt64)data[12] << 32);
                    b += ((UInt64)data[11] << 24);
                    b += ((UInt64)data[10] << 16);
                    b += ((UInt64)data[9] << 8);
                    b += ((UInt64)data[8]);
                    a += ((UInt64)data[7] << 56);
                    a += ((UInt64)data[6] << 48);
                    a += ((UInt64)data[5] << 40);
                    a += ((UInt64)data[4] << 32);
                    a += ((UInt64)data[3] << 24);
                    a += ((UInt64)data[2] << 16);
                    a += ((UInt64)data[1] << 8);
                    a += ((UInt64)data[0]);
                    break;

                case 12:
                    b += ((UInt64)data[11] << 24);
                    b += ((UInt64)data[10] << 16);
                    b += ((UInt64)data[9] << 8);
                    b += ((UInt64)data[8]);
                    a += ((UInt64)data[7] << 56);
                    a += ((UInt64)data[6] << 48);
                    a += ((UInt64)data[5] << 40);
                    a += ((UInt64)data[4] << 32);
                    a += ((UInt64)data[3] << 24);
                    a += ((UInt64)data[2] << 16);
                    a += ((UInt64)data[1] << 8);
                    a += ((UInt64)data[0]);
                    break;

                case 11:
                    b += ((UInt64)data[10] << 16);
                    b += ((UInt64)data[9] << 8);
                    b += ((UInt64)data[8]);
                    a += ((UInt64)data[7] << 56);
                    a += ((UInt64)data[6] << 48);
                    a += ((UInt64)data[5] << 40);
                    a += ((UInt64)data[4] << 32);
                    a += ((UInt64)data[3] << 24);
                    a += ((UInt64)data[2] << 16);
                    a += ((UInt64)data[1] << 8);
                    a += ((UInt64)data[0]);
                    break;

                case 10:
                    b += ((UInt64)data[9] << 8);
                    b += ((UInt64)data[8]);
                    a += ((UInt64)data[7] << 56);
                    a += ((UInt64)data[6] << 48);
                    a += ((UInt64)data[5] << 40);
                    a += ((UInt64)data[4] << 32);
                    a += ((UInt64)data[3] << 24);
                    a += ((UInt64)data[2] << 16);
                    a += ((UInt64)data[1] << 8);
                    a += ((UInt64)data[0]);
                    break;

                case 9:
                    b += ((UInt64)data[8]);
                    a += ((UInt64)data[7] << 56);
                    a += ((UInt64)data[6] << 48);
                    a += ((UInt64)data[5] << 40);
                    a += ((UInt64)data[4] << 32);
                    a += ((UInt64)data[3] << 24);
                    a += ((UInt64)data[2] << 16);
                    a += ((UInt64)data[1] << 8);
                    a += ((UInt64)data[0]);
                    break;

                case 8:
                    a += ((UInt64)data[7] << 56);
                    a += ((UInt64)data[6] << 48);
                    a += ((UInt64)data[5] << 40);
                    a += ((UInt64)data[4] << 32);
                    a += ((UInt64)data[3] << 24);
                    a += ((UInt64)data[2] << 16);
                    a += ((UInt64)data[1] << 8);
                    a += ((UInt64)data[0]);
                    break;

                case 7:
                    a += ((UInt64)data[6] << 48);
                    a += ((UInt64)data[5] << 40);
                    a += ((UInt64)data[4] << 32);
                    a += ((UInt64)data[3] << 24);
                    a += ((UInt64)data[2] << 16);
                    a += ((UInt64)data[1] << 8);
                    a += ((UInt64)data[0]);
                    break;

                case 6:
                    a += ((UInt64)data[5] << 40);
                    a += ((UInt64)data[4] << 32);
                    a += ((UInt64)data[3] << 24);
                    a += ((UInt64)data[2] << 16);
                    a += ((UInt64)data[1] << 8);
                    a += ((UInt64)data[0]);
                    break;

                case 5:
                    a += ((UInt64)data[4] << 32);
                    a += ((UInt64)data[3] << 24);
                    a += ((UInt64)data[2] << 16);
                    a += ((UInt64)data[1] << 8);
                    a += ((UInt64)data[0]);
                    break;

                case 4:
                    a += ((UInt64)data[3] << 24);
                    a += ((UInt64)data[2] << 16);
                    a += ((UInt64)data[1] << 8);
                    a += ((UInt64)data[0]);
                    break;

                case 3:
                    a += ((UInt64)data[2] << 16);
                    a += ((UInt64)data[1] << 8);
                    a += ((UInt64)data[0]);
                    break;

                case 2:
                    a += ((UInt64)data[1] << 8);
                    a += ((UInt64)data[0]);
                    break;

                case 1: a += ((UInt64)data[0]);
                    break;
                /* case 0: nothing left to add */
            }

            Mix64(ref a, ref b, ref c);
            /*-------------------------------------------- report the result */

            return c;
        }

        private static void Mix64(ref UInt64 a, ref UInt64 b, ref UInt64 c)
        {
            a -= b; a -= c; a ^= (c >> 43);
            b -= c; b -= a; b ^= (a << 9);
            c -= a; c -= b; c ^= (b >> 8);
            a -= b; a -= c; a ^= (c >> 38);
            b -= c; b -= a; b ^= (a << 23);
            c -= a; c -= b; c ^= (b >> 5);
            a -= b; a -= c; a ^= (c >> 35);
            b -= c; b -= a; b ^= (a << 49);
            c -= a; c -= b; c ^= (b >> 11);
            a -= b; a -= c; a ^= (c >> 12);
            b -= c; b -= a; b ^= (a << 18);
            c -= a; c -= b; c ^= (b >> 22);
        }
    }
}
