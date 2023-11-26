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
                case "1": addNewPackage();break;
                case "2": PackageContainer.listPackages(); back(); break;
                case "3": deleteReservation(); break;
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
            managerMenu();
        }

        public void addNewPackage()
        {
            Console.Clear();
            string roomType = "";
            int numberOfGuests = 0;
            int packagePrice = 0;
            string startDate = "";
            string endDate = "";
            while (roomType=="" || startDate=="" || endDate=="")
            {
                Console.WriteLine("A csomaghoz tartozó adatok: ");
                Console.Write("Szoba típusa: ");
                roomType = Console.ReadLine();
                numberOfGuests = 0;
                while (numberOfGuests == 0)
                {
                    Console.Write("Vendégek száma: ");
                    try
                    {
                        numberOfGuests = Convert.ToInt32(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("A megadott adat nem szám, vagy egyenlő 0-val!");
                    }

                }
                packagePrice = 0;
                while (packagePrice == 0)
                {
                    Console.Write("Csomag ára: ");
                    try
                    {
                        packagePrice = Convert.ToInt32(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("A megadott adat nem szám, vagy 0!");
                    }

                }
                Console.Write("Kezdeti dátum: ");
                startDate = Console.ReadLine();
                Console.Write("Végdátum: ");
                endDate = Console.ReadLine();

                if (roomType == "" || startDate == "" || endDate == "")
                    Console.WriteLine("Valamelyik adatot nem töltötte ki helyesen!");
            }

            Package newPackage = new Package(roomType, numberOfGuests, packagePrice, startDate, endDate);
            PackageContainer.addPackage(newPackage);
            PackageContainer.writePackages();
            Console.Clear();
            Console.WriteLine("Csomag felvíve az adatbázisba!");
            System.Threading.Thread.Sleep(3000);
            managerMenu();
        }

        public void deleteReservation()
        {
            Console.Clear();
            Console.WriteLine("Foglalások: ");
            ReservationContainer.listReservations();
            Console.Write("Adja meg a törölni kívánt foglalás számát: ");
            int option = 0;
            do
            {
                option = Convert.ToInt32(Console.ReadLine());
            } while (!(option >= 1 && option <= ReservationContainer.numberOfReservations()));
            ReservationContainer.deleteReservation(option-1);
            ReservationContainer.writeReservations();
            Console.Clear();
            Console.WriteLine("Foglalás sikeresen törölve!");
            System.Threading.Thread.Sleep(3000);
            managerMenu();
        }
    }
}
