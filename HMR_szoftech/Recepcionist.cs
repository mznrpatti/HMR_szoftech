using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMR_szoftech
{
    class Recepcionist: User
    {
        public Recepcionist(string userName, string name, string rank) : base(userName, name, rank) {}

        public void login()
        {
            recepcionistMenu();
        }

        public void recepcionistMenu()
        {
            Console.Clear();
            Console.WriteLine($"{this.getName()} főoldala");
            string option;
            do
            {
                Console.WriteLine("Kérlek válassz a lehetőségek közül (1/2/3):");
                Console.WriteLine("1. Foglalások szűrése igazolványszám szerint");
                Console.WriteLine("2. Alapadatok megtekintése");
                Console.WriteLine("3. Kijelentkezés");
                option = Convert.ToString(Console.ReadLine());

            } while (option != "1" && option != "2" && option != "3");
            switch (option)
            {
                case "1": listReservation(); back(); break;
                case "2": showBasicDatas(); back(); break;
                case "3": logout(); break;
            }
        }

        public void listReservation()
        {
            Console.Clear();

            int reservationNumber = 0;
            string identityCardNumber="";
            bool valid = true;
            do
            {
                valid = true;
                Console.Write("Kinek a foglalását szeretné megtekinteni? ");
                identityCardNumber = Console.ReadLine();
                Guest validator = GuestContainer.getGuestByID(identityCardNumber);
                if(validator==null)
                    valid=false;
            } while (!valid);
            Console.WriteLine($"{GuestContainer.getGuestByID(identityCardNumber).getName()} ({identityCardNumber}) foglalásai: ");
            for (int i = 0; i < ReservationContainer.numberOfReservations(); i++)
            {
                if (ReservationContainer.getReservation(i).getGuest().getIdentityCardNumber()==identityCardNumber)
                {
                    Console.Write("-");
                    ReservationContainer.getReservation(i).printDatas();
                    reservationNumber++;
                }
            }

            if (reservationNumber == 0)
                Console.WriteLine("Ennek a felhasználónak nincs foglalása!");
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
