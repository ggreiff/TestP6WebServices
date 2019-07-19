using System;
using System.Collections.Generic;
using System.Linq;

namespace TestP6WebServices.P6Api
{
    public class P6SpreadService
    {
        public SpreadService SpreadService { get; set; }

        public P6SpreadService(P6AuthenticationService authenticationService)
        {
            SpreadService = new SpreadService
                                {
                                    CookieContainer = authenticationService.CookieContainer,
                                    Url = authenticationService.AuthService.Url.Replace("AuthenticationService", "SpreadService")
                                };
        }

        public List<ReadProjectSpreadResponseProjectSpread> ReadProjectSpreads(List<Int32> objectIdList, DateTime? startDate, DateTime? endDate,
                    SummarizedSpreadPeriodType periodType, List<SummarizedSpreadFieldType> fieldsToSpeadList)
        {
            var retVal = new List<ReadProjectSpreadResponseProjectSpread>();

            if (!(objectIdList.HasItems() && fieldsToSpeadList.HasItems() && startDate.HasValue && endDate.HasValue)) return new List<ReadProjectSpreadResponseProjectSpread>();
            if (startDate.Value >= endDate.Value) return retVal;

                var readProjectSpread = new ReadProjectSpread
                                            {
                                                StartDate = startDate.Value,
                                                EndDate = endDate.Value,
                                                PeriodType = periodType,
                                                IncludeCumulative = false,
                                                SpreadField = fieldsToSpeadList.ToArray(),
                                                ProjectObjectId = objectIdList.ToArray()
                                            };

                retVal = SpreadService.ReadProjectSpread(readProjectSpread).ToList();

            return retVal;
        }

        public List<ReadResourceAssignmentSpreadResponseResourceAssignmentSpread> ReadResourceAssignmentSpreads(List<Int32> objectIdList, DateTime? startDate, DateTime? endDate,
                    SpreadPeriodType periodType, List<ResourceAssignmentSpreadFieldType> fieldsToSpeadList)
        {
            var retVal = new List<ReadResourceAssignmentSpreadResponseResourceAssignmentSpread>();

            if (!(objectIdList.HasItems() && fieldsToSpeadList.HasItems() && startDate.HasValue && endDate.HasValue)) return new List<ReadResourceAssignmentSpreadResponseResourceAssignmentSpread>();
            if (startDate.Value >= endDate.Value) return retVal;

            var readWbsSpread = new ReadResourceAssignmentSpread
            {
               StartDate= startDate.Value,
                EndDate = endDate.Value,
                PeriodType = periodType,
                IncludeCumulative = false,
                SpreadField = fieldsToSpeadList.ToArray(),
               ResourceAssignmentObjectId = objectIdList.ToArray()
            };

            return SpreadService.ReadResourceAssignmentSpread(readWbsSpread).ToList();
        }

        public List<ReadWBSSpreadResponseWBSSpread> ReadWbsSpreads(List<Int32> objectIdList, DateTime? startDate, DateTime? endDate,
            SummarizedSpreadPeriodType periodType, List<SummarizedSpreadFieldType> fieldsToSpeadList)
        {
            var retVal = new List<ReadWBSSpreadResponseWBSSpread>();

            if (!(objectIdList.HasItems() && fieldsToSpeadList.HasItems() && startDate.HasValue && endDate.HasValue)) return new List<ReadWBSSpreadResponseWBSSpread>();
            if (startDate.Value >= endDate.Value) return retVal;

            var readWbsSpread = new ReadWBSSpread
            {
                StartDate = startDate.Value,
                EndDate = endDate.Value,
                PeriodType = periodType,
                IncludeCumulative = false,
                SpreadField = fieldsToSpeadList.ToArray(),
                WBSObjectId = objectIdList.ToArray()
            };

            return SpreadService.ReadWBSSpread(readWbsSpread).ToList();
        }

         public List<ReadActivitySpreadResponseActivitySpread> ReadActivitySpreads(Int32 objectId, DateTime? startDate, DateTime? endDate,
                    SpreadPeriodType periodType, List<ActivitySpreadFieldType> fieldsToSpeadList)
         {
             return ReadActivitySpreads(new List<Int32> {objectId}, startDate, endDate, periodType, fieldsToSpeadList);
         }

        public List<ReadActivitySpreadResponseActivitySpread> ReadActivitySpreads(List<Int32> objectIdList, DateTime? startDate, DateTime? endDate,
                    SpreadPeriodType periodType, List<ActivitySpreadFieldType> fieldsToSpeadList)
        {
            var retVal = new List<ReadActivitySpreadResponseActivitySpread>();

            if (!(objectIdList.HasItems() && fieldsToSpeadList.HasItems() && startDate.HasValue && endDate.HasValue)) return new List<ReadActivitySpreadResponseActivitySpread>();
            if (startDate.Value >= endDate.Value) return retVal;

            var readActivitySpread = new ReadActivitySpread
            {
                StartDate = startDate.Value,
                StartDateSpecified = true,
                EndDate = endDate.Value,
                EndDateSpecified = true,
                PeriodType = periodType,
                IncludeCumulative = false,
                SpreadField = fieldsToSpeadList.ToArray(),
                ActivityObjectId = objectIdList.ToArray()
            };

            return SpreadService.ReadActivitySpread(readActivitySpread).ToList();
        }
    }
}
