using Ant.Cargo.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Ant.Cargo.Data
{
    /// <summary>
    /// The class contains methods to configure a connection.
    /// This is registered in an IoC container without having a config file. Also unit tests can be written for different connections.
    /// </summary>
    public class ConnectionConfiguration : IConnectionConfiguration
    {
        /// <summary>
        /// Stores the connection name from config file.
        /// </summary>
        private readonly string _connectionName;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionConfiguration" /> class.
        /// </summary>
        public ConnectionConfiguration(string connectionName)
        {
            _connectionName = connectionName;
        }

        /// <summary>
        /// Returns a connection string.
        /// The implementer should either return a connection string from a configuration file or it could be a mock object if is used for testing.
        /// </summary>
        public string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings[_connectionName].ConnectionString;
        }
    }
}
