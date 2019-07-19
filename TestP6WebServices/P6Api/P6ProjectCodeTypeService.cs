using System;
using System.Collections.Generic;

namespace TestP6WebServices.P6Api
{
    public class P6ProjectCodeTypeService
    {
        private readonly ProjectCodeTypeService _portBinding;

        public String Filter { get; set; }

        public P6ProjectCodeTypeService(P6AuthenticationService authenticationService)
        {
            _portBinding = new ProjectCodeTypeService
                               {
                                   CookieContainer = authenticationService.CookieContainer,
                                   Url = authenticationService.AuthService.Url.Replace("AuthenticationService", "ProjectCodeTypeService")
                               };
        }

        public List<ProjectCodeType> GetProjectCodeTypes(String filterString)
        {
            var readProjectCodeTypes = DefaultFields();
            if (filterString.IsNotNullOrEmpty()) readProjectCodeTypes.Filter = filterString;
            return GetProjectCodeTypes(readProjectCodeTypes);
        }

        public List<ProjectCodeType> GetProjectCodeTypes()
        {
            var readProjectCodeTypes = DefaultFields();
            if (Filter.IsNotNullOrEmpty()) readProjectCodeTypes.Filter = Filter;
            return GetProjectCodeTypes(readProjectCodeTypes);
        }

        public List<ProjectCodeType> GetProjectCodeTypes(ReadProjectCodeTypes readProjectCodeTypes)
        {
            var retVal = new List<ProjectCodeType>(_portBinding.ReadProjectCodeTypes(readProjectCodeTypes));
            return retVal;
        }

        public ReadProjectCodeTypes DefaultFields()
        {
            var defaultFields = new ReadProjectCodeTypes { Field = new ProjectCodeTypeFieldType[4] };
            defaultFields.Field[0] = ProjectCodeTypeFieldType.Name;
            defaultFields.Field[1] = ProjectCodeTypeFieldType.ObjectId;
            defaultFields.Field[2] = ProjectCodeTypeFieldType.SequenceNumber;
            defaultFields.Field[3] = ProjectCodeTypeFieldType.Weight;
            return defaultFields;
        }
    }
}
