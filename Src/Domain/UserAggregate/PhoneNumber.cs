using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.UserAggregate
{
    public sealed class PhoneNumber : ValueObject
    {
        private static readonly Regex MobileRegex = new(@"^(\+98|0)?9\d{9}$", RegexOptions.Compiled);

        public string Value { get; private set; }

        private PhoneNumber() { }
        public PhoneNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Mobile number cannot be empty or null.");

            value = value.Trim();

            if (!MobileRegex.IsMatch(value))
                throw new ArgumentException("Invalid mobile number format.");

            if (!value.StartsWith("+98"))
            {
                if (value.StartsWith("0"))
                    value = string.Concat("+98", value.AsSpan(1));
                else
                    value = "+98" + value;
            }
            Value = value;
        }

        public static PhoneNumber Create() => new();

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}
