using MSASharedLib.DataTypes;
using Newtonsoft.Json;
using System;
using System.IO;

namespace MSASharedLib.Utils
{
    public class CommonDataTypes
    {

    }

    //public class ErrorData
    //{
    //    public string ErrorCode { get; set; }
    //    public string EN { get; set; }
    //    public string VN { get; set; }
    //    public string DESC { get; set; }
    //}

    public class ServerErrorData:ErrorData
    {
        
    }

    //public enum ELang
    //{
    //    En,
    //    Vn
    //}

    public enum EDepartmentType
    {
        QA,
        IT,
        R
    }

    //public class JsonUtil<T>
    //{
    //    public T Read(string _filePath)
    //    {
    //        string text = File.ReadAllText(_filePath);
    //        return JsonConvert.DeserializeObject<T>(File.ReadAllText(_filePath));
    //    }
    //    public void Write(T model, string _filePath)
    //    {
    //        File.WriteAllText(_filePath, JsonConvert.SerializeObject(model));
    //    }
    //}

    public enum AppUserType
    {
        UserName,
        Email,
        MobileNo
    }
    //public enum JwtStatus
    //{
    //    InvalidToken,
    //    TokenIsNotExpired,
    //    TokenIsNotExist,
    //    TokenIsUsed,
    //    IsRevoked,
    //    AccessTokenIdIsNotMatch,
    //    Success,
    //    BadRequest
    //}

    public enum EProcessStatus
    {
        QAOCheck,
        RMConfirm,
        QAMReview
    }

    //public enum ESortOrder { Ascending = 0, Descending = 1 }

    public enum ELogicErrorCode
    {
        Can_Not_Find_Any_Roles,
        Update_Data_Fail,
        Update_Data_Success,
        Invalid_Data, //data gui xuong DB bi thieu hay sai
        Data_Not_Found,//kg tim thay data
        NullValue_Is_Not_Allow,//kg cho phep Null Or Empty Data
        Exist_Key_Data, //check duplicate Number key
        Not_Enough_Right,
        Data_Is_Not_Valid, // data sai 
        MemberClass_Is_Completed_Init,
        Missing_Credential,
        Can_Not_Insert_MemberClass_Middle_Order,
        Can_Not_Find_Last_Item,
        Insert_Data_Fail,
        Insert_Data_Success,
        News_Outlet_Exist,
        Delete_Data_Success,
        Delete_Data_Fail,
        Missing_Image,
        Upload_Image_Fail,
        Must_Have_CanRead,
        Import_Excel_Not_Correct_Template,
        Not_Have_File_Import,
        Excel_File_Not_Correct_Format,
        File_Member_Length_Limit,
        Email_Exist,
        Mobile_Exist,
        Future_DOB,
        Missing_Video,
        Upload_Video_Fail,
        Invalid_Priority,
        Invalid_RefLink,
        Invalid_Website,
        Invalid_Email,
        Can_Not_Find_Result,
        Missing_Subject_En,
        Missing_Subject_Vn,
        Missing_Description_En,
        Missing_Description_Vn,
        Missing_Content_En,
        Missing_Content_Vn,
        Invalid_Period,
        Content_Vn_Max_Length_600,
        Content_En_Max_Length_600,
        News_Exists_In_Priority,
        Missing_Appoval_Note,
        Need_Input_FromPoint,
        ToPoint_Need_Greater,
        Not_Set_Range_Current_Highest_Rank,
        Range_Of_This_Rank,
        PreToPoint_Not_Same_CurFromPoint,
        NextFromPoint_Not_Same_CurToPoint,
        Number_Exist,
        Missing_Required_Field,
        Import_Data_Higher_100,
        No_Data_Import,
        Note_Suspend_Require,
        Suspend_Fail,
        Suspend_Success,
        Not_Check_Change_Pass,
        OTPCOde_Is_Empty,
        OTPCOde_Not_Valid,
        OTPCOde_Valid,
        Send_OTP_Success,
        Send_OTP_Fail,
        Cannot_find_Salt,
        Can_Not_Find_This_Member,
        Update_NewPass_Success,
        Update_NewPass_Fail,
        Send_NewPass_Fail,
        Send_NewPass_Success,
        Mobile_Empty,
        Not_Check_Change_Pass_And_Note,
        Update_Reload_Data_Success,
        Update_Reload_Data_Fail,

        Mobiles_Or_Email_Not_Valid,
        Import_Member_Success,
        Choose_Correct_Year,
        Mobile_NotValid,
        Email_NotValid,
        Mobile_NotVerify,
        DOB_NotValid,
        Member_Not_Found,
        Suspend_Info_Not_Found,
        Active_Member_Fail,
        Active_Member_Success,
        Fullname_NotValid,
        Note_Active_Require,
        Note_Limit_Length,
        Mobile_Duplicate_In_File,
        Email_Duplicate_In_File,
        Import_Field_Empty,
        City_Name_Not_Valid,
        District_Name_Not_Valid,
        Gender_Not_Valid,
        Check_Error_Col,
        Hotkey_is_Empty,
        Hotkey_Data_Not_Found,
        SearchHotkey_Data_Not_Found,
        Limit_Send_OTP,
        Source_Member_Not_Found,
        Dest_Member_Not_Found,
        Search_Key_Is_Not_Valid,
        Merge_Member_Success,
        Merge_Member_Fail,
        Thiso_Email_Is_Not_Valid,
        Thiso_Email_Is_Valid,
        Member_Update_Success,
        Member_Update_Fail,
        Member_Export_Not_Found,
        ImportMember_Export_Error_Success,
        ImportMember_Export_Error_Fail,
        Member_Import_Config_Success,
        Member_Import_Config_Fail,
        Invalid_Parametter,
        Missing_RankingFromPoint,
        Missing_RankingToPoint,
        Missing_RankingRate,
        Missing_RewardRate,
        Missing_MetaDateTime,
        Missing_CLP,
        Missing_Company,
        Missing_ClassNo,
        Miss_ClassName,
        Invalid_Ranking,
        Import_Member_Fail,
        Save_Import_Success,
        Member_Update_Special_Success,
        Member_Update_Special_Fail,
        Reset_Pass_Success,
        Reset_Pass_Fail,
        Old_Pass_Not_Correct,
        Save_Import_Fail,
        Update_User_Role_Success,
        Disable_User_Success,
        Disable_User_Fail,
        Apply_Group_Not_Selected,
        Maximun_Number_Of_AdsBanner,
        Duplicate_Alias,
        Update_Member_Config_Fail,
        Update_Member_Config_Success,
        Data_Is_Required,
        DOB_Is_Required,
        Invalid_DateTime,
        FullName_Is_Required,
        InvalidProperty,
        Password_Is_Required,
        Invalid_IDNo,
        Invalid_Mobile_Number,
        Invalid_FullName,
        UpdateProfile_Failed,
        Invalid_Mobile_No,
        Invalid_Mobile_No_Email,
        Invalid_OTPCode,
        Miss_Required_Fields,
        Password_Does_Not_Match,
        Register_Sucessfully,
        Login_Not_Success,
        Send_RecoverPassword_Failed,
        Miss_Mobile_No_Email_Fields,
        Miss_Pwd_Fields,
        Miss_FullName_Fields,
        Miss_Email_Fields,
        Miss_ShortAddress_Fields,
        Miss_SelectProvince_Fields,
        Miss_SelectDistrict_Fields,
        Miss_NewPwd_Fields,
        Suspended_Member,
        Registered_Newsleter,
        Invalid_Register_DOB,
        Captcha_Not_Verified,
        Import_Voucher_Success,
        Import_Voucher_Fail,
        Delete_Role_Fail,
        Invalid_City,
        Invalid_District,
        Invalid_Gender,
        Missing_Adjust_Qty,
        User_Pass_Require,
        Thiso_Account_Not_Correct,
        User_Pass_Not_Correct,
        Active_User_Success,
        Active_User_Fail,
        Address_Over_Limit_Length,
        Missing_Object_Right,
        Value_Is_Not_Empty,
        You_Have_No_Rights,
        Fullname_Is_Not_Empty,
        Vn_En_Is_Not_Empty,
        Pass_IS_Not_Valid,
        Confirm_Pass_Is_Empty,
        New_Pass_Is_Empty,
        ToPoint_Smaller,
        Missing_User_Name,
        Invalid_User_Name,
        Invalid_Max_Length,
        Member_Not_Login,
        Register_Failed,
        Register_NewsLetter_Failed,
        Fullname_Char_Limit,
        Email_Or_Mobile_Registered
    }
}
