using Ant.Cargo.Data.Contracts;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ant.Cargo.Data
{
    public class ReadyRoomsContextManager : AbstractDataContextManager<CargoContext>, ICargoContextManager
    {
        public ReadyRoomsContextManager(IUnityContainer container)
            : base(container)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }

            base.Dispose(disposing);
        }
    }
}
