using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMR_szoftech
{
    class Admin: Recepcionist
    {
        public Admin(string userName, string name, string rank): base(userName, name, rank) {}

        public void login()
        {
            adminMenu();
        }

        public void adminMenu()
        {
            
        }
    }
}
