using Light.Application.Contracts;
using Light.Command.Contracts;
using Light.DIManager;
using Autofac;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic;
using Light.Command.PersonCommand;
using Light.DataContracts.Repositories;
using static NUnit.Framework.Assert;
using Light.Command.CommandValidaters;
using Light.ErrorHandler;
using Light.ErrorHandler.Contracts;
using Microsoft.Extensions.Logging;
using Light.Logger;
using Light.Domain.Bus.EventHandler;
using Light.Event;
using Autofac.Extras.DynamicProxy;
using Light.Application.QueryServices;
using Light.Application.CommandServices;

namespace Light.Application.UnitTest
{
    [TestFixture]
    public class PersonQueryServiceTest
    {
        private IDIProvider<ContainerBuilder, IContainer> iocProvider;
        private IPersonQueryServiceContract handler;

        [OneTimeSetUp]
        public void SetUp()
        {
            iocProvider = new AutofacProvider($@"{ AppDomain.CurrentDomain.BaseDirectory}repoconfig\autofac.repo.reader.json",
                            $@"{AppDomain.CurrentDomain.BaseDirectory}repoconfig\autofac.repo.writer.json",
                            $@"{AppDomain.CurrentDomain.BaseDirectory}moduleconfig\autofac.modules.json");
            //add the configues out of configue files in PreBuild
            iocProvider.PreBuild((builder) => {

                //Interceptor only can be configured by code
                //PersonCommandHandler dependents object that registered in config file
                builder.RegisterType<PersonCommandHandler>()
                    .As<IPersonCommandHandler>()
                    .EnableInterfaceInterceptors();
                builder.Register(c => new CommandPropertyValidator());


                //Application services
                builder.RegisterType<PersonQueryService>()
                    .As<IPersonQueryServiceContract>()
                    .EnableInterfaceInterceptors();
                builder.RegisterType<PersonCommandService>()
                    .As<IPersonCommandServiceContract>()
                    .EnableInterfaceInterceptors();


                //Register ErrorHandler
                builder.RegisterType<UnKnownErrorHandler>()
                    .As<IUnKnownErrorHandler>();
                builder.RegisterType<NegativeErrorHandler>()
                    .As<INegativeErrorHandler>();

                //Register LoggerController
                builder.RegisterType<LoggerFactory>()
                    .As<ILoggerFactory>();
                builder.RegisterType<LoggerController<VersionedEvent>>()
                    .As<ILoggerController<VersionedEvent>>();
                builder.RegisterType<LoggerController<ExceptionEvent>>()
                    .As<ILoggerController<ExceptionEvent>>();
                builder.RegisterType<LoggerController<UnKnownErrorEvent>>()
                    .As<ILoggerController<UnKnownErrorEvent>>();
            });

            iocProvider.Build();
            handler = iocProvider.GetContainer().Resolve<IPersonQueryServiceContract>();
        }

        [SetUp]
        public void Init() {
            iocProvider.GetContainer().Resolve<IPersonReaderRepositoryContract>().Query();
        }

        [Test]
        [TestCase(50)]
        public async Task Application_Person_Query_All(int range)
        {
            for (int i = 0; i < range; i++)
            {
                var person = new CreateNewPersonCommand() { FirstName = "Adam", LastName = "Liu", Age = new Random().Next(10) };
                iocProvider.GetContainer().Resolve<IPersonCommandHandler>().Handle(person);
            }
            var result = await handler.Query();
            AreEqual(result.Count(), range);
        }

        [Test]
        [TestCase(2)]
        public async Task Application_Person_Query_Toddler(int range)
        {
            for (int i = 0; i < 50; i++)
            {
                var person = new CreateNewPersonCommand() { FirstName = "Adam", LastName = "Liu", Age = new Random().Next(range) };
                iocProvider.GetContainer().Resolve<IPersonCommandHandler>().Handle(person);
            }
            var result = await handler.Query("Age < 2");
            IsTrue(result.Count() > 0);
            AreEqual(result.FirstOrDefault().Group.Description, "Toddler");
        }

        [Test]
        [TestCase(10000)]
        public async Task Application_Person_Query_KauriTree(int range)
        {
            for (int i = 0; i < 50; i++)
            {
                var person = new CreateNewPersonCommand() { FirstName = "Adam", LastName = "Liu", Age = new Random().Next(range) };
                iocProvider.GetContainer().Resolve<IPersonCommandHandler>().Handle(person);
            }
            var result = await handler.Query("Age >= 4999");
            IsTrue(result.Count() > 0);
            AreEqual(result.FirstOrDefault().Group.Description, "Kauri tree");
        }

        [Test]
        [TestCase(1)]
        public async Task Application_Person_Query_GetOne(int id)
        {
            for (int i = 0; i < 1; i++)
            {
                var person = new CreateNewPersonCommand() { FirstName = "Adam", LastName = "Liu", Age = new Random().Next(3) };
                iocProvider.GetContainer().Resolve<IPersonCommandHandler>().Handle(person);
            }
            var result = await handler.GetById(id);
            AreEqual(result.Id, 1);
        }


        [TearDown]
        public void Dispose()
        {
            iocProvider.GetContainer().Resolve<IPersonWriterRepositoryContract>().Delete(null);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            iocProvider.Dispose();
            iocProvider = null;
        }
    }
}
