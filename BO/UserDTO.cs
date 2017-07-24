using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class UserDTO
    {
        public string login { get; set; }

        public string password { get; set; }

        public string code_for_reset { get; set; }
    }
}
