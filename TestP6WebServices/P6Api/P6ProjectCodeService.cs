using System;
using System.Collections.Generic;

namespace TestP6WebServices.P6Api
{
    public class P6ProjectCodeService
    {
        private readonly ProjectCodeService _projectCodeService;

        public String Filter { get; set; }

        public P6ProjectCodeService(P6AuthenticationService authenticationService)
        {
            _projectCodeService = new ProjectCodeService
                               {
                                   CookieContainer = authenticationService.CookieContainer,
                                   Url = authenticationService.AuthService.Url.Replace("AuthenticationService", "ProjectCodeService")
                               };
        }

        public List<ProjectCode> GetProjectCodeTypes(String filterString)
        {
            var readProjectCodeTypes = DefaultFields();
            if (filterString.IsNotNullOrEmpty()) readProjectCodeTypes.Filter = filterString;
            return GetProjectCodeTypes(readProjectCodeTypes);
        }

        public List<ProjectCode> GetProjectCodeTypes()
        {
            var readProjectCodeTypes = DefaultFields();
            if (Filter.IsNotNullOrEmpty()) readProjectCodeTypes.Filter = Filter;
            return GetProjectCodeTypes(readProjectCodeTypes);
        }

        public List<ProjectCode> GetProjectCodeTypes(ReadProjectCodes readProjectCodeTypes)
        {
            var retVal = new List<ProjectCode>(_projectCodeService.ReadProjectCodes(readProjectCodeTypes));
            return retVal;
        }

        public ReadProjectCodes DefaultFields()
        {
            var defaultFields = new ReadProjectCodes { Field = new ProjectCodeFieldType[3] };
            defaultFields.Field[0] = ProjectCodeFieldType.ObjectId;
            defaultFields.Field[1] = ProjectCodeFieldType.ParentObjectId;
            defaultFields.Field[2] = ProjectCodeFieldType.CodeValue;
            return defaultFields;
        }
    }
}
