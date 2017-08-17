using Microsoft.VisualStudio.TestTools.UnitTesting;
using RRD.DSA.Core.DAL;
using System;
using System.Linq;
using Moq;
using System.Data;
using System.Data.Common;
using RRD.FSG.RP.Model.Entities.Client;
using RRD.FSG.RP.Model.Factories.Client;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace RRD.FSG.RP.Model.Test.Factories.Client
{
    /// <summary>
    /// Test class for FootnoteFactory class
    /// </summary>
    [TestClass]
    public class FootnoteFactoryTests : BaseTestFactory<FootnoteObjectModel>
    {
        private Mock<IDataAccess> mockDataAccess;
        ///<summary>
        ///TestInitialize
        ///</summary>
        [TestInitialize]
        public void TestInitialize()
        {
            mockDataAccess = new Mock<IDataAccess>();
        }

        #region GetAllEntities_Returns_IEnumerable_Entity_CallsFactory
        /// <summary>
        /// GetAllEntities_Returns_IEnumerable_Entity_CallsFactory
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_IEnumerable_Entity_CallsFactory()
        {
            //Arrange

            DataTable dtFootnote = new DataTable();
            dtFootnote.Columns.Add("FootnoteId", typeof(Int32));
            dtFootnote.Columns.Add("TaxonomyAssociationId", typeof(Int32));
            dtFootnote.Columns.Add("TaxonomyAssociationGroupId", typeof(Int32));
            dtFootnote.Columns.Add("LanguageCulture", typeof(string));
            dtFootnote.Columns.Add("Text", typeof(string));
            dtFootnote.Columns.Add("Order", typeof(Int32));
            dtFootnote.Columns.Add("UtcLastModified", typeof(DateTime));
            dtFootnote.Columns.Add("ModifiedBy", typeof(Int32));

            DataRow dtrowFootnote = dtFootnote.NewRow();
            dtrowFootnote["FootnoteId"] = 1;
            dtrowFootnote["TaxonomyAssociationId"] = 6;
            dtrowFootnote["TaxonomyAssociationGroupId"] = DBNull.Value;
            dtrowFootnote["LanguageCulture"] = null;
            dtrowFootnote["Text"] = "Footnote for American Century VP Growth Fund";
            dtrowFootnote["Order"] = 1;
            dtrowFootnote["UtcLastModified"] = DateTime.Now;
            dtrowFootnote["ModifiedBy"] = 1;
            dtFootnote.Rows.Add(dtrowFootnote);

            mockDataAccess.Setup(
                 x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
                 .Returns(dtFootnote);

            //Act
            FootnoteFactory objFootnoteFactory = new FootnoteFactory(mockDataAccess.Object);
            var result = objFootnoteFactory.GetAllEntities(0, 0, null);
            List<FootnoteObjectModel> lstExpected = new List<FootnoteObjectModel>
            {
                new FootnoteObjectModel
                {
                    FootnoteId = 1,
                    TaxonomyAssociationId = 6,
                    TaxonomyAssociationGroupId = null,
                    LanguageCulture = null,
                    Text = "Footnote for American Century VP Growth Fund",
                    Order = 1
                }
            };

            List<string> lstExclude = new List<string> { "ModifiedBy", "LastModified", "Key" };
            ValidateListData(lstExpected, result.ToList(), lstExclude);

            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region GetAllEntities_Returns_IEnumerable_Entity
        /// <summary>
        /// GetAllEntities_Returns_IEnumerable_Entity
        /// </summary>
        [TestMethod]
        public void GetAllEntities_Returns_IEnumerable_Entity()
        {
            //Arrange

            DataTable dtFootnote = new DataTable();
            dtFootnote.Columns.Add("FootnoteId", typeof(Int32));
            dtFootnote.Columns.Add("TaxonomyAssociationId", typeof(Int32));
            dtFootnote.Columns.Add("TaxonomyAssociationGroupId", typeof(Int32));
            dtFootnote.Columns.Add("LanguageCulture", typeof(string));
            dtFootnote.Columns.Add("Text", typeof(string));
            dtFootnote.Columns.Add("Order", typeof(Int32));
            dtFootnote.Columns.Add("UtcLastModified", typeof(DateTime));
            dtFootnote.Columns.Add("ModifiedBy", typeof(Int32));

            DataRow dtrowFootnote = dtFootnote.NewRow();
            dtrowFootnote["FootnoteId"] = 1;
            dtrowFootnote["TaxonomyAssociationId"] = 6;
            dtrowFootnote["TaxonomyAssociationGroupId"] = DBNull.Value;
            dtrowFootnote["LanguageCulture"] = null;
            dtrowFootnote["Text"] = "Footnote for American Century VP Growth Fund";
            dtrowFootnote["Order"] = 1;
            dtrowFootnote["UtcLastModified"] = DateTime.Now;
            dtrowFootnote["ModifiedBy"] = 1;
            dtFootnote.Rows.Add(dtrowFootnote);

            mockDataAccess.Setup(
                 x => x.ExecuteDataTable(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DbParameter[]>()))
                 .Returns(dtFootnote);

            //Act
            FootnoteFactory objFootnoteFactory = new FootnoteFactory(mockDataAccess.Object);
            var result = objFootnoteFactory.GetAllEntities<FootnoteObjectModel>(0, 0);
            List<FootnoteObjectModel> lstExpected = new List<FootnoteObjectModel>
            {
                new FootnoteObjectModel
                {
                    FootnoteId = 1,
                    TaxonomyAssociationId = 6,
                    TaxonomyAssociationGroupId = null,
                    LanguageCulture = null,
                    Text = "Footnote for American Century VP Growth Fund",
                    Order = 1
                }
            };

            List<string> lstExclude = new List<string> { "ModifiedBy", "LastModified", "Key" };
            ValidateListData(lstExpected, result.ToList(), lstExclude);

            //Assert
            mockDataAccess.VerifyAll();
        }
        #endregion

        #region DeleteEntity_With_Key
        /// <summary>
        /// DeleteEntity_With_Key
        /// </summary>
        [TestMethod]
        public void DeleteEntity_With_Key()
        {
            //Arrange
            FootnoteObjectModel obj = new FootnoteObjectModel()
            {
                FootnoteId = 1
            };

            var parameters = new[]
            {
                new SqlParameter {ParameterName = "DeletedBy", Value = 1},
                new SqlParameter {ParameterName = "FootnoteId", Value = 1}
            };

            mockDataAccess.SetupSequence(x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
                .Returns(parameters[0])
                .Returns(parameters[1]);
            mockDataAccess.Setup(x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<DbParameter>>()));

            //Act
            FootnoteFactory objFootnoteFactory = new FootnoteFactory(mockDataAccess.Object);
            objFootnoteFactory.DeleteEntity(1);

            //Assert
            mockDataAccess.VerifyAll();

        }
        #endregion

        #region SaveEntity_With_ModifiedBy
        ///<summary>
        ///SaveEntity_With_ModifiedBy
        ///</summary>
        [TestMethod]
        public void SaveEntity_With_ModifiedBy()
        {
            FootnoteObjectModel objFootnoteObjectModel = new FootnoteObjectModel
            {
                FootnoteId = 1,
                TaxonomyAssociationId = 6,
                TaxonomyAssociationGroupId = null,
                LanguageCulture = null,
                Text = "Footnote for American Century VP Growth Fund",
                Order = 1
            };
            
            var parameters = new[]
            {
                new SqlParameter {ParameterName = "ModifiedBy", Value = 1},
                new SqlParameter {ParameterName = "FootnoteId", Value = 1},
                new SqlParameter {ParameterName = "TaxonomyAssociationId", Value = 6},
                new SqlParameter {ParameterName = "TaxonomyAssociationGroupId", Value = null},
                new SqlParameter {ParameterName = "LanguageCulture", Value = null},
                new SqlParameter {ParameterName = "Text", Value = "Footnote for American Century VP Growth Fund"},
                new SqlParameter {ParameterName = "Order", Value = 1}
            };

            mockDataAccess.SetupSequence(
                x => x.CreateParameter(It.IsAny<string>(), It.IsAny<DbType>(), It.IsAny<object>()))
                .Returns(parameters[0])
                .Returns(parameters[1])
                .Returns(parameters[2])
                .Returns(parameters[3])
                .Returns(parameters[4])
                .Returns(parameters[5])
                .Returns(parameters[6]);
            mockDataAccess.Setup(
                x => x.ExecuteNonQuery(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<DbParameter>>()));

            //Act
            FootnoteFactory objFootnoteFactory = new FootnoteFactory(mockDataAccess.Object);
            objFootnoteFactory.SaveEntity(objFootnoteObjectModel, 1);

            //Assert
            mockDataAccess.VerifyAll();

        }
        #endregion

        #region CreateEntities_NullValue
        /// <summary>
        /// CreateEntities_NullValue
        /// </summary>
        [TestMethod]
        public void CreateEntities_NullValue()
        {
            //Arrange
            DataRow dr = null;

            //Act
            FootnoteFactory objFootnoteFactory = new FootnoteFactory(mockDataAccess.Object);
            var result = objFootnoteFactory.CreateEntity<FootnoteObjectModel>(dr);

            //Assert
            Assert.AreEqual(result, null);
        }
        #endregion

    }
}
