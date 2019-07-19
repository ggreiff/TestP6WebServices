using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestP6WebServices.P6Api
{
    public class P6UdfCodeService
    {
        private readonly UDFCodeService _udfCodeService;

        public String Filter { get; set; }

        public P6UdfCodeService(P6AuthenticationService authenticationService)
        {
            _udfCodeService = new UDFCodeService
            {
                CookieContainer = authenticationService.CookieContainer,
                Url = authenticationService.AuthService.Url.Replace("AuthenticationService", "UDFCodeService")
            };
        }

        public List<UDFCode> GetUdfCodeTypes(String filterString)
        {
            var readUdfCodeTypes = DefaultFields();
            if (filterString.IsNotNullOrEmpty()) readUdfCodeTypes.Filter = filterString;
            return GetUdfCodeTypes(readUdfCodeTypes);
        }

        public List<UDFCode> GetUdfCodeTypes()
        {
            var readUdfCodeTypes = DefaultFields();
            if (Filter.IsNotNullOrEmpty()) readUdfCodeTypes.Filter = Filter;
            return GetUdfCodeTypes(readUdfCodeTypes);
        }

        public List<UDFCode> GetUdfCodeTypes(ReadUDFCodes readUdfCodeTypes)
        {
            var retVal = new List<UDFCode>(_udfCodeService.ReadUDFCodes(readUdfCodeTypes));
            return retVal;
        }

        public ReadUDFCodes DefaultFields()
        {
            var fields = new List<UDFCodeFieldType>
                             {
                                UDFCodeFieldType.ObjectId,
                                UDFCodeFieldType.CodeTypeObjectId,
                                UDFCodeFieldType.CodeValue,
                                UDFCodeFieldType.CodeTypeTitle,
                                UDFCodeFieldType.Description
                             };
            var defaultFields = new ReadUDFCodes { Field = fields.ToArray() };
            return defaultFields;
        }
    }
}
