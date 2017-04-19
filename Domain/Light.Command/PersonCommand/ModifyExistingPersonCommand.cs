using Light.Domain.Bus;
using Light.Domain.Bus.CommandHandler;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Light.Command.PersonCommand
{
    public sealed class ModifyExistingPersonCommand : CommnadAggregate, IValidatableObject
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [Range(0,int.MaxValue)]
        public int Age { get; set; }

        public override Operator Operator
        {
            get;
        } = Operator.Update;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            Validator.TryValidateProperty(this.Id, new ValidationContext(this) { MemberName = nameof(this.Id) }, results);
            Validator.TryValidateProperty(this.FirstName, new ValidationContext(this) { MemberName = nameof(this.FirstName) }, results);
            Validator.TryValidateProperty(this.LastName, new ValidationContext(this) { MemberName = nameof(this.LastName) }, results);
            Validator.TryValidateProperty(this.Age, new ValidationContext(this) { MemberName = nameof(this.Age) }, results);
            return results;
        }
    }
}
