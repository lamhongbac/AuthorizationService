using AuthServices.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthSharedLib.Utils
{
    /// <summary>
    /// muc tieu la luu 1 so int thay cho 
    /// </summary>
    public class ObjectRightBinaryOperation
    {
        /// <summary>
        /// tra ve kq la co the Read,Edit???
        /// thong qua number la tong cac quyen
        /// </summary>
        /// <param name="checkNumber"> la so can kiem tra khi can </param>
        /// <param name="storedNumber"> la so luu trong CSDL</param>
        /// <param name="totalCheck"> la so tong so dung de kiem tra can cu vao enum</param>
        /// <returns></returns>
        public bool CanDo(int totalCheck, int storeNumber, int checkNumber)
        {
            return false;
        }

    }
}
