using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BCS.ObjectModel
{
    public class BCSTRPFLTFileInfo
    {
        public string FileName { get; set; }
        public string DateReceived { get; set; }
        public string DirectoryName { get; set; }
    }

    public class BCSFullfillmentInfo
    {
        public string TransId { get; set; }
        public string Cusip { get; set; }
        public string Message { get; set; }
    }
}
