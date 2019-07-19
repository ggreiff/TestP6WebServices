using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using NLog;
using TestP6WebServices.P6Api;

namespace TestP6WebServices
{
    public class Program
    {
        private static readonly Logger NLogger = LogManager.GetCurrentClassLogger();
        public static P6AuthenticationService P6WebServicesLogin;
        public static void Main(string[] args)
        {
            var p = new Program();
            p.Run();
        }

        public void Run()
        {
            try
            {

                var p6WebServerInfo = new P6WebServiceInfo { Url = "http://contessa:8206/p6ws/services", UserName = "admin", Password = "password" };

                var p6Util = new P6Utils();

                P6WebServicesLogin = p6Util.P6LogOn(p6WebServerInfo);

                var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Project Budgets");
         

                var p6ProjectService = new P6ProjectService(P6WebServicesLogin);

                var p6ProjectBudgetChangeLogService = new P6ProjectBudgetChangeLogService(P6WebServicesLogin);

                var projectList = p6ProjectService.GetProjects(p6ProjectService.DefaultFields());

                var row = 1;
                worksheet.Cell(row, 1).Value = "ProjectName";
                worksheet.Cell(row, 2).Value = "CreateUser";
                worksheet.Cell(row, 3).Value = "WBSName";
                worksheet.Cell(row, 4).Value = "ChangeNumber";
                worksheet.Cell(row, 5).Value = "Date";
                worksheet.Cell(row, 6).Value = "Reason";
                worksheet.Cell(row, 7).Value = "Responsible";
                worksheet.Cell(row, 8).Value = "Status";
                worksheet.Cell(row++, 9).Value = "Amount";
                foreach (var project in projectList)
                {
                    var projectName = project.Name;
                    //if (projectName.IsEqualTo("Sub-Project: Test",true)) System.Diagnostics.Debugger.Break();
                    var projectBudgetChangeLogList = p6ProjectBudgetChangeLogService.GetProjectBudgetChangeLogs(project.ObjectId);

                    foreach (var projectBudgetChangeLog in projectBudgetChangeLogList)
                    {
                        worksheet.Cell(row, 1).Value = projectName;
                        worksheet.Cell(row, 2).Value = projectBudgetChangeLog.CreateUser;
                        worksheet.Cell(row, 3).Value = projectBudgetChangeLog.WBSName;
                        worksheet.Cell(row, 4).Value = projectBudgetChangeLog.ChangeNumber;
                        worksheet.Cell(row, 5).Value = projectBudgetChangeLog.Date;
                        worksheet.Cell(row, 6).Value = projectBudgetChangeLog.Reason;
                        worksheet.Cell(row, 7).Value = projectBudgetChangeLog.Responsible;
                        worksheet.Cell(row, 8).Value = projectBudgetChangeLog.Status;
                        worksheet.Cell(row, 9).Value = projectBudgetChangeLog.Amount;
                        row++;
                    }
                }

                /*
                var p6CodeService = new P6ProjectCodeService(P6WebServicesLogin);

                var projectCodeValues = p6CodeService.GetProjectCodeTypes();

                var p6UdfTypeService = new P6UdfTypeService(P6WebServicesLogin);

                var udfTypes = p6UdfTypeService.GetUdfTypesAll();

                foreach (var udfType in udfTypes)
                {
                    Console.WriteLine(String.Format("{0} - {1} - {2}",udfType.Title, udfType.SubjectArea,udfType.DataType ));
                }

                var udfCodeService = new P6UdfCodeService(P6WebServicesLogin);

                var udfCodeValues = udfCodeService.GetUdfCodeTypes();
                */
                P6WebServicesLogin.Logout();
                workbook.SaveAs("ProjectBudgets.xlsx");

            }
            catch (Exception ex)
            {
                NLogger.Fatal(ex.Message);
                    Environment.Exit(1);
            }
        }

    }
}
