/* 
 * Copyright (C) 2023 - present NRRT. 
 * All rights reserved.
 */

using Contracts.V1.Users;
using Contracts.V1.Users.Models;
using Contracts.V1.Users.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessLogic.User
{
    public interface IUserManager
    {
        public Task<AuthenticationResource> LogIn(UserLoginModel email);

        public Task<UserResource> GetByEmailAsync(string email);

        public Task<UserResource> CreateAsync(UserRegisterModel model);

        public Task<UserResource> UpdateAsync(string id, UserModificationModel model);

        public Task DeleteAsync(string id);
    }

    public class UserManager : IUserManager
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserManager(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserResource> GetByEmailAsync(string email)
        {
            var userEntity =  await _userManager.FindByEmailAsync(email);

            if (userEntity is null)
            {
                throw new Exception("User Does Not exist");
            }

            return userEntity.ToResource();
        }

        public async Task<UserResource> CreateAsync(UserRegisterModel model)
        {
            var userCreated = await _userManager.CreateAsync(model.ToEntity(), model.Password);

            if (userCreated.Errors.Any())
            {
                throw new Exception(userCreated.Errors.ToString());
            }

            var userEntity = (await _userManager.FindByEmailAsync(model.Email));

            return userEntity.ToResource(GenerateJwtToken(userEntity));
        }

        public async Task<UserResource> UpdateAsync(string id, UserModificationModel model)
        {
            var userEntity = await _userManager.FindByIdAsync(id);

            if(userEntity is null)
            {
                throw new Exception($"User with {id} does not exist");
            }

            userEntity.UserName = model.UserName;
            userEntity.Email = model.Email;
            
            await _userManager.UpdateAsync(userEntity);

            return (await _userManager
                .FindByEmailAsync(model.Email))
                .ToResource(GenerateJwtToken(userEntity));
        }

        public async Task<UserResource> ResetPassword(string id, PasswordModificationModel model)
        {
            var userEntity = await _userManager.FindByIdAsync(id);

            if (userEntity is null)
            {
                throw new Exception($"User with {id} does not exist");
            }

            var token = GenerateJwtToken(userEntity);
            var result  = await _userManager.ResetPasswordAsync(userEntity, token, model.NewPassword);

            if (result.Errors.Any())
            {
                throw new Exception($"User with {id} does not exist");
            }

            return (await _userManager
                .FindByEmailAsync(userEntity.Email))
                .ToResource(GenerateJwtToken(userEntity));
        }

        public async Task DeleteAsync(string id)
        {
            var userEntity = await _userManager.FindByIdAsync(id);

            if (userEntity is null)
            {
                throw new Exception("User does not exist");
            }

            await _userManager.DeleteAsync(userEntity);
        }

        public async Task<AuthenticationResource> LogIn(UserLoginModel model)
        {
            var userEntity = await _userManager.FindByEmailAsync(model.Email);

            if (userEntity is null)
            {
                throw new Exception($"User with {model.Email} does not exist");
            }

            var isCorrect = await _userManager.CheckPasswordAsync(userEntity, model.Password);

            if (!isCorrect)
            {
                throw new Exception("Login failed, please check your Email or Password");
            }

            return new()
            {
                Token = GenerateJwtToken(userEntity)
            };
        }

        private static string GenerateJwtToken(IdentityUser user)
        {
            var jwtHander = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes("nBPwRGs83PJPdaqvLeK55WyZE596St");

            var tolekDescriper = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", user.Id),
                    new Claim("userName", user.UserName),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = jwtHander.CreateToken(tolekDescriper);

            return jwtHander.WriteToken(token);
        }
    }
}
