using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BAL;
using System.Security.Cryptography;
using System.Text;
using Entities;
using Assignment6_.Models;

namespace Assignment6_.Controllers
{
    public class UserController : ApiController
    {
        [HttpPost]
        public Object Validate(UserDTO dto)
        {
            Object data = null;
            var flag = false;
            var url = "";
            string status = "";
            try
            {

                var obj = BAL.UserBAL.Validate(dto.login, System.Text.Encoding.Default.GetString(CalculateHash(dto.password)));

                if (obj == true)
                {
                    int id = BAL.UserBAL.get_user_id(dto.login, System.Text.Encoding.Default.GetString(CalculateHash(dto.password))); //getting user id 

                    int row_inserted = BAL.UserBAL.Add_In_History(id, DateTime.Now); //inserting in Login History

                    if (row_inserted <= 0)
                    {                                      //record not inserted in History so returing null;
                        data = new  
                        {
                            valid = false,
                            urlToRedirect = "",
                            message = "Email Does not Exist"
                        };
                    }

                    flag = true;
                    url = "/Home/SignIn";
                    status = "Sign In Successfull";
                }
                else
                {
                    flag = false;
                    url = "";
                    status = "Invalid Email/Password";
                }
            }
            catch (Exception)
            {
                data = new
                {
                    valid = false,
                    urlToRedirect = "",
                    message = ""
                };
            }
            data = new
            {
                valid = flag,
                urlToRedirect = url,
                message = status
            };
            return data;
        }
        private static byte[] CalculateHash(string inputString)
        {
            HashAlgorithm algorithm = MD5.Create();  //or use SHA256.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }
        public Object Save(UserDTO info)
        {
            Object data = null;
            try
            {
                var url = "";
                var flag = false;
                string status = "Invalid Email/Password";
                if (BAL.UserBAL.isEmailExist(info.login))
                {
                    data = new
                    {
                        message = "Account with this Email Already Exists",
                        valid = false,
                        urlToRedirect = ""

                    };
                }
                else
                {
                    UserDTO dto = new UserDTO();
                    dto.login = info.login;
                    dto.password = System.Text.Encoding.Default.GetString(CalculateHash(info.password));

                    var obj = BAL.UserBAL.Save(dto);
                    if (obj != 0)
                    {
                        flag = true;
                        status = "Account Created Successfully";
                        url = "/Home/SignUp";
                    }
                    data = new
                    {
                        message = status,
                        valid = flag,
                        urlToRedirect = url
                    };
                }
            }
            catch (Exception)
            {
                data = new
                {
                    message = "",
                    valid = false,
                    urlToRedirect = ""
                };
            }
            return data;
        }

        public Object UpdatePassword(UserDTO dto)
        {            
            //string email = "";
            Object data = null;

            bool Emailflag = BAL.UserBAL.VerifyEmail(dto.login);
            if (!Emailflag)
            {
                return data = new
                {
                    valid = false,
                    urlToRedirect = "",
                    message = "Invalid Token ! Try Again"
                };
            }
            bool CodeFlag = BAL.UserBAL.Verify_Code(dto.login, dto.code_for_reset);
            if(!CodeFlag)
            {
                return data = new
                {
                    valid = false,
                    urlToRedirect = "",
                    message = "Invalid Token ! Try Again"
                };
            }
           
             dto.password = System.Text.Encoding.Default.GetString(CalculateHash(dto.password));
            int rowsEffected = BAL.UserBAL.Update_Password(dto.login, dto.password);
            int Code_NULL = BAL.UserBAL.Update_Reset_Code(dto.login, null);
            try
            {
                bool flag = false;
                var msg = "";
                var url = "";

                if (rowsEffected > 0)
                {
                    flag = true;
                    url = "/Home/SignIn";
                    msg = "Password Successfully Changed";
                }
                else
                {
                    flag = false;
                    url = "";
                    msg = "Error Changing Password";
                }
                data = new
                {
                    valid = flag,
                    urlToRedirect = url,
                    message = msg
                };
            }
            catch (Exception)
            {
                data = new
                {
                    valid = false,
                    urlToRedirect = "",
                    message = "Error"
                };
            }
            return data;
        }
        public Object VerifyEmail(UserDTO dto)
        {
            bool email_flag = BAL.UserBAL.VerifyEmail(dto.login);
            Object data = null;
            try
            {
                bool flag = false;
                var url = "";
                if (email_flag)
                {
                    Guid g = Guid.NewGuid();
                    string code = g.ToString();

                    int rowEffected = BAL.UserBAL.Update_Reset_Code(dto.login, code);
                    if (rowEffected <= 0){
                        return data = new
                        {
                            valid = false,
                            message="Your Password Cannot be reset This Time....Try Later"
                        };
                    }
                    bool email_status = EmailHandler.EmailHandler.SendEmail(dto.login, "Reset Password", "http://assignment6.apphb.com/Home/NewPassword/?login=" + dto.login + "&code_for_reset=" + code);
                    if (email_status == false)
                    {
                        return data = new
                        {
                            valid = false,                            
                            message="Recovery Email Not Sent"
                        };
                    }
                    else
                    {
                        data = new
                        {
                            valid = true,
                            message="A Recovery Email is sent"
                        };
                    }

                    
                }
                else
                {
                    data = new
                    {
                        valid = false,
                        message = "Email Does Not Exist"
                    };
                }
            }
            catch (Exception)
            {
                data = new
                {
                    valid = false,
                    message="Error Occured"
                };
            }
            return data;
        }

        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public Object VerifyCode([FromUri]UserDTO dto)
        {
            Object data = null;

            var url = "";
            var flag = false;
            flag = BAL.UserBAL.Verify_Code(dto.login, dto.code_for_reset);
            if (flag == true)
            {
                return Redirect("http://assignment6.apphb.com/Home/NewPassword");
                //data = new
                //{
                //    url="""/Home/NewPassword"),
                //    valid=flag
                //};
            }
            else
            {
                return Redirect("/Home/InvalidCode");
                //data = new
                //{
                //    url = """/Home/InvalidCode"),
                //    valid = flag
                //};
            }
        }
    }
}
