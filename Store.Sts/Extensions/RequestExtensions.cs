using OpenIddict.Abstractions;

namespace Store.Sts.Extensions
{
    public static class RequestExtensions
    {
        public static bool IsVerificationTokenGrantType(this OpenIddictRequest request)
        {
            return request.GrantType == "verification_token";
        }
    }
}
