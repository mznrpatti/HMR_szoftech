using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMR_szoftech
{
    class Manager: Recepcionist
    {
        public Manager(string userName, string name, string rank): base(userName, name, rank) {}

        public void managerMenu()
        {
            Console.Clear();
            Console.WriteLine($"{this.getName()} főoldala");
            string option;
            do
            {
                Console.WriteLine("Kérlek válassz a lehetőségek közül (1/2/3/4/5/6):");
                Console.WriteLine("1. Új csomag felvitele");
                Console.WriteLine("2. Elérhető csomagok listázása");
                Console.WriteLine("3. Foglalás törlése");
                Console.WriteLine("4. Foglalások szűrése igazolványszám szerint");
                Console.WriteLine("5. Alapadatok megtekintése");
                Console.WriteLine("6. Kijelentkezés");
                option = Convert.ToString(Console.ReadLine());

            } while (option != "1" && option != "2" && option != "3" && option != "4" && option != "5" && option != "6");
            switch (option)
            {
                case "1": break;
                case "2": PackageContainer.listPackages(); back(); break;
                case "3": break;
                case "4": listReservation(); back(); break;
                case "5": showBasicDatas(); back(); break;
                case "6": logout(); break;
            }
        }

        public void login()
        {
            managerMenu();
        }

        private void back()
        {
            Console.Write("Nyomja meg az <Enter>-t a visszalépéshez!");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { };
            Console.Clear();
            recepcionistMenu();
        }

    }
}
