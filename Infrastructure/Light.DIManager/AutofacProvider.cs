using Autofac;
using Autofac.Configuration;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Extras.DynamicProxy;


namespace Light.DIManager
{
    public class AutofacProvider : IDIProvider<ContainerBuilder, IContainer>
    {
        private ConfigurationBuilder _config;
        private ContainerBuilder _builder;
        private string[] _configFiles;

        private IContainer _container;
        public static IContainer Container;

        public AutofacProvider(params string[] configFiles)
        {
            _config = new ConfigurationBuilder();
            _builder = new ContainerBuilder();
            _configFiles = configFiles;
        }

        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="configFiles"></param>
        /// <returns></returns>
        private void _configureDI(params string[] configFiles)
        {
            //Autofac setup
            foreach (var file in configFiles)
            {
                _config.AddJsonFile(file);
                var module = new ConfigurationModule(_config.Build());
                _builder.RegisterModule(module);
            }
        }

        public virtual void PreBuild(Action<ContainerBuilder> builderDelegate)
        {
            builderDelegate?.Invoke(_builder);
        }

        public virtual void Build()
        {
            if (this._container == null)
            {
                _build(_configFiles);
                _container = _builder.Build();
                AutofacProvider.Container = _container;
            }
        }

        public virtual void AfterBuild(Action<IContainer> containerDelegate)
        {
            containerDelegate?.Invoke(_container);
        }

        private void _build(params string[] configFiles)
        {
            _configureDI(configFiles);
        }

        public IContainer GetContainer()
        {
            if (_container == null)
            {
                throw new NullReferenceException("IoC container doesn't exit in context, execute Build() before GetContainer()");
            }
            return _container;
        }
        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this._container.Dispose();
                }
                disposedValue = true;
            }
        }
        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}
