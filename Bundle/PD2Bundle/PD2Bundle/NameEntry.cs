using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PD2Bundle
{
    class NameEntry
    {
        public ulong path;
        public ulong extension;
        public uint language;
        public override string ToString()
        {
            return path.ToString("x") + '.' + language.ToString("x") + '.' + extension.ToString("x");
        }
    };
}
