// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015
// ***********************************************************************
using RRD.DSA.Core.DAL;
using RRD.FSG.RP.Model.Entities;
using RRD.FSG.RP.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;


namespace RRD.FSG.RP.Model.Factories
{
    /// <summary>
    /// Base factory class for audited entity models.
    /// </summary>
    /// <typeparam name="TAuditedModel">Type of entity the factory handles.</typeparam>
    /// <typeparam name="TKey">type of primary key identifier for the entities handled by the factory.</typeparam>
    public abstract class AuditedBaseFactory<TAuditedModel, TKey>
        : BaseFactory<TAuditedModel, TKey>
        where TAuditedModel : AuditedBaseModel<TKey>, new()
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditedBaseFactory" /> class.
        /// </summary>
        /// <param name="dataAccess">Data access entity used to interface between the factory and the persisted storage.</param>
        public AuditedBaseFactory(IDataAccess dataAccess)
            : base(dataAccess) { }

        #endregion

        #region Public Methods

        #region Read Methods

        #region CreateEntity

        /// <summary>
        /// Instantiates a new entity and populates members with the data record passed in.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to create.</typeparam>
        /// <param name="entityRecord">Data record used to create the entity.</param>
        /// <returns>A new entity of type <typeparamref name="TEntity" />.</returns>
        public override TEntity CreateEntity<TEntity>(IDataRecord entityRecord)
        {
            TEntity entity = base.CreateEntity<TEntity>(entityRecord);
            if (entity != null)
            {
                IAuditedModelInternal auditedEntity = entity as IAuditedModelInternal;
                if (auditedEntity != null)
                {
                    auditedEntity.SetLastModified(entityRecord.GetDateTime(entityRecord.GetOrdinal("UtcLastModified")));
                    auditedEntity.SetModifiedBy(entityRecord.GetInt32(entityRecord.GetOrdinal("ModifiedBy")));
                }
            }

            return entity;
        }

        /// <summary>
        /// Instantiates a new entity and populates members with the data record passed in.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity to create.</typeparam>
        /// <param name="entityRow">Data row used to create the entity.</param>
        /// <returns>A new entity of type <typeparamref name="TEntity" />.</returns>
        public override TEntity CreateEntity<TEntity>(DataRow entityRow)
        {
            TEntity entity = base.CreateEntity<TEntity>(entityRow);
            if (entity != null)
            {
                IAuditedModelInternal auditedEntity = entity as IAuditedModelInternal;
                if (auditedEntity != null)
                {
                    auditedEntity.SetLastModified(entityRow.Field<DateTime?>("UtcLastModified"));
                    auditedEntity.SetModifiedBy(entityRow.Field<int?>("ModifiedBy"));
                }
            }

            return entity;
        }

        /// <summary>
        /// Gets the parameters from entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns>IEnumerable&lt;DbParameter&gt;.</returns>
        public override IEnumerable<DbParameter> GetParametersFromEntity<TEntity>(TEntity entity)
        {
            List<DbParameter> parameters = base.GetParametersFromEntity(entity) as List<DbParameter>;

            parameters.Add(DataAccess.CreateParameter("ModifiedBy", DbType.Int32, entity.ModifiedBy));

            return parameters;

        }

        /// <summary>
        /// Gets the delete parameters.
        /// </summary>
        /// <param name="deletedBy">The deleted by.</param>
        /// <returns>IEnumerable&lt;DbParameter&gt;.</returns>
        public IEnumerable<DbParameter> GetDeleteParameters(int deletedBy)
        {
            List<DbParameter> parameters = new List<DbParameter>();

            parameters.Add(DataAccess.CreateParameter("DeletedBy", DbType.Int32, deletedBy));

            return parameters;
        }

        /// <summary>
        /// Sets the modified by.
        /// </summary>
        /// <typeparam name="TEntity">The type of the t entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="modifiedBy">The modified by.</param>
        public void SetModifiedBy<TEntity>(TEntity entity, int modifiedBy)
        {
            IAuditedModelInternal auditedEntity = entity as IAuditedModelInternal;

            auditedEntity.SetModifiedBy(modifiedBy);
        }

        #endregion

        #endregion

        #endregion
    }
}
