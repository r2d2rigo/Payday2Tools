using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace BundleLib.Tests
{
    [TestClass]
    public class BundleLibUnitTests
    {
        private static readonly string TestFilePath = "C:\\Program Files (x86)\\Steam\\SteamApps\\common\\PAYDAY 2\\assets\\0a76b707eba65bc7";

        [TestMethod]
        public void LoadFile()
        {
            using (var dataFile = File.OpenRead(TestFilePath + "_h.bundle"))
            {
                var bundleHeader = BundleLib.BundleHeader.Load(dataFile);

                Assert.IsNotNull(bundleHeader);
            }
        }

        [TestMethod]
        public void CompareOldVersion()
        {
            BundleLib.BundleHeader newBundleHeader;
            PD2Bundle.BundleHeader oldBundleHeader = new PD2Bundle.BundleHeader();

            using (var dataFile = File.OpenRead(TestFilePath + "_h.bundle"))
            {
                newBundleHeader = BundleLib.BundleHeader.Load(dataFile);
            }

            oldBundleHeader.Load(TestFilePath);

            Assert.AreEqual(newBundleHeader.Entries.Count, oldBundleHeader.Entries.Count);

            for (int i = 0; i < newBundleHeader.Entries.Count; i++)
            {
                Assert.AreEqual(newBundleHeader.Entries[i].Id, oldBundleHeader.Entries[i].id, "Id of entry " + i.ToString() + " doesn't match");
                Assert.AreEqual(newBundleHeader.Entries[i].Address, oldBundleHeader.Entries[i].address, "Address of entry " + i.ToString() + " doesn't match");
                Assert.AreEqual(newBundleHeader.Entries[i].Length, oldBundleHeader.Entries[i].length, "Length of entry " + i.ToString() + " doesn't match");
            }
        }
    }
}
