using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ant.Cargo.Data
{
    [Serializable]
    public class DataContextNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataContextNotFoundException" /> class.
        /// </summary>
        public DataContextNotFoundException()
            : base("The DataContext was not found.")
        {
        }
    }
}
