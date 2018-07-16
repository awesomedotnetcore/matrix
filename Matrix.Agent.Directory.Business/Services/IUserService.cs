using Matrix.Agent.Directory.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Matrix.Agent.Directory.Business.Services
{
    public interface IUserService
    {
        Task<List<User>> GetUsers(Guid application);

        Task<User> GetUserById(Guid userId);
        Task<User> GetUserByUsername(Guid application, string username);
        Task<User> GetUserByEmail(Guid application, string email);

        Task<Guid> CreateUser(Guid application, string username, string password, string firstName, string lastName, string email, string phone);

        Task<bool> UpdateUserProfileById(Guid userId, string firstName, string lastName, string email, string phone);
        Task<bool> UpdateUserProfileByUsername(Guid application, string username, string firstName, string lastName, string email, string phone);
        Task<bool> UpdateUserProfileByEmail(Guid application, string email, string firstName, string lastName, string phone);

        Task<bool> UpdateUserPasswordById(Guid userId, string password);
        Task<bool> UpdateUserPasswordByUsername(Guid application, string username, string password);
        Task<bool> UpdateUserPasswordByEmail(Guid application, string email, string password);

        Task<bool> AddUserRolesById(Guid userId, params Guid[] roles);
        Task<bool> AddUserRolesUsername(Guid application, string username, params Guid[] roles);
        Task<bool> AddUserRolesUsername(Guid application, string username, params string[] roles);
        Task<bool> AddUserRolesByEmail(Guid application, string email, params Guid[] roles);
        Task<bool> AddUserRolesByEmail(Guid application, string email, params string[] roles);

        Task<bool> RemoveUserRolesById(Guid userId, params Guid[] roles);
        Task<bool> RemoveUserRolesByUsername(Guid application, string username, params Guid[] roles);
        Task<bool> RemoveUserRolesByUsername(Guid application, string username, params string[] roles);
        Task<bool> RemoveUserRolesByEmail(Guid application, string email, params Guid[] roles);
        Task<bool> RemoveUserRolesByEmail(Guid application, string email, params string[] roles);

        Task<bool> AddUserGroupsById(Guid userId, params Guid[] groups);
        Task<bool> AddUserGroupsByUsername(Guid application, string username, params Guid[] groups);
        Task<bool> AddUserGroupsByUsername(Guid application, string username, params string[] groups);
        Task<bool> AddUserGroupsByEmail(Guid application, string email, params Guid[] groups);
        Task<bool> AddUserGroupsByEmail(Guid application, string email, params string[] groups);

        Task<bool> RemoveUserGroupsById(Guid userId, params Guid[] groups);
        Task<bool> RemoveUserGroupsByUsername(Guid application, string username, params Guid[] groups);
        Task<bool> RemoveUserGroupsByUsername(Guid application, string username, params string[] groups);
        Task<bool> RemoveUserGroupsByEmail(Guid application, string email, params Guid[] groups);
        Task<bool> RemoveUserGroupsByEmail(Guid application, string email, params string[] groups);

        Task<bool> DeleteUserById(Guid userId);
        Task<bool> DeleteUserByUsername(Guid application, string username);
        Task<bool> DeleteUserByEmail(Guid application, string email);
    }
}