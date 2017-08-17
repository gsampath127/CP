using Microsoft.VisualStudio.TestTools.UnitTesting;
using RRD.FSG.RP.Model.Entities.HostedPages;
using RRD.FSG.RP.Model.Entities.System;
using RRD.FSG.RP.Web.UI.HostedAdmin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace RRD.FSG.RP.Model.Test.Factories
{

    public class BaseTestFactory<T>
    {
        protected void ValidateListData(List<T> expected, List<T> resultActual, List<string> excludeProperties = null)
        {
            Assert.AreEqual(expected.Count, resultActual.Count); //Checking for list data count

            PropertyInfo[] myPropertyInfo = expected[0].GetType().GetProperties(); // Checking for data
            if (excludeProperties != null && excludeProperties.Count > 0)
            {
                foreach (string strExc in excludeProperties)
                    myPropertyInfo = myPropertyInfo.Where(p => p.Name != strExc).ToArray();
            }

            for (int i = 0; i < myPropertyInfo.Length; i++)
            {
                for (int j = 0; j < expected.Count; j++)
                {
                    Assert.AreEqual(expected[j].GetType().GetProperty(myPropertyInfo[i].Name).GetValue(expected[j]), resultActual[j].GetType().GetProperty(myPropertyInfo[i].Name).GetValue(resultActual[j]));
                }
            }
        }

        protected void ValidateListData<Z>(List<Z> expected, List<Z> resultActual, List<string> excludeProperties = null)
        {
            Assert.AreEqual(expected.Count, resultActual.Count); //Checking for list data count

            PropertyInfo[] myProperty = expected[0].GetType().GetProperties(); // Checking for data

            PropertyInfo[] myPropertyInfo = myProperty;
            if (excludeProperties != null && excludeProperties.Count > 0)
            {
                foreach (string strExc in excludeProperties)
                    myPropertyInfo = myPropertyInfo.Where(p => p.Name != strExc).Distinct().ToArray();
            }

            for (int i = 0; i < myPropertyInfo.Length; i++)
            {
                for (int j = 0; j < expected.Count; j++)
                {
                    switch (myPropertyInfo[i].Name)
                    {
                        case "TaxonomyAssociationData":
                            {
                                ValidateObjectModelDataCast<TaxonomyAssociationData>(resultActual[j].GetType().GetProperty(myPropertyInfo[i].Name).GetValue(resultActual[j]), expected[j].GetType().GetProperty(myPropertyInfo[i].Name).GetValue(expected[j]));
                                break;
                            }
                        default:
                            {
                                Assert.AreEqual(expected[j].GetType().GetProperty(myPropertyInfo[i].Name).GetValue(expected[j]), resultActual[j].GetType().GetProperty(myPropertyInfo[i].Name).GetValue(resultActual[j]));
                                break;
                            }
                    }

                }
            }
        }

        protected void ValidateEmptyData<Z>(List<Z> result)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<Z> resultActual = serializer.Deserialize<List<Z>>(serializer.Serialize(result));

            Assert.AreEqual(resultActual.Count, 0);
        }

        protected void ValidateEmptyData(IEnumerable<T> result)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<T> resultActual = serializer.Deserialize<List<T>>(serializer.Serialize(result));

            Assert.AreEqual(resultActual.Count, 0);
        }

        protected void ValidateKeyData<Key>(List<T> lstExpected, List<T> resultActual)
        {
            Key objKeyActual = (Key)resultActual.ToList()[0].GetType().GetProperty("Key").GetValue(resultActual.ToList()[0]);
            Key objKeyExpected = (Key)lstExpected[0].GetType().GetProperty("Key").GetValue(lstExpected[0]);

            var properties = objKeyActual.GetType().GetProperties();
            var propertiesExpected = objKeyExpected.GetType().GetProperties();
            Assert.AreEqual((objKeyActual.GetType().GetProperties().Count()), (objKeyExpected.GetType().GetProperties().Count()));
            foreach (var p in properties)
            {
                Assert.AreEqual(p.GetValue(objKeyActual, null), (p.GetValue(objKeyExpected, null)));
            }
        }

        protected void ValidateObjects<B>(object objActual, object objExpected)
        {
            List<B> expected = (List<B>)objExpected;
            List<B> resultActual = (List<B>)objActual;

            Assert.AreEqual(resultActual.Count(), expected.Count());

            if (resultActual.Count() > 0)
            {
                PropertyInfo[] myPropertyInfo = resultActual[0].GetType().GetProperties();

                for (int i = 0; i < myPropertyInfo.Length; i++)
                {
                    for (int j = 0; j < expected.Count; j++)
                    {
                        Assert.AreEqual(expected[j].GetType().GetProperty(myPropertyInfo[i].Name).GetValue(expected[j]), resultActual[j].GetType().GetProperty(myPropertyInfo[i].Name).GetValue(resultActual[j]));
                    }
                }
            }
        }

        public void ValidateObjectModelData<T>(T ActualObjectModel, T objExpected)
        {
            List<string> lstExclude = new List<string>
            {
                "ModifiedBy",
                "LastModified",
                "Key"
            };
            var properties = ActualObjectModel.GetType().GetProperties();
            if (lstExclude != null && lstExclude.Count > 0)
            {
                foreach (string strExc in lstExclude)
                    properties = properties.Where(p => p.Name != strExc).ToArray();
            }

            foreach (var p in properties)
            {
                if (p.PropertyType.Namespace == "System.Collections.Generic")
                {
                    switch (p.Name)
                    {
                        case "ClientDbConnections":
                        {
                            ValidateObjects<ClientDbConnection>((p.GetValue(ActualObjectModel, null)),
                                (p.GetValue(objExpected, null)));
                            break;
                        }
                        case "ClientDnsList":
                        {
                            ValidateObjects<ClientDNSObjectModel>((p.GetValue(ActualObjectModel, null)),
                                (p.GetValue(objExpected, null)));
                            break;
                        }
                        case "Users":
                        {
                            ValidateObjects<int>((p.GetValue(ActualObjectModel, null)),
                                (p.GetValue(objExpected, null)));
                            break;
                        }

                        case "TemplatePages":
                        {
                            ValidateObjects<HostedTemplatePage>((p.GetValue(ActualObjectModel, null)),
                                (p.GetValue(objExpected, null)));
                            break;
                        }
                        case "HostedTemplateNavigations":
                        {
                            ValidateObjects<HostedTemplateNavigation>((p.GetValue(ActualObjectModel, null)),
                                (p.GetValue(objExpected, null)));
                            break;
                        }
                        case "ParentHeaders":
                        case "ChildHeaders":
                        case "DocumentTypeHeaders":
                        {
                            ValidateObjects<HostedDocumentTypeHeader>((p.GetValue(ActualObjectModel, null)),
                                (p.GetValue(objExpected, null)));
                            break;
                        }
                        case "ParentTaxonomyAssociationData":
                        case "ChildTaxonomyAssociationData":
                        case "TaxonomyAssociationDocumentsData":
                        {
                            ValidateObjects<TaxonomyAssociationData>((p.GetValue(ActualObjectModel, null)),
                                (p.GetValue(objExpected, null)));
                            break;
                        }
                        case "FootNotes":
                        {
                            ValidateObjects<HostedSiteFootNotes>((p.GetValue(ActualObjectModel, null)),
                                (p.GetValue(objExpected, null)));
                            break;
                        }
                        default:
                            break;
                    }

                }
                else
                    Assert.AreEqual(p.GetValue(ActualObjectModel, null), (p.GetValue(objExpected, null)));
            }
        }


        public void ValidateObjectModelDataCast<T>(object objActual, object expected)
        {
            T objExpected = (T) expected ;
            T ActualObjectModel = (T)objActual ;
            var properties = ActualObjectModel.GetType().GetProperties();

            foreach (var p in properties)
            {
                if (p.PropertyType.Namespace == "System.Collections.Generic")
                {
                    switch (p.Name)
                    {
                        case "ClientDbConnections":
                            {
                                ValidateObjects<ClientDbConnection>((p.GetValue(ActualObjectModel, null)), (p.GetValue(objExpected, null)));
                                break;
                            }
                        case "TemplatePages":
                            {
                                ValidateObjects<HostedTemplatePage>((p.GetValue(ActualObjectModel, null)), (p.GetValue(objExpected, null)));
                                break;
                            }
                        case "HostedTemplateNavigations":
                            {
                                ValidateObjects<HostedTemplateNavigation>((p.GetValue(ActualObjectModel, null)), (p.GetValue(objExpected, null)));
                                break;
                            }
                        case "HostedTemplatePageNavigations":
                            {
                                ValidateObjects<HostedTemplatePageNavigation>((p.GetValue(ActualObjectModel, null)), (p.GetValue(objExpected, null)));
                                break;
                            }
                        case "DocumentTypes":
                            {
                                ValidateObjects<HostedDocumentType>((p.GetValue(ActualObjectModel, null)), (p.GetValue(objExpected, null)));
                                break;
                            }
                        default:
                            break;
                    }

                }
                else
                    Assert.AreEqual(p.GetValue(ActualObjectModel, null), (p.GetValue(objExpected, null)));
            }
        }
    }
}
