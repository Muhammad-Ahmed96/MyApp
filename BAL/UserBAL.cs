using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BAL
{
    public static class UserBAL
    {
        public static int Save(UserDTO dto)
        {
            return DAL.UserDAL.Save(dto);
        }
        public static bool Validate(string login, string pass)
        {
            return DAL.UserDAL.Validate(login, pass);
        }
        public static bool isEmailExist(string email)
        {
            return DAL.UserDAL.isEmailExist(email);
        }
        public static int Add_In_History(int uid, DateTime datetime)
        {
            return DAL.UserDAL.Add_In_History(uid, datetime);
        }
        public static int get_user_id(string email, string password)
        {
            return DAL.UserDAL.get_user_id(email, password);
        }
        public static bool VerifyEmail(string email)
        {
            return DAL.UserDAL.VerifyEmail(email);
        }
        public static int Update_Reset_Code(string email, string code)
        {
            return DAL.UserDAL.Update_Reset_Code(email,code);
        }
        public static bool Verify_Code(string email, string code)
        {
            return DAL.UserDAL.Verify_Code(email, code);
        }
        public static int Update_Password(string email, string password)
        {
            return DAL.UserDAL.Update_Password(email, password);
        }
    }
}
