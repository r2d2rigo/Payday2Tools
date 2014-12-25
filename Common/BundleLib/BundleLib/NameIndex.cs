using System;
using System.Collections.Generic;
using System.IO;

namespace BundleLib
{
    public class NameIndex
    {
        public Dictionary<UInt32, NameEntry> Entries { get; private set; }

        public NameIndex()
        {
            this.Entries = new Dictionary<UInt32, NameEntry>();
        }

        public static NameIndex Load(Stream nameIndexStream)
        {
            NameIndex loadedIndex = new NameIndex();

            using (BinaryReader indexReader = new BinaryReader(nameIndexStream))
            {
                for (int i = 0; i < 8; ++i)
                {
                    indexReader.ReadBytes(4);
                }

                UInt32 entryCount = indexReader.ReadUInt32();
                UInt32 startOffset = indexReader.ReadUInt32();
                nameIndexStream.Position = (long)startOffset;

                try
                {
                    for (int i = 0; i < entryCount; ++i)
                    {
                        UInt64 extension = indexReader.ReadUInt64();
                        UInt64 fileName = indexReader.ReadUInt64();
                        UInt32 language = indexReader.ReadUInt32();

                        indexReader.ReadBytes(4);

                        UInt32 id = indexReader.ReadUInt32();

                        indexReader.ReadBytes(4);

                        loadedIndex.Entries[id] = new NameEntry(fileName, extension, language);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception loading name indices: " + ex.Message);

                    return null;
                }
            }

            return loadedIndex;
        }
    }
}
