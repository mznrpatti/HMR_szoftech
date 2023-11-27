using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMR_szoftech
{
    abstract class User
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
            Console.WriteLine("Üdvözöljük a Hotel oldalán!");
            Console.WriteLine("Hotelünk általános információit az alábbiakban olvashatja:\n");
            Console.WriteLine("Helyszín:\n\tBudapest, Hess András tér 1-3, 1014");
            Console.WriteLine("Hotel kapacitása: \n\t150 fő");
            Console.WriteLine("Földszinten található: \n\telőtér\n\ttársalgó\n\trecepció");
            Console.WriteLine("Első szinten található:\n\tcsaládi szobák");
            Console.WriteLine("Második szinten található:\n\tegyágyas szobák");
            Console.WriteLine("Harmadik szinten található:\n\tfranciaágyas szobák");
            Console.WriteLine("\nMinden szoba egy fürdőszobával és egy külön helységben elhelyezett illemhellyel van felszerelve.");
            Console.WriteLine("Elérhetőségek:\n\ttelefonszám: +36/30/204/3933\n\temail-cím: besthotel@gmail.com");
            Console.WriteLine("Szerettel várjuk a pihenni vágyókat!\nSok szeretettel: a Hotel csapata!");
        }

        public void login() {}

        public void logout()
        {
            Console.Clear();
            Console.WriteLine("Várjuk vissza!");
            System.Threading.Thread.Sleep(3000);
            Program.begin();
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
