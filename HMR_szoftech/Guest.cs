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

        public void guestMenu()
        {
            Console.Clear();
            Console.WriteLine($"{this.getName()} üdvözlünk a hotel oldalán!");
            string option;
            do
            {
                Console.WriteLine("Kérlek válassz a lehetőségek közül (1/2/3/4/5/6):");
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

        public void login()
        {
            guestMenu();
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
            Console.Write($"Kérjük adja meg a foglalni kívánt csomag számát: {1}-{PackageContainer.numberOfPackages()}");
            int option=0;
            do
            {
                option = Convert.ToInt32(Console.ReadLine());
            } while (!(option >= 1 && option <= PackageContainer.numberOfPackages()));
            Console.Clear();
            Console.WriteLine("Tovább a fizetésre!");
            System.Threading.Thread.Sleep(3000);
            Console.Clear();
            bool successfullPayment = false;
            int tries = 0;
            while (successfullPayment == false && tries < 3)
            {
                if (pay())
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

            guestMenu();
        }

        public bool pay()
        {
            Console.Write("Bankkártyaszám: ");
            string cardNumber = Console.ReadLine();
            Console.Write("Bankkártyán szereplő név: ");
            string nameOnCard = Console.ReadLine();
            Console.Write("Bankkártya lejárati dátuma: ");
            string dateOnCard = Console.ReadLine();
            Console.Write("CVC kód: ");
            string CVC = Console.ReadLine();
            if (cardNumber == "1111 2222 3333 4444" && nameOnCard == "Minta Antal" && dateOnCard == "06/26" && CVC == "666")
                return true;
            else
                return false;
        }

        public string getIdentityCardNumber()
        {
            return identityCardNumber;
        }

        public void listOwnReservations()
        {
            Console.Clear();
            bool reservation = false;
            int db = 1;
            Console.WriteLine("Az Ön foglalásai: ");
            for(int i = 0; i < ReservationContainer.numberOfReservations(); i++)
            {
                if (ReservationContainer.getReservation(i).getGuest().getIdentityCardNumber() == this.identityCardNumber)
                {
                    Console.Write($"{db}.: ");
                    ReservationContainer.getReservation(i).printDatas();
                    reservation = true;
                    db++;
                }
            }

            if (!reservation)
                Console.WriteLine("Önnek nincs foglalása!");
        }

        public int getNumberOfOwnReservations()
        {
            int db = 0;
            for (int i = 0; i < ReservationContainer.numberOfReservations(); i++)
            {
                if (ReservationContainer.getReservation(i).getGuest().getIdentityCardNumber() == this.identityCardNumber)
                    db++;
            }
            return db;
        }

        public void deleteReservation()
        {
            if (getNumberOfOwnReservations() > 0)
            {
                this.listOwnReservations();
                Console.Write("Kérjük adja meg a törölni kívánt foglalás számát: ");
                int option = 0;
                do
                {
                    option = Convert.ToInt32(Console.ReadLine());
                } while (!(option >= 1 && option <= getNumberOfOwnReservations()));
                Package newPackage = ReservationContainer.getPackageFromReservation(option-1);
                PackageContainer.addPackage(newPackage);
                PackageContainer.writePackages();
                ReservationContainer.deleteReservation(option - 1);
                ReservationContainer.writeReservations();
                Console.Clear();
                Console.WriteLine("Foglalás sikeresen törölve!");
                System.Threading.Thread.Sleep(3000);
                guestMenu();
            }
            else
            {
                Console.WriteLine("Önnek nincs foglalása, amit törölhetne!");
            }
            

            Console.ReadKey();
        }
    }
}
