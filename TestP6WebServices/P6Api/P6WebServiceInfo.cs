using System;

namespace TestP6WebServices.P6Api
{
    /// <summary>
    /// Class P6WebServiceInfo
    /// </summary>
    public class P6WebServiceInfo
    {
        /// <summary>
        /// The URI
        /// </summary>
        private Uri _uri;

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        public String Url { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>The name of the user.</value>
        public String UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public String Password { get; set; }

        /// <summary>
        /// Gets or sets the error MSG.
        /// </summary>
        /// <value>The error MSG.</value>
        public String ErrorMsg { get; set; }


        /// <summary>
        /// Gets the name of the host.
        /// </summary>
        /// <value>The name of the host.</value>
        public String HostName
        {
            get
            {
                if (_uri == null) _uri = new Uri(Url);
                return _uri.Host;
            }
        }

        /// <summary>
        /// Gets the host port.
        /// </summary>
        /// <value>The host port.</value>
        public Int32 HostPort
        {
            get
            {
                if (_uri == null) _uri = new Uri(Url);
                return _uri.Port;
            }
        }
    }
}