using System;
using System.Collections.Generic;

namespace TestP6WebServices.P6Api
{
    public class P6ProjectPortfolioService
    {
        private readonly ProjectPortfolioService _projectPortfolioService;

        public P6ProjectPortfolioService(P6AuthenticationService authenticationService)
        {
            _projectPortfolioService = new ProjectPortfolioService
            {
                CookieContainer = authenticationService.CookieContainer,
                Url = authenticationService.AuthService.Url.Replace("AuthenticationService", "ProjectPortfolioService")
            };
        }

        public List<ProjectPortfolio> GetPortfolioProjects(ReadProjectPortfolios readPortfolios)
        {
            var retVal = new List<ProjectPortfolio>(_projectPortfolioService.ReadProjectPortfolios(readPortfolios));
            return retVal;
        }

        public List<ProjectPortfolio> GetProjectPortfolios()
        {
            var defaultFields = DefaultFields();
            return GetPortfolioProjects(defaultFields);
        }

        public ReadProjectPortfolios DefaultFields(String filter)
        {
            var defaultFields = DefaultFields();
            defaultFields.Filter = filter;
            return defaultFields;
        }

        public ReadProjectPortfolios DefaultFields()
        {
            var defaultFields = new ReadProjectPortfolios { Field = new ProjectPortfolioFieldType[3] };
            defaultFields.Field[0] = ProjectPortfolioFieldType.ObjectId;
            defaultFields.Field[1] = ProjectPortfolioFieldType.Name;
            defaultFields.Field[2] = ProjectPortfolioFieldType.MemberProject;
            return defaultFields;
        }

        public ReadProjectPortfolios ResizeDefaultFields(ReadProjectPortfolios defaultFields, long newSize)
        {
            var filetype = new ProjectPortfolioFieldType[newSize];
            defaultFields.Field.CopyTo(filetype, 0);
            defaultFields.Field = filetype;
            return defaultFields;
        }


    }
}
