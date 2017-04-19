using Autofac;
using Autofac.Configuration;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeRanger.Repositories
{
    /// <summary>
    /// Repository Manager
    /// </summary>
    public class RepositoryManager
    {
        /// <summary>
        /// Repository IoC container
        /// </summary>
        public static readonly IContainer Container;

        static RepositoryManager()
        {
            //Autofac setup
            var config = new ConfigurationBuilder();
            config.AddJsonFile($@"{AppDomain.CurrentDomain.BaseDirectory}repoconfig\autofac.repo.reader.json");
            var module = new ConfigurationModule(config.Build());
            config.AddJsonFile($@"{AppDomain.CurrentDomain.BaseDirectory}repoconfig\autofac.repo.writer.json");
            var moduleWriter = new ConfigurationModule(config.Build()); 
            var builder = new ContainerBuilder();
            builder.RegisterModule(module);
            builder.RegisterModule(moduleWriter);
            Container = builder.Build();
        }
    }
}
