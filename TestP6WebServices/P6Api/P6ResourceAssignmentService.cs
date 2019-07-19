using System;
using System.Collections.Generic;

namespace TestP6WebServices.P6Api
{
    public class P6ResourceAssignmentService
    {

        private readonly ResourceAssignmentService _resourceAssignmentService;

        public String Filter { get; set; }

        public P6ResourceAssignmentService(P6AuthenticationService authenticationService)
        {
            Filter = String.Empty;
            _resourceAssignmentService = new ResourceAssignmentService
            {
                CookieContainer = authenticationService.CookieContainer,
                Url = authenticationService.AuthService.Url.Replace("AuthenticationService", "ResourceAssignmentService")
            };
        }

        public List<ResourceAssignment> GetResourceAssignment(String filterString)
        {
            var readActivities = DefaultFields();
            if (filterString.IsNotNullOrEmpty()) readActivities.Filter = filterString;
            return GetResouceAssignments(readActivities);
        }

        public List<ResourceAssignment> GetResourceAssignment()
        {
            var readActivities = DefaultFields();
            if (Filter.IsNotNullOrEmpty()) readActivities.Filter = Filter;
            return GetResouceAssignments(readActivities);
        }


        public List<ResourceAssignment> GetResouceAssignments(ReadResourceAssignments readResouceAssignments)
        {
            var retVal = new List<ResourceAssignment>(_resourceAssignmentService.ReadResourceAssignments(readResouceAssignments));
            return retVal;
        }

        public ReadResourceAssignments DefaultFields()
        {
            var defaultFields = new ReadResourceAssignments();
            var fields = new List<ResourceAssignmentFieldType>
                             {
                                 ResourceAssignmentFieldType.ObjectId,
                                 ResourceAssignmentFieldType.ProjectObjectId,
                                 ResourceAssignmentFieldType.StartDate,
                                 ResourceAssignmentFieldType.FinishDate,
                                 ResourceAssignmentFieldType.RemainingCost
                             };
            defaultFields.Field = fields.ToArray();
            return defaultFields;
        }
    }
}
