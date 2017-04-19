using Light.DataContracts;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Light.Domain.Bus.CommandHandler
{
    public abstract class CommandHandler<TEntity>
    {
        protected IEntityWriterContract<TEntity> _repository;
        public CommandHandler(IEntityWriterContract<TEntity> repository)
        {
            if (ReferenceEquals(null, repository))
            {
                throw new ArgumentNullException(nameof(repository));
            }
            _repository = repository;
        }

        /// <summary>
        /// Command To Entity
        /// </summary>
        /// <typeparam name="TCommand"></typeparam>
        /// <param name="command"></param>
        protected abstract TEntity CommandMapper<TCommand>(TCommand command) where TCommand : CommnadAggregate;
    }
}
