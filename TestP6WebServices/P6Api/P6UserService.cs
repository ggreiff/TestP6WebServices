// ***********************************************************************
// Assembly         : NCIExtensions
// Author           : ggreiff
// Created          : 11-24-2013
//
// Last Modified By : ggreiff
// Last Modified On : 11-24-2013
// ***********************************************************************
// <copyright file="P6UserService.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;


namespace TestP6WebServices.P6Api
{
    /// <summary>
    /// Class P6UserService
    /// </summary>
    public class P6UserService {

        /// <summary>
        /// The user service
        /// </summary>
        public readonly UserService UserService;


        /// <summary>
        /// Initializes a new instance of the <see cref="P6UserService"/> class.
        /// </summary>
        /// <param name="authenticationService">The authentication service.</param>
        public P6UserService(P6AuthenticationService authenticationService)
        {
            UserService = new UserService
            {
                CookieContainer = authenticationService.CookieContainer,
                Url = authenticationService.AuthService.Url.Replace("AuthenticationService", "UserService")
            };
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="userObjectId">The user object id.</param>
        /// <returns>List{User}.</returns>
        public List<User> GetUser(int userObjectId)
        {
            var defaultFields = DefaultFields();
            defaultFields.Filter = String.Format("UserObjectId = {0}", userObjectId);
            return GetUser(defaultFields);
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="readUser">The read user.</param>
        /// <returns>List{User}.</returns>
        public List<User> GetUser(ReadUsers readUser)
        {
            var retVal = new List<User>(UserService.ReadUsers(readUser));
            return retVal;
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="filterString">The filter string.</param>
        /// <returns>List{User}.</returns>
        public List<User> GetUser(String filterString)
        {
            var defaultFields = DefaultFields();
            if (filterString.IsNotNullOrEmpty()) defaultFields.Filter = filterString;
            return GetUser(defaultFields);
        }

        /// <summary>
        /// Defaults the fields.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>ReadUsers.</returns>
        public ReadUsers DefaultFields(String filter)
        {
            var defaultFields = DefaultFields();
            defaultFields.Filter = filter;
            return defaultFields;
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="wildcard">if set to <c>true</c> [wildcard].</param>
        /// <returns>List{User}.</returns>
        public List<User> GetUser(string userName, bool wildcard)
        {
            var defaultFields = DefaultFields();
            defaultFields.Filter = String.Format("Name = '{0}'", userName);
            if (wildcard) defaultFields.Filter = String.Format("Name like '%{0}%'", userName);
            var user = GetUser(defaultFields);
            return user.Count > 0 ? user :  new List<User>() ;
        }

        /// <summary>
        /// Gets the name of the user by.
        /// </summary>
        /// <param name="projectObjectId">The project object id.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns>User.</returns>
        public User GetUserByName(Int32 projectObjectId, String userName)
        {
            var userList = GetUser(String.Format("ProjectObjectId = {0} AND Name = '{1}'", projectObjectId, userName));
            return userList.HasItems() ? userList[0] : null;
        }

        /// <summary>
        /// Gets the name of the user like.
        /// </summary>
        /// <param name="projectObjectId">The project object id.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns>User.</returns>
        public User GetUserLikeName(Int32 projectObjectId, String userName)
        {
            var userList = GetUser(String.Format("ProjectObjectId = {0} AND Name like '%{1}%'", projectObjectId, userName));
            return userList.HasItems() ? userList[0] : null;
        }

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>UpdateUsersResponse.</returns>
        public UpdateUsersResponse UpdateUser(User user)
        {
            var usernodes = new[] {user};
            return UserService.UpdateUsers(usernodes);
        }


        /// <summary>
        /// Adds the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>List{Int32}.</returns>
        public List<Int32> AddUser(User user)
        {
            var usernodes = new[] {user};
            return UserService.CreateUsers(usernodes).ToList();
        }

        /// <summary>
        /// Defaults the fields.
        /// </summary>
        /// <returns>ReadUsers.</returns>
        public ReadUsers DefaultFields()
        {
            var fields = new List<UserFieldType>
                             {
                                 UserFieldType.Name,
                                 UserFieldType.CreateDate,
                                 UserFieldType.ObjectId,
                                 UserFieldType.EmailAddress,
                                 UserFieldType.PersonalName,
                                 UserFieldType.OfficePhone,
                                 UserFieldType.LastUpdateDate
                             };
            var defaultFields = new ReadUsers { Field = fields.ToArray() };
            return defaultFields;
        }
    }
}