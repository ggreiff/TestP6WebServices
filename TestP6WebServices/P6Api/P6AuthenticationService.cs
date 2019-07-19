// ***********************************************************************
// Assembly         : NCIExtensions
// Author           : ggreiff
// Created          : 11-24-2013
//
// Last Modified By : ggreiff
// Last Modified On : 11-24-2013
// ***********************************************************************
// <copyright file="P6AuthenticationService.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;

namespace TestP6WebServices.P6Api
{

    /// <summary>
    /// Class P6AuthenticationService
    /// </summary>
    public class P6AuthenticationService
    {
        /// <summary>
        /// The transport URL
        /// </summary>
        private const String TransportUrl = "http://<hostname>:<p6WsPort number>/p6ws/services/AuthenticationService";

        /// <summary>
        /// The auth service
        /// </summary>
        public AuthenticationService AuthService;

        /// <summary>
        /// The login
        /// </summary>
        private readonly Login _login;

        /// <summary>
        /// Gets or sets the cookie container.
        /// </summary>
        /// <value>The cookie container.</value>
        public CookieContainer CookieContainer { get; set; }

        /// <summary>
        /// The server
        /// </summary>
        private string _server;
        /// <summary>
        /// Gets or sets the server.
        /// </summary>
        /// <value>The server.</value>
        public String Server
        {
            get { return _server; }
            set
            {
                _server = value;
                AuthService.Url = UpdateP6HostUrl();
            }
        }

        /// <summary>
        /// The port
        /// </summary>
        private int _port;
        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>The port.</value>
        public Int32 Port
        {
            get { return _port; }
            set
            {
                _port = value;
                AuthService.Url = UpdateP6HostUrl();
            }
        }

        /// <summary>
        /// Gets or sets the is logged on.
        /// </summary>
        /// <value>The is logged on.</value>
        public Boolean IsLoggedOn {
            get
            {
                return CheckSession();
            } }

        /// <summary>
        /// The _username
        /// </summary>
        private string _username;
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        public String Username
        {
            get { return _username; }
            set
            {
                _username = value;
                _login.UserName = _username;
            }
        }

        /// <summary>
        /// The password
        /// </summary>
        private string _password;
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public String Password
        {
            get { return _password; }
            set
            {
                _password = value;
                _login.Password = _password;
            }
        }

        /// <summary>
        /// The database instance id
        /// </summary>
        private int _databaseInstanceId;
        /// <summary>
        /// Gets or sets the database instance id.
        /// </summary>
        /// <value>The database instance id.</value>
        public Int32 DatabaseInstanceId
        {
            get { return _databaseInstanceId; }
            set
            {
                _databaseInstanceId = value;
                _login.DatabaseInstanceId = _databaseInstanceId;
                _login.DatabaseInstanceIdSpecified = true;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="P6AuthenticationService"/> class.
        /// </summary>
        public P6AuthenticationService()
        {
            _login = new Login();
            AuthService = new AuthenticationService();
            //IsLoggedOn = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="P6AuthenticationService"/> class.
        /// </summary>
        /// <param name="p6WsServer">The p6 ws server.</param>
        /// <param name="p6WsPort">The p6 ws port.</param>
        public P6AuthenticationService(String p6WsServer, Int32 p6WsPort)
            : this()
        {
            Server = p6WsServer;
            Port = p6WsPort;
            AuthService.Url = UpdateP6HostUrl();
            AuthService.CookieContainer = new CookieContainer();
        }

        /// <summary>
        /// Updates the p6 host URL.
        /// </summary>
        /// <returns>String.</returns>
        public String UpdateP6HostUrl()
        {
            return TransportUrl.Replace("<hostname>", Server).Replace("<p6WsPort number>", Port.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Databases the exists.
        /// </summary>
        /// <param name="databaseName">Name of the database.</param>
        /// <returns>Boolean.</returns>
        /// <exception cref="System.ArgumentNullException">databaseName</exception>
        public Boolean DatabaseExists(String databaseName)
        {
            var retVal = false;
            if (databaseName == null) throw new ArgumentNullException("databaseName");
            if (GetDatabaseInstance(databaseName) != null) retVal = true;
            return retVal;
        }

        /// <summary>
        /// Databases the exists.
        /// </summary>
        /// <param name="databaseId">The database id.</param>
        /// <returns>Boolean.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">databaseId</exception>
        public Boolean DatabaseExists(int databaseId)
        {
            var retVal = false;
            if (databaseId < 1) throw new ArgumentOutOfRangeException("databaseId");
            if (GetDatabaseInstance(databaseId) != null) retVal = true;
            return retVal;
        }

        /// <summary>
        /// Gets the database instance id.
        /// </summary>
        /// <param name="databaseName">Name of the database.</param>
        /// <returns>System.Nullable{Int32}.</returns>
        /// <exception cref="System.ArgumentNullException">databaseName</exception>
        public Int32? GetDatabaseInstanceId(String databaseName)
        {
            if (databaseName == null) throw new ArgumentNullException("databaseName");
            var databaseInstances = ReadDatabaseInstances().ToList();
            var instance = databaseInstances.FirstOrDefault(x => x.DatabaseName.IsEqualTo(databaseName, true));
            if (instance != null) return instance.DatabaseInstanceId;
            return null;
        }

        /// <summary>
        /// Gets the database instance.
        /// </summary>
        /// <param name="databaseName">Name of the database.</param>
        /// <returns>ReadDatabaseInstancesResponseDatabaseInstance.</returns>
        /// <exception cref="System.ArgumentNullException">databaseName</exception>
        public ReadDatabaseInstancesResponseDatabaseInstance GetDatabaseInstance(String databaseName)
        {
            if (databaseName == null) throw new ArgumentNullException("databaseName");
            var databaseInstances = ReadDatabaseInstances().ToList();
            return databaseInstances.FirstOrDefault(instance => instance.DatabaseName.IsEqualTo(databaseName, true));
        }

        /// <summary>
        /// Gets the database instance.
        /// </summary>
        /// <param name="databaseId">The database id.</param>
        /// <returns>ReadDatabaseInstancesResponseDatabaseInstance.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">databaseId</exception>
        public ReadDatabaseInstancesResponseDatabaseInstance GetDatabaseInstance(int databaseId)
        {
            if (databaseId < 1) throw new ArgumentOutOfRangeException("databaseId");
            var databaseInstances = ReadDatabaseInstances().ToList();
            return databaseInstances.FirstOrDefault(instance => databaseId == instance.DatabaseInstanceId);
        }

        /// <summary>
        /// Reads the database instances.
        /// </summary>
        /// <returns>List{ReadDatabaseInstancesResponseDatabaseInstance}.</returns>
        public List<ReadDatabaseInstancesResponseDatabaseInstance> ReadDatabaseInstances()
        {
            var dbinstances = AuthService.ReadDatabaseInstances(new object());
            return dbinstances.ToList();
        }

        /// <summary>
        /// Checks the session.
        /// </summary>
        /// <returns>Boolean.</returns>
        public Boolean CheckSession()
        {
            try
            {
                var readSession = AuthService.ReadSessionProperties(new object());
                return readSession.IsValid;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Logins the specified p6 username.
        /// </summary>
        /// <param name="p6Username">The p6 username.</param>
        /// <param name="p6Password">The p6 password.</param>
        /// <returns>Boolean.</returns>
        public Boolean Login(string p6Username, string p6Password)
        {
            _login.UserName = p6Username;
            _login.Password = p6Password;
            if (AuthService == null) return false;
            AuthService.Login(_login);
            CookieContainer = AuthService.CookieContainer;
            //IsLoggedOn = true;
            return true;
        }

        /// <summary>
        /// Logouts this instance.
        /// </summary>
        public void Logout()
        {
            AuthService.Logout(new object());
            //IsLoggedOn = false;
        }
    }
}
