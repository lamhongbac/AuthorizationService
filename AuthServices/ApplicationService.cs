using AuthenticationDAL;
using AuthenticationDAL.DTO;
using AuthorizationService.BaseObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthServices
{
    public class ApplicationService
    {
        ApplicationDataPortal applicationDataPortal;
        public ApplicationService(ApplicationDataPortal applicationDataPortal)
        {
            this.applicationDataPortal = applicationDataPortal;


        }
        public BODataProcessResult Create(BaseApplication data)
        {
            BODataProcessResult processResult = new BODataProcessResult();
            ApplicationUI applicationUI = ConvertToData(data);
            processResult.OK= applicationDataPortal.Create(applicationUI) >0;
            return processResult;
        }

        private ApplicationUI ConvertToData(BaseApplication data)
        {
            throw new NotImplementedException();
        }

        public BODataProcessResult Update(BaseApplication data)
        {
            return new BODataProcessResult();
        }
        public BODataProcessResult Delete(BaseApplication data)
        {
            return new BODataProcessResult();
        }
        public BODataProcessResult MarkDelete(BaseApplication data)
        {
            return new BODataProcessResult();
        }
    }
}
