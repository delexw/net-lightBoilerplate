using Light.Command.CommandValidaters;
using Light.Command.Contracts;
using Light.Command.PersonCommand;
using Light.DataContracts.Repositories;
using Light.DIManager;
using Light.Domain.Bus.EventHandler;
using Light.ErrorHandler;
using Light.ErrorHandler.Contracts;
using Light.Event;
using Light.Event.PersonEvent;
using Light.Logger;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NUnit.Framework.Assert;

namespace Light.Command.UnitTest
{
    [TestFixture]
    public class ModifyPersonCommandTest
    {
        private IDIProvider<ContainerBuilder, IContainer> iocProvider;
        private IPersonCommandHandler handler;

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
            handler = iocProvider.GetContainer().Resolve<IPersonCommandHandler>();
        }

        [SetUp]
        public void Init()
        {
            iocProvider.GetContainer().Resolve<IPersonReaderRepositoryContract>().Query();
        }

        [Test]
        public void Command_Update_Person_Invalid_Age()
        {
            var person = new ModifyExistingPersonCommand() { Id = 1, FirstName = "1", LastName = "2", Age = -1 };
            var result = new List<ValidationResult>();
            Validator.TryValidateObject(person, new ValidationContext(person), result);
            IsTrue(result.First().MemberNames.First() == nameof(person.Age));
        }

        [Test]
        public void Command_Update_Person_Invalid_FirstName()
        {
            var person = new ModifyExistingPersonCommand() { Id = 1, LastName = "2", Age = 1 };
            var result = person.Validate(new ValidationContext(person));
            IsTrue(result.First().MemberNames.First() == nameof(person.FirstName));
        }

        [Test]
        public void Command_Update_Person_Invalid_Id()
        {
            var person = new ModifyExistingPersonCommand() { Id = 0, LastName = "2", Age = 1, FirstName="2" };
            var result = person.Validate(new ValidationContext(person));
            IsTrue(result.First().MemberNames.First() == nameof(person.Id));
        }

        [Test]
        public void Command_Update_Person_Valid_Entity()
        {
            handler.Handle(new CreateNewPersonCommand()
            {
                FirstName = "Test",
                LastName = "Test",
                Age = 10
            });

            handler = iocProvider.GetContainer().Resolve<IPersonCommandHandler>();

            var person = new ModifyExistingPersonCommand() { Id = 1, FirstName = "Adam111111", LastName = "Liu11111", Age = 1000 };
            handler.Handle(person);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            iocProvider.Dispose();
            iocProvider = null;
        }
    }
}
