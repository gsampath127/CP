using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BCS.ObjectModel
{
    public class BCSDocUpdateApprovalCUSIPData
    {
        public List<BCSDocUpdateApprovalCUSIPDetails> DuplicateCUSIPDetails { get; set; }
        public List<BCSDocUpdateApprovalCUSIPDetails> AllCUSIPDetails { get; set; }
        public int DuplicateCUSIPDetailsTotalCount { get; set; }
        public int AllCUSIPDetailsTotalCount { get; set; }
    }
}
