using System;
using System.Collections.Generic;
using NLog;


namespace TestP6WebServices.P6Api
{
    /// <summary>
    /// Class P6Utils
    /// </summary>
    public class P6Utils
    {
        /// <summary>
        /// The NLOG Logger for this class.
        /// </summary>
        public static Logger PsLogger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Bridge Server Id
        /// </summary>
        public Int32? P6ServerId = null;

        /// <summary>
        /// Gets or sets the p6 authentication.
        /// </summary>
        /// <value>The p6 authentication.</value>
        public P6AuthenticationService P6Authentication { get; set; }

        public P6WebServiceInfo P6WebServiceInfo { get; set; }

        public P6AuthenticationService P6LogOn(P6WebServiceInfo p6WebServiceInfo)
        {
            P6WebServiceInfo = p6WebServiceInfo;
            return P6LogOn();
        }

        /// <summary>
        /// P6s the log on.
        /// </summary>
        /// <returns>P6AuthenticationService.</returns>
        public P6AuthenticationService P6LogOn()
        {
            var retValue = new P6AuthenticationService();
            //
            // If we don't have a global P6
            //
            if (Program.P6WebServicesLogin == null)
                retValue = new P6AuthenticationService(P6WebServiceInfo.HostName, P6WebServiceInfo.HostPort);
                
                //
                // Check to see if we need to login.
                //
                if (!retValue.IsLoggedOn)
                { 
                    retValue.Login(P6WebServiceInfo.UserName, P6WebServiceInfo.Password);
                }
            //
            // TODO check if session is still valid
            //
            Program.P6WebServicesLogin = retValue;
            return retValue;
        }

        /// <summary>
        /// Gets the project milestones.
        /// </summary>
        /// <param name="projectId">The project id.</param>
        public List<Activity> GetProjectMilestones(Int32 projectId)
        {
            List<Activity> retVal;
            var filter = String.Format("ProjectObjectId = {0} and Type in ('Finish Milestone', 'Start Milestone')", projectId);
            //var filter = String.Format("ProjectObjectId = {0}", projectId);


            try
            {
                P6Authentication = P6LogOn();
                var activityService = new P6ActivityService(P6Authentication);
                retVal = activityService.GetActivities(filter);
            }
            catch (Exception ex)
            {
                PsLogger.Error(ex.Message);
                retVal = new List<Activity>();
            }
            return retVal;

        }

        /// <summary>
        /// Gets the name of the project.
        /// </summary>
        /// <param name="projectId">The project id.</param>
        /// <returns>Project.</returns>
        public Project GetProjectName(Int32 projectId)
        {
            Project retVal;
            try
            {
                P6Authentication = P6LogOn();
                var projectService = new P6ProjectService(P6Authentication);
                retVal = projectService.GetProject(projectId);

            }
            catch (Exception ex)
            {
                PsLogger.Error(ex.Message);
                retVal = new Project();
            }
            return retVal;
        }
    }
}