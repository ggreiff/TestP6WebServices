// ***********************************************************************
// Assembly         : NCIExtensions
// Author           : ggreiff
// Created          : 11-24-2013
//
// Last Modified By : ggreiff
// Last Modified On : 11-24-2013
// ***********************************************************************
// <copyright file="P6ActivityService.cs" company="">
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
    /// Class P6ActivityService
    /// </summary>
    public class P6ActivityService
    {

        /// <summary>
        /// The activity service
        /// </summary>
        private readonly ActivityService _activityService;
        /// <summary>
        /// The resource assignment service
        /// </summary>
        private readonly ResourceAssignmentService _resourceAssignmentService;

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        /// <value>The filter.</value>
        public String Filter { get; set; }

        /// <summary>
        /// Gets the activity service p6.
        /// </summary>
        /// <value>The activity service p6.</value>
        public ActivityService ActivityServiceP6 { get { return _activityService; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="P6ActivityService"/> class.
        /// </summary>
        /// <param name="authenticationService">The authentication service.</param>
        public P6ActivityService(P6AuthenticationService authenticationService)
        {
            Filter = String.Empty;
            _activityService = new ActivityService
            {
                CookieContainer = authenticationService.CookieContainer,
                Url = authenticationService.AuthService.Url.Replace("AuthenticationService", "ActivityService")
            };

            _resourceAssignmentService = new ResourceAssignmentService
            {
                CookieContainer = authenticationService.CookieContainer,
                Url = authenticationService.AuthService.Url.Replace("AuthenticationService", "ActivityService")
            };
        }

        /// <summary>
        /// Gets the activity.
        /// </summary>
        /// <param name="filterString">The filter string.</param>
        /// <returns>Activity.</returns>
        public Activity GetActivity(String filterString)
        {
            Activity retVal = null;
            var readActivities = DefaultFields();
            readActivities.Filter = filterString;
            var projects = GetActivities(readActivities);
            if (projects.Count > 0) retVal = projects[0];
            return retVal;
        }

        /// <summary>
        /// Gets the activity.
        /// </summary>
        /// <param name="projectName">Name of the project.</param>
        /// <param name="wildcard">if set to <c>true</c> [wildcard].</param>
        /// <returns>Activity.</returns>
        public Activity GetActivity(string projectName, bool wildcard)
        {
            Activity retVal = null;
            var readActivities = DefaultFields();
            if (wildcard) readActivities.Filter = String.Format("Name like '{0}%'", projectName);
            var projects = GetActivities(readActivities);
            if (projects.Count > 0) retVal = projects[0];
            return retVal;
        }

        /// <summary>
        /// Gets the activity.
        /// </summary>
        /// <param name="projectObjectId">The project object id.</param>
        /// <returns>Activity.</returns>
        public Activity GetActivity(int projectObjectId)
        {
            Activity retVal = null;
            var readActivities = DefaultFields();
            readActivities.Filter = String.Format("ObjectId = {0}", projectObjectId.ToString(CultureInfo.InvariantCulture));
            var projects = GetActivities(readActivities);
            if (projects.Count > 0) retVal = projects[0];
            return retVal;
        }

        /// <summary>
        /// Gets the activities.
        /// </summary>
        /// <param name="filterString">The filter string.</param>
        /// <returns>List{Activity}.</returns>
        public List<Activity> GetActivities(String filterString)
        {
            var readActivities = DefaultFields();
            if (filterString.IsNotNullOrEmpty()) readActivities.Filter = filterString;
            return GetActivities(readActivities);
        }

        /// <summary>
        /// Gets the project activities.
        /// </summary>
        /// <param name="projectObjectId">The project object id.</param>
        /// <returns>List{Activity}.</returns>
        public List<Activity> GetProjectActivities(Int32 projectObjectId)
        {
            return GetActivities(String.Format("ProjectObjectId = {0}", projectObjectId));

        }

        /// <summary>
        /// Gets the WBS activities.
        /// </summary>
        /// <param name="wbsObjectId">The WBS object id.</param>
        /// <returns>List{Activity}.</returns>
        public List<Activity> GetWbsActivities(Int32 wbsObjectId)
        {
            return GetActivities(String.Format("WBSObjectId = {0}", wbsObjectId));

        }

        /// <summary>
        /// Gets the activities.
        /// </summary>
        /// <returns>List{Activity}.</returns>
        public List<Activity> GetActivities()
        {
            var readActivities = DefaultFields();
            if (Filter.IsNotNullOrEmpty()) readActivities.Filter = Filter;
            return GetActivities(readActivities);
        }

        /// <summary>
        /// Gets the activities.
        /// </summary>
        /// <param name="readActivities">The read activities.</param>
        /// <returns>List{Activity}.</returns>
        public List<Activity> GetActivities(ReadActivities readActivities)
        {
            var retVal = new List<Activity>(_activityService.ReadActivities(readActivities));
            return retVal;
        }


        /// <summary>
        /// Defaults the fields.
        /// </summary>
        /// <returns>ReadActivities.</returns>
        public ReadActivities DefaultFields()
        {
            var defaultFields = new ReadActivities();
            var fields = new List<ActivityFieldType>
                             {
                                 ActivityFieldType.ObjectId,
                                 ActivityFieldType.Id,
                                 ActivityFieldType.Name,
                                 ActivityFieldType.Status,
                                 ActivityFieldType.PlannedStartDate,
                                 ActivityFieldType.PlannedFinishDate,
                                 ActivityFieldType.ActualStartDate,
                                 ActivityFieldType.ActualFinishDate,
                                 ActivityFieldType.BaselineStartDate,
                                 ActivityFieldType.BaselineFinishDate,
                                 ActivityFieldType.DataDate,
                                 ActivityFieldType.Type
                             };
            defaultFields.Field = fields.ToArray();
            return defaultFields;
        }

        /// <summary>
        /// Defaults the resouce fields.
        /// </summary>
        /// <returns>ReadResourceAssignments.</returns>
        public ReadResourceAssignments DefaultResouceFields()
        {
            var defaultFields = new ReadResourceAssignments();
            var fields = new List<ResourceAssignmentFieldType>
                                     {
                                         ResourceAssignmentFieldType.ObjectId,
                                         ResourceAssignmentFieldType.ProjectObjectId,
                                         //ResourceAssignmentFieldType.RemainingCost,
                                         ResourceAssignmentFieldType.ResourceType,
                                         ResourceAssignmentFieldType.StartDate,
                                         ResourceAssignmentFieldType.FinishDate,
                                     };
            defaultFields.Field = fields.ToArray();
            return defaultFields;
        }

        /// <summary>
        /// Adds the activity field.
        /// </summary>
        /// <param name="readActivities">The read activities.</param>
        /// <param name="projectField">The project field.</param>
        /// <returns>ReadActivities.</returns>
        public ReadActivities AddActivityField(ReadActivities readActivities, ActivityFieldType projectField)
        {
            var fieldtypes = new List<ActivityFieldType>(readActivities.Field) { projectField };
            readActivities.Field = fieldtypes.ToArray();
            return readActivities;
        }

        /// <summary>
        /// Gets the resouce assignments.
        /// </summary>
        /// <param name="filterString">The filter string.</param>
        /// <returns>List{ResourceAssignment}.</returns>
        public List<ResourceAssignment> GetResouceAssignments(String filterString)
        {
            var readResourceAssignments = DefaultResouceFields();
            if (filterString.IsNotNullOrEmpty()) readResourceAssignments.Filter = filterString;
            return GetResouceAssignments(readResourceAssignments);
        }

        /// <summary>
        /// Gets the resouce assignments.
        /// </summary>
        /// <param name="readResourceAssignments">The read resource assignments.</param>
        /// <returns>List{ResourceAssignment}.</returns>
        public List<ResourceAssignment> GetResouceAssignments(ReadResourceAssignments readResourceAssignments)
        {
            var retVal = new List<ResourceAssignment>(_resourceAssignmentService.ReadResourceAssignments(readResourceAssignments));
            return retVal;
        }

    }
}
