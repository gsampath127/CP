using Microsoft.VisualStudio.TestTools.UnitTesting;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace RRD.FSG.RP.Web.UI.HostedAdmin.UnitTests.Controller
{
    public class BaseTestController<T>
    {
        protected void ValidateDisplayValuePair(List<DisplayValuePair> expected, JsonResult actual)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<DisplayValuePair> result = serializer.Deserialize<List<DisplayValuePair>>(serializer.Serialize(actual.Data));
            Assert.AreEqual(expected.Count, result.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(result[i].Display, expected[i].Display);
                Assert.AreEqual(result[i].Value, expected[i].Value);
            }
        }


        protected void ValidateDisplayValuePair(object expectedOBJ, object resultOBJ)
        {

            if (expectedOBJ != null && resultOBJ != null) // to ignore the case if any list is null.
            {
                List<DisplayValuePair> expected = (List<DisplayValuePair>)expectedOBJ;
                List<DisplayValuePair> result = (List<DisplayValuePair>)resultOBJ;

                Assert.AreEqual(expected.Count, result.Count);
                for (int i = 0; i < expected.Count; i++)
                {
                    Assert.AreEqual(result[i].Display, expected[i].Display);
                    Assert.AreEqual(result[i].Value, expected[i].Value);
                }
            }
        }

        // 
        protected void ValidateData(List<T> expected, JsonResult result)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            BaseTestObject<T> resultDataTotal = serializer.Deserialize<BaseTestObject<T>>(serializer.Serialize(result.Data));//  Deserialising to Total and Data
            List<T> resultActual = serializer.Deserialize<List<T>>(serializer.Serialize(resultDataTotal.data)); // Deserialising Data  to data(List)

            Assert.AreEqual(expected.Count, resultDataTotal.Total); // Checking for Total
            Assert.AreEqual(expected.Count, resultActual.Count); //Checking for list data count

            PropertyInfo[] myPropertyInfo = expected[0].GetType().GetProperties(); // Checking for data
            for (int i = 0; i < myPropertyInfo.Length; i++)
            {
                for (int j = 0; j < expected.Count; j++)
                {
                    Assert.AreEqual(expected[j].GetType().GetProperty(myPropertyInfo[i].Name).GetValue(expected[j]), resultActual[j].GetType().GetProperty(myPropertyInfo[i].Name).GetValue(resultActual[j]));
                }
            }
        }

        // Used for other than expected
        protected void ValidateData<Z>(List<Z> expected, JsonResult result)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<Z> resultActual = serializer.Deserialize<List<Z>>(serializer.Serialize(result.Data)); // Deserialising Data  to data(List)

            Assert.AreEqual(expected.Count, resultActual.Count); //Checking for list data count

            PropertyInfo[] myPropertyInfo = expected[0].GetType().GetProperties(); // Checking for data
            for (int i = 0; i < myPropertyInfo.Length; i++)
            {
                for (int j = 0; j < expected.Count; j++)
                {
                    Assert.AreEqual(expected[j].GetType().GetProperty(myPropertyInfo[i].Name).GetValue(expected[j]), resultActual[j].GetType().GetProperty(myPropertyInfo[i].Name).GetValue(resultActual[j]));
                }
            }
        }

        public void ValidateViewModelData<T>(T ActualViewModel, T objExpected)
        {
            var properties = ActualViewModel.GetType().GetProperties();

            foreach (var p in properties)
            {
                Type type = p.GetType();
                if (p.PropertyType.Namespace == "System.Collections.Generic")
                    ValidateDisplayValuePair(p.GetValue(ActualViewModel, null), p.GetValue(objExpected, null));
                else
                    Assert.AreEqual(p.GetValue(ActualViewModel, null), (p.GetValue(objExpected, null)));
            }
        }

        protected void ValidateEmptyData<T>(JsonResult result)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<T> resultActual = serializer.Deserialize<List<T>>(serializer.Serialize(result.Data));

            Assert.AreEqual(resultActual.Count, 0);
        }
    }

    public class BaseTestObject<T>
    {
        public int Total { get; set; }
        public List<T> data { get; set; }
    }
}
