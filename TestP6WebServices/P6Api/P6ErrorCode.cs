using System;
using System.Collections.Generic;
using System.Web.Services.Protocols;

namespace TestP6WebServices.P6Api
{
    /// <summary>
    /// 
    /// </summary>
    public static class P6ErrorCode
    {
        /// <summary>
        /// Gets the oppmt error.
        /// </summary>
        /// <param name="ex">The soap exception</param>
        /// <returns>
        ///   <see cref="string"/>
        /// </returns>
        public static KeyValuePair<string, string> GetP6Error(SoapException ex)
        {
            var retVal = new KeyValuePair<string, string>(@"UNKNOWN P6 ERROR", ex.Message);
            var errorCodeNumber = ex.Detail.InnerText.ToInt();
            if (errorCodeNumber.HasValue)
            {
                var errorCode = "-1"; //GetErrorCode(errorCodeNumber.Value);
                var index = ex.Message.IndexOf('\n');
                var firstLine = ex.Message.Trim();    
                if (index > 0) firstLine = ex.Message.Substring(0, index).Trim();
                retVal = new KeyValuePair<string, string>(errorCode, firstLine);
            }
            return retVal;
        }

        /// <summary>
        /// Determines whether [is this error] [the specified API Error Constant].
        /// </summary>
        /// <param name="ex">The soap exception.</param>
        /// <param name="errorConstant">The error constant.</param>
        /// <returns>
        ///   <c>true</c> if [is this error] [the specified ex]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsThisError(SoapException ex, String errorConstant)
        {
            var error = GetP6Error(ex);
            return error.Key.IsEqualTo(errorConstant, true);
        }


    }
}

