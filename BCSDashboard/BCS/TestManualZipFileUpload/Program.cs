using BCS.Core.DAL;
using BCS.ObjectModel.Factories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestManualZipFileUpload
{
    class Program
    {
        static void Main(string[] args)
        {
            new ServiceFactory().ManualZipFileUpload();            
        }
    }
}
