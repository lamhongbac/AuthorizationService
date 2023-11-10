using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace SharedLib.Services
{
    public class MessageTranslation
    {
        public MessageTranslation(string jsonfilePath)
        {
            ErrorCodeDatas = BuildDataSourceFromJson(jsonfilePath);
        }
        public List<ErrorData> ErrorCodeDatas { get; set; }

        /// <summary>
        /// chua dung toi
        /// </summary>
        /// <param name="jsonfilePath"></param>
        public void GetErrorCodeDataSource(string jsonfilePath)
        {
            ErrorCodeDatas = BuildDataSourceFromJson(jsonfilePath);
        }

        private List<ErrorData> BuildDataSourceFromJson(string fileName)
        {
            try
            {
                JsonUtil<AllErrorData> jsonUtilCardCode = new JsonUtil<AllErrorData>();
                AllErrorData data = jsonUtilCardCode.Read(fileName);

                return data.ErrorDatas;

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                throw;
            }

        }

        public List<ErrorData> GetErrorDatas()
        {
            return ErrorCodeDatas;
        }
        public class AllErrorData
        {
            public List<ErrorData> ErrorDatas { get; set; }
        }
        public string Translate(string errorCode, string language)
        {
            if (string.IsNullOrWhiteSpace(errorCode))
            {
                return "OK";
            }
            if (string.IsNullOrWhiteSpace(language))
            {
                language = ELang.Vn.ToString();
            }
            ErrorData errorData = ErrorCodeDatas.Where(x => x.ErrorCode == errorCode).FirstOrDefault();
            if (errorData == null)
            {

                return errorCode.Replace("_", " ");



            }
            else
            {
                if (language.ToUpper() == ELang.En.ToString().ToUpper())
                {
                    return errorData.EN;
                }
                else
                {
                    return errorData.VN;
                }
            }
        }
    }
}
