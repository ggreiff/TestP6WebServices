// ***********************************************************************
// Assembly         : NCIExtensions
// Author           : ggreiff
// Created          : 11-24-2013
//
// Last Modified By : ggreiff
// Last Modified On : 11-24-2013
// ***********************************************************************
// <copyright file="P6WbsService.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;

namespace TestP6WebServices.P6Api
{
    /// <summary>
    /// Class P6WbsService
    /// </summary>
    public class P6WbsService
    {

        /// <summary>
        /// The WBS service
        /// </summary>
        public readonly WBSService WbsService;


        /// <summary>
        /// Initializes a new instance of the <see cref="P6WbsService"/> class.
        /// </summary>
        /// <param name="authenticationService">The authentication service.</param>
        public P6WbsService(P6AuthenticationService authenticationService)
        {
            WbsService = new WBSService
            {
                CookieContainer = authenticationService.CookieContainer,
                Url = authenticationService.AuthService.Url.Replace("AuthenticationService", "WBSService")
            };
        }

        /// <summary>
        /// Gets the WBS.
        /// </summary>
        /// <param name="wbsObjectId">The WBS object id.</param>
        /// <returns>List{WBS}.</returns>
        public List<WBS> GetWbs(int wbsObjectId)
        {
            var defaultFields = DefaultFields();
            defaultFields.Filter = String.Format("WBSObjectId = {0}", wbsObjectId);
            return GetWbs(defaultFields);
        }

        /// <summary>
        /// Gets the WBS.
        /// </summary>
        /// <param name="readWbs">The read WBS.</param>
        /// <returns>List{WBS}.</returns>
        public List<WBS> GetWbs(ReadWBS readWbs)
        {
            var retVal = new List<WBS>(WbsService.ReadWBS(readWbs));
            return retVal;
        }

        /// <summary>
        /// Gets the WBS.
        /// </summary>
        /// <param name="filterString">The filter string.</param>
        /// <returns>List{WBS}.</returns>
        public List<WBS> GetWbs(String filterString)
        {
            var defaultFields = DefaultFields();
            if (filterString.IsNotNullOrEmpty()) defaultFields.Filter = filterString;
            return GetWbs(defaultFields);
        }

        /// <summary>
        /// Defaults the fields.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>ReadWBS.</returns>
        public ReadWBS DefaultFields(String filter)
        {
            var defaultFields = DefaultFields();
            defaultFields.Filter = filter;
            return defaultFields;
        }

        /// <summary>
        /// Gets the WBS.
        /// </summary>
        /// <param name="wbsName">Name of the WBS.</param>
        /// <param name="wildcard">if set to <c>true</c> [wildcard].</param>
        /// <returns>List{WBS}.</returns>
        public List<WBS> GetWbs(string wbsName, bool wildcard)
        {
            var defaultFields = DefaultFields();
            defaultFields.Filter = String.Format("Name = '{0}'", wbsName);
            if (wildcard) defaultFields.Filter = String.Format("Name like '%{0}%'", wbsName);
            var wbs = GetWbs(defaultFields);
            return wbs.Count > 0 ? wbs :  new List<WBS>() ;
        }

        /// <summary>
        /// Gets the name of the WBS by.
        /// </summary>
        /// <param name="projectObjectId">The project object id.</param>
        /// <param name="wbsName">Name of the WBS.</param>
        /// <returns>WBS.</returns>
        public WBS GetWbsByName(Int32 projectObjectId, String wbsName)
        {
            var wbsList = GetWbs(String.Format("ProjectObjectId = {0} AND Name = '{1}'", projectObjectId, wbsName));
            return wbsList.HasItems() ? wbsList[0] : null;
        }

        /// <summary>
        /// Gets the name of the WBS like.
        /// </summary>
        /// <param name="projectObjectId">The project object id.</param>
        /// <param name="wbsName">Name of the WBS.</param>
        /// <returns>WBS.</returns>
        public WBS GetWbsLikeName(Int32 projectObjectId, String wbsName)
        {
            var wbsList = GetWbs(String.Format("ProjectObjectId = {0} AND Name like '%{1}%'", projectObjectId, wbsName));
            return wbsList.HasItems() ? wbsList[0] : null;
        }

        /// <summary>
        /// Updates the WBS.
        /// </summary>
        /// <param name="wbs">The WBS.</param>
        /// <returns>UpdateWBSResponse.</returns>
        public UpdateWBSResponse UpdateWbs(WBS wbs)
        {
            var wbsnodes = new[] {wbs};
            return WbsService.UpdateWBS(wbsnodes);
        }


        /// <summary>
        /// Adds the WBS.
        /// </summary>
        /// <param name="wbs">The WBS.</param>
        /// <returns>List{Int32}.</returns>
        public List<Int32> AddWbs(WBS wbs)
        {
            var wbsnodes = new[] {wbs};
            return WbsService.CreateWBS(wbsnodes).ToList();
        }

        /// <summary>
        /// Defaults the fields.
        /// </summary>
        /// <returns>ReadWBS.</returns>
        public ReadWBS DefaultFields()
        {
            var fields = new List<WBSFieldType>
                             {
                                 WBSFieldType.Name,
                                 WBSFieldType.Code,
                                 WBSFieldType.ObjectId,
                                 WBSFieldType.StartDate,
                                 WBSFieldType.FinishDate,
                                 WBSFieldType.ProjectObjectId,
                                 WBSFieldType.ParentObjectId
                             };
            var defaultFields = new ReadWBS {Field = fields.ToArray()};
            return defaultFields;
        }
    }
}
