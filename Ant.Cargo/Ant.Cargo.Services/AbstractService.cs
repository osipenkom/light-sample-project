using Ant.Cargo.Data.Contracts;
using Ant.Cargo.Services.Mappers;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ant.Cargo.Services
{
    public class AbstractService
    {
        public AbstractService(IDataContextManager dataContextManager, IUnityContainer container)
        {
            DataContextManager = dataContextManager;
            MapperFactory = new MapperFactory(container);
        }

        protected IDataContextManager DataContextManager { get; private set; }

        protected MapperFactory MapperFactory { get; private set; }
    }
}
