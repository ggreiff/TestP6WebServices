using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace TestP6WebServices.P6Api
{
    public class P6ActivityExpenseService
    {
        private readonly ActivityExpenseService _activityExpenseService;

        public String Filter { get; set; }
        public ActivityExpenseService ActivityExpenseServiceP6 { get { return _activityExpenseService; } }

        public P6ActivityExpenseService(P6AuthenticationService authenticationService)
        {
            Filter = String.Empty;
            _activityExpenseService = new ActivityExpenseService
            {
                CookieContainer = authenticationService.CookieContainer,
                Url = authenticationService.AuthService.Url.Replace("AuthenticationService", "ActivityExpenseService")
            };
        }

        public List<ActivityExpense> GetActivityExpenses(String expenseFilter)
        {
            var readActivityExpenses = DefaultFields();
            readActivityExpenses.Filter = expenseFilter;
            var expenses = GetActivityExpenses(readActivityExpenses);
            return expenses.HasItems() ? expenses : null;
        }

        public List<ActivityExpense> GetActivityExpenses(int activityObjectId)
        {
            var readActivityExpenses = DefaultFields();
            readActivityExpenses.Filter = String.Format("ActivityObjectId = {0}", activityObjectId.ToString(CultureInfo.InvariantCulture));
            var expenses = GetActivityExpenses(readActivityExpenses);
            return expenses.HasItems() ? expenses : null;
        }

        public List<ActivityExpense> GetActivityExpenses(ReadActivityExpenses readActivityExpenses)
        {
            return _activityExpenseService.ReadActivityExpenses(readActivityExpenses).ToList();
        }

        public void DeleteActivityExpenses(ActivityExpense activityExpense)
        {
            var activtiyExpenseId = new List<ActivityExpense> { activityExpense };
            DeleteActivityExpenses(activtiyExpenseId);
        }

        public void DeleteActivityExpenses(List<ActivityExpense> activityExpenseList)
        {
            if (!activityExpenseList.HasItems()) return;

            var activityExpenseIdList = new List<Int32>();

            var i = 0;
            foreach (var activtiyExpense in activityExpenseList)
            {
                activityExpenseIdList.Add(activtiyExpense.ObjectId);
                if (i++ < 100) continue;

                _activityExpenseService.DeleteActivityExpenses(activityExpenseIdList.ToArray());
                activityExpenseIdList.Clear();
                i = 0;
            }

            if (activityExpenseIdList.HasItems())
                _activityExpenseService.DeleteActivityExpenses(activityExpenseIdList.ToArray());
        }


        public UpdateActivityExpensesResponse UpdateActivityExpense(ActivityExpense activityExpense)
        {
            var activityExpenses = new[] {activityExpense};
            var retVal =  _activityExpenseService.UpdateActivityExpenses(activityExpenses);
            return retVal;
        }

        public ReadActivityExpenses DefaultFields()
        {
            var fields = new List<ActivityExpenseFieldType>
            {
                ActivityExpenseFieldType.ObjectId,
                ActivityExpenseFieldType.ActivityObjectId,
                ActivityExpenseFieldType.DocumentNumber,
                ActivityExpenseFieldType.ExpenseItem,
                ActivityExpenseFieldType.ExpenseDescription,
                ActivityExpenseFieldType.Vendor,
                ActivityExpenseFieldType.ActualCost
            };
            var defaultFields = new ReadActivityExpenses {Field = fields.ToArray(),OrderBy = String.Empty};
            return defaultFields;
        }
    }
}
