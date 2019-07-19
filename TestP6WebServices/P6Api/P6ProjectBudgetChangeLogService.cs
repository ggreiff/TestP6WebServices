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

namespace TestP6WebServices.P6Api
{
    /// <summary>
    /// Class P6ProjectBudgetChangeLogService.
    /// </summary>
    public class P6ProjectBudgetChangeLogService
    {
        /// <summary>
        /// The _project budget change log service
        /// </summary>
        private readonly ProjectBudgetChangeLogService _projectBudgetChangeLogService;

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        /// <value>The filter.</value>
        public String Filter { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="P6ProjectBudgetChangeLogService"/> class.
        /// </summary>
        /// <param name="authenticationService">The authentication service.</param>
        public P6ProjectBudgetChangeLogService(P6AuthenticationService authenticationService)
        {
            Filter = String.Empty;
            _projectBudgetChangeLogService = new ProjectBudgetChangeLogService
            {
                CookieContainer = authenticationService.CookieContainer,
                Url = authenticationService.AuthService.Url.Replace("AuthenticationService", "ProjectBudgetChangeLogService")
            };
        }

        /// <summary>
        /// Gets the project budget change logs.
        /// </summary>
        /// <param name="projectObjectId">The project object identifier.</param>
        /// <returns>List&lt;ProjectBudgetChangeLog&gt;.</returns>
        public List<ProjectBudgetChangeLog> GetProjectBudgetChangeLogs(Int32 projectObjectId)
        {
            var readProjectBudgetChangeLogs = DefaultFields();
            readProjectBudgetChangeLogs.Filter = String.Format(" ProjectObjectId = {0}", projectObjectId);
            return GetProjectBudgetChangeLogs(readProjectBudgetChangeLogs);
        }

        /// <summary>
        /// Gets the project budget change logs.
        /// </summary>
        /// <param name="filterString">The filter string.</param>
        /// <returns>List&lt;ProjectBudgetChangeLog&gt;.</returns>
        public List<ProjectBudgetChangeLog> GetProjectBudgetChangeLogs(String filterString)
        {
            var readProjectBudgetChangeLogs = DefaultFields();
            if (filterString.IsNotNullOrEmpty()) readProjectBudgetChangeLogs.Filter = filterString;
            return GetProjectBudgetChangeLogs(readProjectBudgetChangeLogs);
        }

        /// <summary>
        /// Gets the project budget change logs.
        /// </summary>
        /// <returns>List&lt;ProjectBudgetChangeLog&gt;.</returns>
        public List<ProjectBudgetChangeLog> GetProjectBudgetChangeLogs()
        {
            var readProjectBudgetChangeLogs = DefaultFields();
            if (Filter.IsNotNullOrEmpty()) readProjectBudgetChangeLogs.Filter = Filter;
            return GetProjectBudgetChangeLogs(readProjectBudgetChangeLogs);
        }

        /// <summary>
        /// Gets the project budget change logs.
        /// </summary>
        /// <param name="readProjectBudgetChangeLogs">The read project budget change logs.</param>
        /// <returns>List&lt;ProjectBudgetChangeLog&gt;.</returns>
        public List<ProjectBudgetChangeLog> GetProjectBudgetChangeLogs(ReadProjectBudgetChangeLogs readProjectBudgetChangeLogs)
        {
            var retVal = new List<ProjectBudgetChangeLog>(_projectBudgetChangeLogService.ReadProjectBudgetChangeLogs(readProjectBudgetChangeLogs));
            return retVal;
        }


        /// <summary>
        /// Defaults the fields.
        /// </summary>
        /// <returns>ReadProjectBudgetChangeLogs.</returns>
        public ReadProjectBudgetChangeLogs DefaultFields()
        {
            var fields = new List<ProjectBudgetChangeLogFieldType>
                             {
                                 ProjectBudgetChangeLogFieldType.Amount,
                                 ProjectBudgetChangeLogFieldType.ChangeNumber,
                                 ProjectBudgetChangeLogFieldType.CreateDate,
                                 ProjectBudgetChangeLogFieldType.CreateUser,
                                 ProjectBudgetChangeLogFieldType.Date,
                                 ProjectBudgetChangeLogFieldType.IsBaseline,
                                 ProjectBudgetChangeLogFieldType.IsTemplate,
                                 ProjectBudgetChangeLogFieldType.LastUpdateDate,
                                 ProjectBudgetChangeLogFieldType.LastUpdateUser,
                                 ProjectBudgetChangeLogFieldType.ObjectId,
                                 ProjectBudgetChangeLogFieldType.ProjectId,
                                 ProjectBudgetChangeLogFieldType.ProjectObjectId,
                                 ProjectBudgetChangeLogFieldType.Reason,
                                 ProjectBudgetChangeLogFieldType.Responsible,
                                 ProjectBudgetChangeLogFieldType.Status,
                                 ProjectBudgetChangeLogFieldType.WBSCode,
                                 ProjectBudgetChangeLogFieldType.WBSName,
                                 ProjectBudgetChangeLogFieldType.WBSObjectId
                             };
            var defaultFields = new ReadProjectBudgetChangeLogs { Field = fields.ToArray() };
            return defaultFields;
        }
    }

}

