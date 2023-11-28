using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HMR_szoftech
{
    class Guest: User
    {
        private string identityCardNumber;
        private List<Reservation> reservations;
        private DateTime birthDate;

        public Guest(string userName, string name, string rank, string identityCardNumber, DateTime birthDate): base(userName, name, rank)
        {
            this.identityCardNumber = identityCardNumber;
            this.birthDate = birthDate;
            reservations = new List<Reservation>();
        }

        public string getIdentityCardNumber()
        {
            return identityCardNumber;
        }

        public void registration()
        {
            GuestContainer.addGuest(this);
            FileStream output = new FileStream("guestdatas.txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(output);
            string outputRow = this.getUserName() + ";" + this.getName() + ";" + this.identityCardNumber + ";" + Convert.ToString(this.birthDate.Year) + ";" + Convert.ToString(this.birthDate.Month) + ";" + Convert.ToString(this.birthDate.Day);
            sw.WriteLine(outputRow);
            sw.Close();
            output.Close();
        }

        public void login()
        {
            guestMenu();
        }

        public void guestMenu()
        {
            Console.Clear();
            Console.WriteLine($"{this.getName()} üdvözlünk a hotel oldalán!");
            string option;
            do
            {
                Console.WriteLine("Kérem válasszon a lehetőségek közül (1/2/3/4/5/6):");
                Console.WriteLine("1. Elérhető csomagok listázása");
                Console.WriteLine("2. Foglalás");
                Console.WriteLine("3. Saját foglalások kilistázása");
                Console.WriteLine("4. Foglalás törlése");
                Console.WriteLine("5. Alapadatok megtekintése");
                Console.WriteLine("6. Kijelentkezés");
                option = Convert.ToString(Console.ReadLine());

            } while (option != "1" && option != "2" && option != "3" && option != "4" && option != "5" && option != "6");
            switch (option)
            {
                case "1": PackageContainer.listPackages(); back(); break;
                case "2": reserve(); break;
                case "3": listOwnReservations(); back(); break;
                case "4": deleteReservation(); break;
                case "5": showBasicDatas(); back(); break;
                case "6": logout(); break;
            }
        }

        private void back()
        {
            Console.Write("Nyomja meg az <Enter>-t a visszalépéshez!");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { };
            Console.Clear();
            guestMenu();
        }

        public void reserve()
        {
            Console.WriteLine("Üdvözöljük foglalási felületünkön!");
            PackageContainer.listPackages();
            int option=-1;
            do
            {
                try
                {
                    Console.Write($"Kérjük adja meg a foglalni kívánt csomag számát {1}-{PackageContainer.numberOfPackages()}, vagy adjon meg 0-t a visszalépéshez: ");
                    option = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine($"Kérjük érvényes számot adjon meg ({1}-{PackageContainer.numberOfPackages()})!");
                }
            } while (!(option >= 0 && option <= PackageContainer.numberOfPackages()));
            if (option == 0)
            {
                guestMenu();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Tovább a fizetésre!");
                System.Threading.Thread.Sleep(3000);
                Console.Clear();
                pay(option);

                guestMenu();
            }   
        }

        public void pay(int option)
        {
            bool successfullPayment = false;
            int tries = 0;

            Console.WriteLine("A válaszott csomag adatai:");
            PackageContainer.getPackage(option - 1).printPackageDatas();
            Console.Write("Nyomja meg az <Enter>-t a továbblépéshez!");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { };
            Console.Clear();

            while (successfullPayment == false && tries < 3)
            {
                bool fineDatas = false;
                Console.Write("Bankkártyaszám: ");
                string cardNumber = Console.ReadLine();
                Console.Write("Bankkártyán szereplő név: ");
                string nameOnCard = Console.ReadLine();
                Console.Write("Bankkártya lejárati dátuma: ");
                string dateOnCard = Console.ReadLine();
                Console.Write("CVC kód: ");
                string CVC = Console.ReadLine();
                if (cardNumber == "1111 2222 3333 4444" && nameOnCard == "Minta Antal" && dateOnCard == "06/26" && CVC == "666")
                    fineDatas = true;
                if (fineDatas)
                {
                    Console.Clear();
                    Console.WriteLine("Sikeres fizetés és foglalás!");
                    System.Threading.Thread.Sleep(3000);
                    successfullPayment = true;
                    string date = Convert.ToString(PackageContainer.getPackage(option - 1).getEndDate() + "-" + PackageContainer.getPackage(option - 1).getStartDate());
                    Reservation newReservation = new Reservation(PackageContainer.getPackage(option - 1), this, date);
                    ReservationContainer.addReservation(newReservation);
                    this.reservations.Add(newReservation);

                    FileStream output = new FileStream("reservations.txt", FileMode.Append);
                    StreamWriter sw = new StreamWriter(output);
                    string sor = Convert.ToString(this.identityCardNumber + ";" + PackageContainer.getPackage(option - 1).getRoomType()) + ";" + PackageContainer.getPackage(option - 1).getNumberOfGuests() + ";" + PackageContainer.getPackage(option - 1).getPackagePrice() + ";" + PackageContainer.getPackage(option - 1).getStartDate() + ";" + PackageContainer.getPackage(option - 1).getEndDate();
                    sw.WriteLine(sor);
                    sw.Close();
                    output.Close();
                    PackageContainer.deletePackage(option - 1);
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("Érvénytelen banki adatok, a foglalás sikertelen!");
                    tries++;
                }
            }

            if (tries == 3)
            {
                Console.WriteLine("Túl sok sikertelen próbálkozás!");
                System.Threading.Thread.Sleep(3000);
            }

            
        }

        public void listOwnReservations()
        {
            Console.Clear();

            if (reservations.Count == 0)
            {
                Console.WriteLine("Önnek még nincs foglalása!");
            }
            for (int i = 0; i < reservations.Count; i++)
            {
                Console.Write($"{i+1}.: ");
                reservations[i].printDatas();
            }
        }

        public void deleteReservation()
        {
            if (reservations.Count > 0)
            {
                this.listOwnReservations();
                int option = -1;
                do
                {
                    Console.Write("Kérjük adja meg a törölni kívánt foglalás számát, vagy írjon 0-t a visszalépéshez: ");
                    try
                    {
                        option = Convert.ToInt32(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("Érvényes számot adjon meg!");
                    }
                } while (!(option >=0 && option <=reservations.Count));
                if (option == 0)
                {
                    guestMenu();
                }
                else
                {
                    Package newPackage = reservations[option - 1].getPackage();
                    PackageContainer.addPackage(newPackage);
                    ReservationContainer.deleteReservation(reservations[option - 1]);
                    reservations.RemoveAt(option - 1);
                    PackageContainer.writePackages();
                    ReservationContainer.writeReservations();
                    Console.Clear();
                    Console.WriteLine("Foglalás sikeresen törölve!");
                    System.Threading.Thread.Sleep(3000);
                    guestMenu();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Önnek nincs foglalása, amit törölhetne!");
                System.Threading.Thread.Sleep(3000);
                guestMenu();
            }
        }

        public void deleteReservation(Reservation delete)
        {
            reservations.Remove(delete);
        }

        public void addReservation(Reservation reservation)
        {
            reservations.Add(reservation);
        }
    }
}
