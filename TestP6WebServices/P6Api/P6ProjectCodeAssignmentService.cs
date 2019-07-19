using System;
using System.Collections.Generic;
using System.Globalization;

namespace TestP6WebServices.P6Api
{
    public class P6ProjectCodeAssignmentService
    {
        private readonly ProjectCodeAssignmentService _projectCodeAssignmentService;

        public String Filter { get; set; }

        public P6ProjectCodeAssignmentService(P6AuthenticationService authenticationService)
        {
            _projectCodeAssignmentService = new ProjectCodeAssignmentService
            {
                CookieContainer = authenticationService.CookieContainer,
                Url = authenticationService.AuthService.Url.Replace("AuthenticationService", "ProjectCodeAssignmentService")
            };
        }

        public List<ProjectCodeAssignment> GetProjectCodeAssignment(int projectObjectId)
        {
            var filter = "ProjectObjectId = " + projectObjectId.ToString(CultureInfo.InvariantCulture);
            var defaultFields = DefaultFields(filter);
            return GetProjectCodeAssignment(defaultFields);
        }

        public List<ProjectCodeAssignment> GetProjectCodeAssignment(Project project)
        {
            return GetProjectCodeAssignment(project.ObjectId);
        }

        public List<ProjectCodeAssignment> GetProjectCodeAssignment(String filterString)
        {
            var codeAssignments = DefaultFields(filterString);
            return GetProjectCodeAssignment(codeAssignments);
        }

        public List<ProjectCodeAssignment> GetProjectCodeAssignment(ReadProjectCodeAssignments readProjectCodeAssignments)
        {
            var retVal = new List<ProjectCodeAssignment>(_projectCodeAssignmentService.ReadProjectCodeAssignments(readProjectCodeAssignments));
            return retVal;
        }


        public ReadProjectCodeAssignments DefaultFields(String filterString)
        {
            var defaultFields = DefaultFields();
            if (filterString.IsNotNullOrEmpty()) defaultFields.Filter = filterString;
            return defaultFields;
        }

        public ReadProjectCodeAssignments DefaultFields()
        {
            var defaultFields = new ReadProjectCodeAssignments();
            var fields = new List<ProjectCodeAssignmentFieldType>
                             {
                                ProjectCodeAssignmentFieldType.ProjectName,
                                ProjectCodeAssignmentFieldType.ProjectObjectId,
                                ProjectCodeAssignmentFieldType.ProjectCodeDescription,
                                ProjectCodeAssignmentFieldType.ProjectCodeTypeName,
                                ProjectCodeAssignmentFieldType.ProjectCodeObjectId,
                                ProjectCodeAssignmentFieldType.ProjectCodeValue
                             };
            defaultFields.Field = fields.ToArray();
            return defaultFields;
        }
    }
}
