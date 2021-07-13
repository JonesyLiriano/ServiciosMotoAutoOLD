using ComplianceApi.Models;
using ComplianceApi.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplianceApi.Contracts
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<PagedList<User>> GetAllUsersAsync(UserPagingParameters userPagingParameters);
        Task<User> GetUserByIdAsync(int userId);
        Task<User> GetUserByRowVersionAsync(string UserRowNumber);
        void CreateUser(User user);
        void UpdateUser(User user, User entity);
        void DeleteUser(User user);
        bool AuthenticateUser(User user);
        Task<string> GenerateJWTAsync(User user, IConfiguration configuration);
    }
}
