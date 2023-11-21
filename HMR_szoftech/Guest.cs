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
        private string identityCardNummber;
        private List<Reservation> reservations;
        private DateTime birthDate;

        public Guest(string userName, string name, string rank, string identityCardNumber, DateTime birthDate): base(userName, name, rank)
        {
            this.identityCardNummber = identityCardNumber;
            this.birthDate = birthDate;
            reservations = new List<Reservation>();
        }

        public void registration()
        {
            GuestContainer.addGuest(this);
            FileStream output = new FileStream("guestdatas.txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(output);
            string outputRow = this.getUserName() + ";" + this.getName() + ";" + this.identityCardNummber + ";" + Convert.ToString(this.birthDate.Year) + ";" + Convert.ToString(this.birthDate.Month) + ";" + Convert.ToString(this.birthDate.Day);
            sw.WriteLine(outputRow);
            sw.Close();
            output.Close();
        }

        public void guestMenu()
        {
            Console.WriteLine($"{0} üdvözlünk a hotel oldalán!", this.getName());
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
                case "1": login(); break;
                case "2": registration(); break;
                case "3": Console.WriteLine("harmas"); break;
                case "4": registration(); break;
                case "5": registration(); break;
                case "6": registration(); break;
            }
        }

        public void login()
        {

        }
    }
}
