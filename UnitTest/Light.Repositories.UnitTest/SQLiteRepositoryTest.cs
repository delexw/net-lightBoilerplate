using Light.Domain.Entities;
using Light.Repositories;
using Light.SQLite;
using NUnit.Framework;
using Repository.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using EntityFramework.Toolkit.Core;
using EntityFramework.Toolkit;
using static NUnit.Framework.Assert;
using Light.DataContracts.Repositories;
using Light.DIManager;
using System.Threading;

namespace Light.Repositories.UnitTest
{
    [TestFixture]
    public class SQLiteRepositoryTest
    {
        private IAgeGroupReaderRepositoryContract agRepo;
        private IPersonReaderRepositoryContract pRepo;
        private IAgeGroupWriterRepositoryContract agWRepo;
        private IPersonWriterRepositoryContract pWRepo;
        private IDIProvider<ContainerBuilder, IContainer> iocProvider;
        private IContainer container;

        [OneTimeSetUp]
        public void SetUp()
        {
            iocProvider = new AutofacProvider($@"{ AppDomain.CurrentDomain.BaseDirectory}repoconfig\autofac.repo.reader.json",
                            $@"{AppDomain.CurrentDomain.BaseDirectory}repoconfig\autofac.repo.writer.json");
            iocProvider.Build();
            agRepo = iocProvider.GetContainer().Resolve<IAgeGroupReaderRepositoryContract>();
            pRepo = iocProvider.GetContainer().Resolve<IPersonReaderRepositoryContract>();

            agWRepo = iocProvider.GetContainer().Resolve<IAgeGroupWriterRepositoryContract>();
            pWRepo = iocProvider.GetContainer().Resolve<IPersonWriterRepositoryContract>();
            Thread.Sleep(2000);
        }

        [Test]
        [TestCase(12)]
        public void Repositories_Get_AgeGroup_All(int range)
        {
            var allAgeGroup = agRepo.Query();
            AreEqual(allAgeGroup.Count(), range);
        }

        [Test]
        public void Repositories_Get_Person_All()
        {
            var allPersons = pRepo.Query();
        }

        [Test]
        public void Repositories_Get_Person_AgeEqual4()
        {
            var persons = pRepo.Query(person => person.Age == 4);
        }

        [Test]
        public void Repositories_Get_Person_AgeEqual3_OrderByIDDesceding()
        {
            var persons = pRepo.Query(person => person.Age == 3,
                person => person.OrderByDescending(p => p.Id));
        }

        [Test]
        public void Repositories_Get_Person_AgeEqual3_OrderByIDDesceding_Page2_PageCount5()
        {
            var persons = pRepo.Query(person => person.Age == 3,
                person => person.OrderByDescending(p => p.Id),
                2,5);
        }


        [Test]
        [TestCase("NewPerson", "NewPerson", 50)]
        public void Repositories_Insert_Person(string firstName, string lastName, int age)
        {
            var expect = new Person { FirstName = firstName, LastName = lastName, Age = age };
            pWRepo.Create(expect);
            pWRepo.Commit();
        }

        [Test]
        [TestCase(1, "UpdatePerson", "UpdatePerson", 50)]
        public void Repositories_Update_Person(int id, string firstName, string lastName, int age)
        {
            pWRepo = iocProvider.GetContainer().Resolve<IPersonWriterRepositoryContract>();
            for (int i = 0; i < 1; i++)
            {
                Thread.Sleep(1000);
                pWRepo.Create(new Person { FirstName = "A", LastName = "B", Age = 3 });
                pWRepo.Commit();
            }
            pWRepo = iocProvider.GetContainer().Resolve<IPersonWriterRepositoryContract>();
            var expect = new Person { Id = id, FirstName = firstName, LastName = lastName, Age = age };
            pWRepo.Update(expect);
            pWRepo.Commit();
        }

        [Test]
        [TestCase(1)]
        public void Repositories_Delete_Person(int id)
        {
            for (int i = 0; i < 1; i++)
            {
                pWRepo.Create(new Person { FirstName = "A", LastName = "B", Age = 3 });
                pWRepo.Commit();
            }
            pWRepo = iocProvider.GetContainer().Resolve<IPersonWriterRepositoryContract>();
            pWRepo.Delete(id);
            pWRepo.Commit();
        }

        [TearDown]
        public void Dispose()
        {
            pWRepo.Delete(null);
        }


        [OneTimeTearDown]
        public void TearDown()
        {
            container.Dispose();
            container = null;
        }
    }
}
