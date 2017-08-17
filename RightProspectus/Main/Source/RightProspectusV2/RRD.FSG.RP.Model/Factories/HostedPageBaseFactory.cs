// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015
// ***********************************************************************
using RRD.DSA.Core.DAL;

namespace RRD.FSG.RP.Model.Factories
{
    /// <summary>
    /// Class HostedPageBaseFactory.
    /// </summary>
    public abstract class HostedPageBaseFactory
    {
        /// <summary>
        /// Data access used to interface between factory and persisted storage of entity data.
        /// </summary>
        /// <value>The data access.</value>
        internal IDataAccess DataAccess { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="BaseFactory" /> class.
        /// </summary>
        /// <param name="dataAccess">Data access entity used to interface between the factory and the persisted storage.</param>
        public HostedPageBaseFactory(IDataAccess dataAccess)
        {
            DataAccess = dataAccess;            
        }

    }
}
