using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace HMR_szoftech
{
    class Manager: Recepcionist
    {
        public Manager(string userName, string name, string rank): base(userName, name, rank) {}

        public void login()
        {
            managerMenu();
        }

        public void managerMenu()
        {
            Console.Clear();
            Console.WriteLine($"{this.getName()} főoldala");
            string option;
            do
            {
                Console.WriteLine("Kérlek válassz a lehetőségek közül (1/2/3/4/5/6/7):");
                Console.WriteLine("1. Új csomag felvitele");
                Console.WriteLine("2. Meglévő csomag törlése");
                Console.WriteLine("3. Elérhető csomagok listázása");
                Console.WriteLine("4. Foglalás törlése");
                Console.WriteLine("5. Foglalások szűrése igazolványszám szerint");
                Console.WriteLine("6. Alapadatok megtekintése");
                Console.WriteLine("7. Kijelentkezés");
                option = Convert.ToString(Console.ReadLine());

            } while (option != "1" && option != "2" && option != "3" && option != "4" && option != "5" && option != "6" && option != "7");
            switch (option)
            {
                case "1": addNewPackage();break;
                case "2": deletePackage();break;
                case "3": PackageContainer.listPackages(); back(); break;
                case "4": deleteReservation(); break;
                case "5": listReservation(); back(); break;
                case "6": showBasicDatas(); back(); break;
                case "7": logout(); break;
            }
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
            bool correctStartDate=true;
            bool correctEndDate=true;
            bool validDate = false;
            while (roomType=="" || startDate=="" || endDate=="" || validDate==false)
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
                do
                {
                    correctStartDate = true;
                    Console.Write("Kezdeti dátum (yyyy.mm.dd.): ");
                    startDate = Console.ReadLine();
                    if (startDate.Length == 11)
                    {
                        try
                        {
                            Convert.ToInt32(startDate.Substring(0, 4));
                            Convert.ToInt32(startDate.Substring(5, 2));
                            Convert.ToInt32(startDate.Substring(8, 2));
                        }
                        catch
                        {
                            Console.WriteLine("Kérjük adjon meg érvényes dátumot!");
                            correctStartDate = false;
                        }
                        if (startDate.Substring(4, 1) != "." || startDate.Substring(7, 1) != "." || startDate.Substring(10, 1) != ".")
                        {
                            correctStartDate = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Kérjük adjon meg érvényes dátumot!");
                        correctStartDate = false;
                    }
                        
                } while (!correctStartDate);
                do
                {
                    correctEndDate = true;
                    Console.Write("Végdátum (yyyy.mm.dd.): ");
                    endDate = Console.ReadLine();
                    if (endDate.Length == 11)
                    {
                        try
                        {
                            Convert.ToInt32(endDate.Substring(0, 4));
                            Convert.ToInt32(endDate.Substring(5, 2));
                            Convert.ToInt32(endDate.Substring(8, 2));
                        }
                        catch
                        {
                            Console.WriteLine("Kérjük adjon meg érvényes dátumot!");
                            correctEndDate = false;
                        }
                        if (endDate.Substring(4, 1) != "." || endDate.Substring(7, 1) != "." || endDate.Substring(10, 1) != ".")
                        {
                            correctEndDate = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Kérjük adjon meg érvényes dátumot!");
                        correctEndDate = false;
                    }

                } while (!correctEndDate);
                if (roomType == "" || startDate == "" || endDate == "")
                    Console.WriteLine("Valamelyik adatot nem töltötte ki helyesen!");
                validDate=dateValidator(startDate, endDate);
                if (!validDate)
                    Console.WriteLine("Érvénytelen dátumot adott meg!");
            }

            Package newPackage = new Package(roomType, numberOfGuests, packagePrice, startDate, endDate);
            PackageContainer.addPackage(newPackage);
            PackageContainer.writePackages();
            Console.Clear();
            Console.WriteLine("Csomag felvíve az adatbázisba!");
            System.Threading.Thread.Sleep(3000);
            managerMenu();
        }

        public void deletePackage()
        {
            PackageContainer.listPackages();
            int option = -1;
            do
            {
                try
                {
                    Console.Write("Adja meg a törölni kívánt csomag számát, vagy írjon 0-t a visszalépéshez: ");
                    option = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Érvényes számot adjon meg!");
                }
            } while (!(option >= 0 && option <= PackageContainer.numberOfPackages()));
            if (option == 0)
            {
                managerMenu();
            }
            else 
            {
                PackageContainer.deletePackage(option - 1);
                Console.Clear();
                Console.WriteLine("Csomag sikeresen törölve!");
                System.Threading.Thread.Sleep(3000);
                managerMenu();
            }
        }

        private bool dateValidator(string startDate, string endDate)
        {
            DateTime current=DateTime.Now;
            if (startDate.Length == 11 && endDate.Length == 11)
            {
                int startYear = Convert.ToInt32(startDate.Substring(0, 4));
                int startMonth = Convert.ToInt32(startDate.Substring(5, 2));
                int startDay = Convert.ToInt32(startDate.Substring(8, 2));
                int endYear = Convert.ToInt32(endDate.Substring(0, 4));
                int endMonth = Convert.ToInt32(endDate.Substring(5, 2));
                int endDay = Convert.ToInt32(endDate.Substring(8, 2));
                if(current.Year>startYear || (current.Year==startYear && current.Month>startMonth) || (current.Year==startYear && current.Month==startMonth && current.Day>startDay) || startYear>endYear || (startYear==endYear && startMonth>endMonth) ||(startYear==endYear && startMonth==endMonth && startDay > endDay))
                {
                    return false;
                }
                else
                    return true;
            }
            else return false;
        }

        public void deleteReservation()
        {
            Console.Clear();
            Console.WriteLine("Foglalások: ");
            ReservationContainer.listReservations();
            int option = -1;
            do
            {
                try
                {
                    Console.Write("Adja meg a törölni kívánt foglalás számát, vagy írjon 0-t a visszalépéshez: ");
                    option = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Érvényes számot adjon meg!");
                }
            } while (!(option >= 0 && option <= ReservationContainer.numberOfReservations()));
            if (option == 0)
            {
                managerMenu();
            }
            else
            {
                Package newPackage = ReservationContainer.getReservation(option - 1).getPackage();
                PackageContainer.addPackage(newPackage);
                ReservationContainer.getReservation(option - 1).getGuest().deleteReservation(ReservationContainer.getReservation(option - 1));
                ReservationContainer.deleteReservation(ReservationContainer.getReservation(option - 1));
                ReservationContainer.writeReservations();
                PackageContainer.writePackages();
                Console.Clear();
                Console.WriteLine("Foglalás sikeresen törölve!");
                System.Threading.Thread.Sleep(3000);
                managerMenu();
            }
        }
    }
}
