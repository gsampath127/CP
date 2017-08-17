using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BCS.ObjectModel
{
    public class BCSClientIPFileConfigDetails
    {
        public string ClientPrefix { get; set; }
        public bool NeedIPConfirmationFile { get; set; }
        public string IPConfirmationFileDropFTPPath { get; set; }
        public string IPConfirmationFileDropFTPUserName { get; set; }
        public string IPConfirmationFileDropFTPPassword { get; set; }
        public DateTime LastFileSentDate { get; set; }
        public DateTime ProcessedDate { get; set; }
    }
}
