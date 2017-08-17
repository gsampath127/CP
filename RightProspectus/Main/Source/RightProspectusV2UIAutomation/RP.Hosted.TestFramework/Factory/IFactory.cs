using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RP.Hosted.TestFramework
{
    public interface IFactory <TPage, TBrowser> 
        where TPage : IPage
        where TBrowser : IBrowser
    {
        bool GoTo(string url, string browser);
    }
}
