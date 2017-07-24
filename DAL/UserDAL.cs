using Entities;
using PMS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace DAL
{
    public static class UserDAL
    {
        public static int Save(UserDTO dto)
        {
            ArrayList list=new ArrayList();
            string query = "Add_User";
            list.Add("@login");
            list.Add(dto.login);
            list.Add("@password");
            list.Add(dto.password);
            list.Add("@code");
            list.Add(dto.code_for_reset);
            using(DBHelper helper=new DBHelper())
            {
                return helper.ExecuteQuery(query,list);
            }
        }
        public static bool isEmailExist(string email)
        {
            string query = "isEmailExists";
            ArrayList list = new ArrayList();
            list.Add("@email");
            list.Add(email);
            using (DBHelper helper = new DBHelper())
            {
                var reader = helper.ExecuteReader(query, list);
                return reader.Read();
            }
        }
        public static bool Validate(string login,string pass)
        {
            string query="Validate_User";
            ArrayList list = new ArrayList();
            list.Add("@login");
            list.Add(login);
            list.Add("@password");
            list.Add(pass);
            using(DBHelper helper=new DBHelper())
            {
                var reader=helper.ExecuteReader(query,list);
                return reader.Read();
            }
        }
        public static int Add_In_History(int uid,DateTime datetime)
        {
            string query = "Add_In_History";
            ArrayList list = new ArrayList();
            list.Add("@id");
            list.Add(uid);
            list.Add("@time");
            list.Add(datetime);
            using(DBHelper helper=new DBHelper())
            {
                return helper.ExecuteQuery(query, list);
            }
        }
        public static int get_user_id(string email, string password)
        {
            string query = "Get_User_Id";
            ArrayList list = new ArrayList();
            list.Add("@email");
            list.Add(email);
            list.Add("@password");
            list.Add(password);
            using (DBHelper helper = new DBHelper())
            {
                var reader = helper.ExecuteReader(query, list);
                if (reader.Read())
                   return  reader.GetInt32(0);
                return -1;
            }
        }
        public static bool VerifyEmail(string email)
        {
            ArrayList list = new ArrayList();
            string query = "VerifyEmail";
            list.Add("@email");
            list.Add(email);
            using (DBHelper helper = new DBHelper())
            {
                var reader = helper.ExecuteReader(query, list);
                if (reader.Read())
                    return true;
                return false;
            }
        }
        public static int Update_Reset_Code(string email,string code)
        {
            string query = "Update_Reset_Code";
            ArrayList list = new ArrayList();
            list.Add("@email");
            list.Add(email);
            list.Add("@code");
            list.Add(code);
            using (DBHelper helper = new DBHelper())
            {
                return helper.ExecuteQuery(query, list);
            }
        }

        public static bool Verify_Code(string email,string code)
        {
            string query = "Verify_Code";
            ArrayList list = new ArrayList();
            list.Add("@email");
            list.Add(email);
            list.Add("@code");
            list.Add(code);

            using(DBHelper helper=new DBHelper())
            {
                var reader = helper.ExecuteReader(query, list);
                if (reader.Read())
                    return true;
                return false;
            }
        }
        public static int Update_Password(string email,string password)
        {
            string query = "Update_Password";
            ArrayList list = new ArrayList();
            list.Add("@email");
            list.Add(email);
            list.Add("@password");
            list.Add(password);

            using(DBHelper helper=new DBHelper())
            {
                return helper.ExecuteQuery(query, list);
            }
        }
    }
}
