// ***********************************************************************
// Assembly         : NCIExtensions
// Author           : ggreiff
// Created          : 11-24-2013
//
// Last Modified By : ggreiff
// Last Modified On : 11-28-2013
// ***********************************************************************
// <copyright file="P6ProjectService.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Globalization;

namespace TestP6WebServices.P6Api
{
    /// <summary>
    /// Class P6ProjectService
    /// </summary>
    public class P6ProjectService
    {
        /// <summary>
        /// The project service
        /// </summary>
        private readonly ProjectService _projectService;

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        /// <value>The filter.</value>
        public String Filter { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="P6ProjectService"/> class.
        /// </summary>
        /// <param name="authenticationService">The authentication service.</param>
        public P6ProjectService(P6AuthenticationService authenticationService)
        {
            Filter = String.Empty;
            _projectService = new ProjectService
                                      {
                                          CookieContainer = authenticationService.CookieContainer,
                                          Url = authenticationService.AuthService.Url.Replace("AuthenticationService", "ProjectService")
                                      };
        }

        /// <summary>
        /// Gets the project.
        /// </summary>
        /// <param name="filterString">The filter string.</param>
        /// <returns>Project.</returns>
        public Project GetProject(String filterString)
        {
            var readProjects = DefaultFields();
            readProjects.Filter = filterString;
            var projects = GetProjects(readProjects);
            return projects.HasItems() ? projects[0] : null;
        }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <param name="projectName">Name of the project.</param>
        /// <param name="wildcard">if set to <c>true</c> [wildcard].</param>
        /// <returns>List{Project}.</returns>
        public List<Project> GetProjects(string projectName, bool wildcard)
        {
            var readProjects = DefaultFields();
            readProjects.Filter = String.Format("Name = '{0}'", projectName);
            if (wildcard) readProjects.Filter = String.Format("Name like '%{0}%'", projectName);
            var projects = GetProjects(readProjects);
            return projects.Count == 0 ? new List<Project>() : projects;
        }

        /// <summary>
        /// Gets the project.
        /// </summary>
        /// <param name="projectObjectId">The project object id.</param>
        /// <returns>Project.</returns>
        public Project GetProject(int projectObjectId)
        {
            Project retVal = null;
            var readProjects = DefaultFields();
            readProjects.Filter = String.Format("ObjectId = {0}", projectObjectId.ToString(CultureInfo.InvariantCulture));
            var projects = GetProjects(readProjects);
            if (projects.Count > 0) retVal = projects[0];
            return retVal;
        }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <param name="filterString">The filter string.</param>
        /// <returns>List{Project}.</returns>
        public List<Project> GetProjects(String filterString)
        {
            var readProjects = DefaultFields();
            if (filterString.IsNotNullOrEmpty()) readProjects.Filter = filterString;
            return GetProjects(readProjects);
        }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <returns>List{Project}.</returns>
        public List<Project> GetProjects()
        {
            var readProjects = DefaultFields();
            if (Filter.IsNotNullOrEmpty()) readProjects.Filter = Filter;
            return GetProjects(readProjects);
        }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <param name="readProjects">The read projects.</param>
        /// <returns>List{Project}.</returns>
        public List<Project> GetProjects(ReadProjects readProjects)
        {
            var retVal = new List<Project>(_projectService.ReadProjects(readProjects));
            return retVal;
        }

        /// <summary>
        /// Creates the one project.
        /// </summary>
        /// <param name="projectName">Name of the project.</param>
        /// <param name="projectId">The project id.</param>
        /// <param name="parentEpsObjectId">The parent eps object id.</param>
        /// <returns>System.Int32.</returns>
        public int CreateOneProject(String projectName, String projectId, int parentEpsObjectId)
        {
            var projects = new Project[1];
            projects[0].Name = projectName;
            projects[0].Id = projectId;
            projects[0].ParentEPSObjectId = parentEpsObjectId;
            var retVal = _projectService.CreateProjects(projects);
            return retVal[0];
        }

        /// <summary>
        /// Defaults the fields.
        /// </summary>
        /// <returns>ReadProjects.</returns>
        public ReadProjects DefaultFields()
        {  
            var fields = new List<ProjectFieldType>
                             {
                                 ProjectFieldType.ObjectId,
                                 ProjectFieldType.Id,
                                 ProjectFieldType.Name,
                                 ProjectFieldType.Status,
                                 ProjectFieldType.StartDate,
                                 ProjectFieldType.FinishDate,
                                 ProjectFieldType.DataDate,
                             };
            var defaultFields = new ReadProjects {Field = fields.ToArray()};
            return defaultFields;
        }
    }

    /// <summary>
    /// Class ProjectServiceExtensions
    /// </summary>
    public static partial class ProjectServiceExtensions
    {
        /// <summary>
        /// Adds the project field.
        /// </summary>
        /// <param name="readProjects">The read projects.</param>
        /// <param name="projectField">The project field.</param>
        /// <returns>ReadProjects.</returns>
        public static ReadProjects AddProjectField(this ReadProjects readProjects, ProjectFieldType projectField)
        {
            var fieldtypes = new List<ProjectFieldType>(readProjects.Field) { projectField };
            readProjects.Field = fieldtypes.ToArray();
            return readProjects;
        }
    }


}
