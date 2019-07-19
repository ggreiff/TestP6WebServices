using System;
using System.Collections.Generic;
using System.Linq;

namespace TestP6WebServices.P6Api
{
    public class P6EpsService
    {
        private readonly EPSService _epsService;

        public P6EpsService(P6AuthenticationService authenticationService)
        {
            _epsService = new EPSService
            {
                CookieContainer = authenticationService.CookieContainer,
                Url = authenticationService.AuthService.Url.Replace("AuthenticationService", "EPSService")
            };
        }

        //TODO fix this to use a filter.
        public EPS GetEps(String epsName)
        {
            var epses = GetEps();
            return epses.FirstOrDefault(eps => eps.Name == epsName);
        }

        public List<EPS> GetEps()
        {
            var eps = new ReadEPS {Field = new EPSFieldType[3]};
            eps.Field[0] = EPSFieldType.ObjectId;
            eps.Field[1] = EPSFieldType.Name;
            eps.Field[2] = EPSFieldType.Id;
            var retVal = new List<EPS>(_epsService.ReadEPS(eps));
            return retVal;
        }

    }
}
