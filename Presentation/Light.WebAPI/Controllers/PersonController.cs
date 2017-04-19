using Light.DIManager;
using Light.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Autofac;
using Light.Application.Contracts;
using System.Threading.Tasks;
using Light.Domain.Bus.Interfaces;
using Light.Command.PersonCommand;
using Light.WebAPI.Base;

namespace Light.WebAPI.Controllers
{
    [EnableCors("*", "*", "*")]
    public class PersonController : ApiControllerExtension
    {
        private IPersonQueryServiceContract _queryService;
        private IPersonCommandServiceContract _commandService;

        public PersonController(IPersonQueryServiceContract queryService,
            IPersonCommandServiceContract commandService)
        {
            _queryService = queryService;
            _commandService = commandService;
        }

        // GET api/<controller>
        [HttpGet]
        public async Task<IHttpActionResult> GetPersons(
            string filter = null,
            string orderBy = null,
            int? pageIndex = null,
            int? pageCount = null)
        {
            var result = await _queryService.Query(filter, orderBy, pageIndex, pageCount);
            return Ok(result);
        }

        // GET api/<controller>/5
        [HttpGet]
        public async Task<IHttpActionResult> GetPerson(int Id)
        {
            var result = await _queryService.GetById(Id);
            return Ok(result);
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IHttpActionResult> CreatePerson(CreateNewPersonCommand command)
        {
            await _commandService.ApplyAsync(command);
            return Ok();
        }

        // PUT api/<controller>/5
        [HttpPut]
        public async Task<IHttpActionResult> EditPerson(int Id, ModifyExistingPersonCommand command)
        {
            command.Id = Id;
            await _commandService.ApplyAsync(command);
            return Ok();
        }

    }
}