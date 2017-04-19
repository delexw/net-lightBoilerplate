using Light.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Light.Domain.Entities;
using Light.Dtos;
using Light.Application.Contracts;
using Light.DataContracts.Repositories;
using AutoMapper;
using Light.Dtos.QueryExpressionMappers;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Data.Entity;

namespace Light.Application.QueryServices
{
    public class PersonQueryService : IPersonQueryServiceContract
    {
        internal IPersonReaderRepositoryContract _personRepo;
        internal IAgeGroupReaderRepositoryContract _ageGroupRepo;
        public PersonQueryService(
            IPersonReaderRepositoryContract personRepo,
            IAgeGroupReaderRepositoryContract ageGroupRepo)
        {
            _personRepo = personRepo;
            _ageGroupRepo = ageGroupRepo;
        }



        public async Task<IEnumerable<PersonAgeGroupDto>> Query(
            string filter = null,
            string orderBy = null,
            int? pageIndex = default(int?),
            int? pageCount = default(int?),
            params string[] includeProperties)
        {
            var expressionFilter = DtoExpressionMapper.Convert<PersonAgeGroupDto, bool>(filter);
            var personFilter = DtoExpressionMapper.Convert<PersonAgeGroupDto, Person, bool>(expressionFilter);
            var persons = await _personRepo.Query(personFilter, null, pageIndex, pageCount, includeProperties).ToListAsync();

            var personGroup = from person in persons
                              select new PersonAgeGroupDto
                              {
                                  Id = person.Id,
                                  Age = person.Age,
                                  FirstName = person.FirstName,
                                  LastName = person.LastName,
                                  RowVersion = person.RowVersion,
                                  Group = _ageGroupRepo.Query(
                                      ag => person.Age >= (ag.MinAge ?? 0) && person.Age < (ag.MaxAge ?? int.MaxValue),
                                  null, null, null, includeProperties).FirstOrDefault()
                              };

            return await Task.FromResult(personGroup);
        }

        public async Task<PersonAgeGroupDto> GetById(int Id)
        {
            var r = await this.Query($"Id = {Id}");
            return r.FirstOrDefault();
        }
    }
}
