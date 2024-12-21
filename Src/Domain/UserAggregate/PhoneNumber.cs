using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.UserAggregate
{
    public class PhoneNumber : ValueObject
    {
        private static readonly Regex MobileRegex = new Regex(@"^(\+98|0)?9\d{9}$", RegexOptions.Compiled);

        public string Value { get; }

        private PhoneNumber(string value)
        {
            Value = value;
        }

        public static PhoneNumber Create(string mobileNumber)
        {
            if (string.IsNullOrWhiteSpace(mobileNumber))
                throw new ArgumentException("Mobile number cannot be empty or null.");

            mobileNumber = mobileNumber.Trim();

            if (!MobileRegex.IsMatch(mobileNumber))
                throw new ArgumentException("Invalid mobile number format.");

            if (!mobileNumber.StartsWith("+98"))
            {
                if (mobileNumber.StartsWith("0"))
                    mobileNumber = "+98" + mobileNumber.Substring(1);
                else
                    mobileNumber = "+98" + mobileNumber;
            }

            return new PhoneNumber(mobileNumber);
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}
