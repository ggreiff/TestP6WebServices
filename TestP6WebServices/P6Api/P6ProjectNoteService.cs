using System;
using System.Collections.Generic;
using System.Globalization;

namespace TestP6WebServices.P6Api
{
    public class P6ProjectNoteService
    {
        private readonly ProjectNoteService _projectNoteService;

        public P6ProjectNoteService(P6AuthenticationService authenticationService)
        {
            _projectNoteService = new ProjectNoteService
            {
                CookieContainer = authenticationService.CookieContainer,
                Url = authenticationService.AuthService.Url.Replace("AuthenticationService", "ProjectNoteService")
            };
        }

        public  List<ProjectNote> GetProjectNotes(int projectObjectId)
        {
            var readProjectNotes = DefaultFields();
            readProjectNotes.Filter = "ProjectObjectId = " + projectObjectId.ToString(CultureInfo.InvariantCulture);
            return GetProjectNotes(readProjectNotes);
        }

        public List<ProjectNote> GetProjectNotes(Project project)
        {
            return GetProjectNotes(project.ObjectId);
        }

        public List<ProjectNote> GetProjectNotes(string projectId)
        {
            var readProjectNotes = DefaultFields();
            readProjectNotes.Filter =  "ProjectId like '" + projectId + "'";
            return GetProjectNotes(readProjectNotes);
        }

        public List<ProjectNote> GetProjectNotes(ReadProjectNotes readProjectNotes)
        {
            var retVal = new List<ProjectNote>(_projectNoteService.ReadProjectNotes(readProjectNotes));
            return retVal;
        }

        public ReadProjectNotes DefaultFields(String filter)
        {
            var defaultFields = DefaultFields();
            defaultFields.Filter = filter;
            return defaultFields;
        }

        public ReadProjectNotes DefaultFields()
        {
            var defaultFields = new ReadProjectNotes {Field = new ProjectNoteFieldType[8]};
            defaultFields.Field[0] = ProjectNoteFieldType.ProjectObjectId;
            defaultFields.Field[1] = ProjectNoteFieldType.NotebookTopicName;
            defaultFields.Field[2] = ProjectNoteFieldType.Note;
            defaultFields.Field[3] = ProjectNoteFieldType.ObjectId;
            defaultFields.Field[4] = ProjectNoteFieldType.RawTextNote;
            defaultFields.Field[5] = ProjectNoteFieldType.AvailableForWBS;
            defaultFields.Field[6] = ProjectNoteFieldType.ProjectId;
            defaultFields.Field[7] = ProjectNoteFieldType.WBSName;
            return defaultFields;
        }

    }
}
