using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HMR_szoftech
{
    class Program
    {
        static void Main(string[] args)
        {
            GuestContainer.readDatas();
            Program obj=new Program();
            obj.begin();

            Console.ReadKey();
        }

        void begin()
        {
            Console.WriteLine("Üdvözöllek a Hotel oldalán!");
            string option;
            do
            {
                Console.WriteLine("Kérlek válassz a lehetőségek közül (1/2/3):");
                Console.WriteLine("1. Bejelentkezés");
                Console.WriteLine("2. Regisztráció");
                Console.WriteLine("3. Nézelődés látogatóként");
                option = Convert.ToString(Console.ReadLine());
                
            } while (option != "1" && option!= "2" && option!="3");
            switch (option)
            {
                case "1": login(); break;
                case "2": registration(); break;
                case "3": Console.WriteLine("harmas"); break;
            }
        }

        void login()
        {
            Console.Clear();
            Console.Write("Felhasználónév: ");
            string userName=Console.ReadLine();
            Console.Write("Jelszó: ");
            string password = Console.ReadLine();
        }

        bool birthDateChecker(DateTime birthdate)
        {
            DateTime current = DateTime.Now;
            if (birthdate.Year < current.Year - 18 || (birthdate.Year == current.Year - 18 && birthdate.Month < current.Month) || (birthdate.Year == current.Year - 18 && birthdate.Month == current.Month && birthdate.Day <= current.Day))
            {
                return true;
            }
            return false;

        }

        void registration()
        {
            Console.Clear();
            Console.WriteLine("Kérjük adja meg a regisztrációhoz szükséges adatait:");
            Console.Write("Teljes név: ");
            string name=Console.ReadLine();
            Console.Write("Felhasználónév: ");
            string username = Console.ReadLine();
            Console.Write("Jelszó: ");
            string password = Console.ReadLine();
            Console.Write("Személyazonosító igazolvány száma: ");
            string identitycardnumber = Console.ReadLine();
            int year = -1;
            do
            {
                Console.Write("Születési év: ");
                try
                {
                    year = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Kérlek érvényes születési évet adj meg!");
                }  
            } while (year==-1);
            int month = -1;
            do
            {
                Console.Write("Születési hónap: ");
                try
                {
                    month = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Kérlek érvényes születési hónapot adj meg!");
                }
            } while (month == -1);
            int day = -1;
            do
            {
                Console.Write("Születési nap: ");
                try
                {
                    day = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Kérlek érvényes születési napot adj meg!");
                }
            } while (day == -1);
            DateTime birthdate = new DateTime(year, month, day);
            if (birthDateChecker(birthdate))
            {
                Guest newGuest = new Guest(username, name, "guest", identitycardnumber, birthdate);
                newGuest.registration();
                FileStream output = new FileStream("logindatas.txt", FileMode.Append);
                StreamWriter sw = new StreamWriter(output);
                string outputString = username + ";" + password + ";guest";
                sw.WriteLine(outputString);
                sw.Close();
                output.Close();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Sikertelen regisztráció! Nem töltötte be a 18. életévét!");
                Console.Write("Nyomja meg az <Enter>-t a továbblépéshez!");
                while (Console.ReadKey().Key != ConsoleKey.Enter) {};
                Console.Clear();
                begin();
            }
        }
    }
}
