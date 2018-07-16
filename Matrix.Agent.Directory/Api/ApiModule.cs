using Matrix.Agent.Directory.Api.Model;
using Matrix.Agent.Directory.Business.Services;
using Matrix.Agent.Directory.Model;
using Matrix.Threading;
using Nancy;
using Nancy.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Matrix.Agent.Directory.Api
{
    public class ApiModule : NancyModule
    {
        private const string ResponseContentType = "application/json";

        private IApplicationService Applications { get; }

        private IUserService Users { get; }

        private IUserGroupService UserGroups { get; }

        private IUserRoleService UserRoles { get; }

        public ApiModule(IApplicationService applications, IUserService users, IUserGroupService userGroups, IUserRoleService userRoles)
            : base("/api")
        {
            Applications = applications ?? throw new ArgumentNullException(nameof(applications));

            Users = users ?? throw new ArgumentNullException(nameof(users));

            UserGroups = userGroups ?? throw new ArgumentNullException(nameof(userGroups));

            UserRoles = userRoles ?? throw new ArgumentNullException(nameof(userRoles));

            Get["/"] = GetVersion;

            Get["/apps"] = GetApplications;

            Get["/apps/{app}/users"] = GetUsers;

            Post["/apps/{app}/users"] = CreateUser;

            Put["/apps/{app}/users"] = UpdateUser;

            Delete["/apps/{app}/users/{id}"] = DeleteUser;

            Get["/apps/{app}/usergroups"] = GetUserGroups;

            Post["/apps/{app}/usergroups"] = CreateUserGroup;

            Put["/apps/{app}/usergroups"] = UpdateUserGroup;

            Delete["/apps/{app}/usergroups/{id}"] = DeleteUserGroup;

            Get["/apps/{app}/userroles"] = GetUserRoles;

            Post["/apps/{app}/userroles"] = CreateUserRole;

            Put["/apps/{app}/userroles"] = UpdateUserRole;

            Delete["/apps/{app}/userroles/{id}"] = DeleteUserRole;
        }

        private Response GetVersion(dynamic args)
        {
            var response = (Response)JsonConvert.SerializeObject(new { id = AssemblyInfo.Id, name = AssemblyInfo.Name, description = AssemblyInfo.Description, version = AssemblyInfo.Version, build = AssemblyInfo.Build, copyright = AssemblyInfo.Copyright, hash = AssemblyInfo.Hash });

            response.ContentType = ResponseContentType;

            return response;
        }

        private Response GetApplications(dynamic args)
        {
            var apps = Async.Execute(() => Applications.GetApplications());

            var response = (Response)JsonConvert.SerializeObject(apps);

            response.ContentType = ResponseContentType;

            return response;
        }

        private Response GetUsers(dynamic args)
        {
            var users = Async.Execute<List<User>>(() => Users.GetUsers(args.app));

            var response = (Response)JsonConvert.SerializeObject(users);

            response.ContentType = ResponseContentType;

            return response;
        }

        private Response CreateUser(dynamic args)
        {
            var result = Guid.Empty;

            var model = this.BindAndValidate<CreateUserModel>();

            if (model != null)
                result = Async.Execute(() => Users.CreateUser(model.Application, model.Username, model.Password, model.FirstName, model.LastName, model.Email, model.Phone));

            var response = (Response)JsonConvert.SerializeObject(result);

            response.ContentType = ResponseContentType;

            return response;
        }

        private Response UpdateUser(dynamic args)
        {
            var result = false;

            var model = this.BindAndValidate<UpdateUserProfileModel>();

            if (model != null)
                result = Async.Execute(() => Users.UpdateUserProfileById(model.Id, model.FirstName, model.LastName, model.Email, model.Phone));

            var response = (Response)JsonConvert.SerializeObject(result);

            response.ContentType = ResponseContentType;

            return response;
        }

        private Response DeleteUser(dynamic args)
        {
            var result = false;

            result = Async.Execute<bool>(() => Users.DeleteUserById(args.id));

            var response = (Response)JsonConvert.SerializeObject(result);

            response.ContentType = ResponseContentType;

            return response;
        }

        private Response GetUserGroups(dynamic args)
        {
            var groups = Async.Execute<List<UserGroup>>(() => UserGroups.GetUserGroups(args.app));

            var response = (Response)JsonConvert.SerializeObject(groups);

            response.ContentType = ResponseContentType;

            return response;
        }

        private Response CreateUserGroup(dynamic args)
        {
            var result = Guid.Empty;

            var model = this.BindAndValidate<CreateUserGroupModel>();

            if (model != null)
                result = Async.Execute(() => UserGroups.CreateUserGroup(model.Application, model.Name, model.Description));

            var response = (Response)JsonConvert.SerializeObject(result);

            response.ContentType = ResponseContentType;

            return response;
        }

        private Response UpdateUserGroup(dynamic args)
        {
            var result = false;

            var model = this.BindAndValidate<UpdateUserGroupModel>();

            if (model != null)
                result = Async.Execute(() => UserGroups.UpdateUserGroup(model.Id, model.Name, model.Description));

            var response = (Response)JsonConvert.SerializeObject(result);

            response.ContentType = ResponseContentType;

            return response;
        }

        private Response DeleteUserGroup(dynamic args)
        {
            var result = false;

            result = Async.Execute<bool>(() => Users.DeleteUserById(args.id));

            var response = (Response)JsonConvert.SerializeObject(result);

            response.ContentType = ResponseContentType;

            return response;
        }

        private Response GetUserRoles(dynamic args)
        {
            var roles = Async.Execute<List<UserRole>>(() => UserRoles.GetUserRoles(args.app));

            var response = (Response)JsonConvert.SerializeObject(roles);

            response.ContentType = ResponseContentType;

            return response;
        }

        private Response CreateUserRole(dynamic args)
        {
            var result = Guid.Empty;

            var model = this.BindAndValidate<CreateUserRoleModel>();

            if (model != null)
                result = Async.Execute(() => UserRoles.CreateUserRole(model.Application, model.Name, model.Description));

            var response = (Response)JsonConvert.SerializeObject(result);

            response.ContentType = ResponseContentType;

            return response;
        }

        private Response UpdateUserRole(dynamic args)
        {
            var result = false;

            var model = this.BindAndValidate<UpdateUserRoleModel>();

            if (model != null)
                result = Async.Execute(() => UserRoles.UpdateUserRole(model.Id, model.Name, model.Description));

            var response = (Response)JsonConvert.SerializeObject(result);

            response.ContentType = ResponseContentType;

            return response;
        }

        private Response DeleteUserRole(dynamic args)
        {
            var result = false;

            result = Async.Execute<bool>(() => Users.DeleteUserById(args.id));

            var response = (Response)JsonConvert.SerializeObject(result);

            response.ContentType = ResponseContentType;

            return response;
        }
    }
}