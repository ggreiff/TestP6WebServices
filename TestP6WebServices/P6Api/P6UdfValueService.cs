using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace TestP6WebServices.P6Api
{
    public class P6UdfValueService
    {
        private readonly UDFValueService _udfValueService;

        public P6UdfValueService(P6AuthenticationService authenticationService)
        {
            _udfValueService = new UDFValueService
            {
                CookieContainer = authenticationService.CookieContainer,
                Url = authenticationService.AuthService.Url.Replace("AuthenticationService", "UDFValueService")
            };
        }


        public List<UDFValue> GetUdfValues(int projectObjectId)
        {
            var defaultFields = DefaultFields();
            defaultFields.Filter = "ProjectObjectId = '" + projectObjectId.ToString(CultureInfo.InvariantCulture) + "'";
            return GetUdfValues(defaultFields);
        }

        public List<UDFValue> GetUdfValues(String filterString)
        {
            var readUdfValues = DefaultFields(filterString);
            return GetUdfValues(readUdfValues);
        }

        public List<UDFValue> GetUdfValues(ReadUDFValues readUdfValues)
        {
            var retVal = new List<UDFValue>(_udfValueService.ReadUDFValues(readUdfValues));
            return retVal;
        }

        public ReadUDFValues DefaultFields(String filter)
        {
            var defaultFields = DefaultFields();
            defaultFields.Filter = filter;
            return defaultFields;
        }

        public ReadUDFValues DefaultFields()
        {
            var defaultFields = new ReadUDFValues();
            var fields = new List<UDFValueFieldType>
                             {
                                 UDFValueFieldType.ProjectObjectId,
                                 UDFValueFieldType.ForeignObjectId,
                                 UDFValueFieldType.Description,
                                 UDFValueFieldType.FinishDate,
                                 UDFValueFieldType.StartDate,
                                 UDFValueFieldType.Integer,
                                 UDFValueFieldType.Text,
                                 UDFValueFieldType.Double,
                                 UDFValueFieldType.Cost,
                                 UDFValueFieldType.Indicator,
                                 UDFValueFieldType.UDFTypeSubjectArea,
                                 UDFValueFieldType.UDFTypeTitle,
                                 UDFValueFieldType.UDFTypeDataType
                             };
            defaultFields.Field = fields.ToArray();
            return defaultFields;
        }

        public UpdateUDFValuesResponse UpdateUdfValueText(int udfTypeObjectId, int foreignObjectId, String inputString )
        {
            var udfTextValue = new List<UDFValue>
                                   { new UDFValue {
                                               UDFTypeObjectId = udfTypeObjectId,
                                               ForeignObjectId = foreignObjectId,
                                               Text = inputString
                                           }
                                   };
           return _udfValueService.UpdateUDFValues(udfTextValue.ToArray());

        }

        public List<CreateUDFValuesResponseObjectId> CreateUdfValueText(int udfTypeObjectId, int foreignObjectId, String inputString)
        {
            var udfValues = new List<UDFValue>
                                   { new UDFValue {
                                               UDFTypeObjectId = udfTypeObjectId,
                                               ForeignObjectId = foreignObjectId,
                                               Text = inputString
                                               }
                                   };
            return _udfValueService.CreateUDFValues(udfValues.ToArray()).ToList();
        }

        public List<CreateUDFValuesResponseObjectId> CreateUdfValueFinishDate(int udfTypeObjectId, int foreignObjectId, DateTime finishDate)
        {
            //set:ForeignObjectId 
            var udfValues = new List<UDFValue>
                                   { new UDFValue {
                                               UDFTypeObjectId = udfTypeObjectId,
                                               UDFTypeObjectIdSpecified = true,
                                               ForeignObjectId = foreignObjectId,
                                               ForeignObjectIdSpecified = true,
                                               UDFTypeSubjectArea = UDFValueUDFTypeSubjectArea.Activity,
                                               UDFTypeSubjectAreaSpecified = true,
                                               FinishDate = finishDate,
                                               FinishDateSpecified = true
                                           }
                                   };
            return _udfValueService.CreateUDFValues(udfValues.ToArray()).ToList();
        }
    }
}
