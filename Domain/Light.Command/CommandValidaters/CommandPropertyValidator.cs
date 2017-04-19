using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Light.Event.PersonEvent;
using Light.Command.Contracts;
using Light.Command.PersonCommand;
using Light.Domain.Bus.Interfaces;
using Light.Domain.Bus.EventBus;

namespace Light.Command.CommandValidaters
{
    public class CommandPropertyValidator : ICommandPropertyValidator
    {
        public void Intercept(IInvocation invocation)
        {
            if (typeof(ICommandHandler<ICommand>).GetMethods().Select(m => m.Name).Contains(invocation.Method.Name))
            {
                //Check the result of Validate method
                var validation = invocation.Arguments[0] as IValidatableObject;
                if (validation != null)
                {
                    //Validate properties
                    var result = validation.Validate(new ValidationContext(validation));
                    if (result != null && result.Count() > 0)
                    {
                        var error = result.Aggregate((first, second) =>
                        {
                            return new ValidationResult($"{first.ErrorMessage}{Environment.NewLine}{second.ErrorMessage}");
                        });

                        //Throw exception events;
                        if (invocation.Arguments[0].GetType() == typeof(CreateNewPersonCommand))
                        {
                            //Trigger event
                            EventBus.Instance.Trigger(new PersonNotCreatedEvent(error.ErrorMessage));
                        }
                        else if (invocation.Arguments[0].GetType() == typeof(CreateNewPersonCommand))
                        {
                            //Trigger event
                            EventBus.Instance.Trigger(new PersonNotUpdatedEvent(error.ErrorMessage));
                        }
                    }
                }
            }
            invocation.Proceed();
        }
    }
}
