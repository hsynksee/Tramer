using JWT.Algorithms;
using JWT.Builder;

namespace SharedKernel.Helpers
{
    public static class JwtBuilder
    {
        public static object Build(Dictionary<string, object> claims, JwtSettings jwtSettings)
        {
            long expireDate = DateTimeOffset.UtcNow.AddHours(jwtSettings.Expires).ToUnixTimeSeconds();

            string token = new JWT.Builder.JwtBuilder()
                                          .WithAlgorithm(new HMACSHA512Algorithm())
                                          .WithSecret(jwtSettings.Secret)
                                          .AddClaim("exp", expireDate)
                                          .AddClaim("iss", jwtSettings.Issuer)
                                          .AddClaim("jti", Guid.NewGuid().ToString("N"))
                                          .AddClaims(claims)
                                          .Encode();

            return new
            {
                access_token = "Bearer " + token,
                expires_in = expireDate,
                token_type = "bearer"
            };
        }
    }
}
