using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMR_szoftech
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Push proba");

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

        void registration()
        {
            Console.WriteLine("Kérjük adja meg a regisztrációhoz szükséges adatait:");
            Console.Write("Teljes név: ");
            string name=Console.ReadLine();
            Console.Write("Felhasználónév: ");
            string username = Console.ReadLine();
            Console.Write("Személyazonosító igazolvány száma: ");
            string identitycardnumber = Console.ReadLine();
            Console.Write("Születési dátum (yyyy/hh/nn): ");
            string birthdate=Console.ReadLine();
        }
    }
}
