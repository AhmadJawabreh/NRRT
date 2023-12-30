using Contracts.V1.Users;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogic.User
{
    public static class UserMapper
    {
        public static IdentityUser ToEntity(this UserRegisterModel model)
        {
            return new()
            {
                UserName = model.UserName,
                Email = model.Email
            };
        }

        public static UserResource ToResource(this IdentityUser entity, string? token = null, DateTimeOffset? expireAt = null)
        {
            return new()
            {
                UserName = entity.UserName,
                Email = entity.Email,
                Token = token,
                ExpireAt = expireAt
            };
        }
    }
}
