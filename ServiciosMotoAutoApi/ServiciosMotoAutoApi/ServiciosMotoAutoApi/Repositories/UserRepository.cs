using ComplianceApi.Contexts;
using ComplianceApi.Contracts;
using ComplianceApi.Models;
using ComplianceApi.Models.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace ComplianceApi.Repositories
{
    public class UserRepository : RepositoryComplianceBase<User>, IUserRepository
    {        
        public UserRepository(ComplianceDbContext repositoryContext)
           : base(repositoryContext)
        {
        }
        public async Task<PagedList<User>> GetAllUsersAsync(UserPagingParameters userPagingParameters)
        {
            var users = FindAll();

            SearchBy(ref users, userPagingParameters);

            return await PagedList<User>.ToPagedList(users.OrderBy(user => user.UserName),
                userPagingParameters.PageNumber,
                userPagingParameters.PageSize);
        }

        private void SearchBy(ref IQueryable<User> users, UserPagingParameters userPagingParameters)
        {
            if (!users.Any() || (userPagingParameters.UserName == null))
                return;

            if (userPagingParameters.UserName != null)
                users = users.Where(o => o.UserName.ToLower().Contains(userPagingParameters.UserName.Trim().ToLower()));

        }
        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await FindByCondition(user => user.UserId.Equals(userId))
                .FirstOrDefaultAsync();
        }
        public async Task<User> GetUserByRowVersionAsync(string userRowVersion)
        {
            return await FindByCondition(user => user.RowVersion.Equals(userRowVersion))
                .FirstOrDefaultAsync();
        }
        public void CreateUser(User user)
        {
            Create(user);
        }
        public void UpdateUser(User user, User entity)
        {
            user.UserName = entity.UserName;
            user.Enabled = entity.Enabled;
            user.IsAdmin = entity.IsAdmin;
            Update(user);
        }
        public void DeleteUser(User user)
        {
            Delete(user);
        }

        public bool AuthenticateUser(User user)
        {
            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, user.Domain))
            {
                return pc.ValidateCredentials(user.UserName, user.Password);               
            }
        }

        public async Task<string> GenerateJWTAsync(User user, IConfiguration configuration)
        {
            var userData = await FindByCondition(findUser => findUser.UserName.ToLower().Equals(user.UserName.ToLower()))
                .FirstOrDefaultAsync();
            if (userData == null)
                return "null";

            if(userData.Enabled)
            {
                var claims = new[]
               {
                    new Claim("user", user.UserName),
                    new Claim("isAdmin", userData.IsAdmin.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
               };
                var secretKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("ApiAuth:SecretKey")));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var expritaion = DateTime.UtcNow.AddDays(1);
                var tokeOptions = new JwtSecurityToken(
                    issuer: configuration.GetValue<string>("ApiAuth:Issuer"),
                    audience: configuration.GetValue<string>("ApiAuth:Audience"),
                    claims: claims,
                    expires: expritaion,
                    signingCredentials: signinCredentials
                );
                return new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            }
            else
            {
                return "null";
            }
            
        }
    }
}
