using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PD2Bundle
{
    class NameEntry
    {
        public UInt64 Path { get; private set; }
        public UInt64 Extension { get; private set; }
        public UInt32 Language { get; private set; }

        public NameEntry(UInt64 path, UInt64 extension, UInt32 language)
        {
            this.Path = path;
            this.Extension = extension;
            this.Language = language;
        }

        public override string ToString()
        {
            return this.Path.ToString("x") + '.' + this.Language.ToString("x") + '.' + this.Extension.ToString("x");
        }
    }
}
