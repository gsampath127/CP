using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public enum TestResultEnum
    {
        Success,
        Failure,
        Warning
    }
    public class TestResult
    {
        public string Url { get; set; }
        public string Browser { get; set; }
        public string Page { get; set; }
        public string TestCase { get; set; }
        public TestResultEnum Type { get; set; }
        public string Description { get; set; }
    }
}
