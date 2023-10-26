using AuthenticationDAL;
using AuthenticationDAL.DTO;
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
        public BODataProcessResult Create(Application data)
        {
            BODataProcessResult processResult = new BODataProcessResult();
            ApplicationUI applicationUI = ConvertToData(data);
            processResult.OK= applicationDataPortal.Create(applicationUI) >0;
            return processResult;
        }

        private ApplicationUI ConvertToData(Application data)
        {
            throw new NotImplementedException();
        }

        public BODataProcessResult Update(Application data)
        {
            return new BODataProcessResult();
        }
        public BODataProcessResult Delete(Application data)
        {
            return new BODataProcessResult();
        }
        public BODataProcessResult MarkDelete(Application data)
        {
            return new BODataProcessResult();
        }
    }
}
