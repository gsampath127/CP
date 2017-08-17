using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BCS.ObjectModel
{
    public class Slink
    {
        public string SlinkPDFName { get; set; }

        public string SlinkNameWithoutPdf { get; set; }

        public string SlinkNameWithFilePath { get; set; }

        public bool SlinkExists { get; set; }

        public string PDFURL { get; set; }

        public bool RecentlyCreated { get; set; }
    }
}
