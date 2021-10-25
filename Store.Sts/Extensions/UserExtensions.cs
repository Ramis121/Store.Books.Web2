using Store.Sts.Extensions;
using Store.Sts.Models;
using System;
using System.Text.RegularExpressions;

namespace Api.OAuth2.Extensions
{
    public static class UserExtensions
    {
        public const string DefaultPhonePattern = @"^(?:\(?)(?<AreaCode>\d{3})(?:[\).\s]?)(?<Prefix>\d{3})(?:[-\.\s]?)(?<Suffix>\d{4})(?!\d)";
        /*@"
        ^                  # From Beginning of line
        (?:\(?)            # Match but don't capture optional (
        (?<AreaCode>\d{3}) # 3 digit area code
        (?:[\).\s]?)       # Optional ) or . or space
        (?<Prefix>\d{3})   # Prefix
        (?:[-\.\s]?)       # optional - or . or space
        (?<Suffix>\d{4})   # Suffix
        (?!\d)             # Fail if eleventh number found";*/
        public static Match CheckPhone(this string phoneNumber, string phonePattern = DefaultPhonePattern)
        {
            return Regex.Match(phoneNumber, DefaultPhonePattern);
        }
        public static int GetAge(this DateTime dateOfBirth)
        {
            var today = DateTime.Today;

            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (dateOfBirth.Year * 100 + dateOfBirth.Month) * 100 + dateOfBirth.Day;

            return (a - b) / 10000;
        }
        public static ApplicationUser NewVerificationTokenUser(this string phoneNumber)
        {
            var password = Guid.NewGuid().ToString();
            return new ApplicationUser
            {
                UserName = phoneNumber,
                PhoneNumber = phoneNumber,
                LockoutEnabled = true,
                PhoneNumberConfirmed = false,
                EmailConfirmed = false,
                SecurityStamp = (password.GetHash(SecurityExtensions.HashType.SHA512) + phoneNumber.GetHash(SecurityExtensions.HashType.SHA256))
                    .GetHash(SecurityExtensions.HashType.MD5)
            };
        }
    }
}
