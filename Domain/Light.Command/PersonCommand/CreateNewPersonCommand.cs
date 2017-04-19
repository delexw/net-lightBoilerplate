using Light.Command.CommandValidaters;
using Light.Domain.Bus;
using Light.Domain.Bus.CommandHandler;
using Autofac.Extras.DynamicProxy;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Light.Command.PersonCommand
{
    public sealed class CreateNewPersonCommand : CommnadAggregate, IValidatableObject
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Age { get; set; }

        public override Operator Operator
        {
            get;
        } = Operator.Add;

        /// <summary>
        /// Validate properties only
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            Validator.TryValidateProperty(this.FirstName, new ValidationContext(this) { MemberName = nameof(this.FirstName) }, results);
            Validator.TryValidateProperty(this.LastName, new ValidationContext(this) { MemberName = nameof(this.LastName) }, results);
            Validator.TryValidateProperty(this.Age, new ValidationContext(this) { MemberName = nameof(this.Age) }, results);
            return results;
        }
    }
}
