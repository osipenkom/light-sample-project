using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ant.Cargo.Data
{
    /// <summary>
    /// Forces to load the SQL provider by holding a reference.
    /// </summary>
    public class FixEfProviderServicesProblemClass
    {
        /// <summary>
        /// Fake method to force a reference to EF SQL provider.
        /// </summary>
        public void FixEfProviderServicesProblem()
        {
            // The Entity Framework provider type 'System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer'
            // for the 'System.Data.SqlClient' ADO.NET provider could not be loaded. 
            // Make sure the provider assembly is available to the running application. 
            // See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.

            // ReSharper disable once UnusedVariable
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
    }
}
