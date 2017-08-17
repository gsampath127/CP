using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BCS.ObjectModel
{
    public class BCSGatewayDocUpdateData
    {
        public string HeaderRecordType { get; set; }
        public string HeaderDataType { get; set; }
        public string HeaderSystem { get; set; }
        public string HeaderFileName { get; set; }
        public string HeaderDateTime { get; set; }
        public int HeaderTotalRecordCount { get; set; }
        public int HeaderFLRecordCount { get; set; }
        public int HeaderEXRecordCount { get; set; }
        public int HeaderAPRecordCount { get; set; }
        public int HeaderOPRecordCount { get; set; }
        public int HeaderAPCRecordCount { get; set; }
        //public int HeaderOPCRecordCount { get; set; }
        //public string HeaderField13Reserved { get; set; }
    }
}
