using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Factories.Client;
using RRD.FSG.RP.Model.Factories.System;
using RRD.FSG.RP.Model.SearchEntities.System;
using RRD.FSG.RP.Model.SortDetail.System;
using RRD.FSG.RP.Model.Factories;
using System.Web.Script.Serialization;
using Moq;
using System.Data.Common;

namespace RRD.FSG.RP.Model.Test.Factories.Client
{
    [TestClass]
    public class ApproveProofingFactoryTest : BaseTestFactory<ApproveProofingObjectModel>
    {
        Mock<IDataAccess> mockDataAccess;
        

        /// <summary>
        /// TestInitialze
        /// </summary>
        [TestInitialize]
        public void TestInitialze()
        {
            mockDataAccess = new Mock<IDataAccess>();
        }

        //#region ClientData
        ///// <summary>
        ///// ClientData
        ///// </summary>
        //public void ClientData()
        //{
        //    DataSet dSet = new DataSet();
        //    //Table 0
        //    DataTable dt1 = new DataTable();
        //    dt1.Columns.Add("ClientID", typeof(Int32));
        //    dt1.Columns.Add("ClientName", typeof(string));
        //    dt1.Columns.Add("ClientDNS", typeof(string));
        //    dt1.Columns.Add("ClientConnectionStringName", typeof(string));
        //    dt1.Columns.Add("ClientDatabaseName", typeof(string));
        //    dt1.Columns.Add("VerticalMarketConnectionStringName", typeof(string));
        //    dt1.Columns.Add("VerticalMarketsDatabaseName", typeof(string));

        //    DataRow dtrow1 = dt1.NewRow();
        //    dtrow1["ClientID"] = 2;
        //    dtrow1["ClientName"] = "Forethought";
        //    dtrow1["ClientDNS"] = null;
        //    dtrow1["ClientConnectionStringName"] = "ClientDBInstance1";
        //    dtrow1["ClientDatabaseName"] = "RPV2ClientDb1";
        //    dtrow1["VerticalMarketConnectionStringName"] = "USVerticalMarketDBInstance";
        //    dtrow1["VerticalMarketsDatabaseName"] = "RPV2USDB";
        //    dt1.Rows.Add(dtrow1);
        //    dSet.Tables.Add(dt1);

        //    //Table 1
        //    DataTable dt2 = new DataTable();
        //    dt2.Columns.Add("TemplateId", typeof(Int32));
        //    dt2.Columns.Add("TemplateName", typeof(string));
        //    dt2.Columns.Add("PageID", typeof(Int32));
        //    dt2.Columns.Add("PageName", typeof(string));

        //    DataRow dtrow2 = dt2.NewRow();
        //    dtrow2["TemplateId"] = 1;
        //    dtrow2["TemplateName"] = "Default";
        //    dtrow2["PageID"] = 1;
        //    dtrow2["PageName"] = "TAL";

        //    dt2.Rows.Add(dtrow2);
        //    dSet.Tables.Add(dt2);

        //    //Table 2
        //    DataTable dt3 = new DataTable();
        //    dt3.Columns.Add("TemplateId", typeof(Int32));
        //    dt3.Columns.Add("DefaultNavigationXml", typeof(string));
        //    dt3.Columns.Add("NavigationKey", typeof(string));
        //    dt3.Columns.Add("XslTransform", typeof(string));

        //    dSet.Tables.Add(dt3);

        //    //Table 3
        //    DataTable dt4 = new DataTable();
        //    dt4.Columns.Add("TemplateId", typeof(Int32));
        //    dt4.Columns.Add("PageId", typeof(Int32));
        //    dt4.Columns.Add("NavigationKey", typeof(string));
        //    dt4.Columns.Add("XslTransform", typeof(XmlReadMode));
        //    dt4.Columns.Add("DefaultNavigationXml", typeof(XmlReadMode));

        //    DataRow dtrow4 = dt4.NewRow();
        //    dtrow4["TemplateId"] = 1;
        //    dtrow4["PageId"] = 3;
        //    dtrow4["NavigationKey"] = "TaxonomySpecificDocumentFrame_DocumentType";
        //    dtrow4["XslTransform"] = DBNull.Value;
        //    dtrow4["DefaultNavigationXml"] = DBNull.Value;
        //    dt4.Rows.Add(dtrow4);
        //    dSet.Tables.Add(dt4);

        //    Mock.Arrange(() => mockDataAccess.ExecuteDataSet(string.Empty, string.Empty, null))
        //     .IgnoreArguments()
        //     .Returns(dSet)
        //     .MustBeCalled();
        //}
        //#endregion

        //#region GetEntityByKey_With_OneParameter
        ///// <summary>
        ///// GetEntityByKey_With_OneParameter
        ///// </summary>
        //[TestMethod]
        //public void GetEntityByKey_With_OneParameter()
        //{
        //    //Arrange           

        //    ClientData();
        //    Exception exe = null; ;

        //    //Act
        //    ApproveProofingFactory objAproveProofingFactory = new ApproveProofingFactory(mockDataAccess);
        //    try
        //    {
        //        objAproveProofingFactory.GetEntityByKey(1);
        //    }
        //    catch (Exception e)
        //    {
        //        if (e is NotImplementedException)
        //            exe = e;
        //    }

        //    //Assert
        //    Assert.IsInstanceOfType(exe, typeof(NotImplementedException));
        //}
        //#endregion

        //#region GetAllEntities_Returns_IEnumerable
        ///// <summary>
        ///// GetAllEntities_Returns_IEnumerable
        ///// </summary>
        //[TestMethod]
        //public void GetAllEntities_Returns_IEnumerable()
        //{
        //    //Arrange           

        //    ClientData();
        //    Exception exe = null; ;

        //    //Act
        //    ApproveProofingFactory objAproveProofingFactory = new ApproveProofingFactory(mockDataAccess);
        //    try
        //    {
        //        objAproveProofingFactory.GetAllEntities(1, 2, null);
        //    }
        //    catch (Exception e)
        //    {
        //        if (e is NotImplementedException)
        //            exe = e;
        //    }

        //    //Assert
        //    Assert.IsInstanceOfType(exe, typeof(NotImplementedException));
        //}
        //#endregion

        //#region GetEntitiesBySearch_Returns_IEnumerable
        ///// <summary>
        ///// GetEntitiesBySearch_Returns_IEnumerable
        ///// </summary>
        //[TestMethod]
        //public void GetEntitiesBySearch_Returns_IEnumerable()
        //{
        //    //Arrange           
        //    ClientData();
        //    Exception exe = null; ;

        //    //Act
        //    ApproveProofingFactory objAproveProofingFactory = new ApproveProofingFactory(mockDataAccess);
        //    try
        //    {
        //        objAproveProofingFactory.GetEntitiesBySearch(1, 2, null);
        //    }
        //    catch (Exception e)
        //    {
        //        if (e is NotImplementedException)
        //            exe = e;
        //    }

        //    //Assert
        //    Assert.IsInstanceOfType(exe, typeof(NotImplementedException));
        //}
        //#endregion

        #region SaveEntity_With_ModifiedBy
        /// <summary>
        /// SaveEntity_WithModifiedBy
        /// </summary>
        [TestMethod]
        public void SaveEntity_With_ModifiedBy()
        {
            //Arrange
            ApproveProofingObjectModel objAproveproofingObjectModel = new ApproveProofingObjectModel();

            var parameters = new[]
            { 
                new SqlParameter(){ ParameterName="ModifiedBy", Value=1 },
            };

            mockDataAccess.Setup(x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
                .Returns(parameters[0]);

            mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<DbParameter>>()));

            //Act

            ApproveProofingFactory ObjAproveProofing = new ApproveProofingFactory(mockDataAccess.Object);
            ObjAproveProofing.SaveEntity(objAproveproofingObjectModel,1);
           //Assert and Verify
            mockDataAccess.VerifyAll();
        }
        #endregion

        //#region DeleteEntity_With_Key
        ///// <summary>
        ///// DeleteEntity_With_Key
        ///// </summary>
        //[TestMethod]
        //public void DeleteEntity_With_Key()
        //{
        //    //Arrange
            
        //    Exception exe = null; ;

        //    //Act
        //    ApproveProofingFactory objApproveProofingFactoryFactory = new ApproveProofingFactory(mockDataAccess);
        //    try
        //    {
        //        objApproveProofingFactoryFactory.DeleteEntity(3);
        //    }
        //    catch (Exception e)
        //    {
        //        if (e is NotImplementedException)
        //            exe = e;
        //    }

        //    //Assert
        //    Assert.IsInstanceOfType(exe, typeof(NotImplementedException));
        //}
        //#endregion

        //#region DeleteEntity_With_modifiedBy
        ///// <summary>
        ///// DeleteEntity_With_modifiedBy
        ///// </summary>
        //[TestMethod]
        //public void DeleteEntity_With_modifiedBy()
        //{
        //    //Arrange

        //    Exception exe = null; ;

        //    //Act
        //    ApproveProofingFactory objApproveProofingFactoryFactory = new ApproveProofingFactory(mockDataAccess);
        //    try
        //    {
        //        objApproveProofingFactoryFactory.DeleteEntity(2,32);
        //    }
        //    catch (Exception e)
        //    {
        //        if (e is NotImplementedException)
        //            exe = e;
        //    }

        //    //Assert
        //    Assert.IsInstanceOfType(exe, typeof(NotImplementedException));
        //}
        //#endregion

        //#region DeleteEntity_Returns_ApproveProofingObjectModel
        ///// <summary>
        ///// DeleteEntity_Returns_ApproveProofingObjectModel
        ///// </summary>
        //[TestMethod]
        //public void DeleteEntity_Returns_ApproveProofingObjectModel()
        //{
        //    //Arrange
        //    ApproveProofingObjectModel ObjApproveProofingObjectModel = new ApproveProofingObjectModel();
           
        //    Exception exe = null; ;

        //    //Act
        //    ApproveProofingFactory objApproveProofingFactoryFactory = new ApproveProofingFactory(mockDataAccess);
        //    try
        //    {
        //        objApproveProofingFactoryFactory.DeleteEntity(ObjApproveProofingObjectModel);
        //    }
        //    catch (Exception e)
        //    {
        //        if (e is NotImplementedException)
        //            exe = e;
        //    }

        //    //Assert
        //    Assert.IsInstanceOfType(exe, typeof(NotImplementedException));
        //}
        //#endregion

        //#region DeleteEntity_With_deletedBy
        ///// <summary>
        ///// DeleteEntity_With_deletedBy
        ///// </summary>
        //[TestMethod]
        //public void DeleteEntity_With_deletedBy()
        //{
        //    //Arrange
        //    ApproveProofingObjectModel ObjApproveProofingObjectModel = new ApproveProofingObjectModel();

        //    Exception exe = null; ;

        //    //Act
        //    ApproveProofingFactory objApproveProofingFactoryFactory = new ApproveProofingFactory(mockDataAccess);
        //    try
        //    {
        //        objApproveProofingFactoryFactory.DeleteEntity(ObjApproveProofingObjectModel,32);
        //    }
        //    catch (Exception e)
        //    {
        //        if (e is NotImplementedException)
        //            exe = e;
        //    }

        //    //Assert
        //    Assert.IsInstanceOfType(exe, typeof(NotImplementedException));
        //}
        //#endregion
    }
}
