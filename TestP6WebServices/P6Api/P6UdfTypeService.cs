using System;
using System.Collections.Generic;
using System.Globalization;

namespace TestP6WebServices.P6Api
{
    public class P6UdfTypeService
    {
        private readonly UDFTypeService _udfTypeService;

        public P6UdfTypeService(P6AuthenticationService authenticationService)
        {
            _udfTypeService = new UDFTypeService
            {
                CookieContainer = authenticationService.CookieContainer,
                Url = authenticationService.AuthService.Url.Replace("AuthenticationService", "UDFTypeService")
            };
        }

        public List<UDFType> GetUdfTypes(int objectId)
        {
            var defaultFields = DefaultFields();
            defaultFields.Filter = String.Format("ObjectId = {0}", objectId.ToString(CultureInfo.InvariantCulture));
            return GetUdfTypes(defaultFields);
        }

        public List<UDFType> GetUdfTypes(String filterString)
        {
            var readUdfTypes = DefaultFields(filterString);
            return GetUdfTypes(readUdfTypes);
        }

        public List<UDFType> GetUdfTypesAll()
        {
            var defaultFields = DefaultFields();
            var retVal = new List<UDFType>(_udfTypeService.ReadUDFTypes(defaultFields));
            return retVal;
        }

        public List<UDFType> GetUdfTypes(ReadUDFTypes readUdfTypes)
        {
            var retVal = new List<UDFType>(_udfTypeService.ReadUDFTypes(readUdfTypes));
            return retVal;
        }

        public ReadUDFTypes DefaultFields(String filter)
        {
            var defaultFields = DefaultFields();
            defaultFields.Filter = filter;
            return defaultFields;
        }

        public ReadUDFTypes DefaultFields()
        {
            var defaultFields = new ReadUDFTypes();
            var fields = new List<UDFTypeFieldType>
                             {
                                 UDFTypeFieldType.ObjectId,
                                 UDFTypeFieldType.SubjectArea,
                                 UDFTypeFieldType.Title,
                                 UDFTypeFieldType.DataType
                             };
            defaultFields.Field = fields.ToArray();
            return defaultFields;
        }
    }
}
