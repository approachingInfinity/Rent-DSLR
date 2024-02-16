using System.ComponentModel.DataAnnotations;

namespace DslrCustomerRecord.Utilities
{
    public class ValidEmailDomainAttribute:ValidationAttribute
    {
        private readonly string reqEmail;
        public ValidEmailDomainAttribute(string reqEmail)
        {
            this.reqEmail = reqEmail;
        }
        public override bool IsValid(object? value)
        {
            string[] email = value.ToString().Split('@');
            return email[1].ToUpper() == reqEmail.ToUpper();
        }
    }
}
