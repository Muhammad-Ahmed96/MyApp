using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            UserDTO dto = new UserDTO();
            dto.login = "ahmed.pucit01@gmail.com";
            dto.password = "12345";
            BAL.UserBAL.Validate(dto.login, dto.password);
        }
    }
}
