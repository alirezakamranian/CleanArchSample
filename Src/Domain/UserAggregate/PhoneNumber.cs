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
            Value = value;
        }

        public static PhoneNumber Create(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("Mobile number cannot be empty or null.");

            phoneNumber = phoneNumber.Trim();

            if (!MobileRegex.IsMatch(phoneNumber))
                throw new ArgumentException("Invalid mobile number format.");

            if (!phoneNumber.StartsWith("+98"))
            {
                if (phoneNumber.StartsWith("0"))
                    phoneNumber = string.Concat("+98", phoneNumber.AsSpan(1));
                else
                    phoneNumber = "+98" + phoneNumber;
            }

            return new PhoneNumber(phoneNumber);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}
