using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;

namespace Hash64Managed.Test
{
    [TestClass]
    public class Hash64UnitTests
    {
        [DllImport("hash64.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern ulong Hash(byte[] k, ulong length, ulong level);

        public static ulong HashStringNative(string input, ulong level = 0)
        {
            return Hash(UTF8Encoding.UTF8.GetBytes(input), (ulong)UTF8Encoding.UTF8.GetByteCount(input), level);
        }

        public static unsafe ulong HashStringManaged(string input, ulong level = 0)
        {
            fixed (byte* data = UTF8Encoding.UTF8.GetBytes(input))
            {
                return Hash64.Hash(data , (ulong)UTF8Encoding.UTF8.GetByteCount(input), level);
            }
        }

        [TestMethod]
        public void EmptyString()
        {
            string testString = string.Empty;
            ulong nativeResult = HashStringNative(testString);
            ulong managedResult = HashStringManaged(testString);

            Assert.AreEqual(nativeResult, managedResult);
        }

        [TestMethod]
        public void HelloWorld()
        {
            string testString = "Hello, World!";
            ulong nativeResult = HashStringNative(testString);
            ulong managedResult = HashStringManaged(testString);

            Assert.AreEqual(nativeResult, managedResult);
        }

        [TestMethod]
        public void RandomStrings()
        {
            using (var fileStream = File.OpenRead("randomstrings.txt"))
            {
                using (var streamReader = new StreamReader(fileStream))
                {
                    while (!streamReader.EndOfStream)
                    {
                        string testString = streamReader.ReadLine();
                        ulong nativeResult = HashStringNative(testString);
                        ulong managedResult = HashStringManaged(testString);

                        Assert.AreEqual(nativeResult, managedResult);
                    }
                }
            }
        }
    }
}
