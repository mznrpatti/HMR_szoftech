using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMR_szoftech
{
    internal class User
    {
        private string userName;
        private string name;
        private string rank;

        public User(string userName, string name, string rank)
        {
            this.userName=userName;
            this.name=name;
            this.rank=rank;
        }

        public void showBasicDatas()
        {
            Console.Clear();
            Console.WriteLine("Ezek az alapadatok!");
        }

        public void login()
        {
            //TODO
        }

        public void logout()
        {
            //TODO
        }

        public string getUserName()
        {
            return userName;
        }

        public string getName()
        {
            return name;
        }

        public string getRank()
        {
            return rank;
        }

        public void changePassword()
        {
            //TODO
        }
    }
}
