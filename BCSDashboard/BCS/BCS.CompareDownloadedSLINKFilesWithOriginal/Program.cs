using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BCS.ObjectModel.Factories;

namespace BCS.CompareDownloadedSLINKFilesWithOriginal
{
    class Program
    {
       public  static void Main(string[] args)
        {
            new ServiceFactory().CompareProsDocURLwithDownloadedSLINKFiles();
        }
    }
}
