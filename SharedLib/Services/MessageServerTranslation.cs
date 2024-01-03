using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharedLib.Services
{
    public class MessageServerTranslation
    {
        public MessageServerTranslation(string jsonfilePath)
        {
            ErrorCodeDatas = BuildDataSourceFromJson(jsonfilePath);
        }
        public List<ServerErrorData> ErrorCodeDatas { get; set; }

        /// <summary>
        /// chua dung toi
        /// </summary>
        /// <param name="jsonfilePath"></param>
        public void GetErrorCodeDataSource(string jsonfilePath)
        {
            ErrorCodeDatas = BuildDataSourceFromJson(jsonfilePath);
        }

        private List<ServerErrorData> BuildDataSourceFromJson(string fileName)
        {
            try
            {
                JsonUtil<AllServerErrorData> jsonUtilCardCode = new JsonUtil<AllServerErrorData>();
                AllServerErrorData data = jsonUtilCardCode.Read(fileName);

                return data.ErrorDatas;

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                throw;
            }

        }

        public List<ServerErrorData> GetErrorDatas()
        {
            return ErrorCodeDatas;
        }
        public class AllServerErrorData
        {
            public List<ServerErrorData> ErrorDatas { get; set; }
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
            ServerErrorData errorData = ErrorCodeDatas.Where(x => x.ErrorCode == errorCode).FirstOrDefault();
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
