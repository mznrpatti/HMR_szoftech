using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HMR_szoftech
{
    static class Program
    {

        static private Recepcionist recepcionist = new Recepcionist("recepcionist", "Recepció Szolgálat", "recepcionist");
        static private Manager admin = new Manager("admin", "Hotel Adminisztráció", "manager");

        static void Main(string[] args)
        {
            GuestContainer.readDatas();
            PackageContainer.readDatas();
            ReservationContainer.readDatas();
            begin();

            Console.ReadKey();
        }

        static public void begin()
        {
            Console.Clear();
            Console.WriteLine("Üdvözöllek a Hotel oldalán!");
            string option;
            do
            {
                Console.WriteLine("Kérem válasszon a lehetőségek közül (1/2/3):");
                Console.WriteLine("1. Bejelentkezés");
                Console.WriteLine("2. Regisztráció");
                Console.WriteLine("3. Nézelődés látogatóként");
                option = Convert.ToString(Console.ReadLine());
                
            } while (option != "1" && option!= "2" && option!="3");
            switch (option)
            {
                case "1": login(); break;
                case "2": registration(); break;
                case "3": visitor(); break;
            }
        }

        static public void registration()
        {
            Console.Clear();
            Console.WriteLine("Kérjük adja meg a regisztrációhoz szükséges adatait:");
            string name="";
            do
            {
                Console.Write("Teljes név: ");
                name = Console.ReadLine();
                if(name=="")
                    Console.WriteLine("Kérjük adja meg a nevét!");
            } while (name == "");
            string username;
            bool validName = false;
            do
            {
                Console.Write("Felhasználónév: ");
                username = Console.ReadLine();
                validName = true;
                if (username == "admin" || username == "recepcionist")
                {
                    validName = false;
                    Console.WriteLine("Ez a felhasználónév már foglalt vagy nem engedélyezett. Kérjük adjon meg másikat!");
                }
                else
                {
                    for (int i = 0; i < GuestContainer.numberOfGuests(); i++)
                    {
                        if (GuestContainer.getGuest(i).getUserName() == username)
                        {
                            validName = false;
                            Console.WriteLine("Ez a felhasználónév már foglalt vagy nem engedélyezett. Kérjük adjon meg másikat!");
                        }
                    }
                }
                if (username == "")
                {
                    validName = false;
                    Console.WriteLine("Kérjük adjon meg egy felhasználónevet!");
                }
            } while (!validName);
            string password = "";
            do
            {
                Console.Write("Jelszó: ");
                password = Console.ReadLine();
                if (password.Length < 6)
                    Console.WriteLine("A jelszó túl rövid!");
            } while (password.Length<6);
            string identitycardnumber = "";
            bool valid = false;
            do
            {
                Console.Write("Személyazonosító igazolvány száma: ");
                identitycardnumber = Console.ReadLine();
                bool validIdentity = true;
                bool betu1=false;
                bool betu2=false;
                if (identitycardnumber.Length == 8)
                {
                    try
                    {
                        Convert.ToInt32(identitycardnumber.Substring(0, 6));
                    }
                    catch
                    {
                        validIdentity = false;
                    }
                    betu1 = false;
                    betu2 = false;
                    try 
                    {
                        Convert.ToInt32(identitycardnumber.Substring(6, 1));
                    }
                    catch
                    {
                        betu1 = true;
                    }
                    try
                    {
                        Convert.ToInt32(identitycardnumber.Substring(7, 1));
                    }
                    catch
                    {
                        betu2 = true;
                    }
                }

                if (validIdentity && betu1 && betu2)
                    valid = true;
                else
                    Console.WriteLine("Kérjük adja meg érvényes magyar személyi azonosító számát!");

            } while (!valid);
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
            } while (year == -1);
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
                Console.Clear();
                Console.WriteLine("Sikeres regisztráció!");
                Console.Write("Nyomja meg az <Enter>-t a továbblépéshez!");
                while (Console.ReadKey().Key != ConsoleKey.Enter) { };
                Console.Clear();
                begin();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Sikertelen regisztráció! Nem töltötte be a 18. életévét!");
                Console.Write("Nyomja meg az <Enter>-t a továbblépéshez!");
                while (Console.ReadKey().Key != ConsoleKey.Enter) { };
                Console.Clear();
                begin();
            }
        }

        static private bool birthDateChecker(DateTime birthdate)
        {
            DateTime current = DateTime.Now;
            if (birthdate.Year < current.Year - 18 || (birthdate.Year == current.Year - 18 && birthdate.Month < current.Month) || (birthdate.Year == current.Year - 18 && birthdate.Month == current.Month && birthdate.Day <= current.Day))
            {
                return true;
            }
            return false;

        }

        static public void login()
        {
            Console.Clear();
            Console.Write("Felhasználónév: ");
            string userName=Console.ReadLine();
            Console.Write("Jelszó: ");
            string password = Console.ReadLine();
            if (userName == "admin")
            {
                StreamReader sr = new StreamReader("adminlogin.txt");
                if (password == sr.ReadLine())
                    admin.login();
                else
                {
                    Console.Clear();
                    Console.WriteLine("Helytelen felhasználónév vagy jelszó!");
                    System.Threading.Thread.Sleep(3000);
                    begin();
                }
                sr.Close();
            }
            else if (userName == "recepcionist")
            {
                StreamReader sr = new StreamReader("recepcionistlogin.txt");
                if (password == sr.ReadLine())
                    recepcionist.login();
                else
                {
                    Console.Clear();
                    Console.WriteLine("Helytelen felhasználónév vagy jelszó!");
                    System.Threading.Thread.Sleep(3000);
                    begin();
                }
                sr.Close();
            }
            else
            {
                bool log = false;
                for (int i=0;i<GuestContainer.numberOfGuests(); i++)
                {
                    if (GuestContainer.getGuest(i).getUserName() == userName)
                    {
                        
                        StreamReader sr = new StreamReader("logindatas.txt");
                        bool success = false;
                        while (!sr.EndOfStream)
                        {
                            string[] logindatas = (sr.ReadLine()).Split(';');
                            if (logindatas[1] == password)
                            {
                                success = true;
                                break;
                            }
                        }
                        sr.Close();
                        if (success)
                        {
                            log=true;
                            GuestContainer.getGuest(i).login();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Helytelen felhasználónév vagy jelszó!");
                            System.Threading.Thread.Sleep(3000);
                            begin();
                        }
                    }
                }
                if (!log)
                {
                    Console.Clear();
                    Console.WriteLine("Helytelen felhasználónév vagy jelszó!");
                    System.Threading.Thread.Sleep(3000);
                    begin();
                }
            }
        }

        static public void visitor()
        {
            Console.Clear();
            Console.WriteLine("Kedves Látogató! Üdvözöljük hotelünk oldalán!");
            string option;
            do
            {
                Console.WriteLine("Kérem válasszon a lehetőségek közül (1/2/3/4):");
                Console.WriteLine("1. Alapadatok megtekintése");
                Console.WriteLine("2. Elérhető csomagok listázása");
                Console.WriteLine("3. Regisztráció");
                Console.WriteLine("4. Vissza a főmenübe");
                option = Convert.ToString(Console.ReadLine());

            } while (option != "1" && option != "2" && option != "3" && option != "4");
            switch (option)
            {
                case "1": Visitor.showBasicDatas(); break;
                case "2": PackageContainer.listPackages(); Console.Write("Nyomja meg az <Enter>-t a visszalépéshez!"); while (Console.ReadKey().Key != ConsoleKey.Enter) { }; visitor(); break;
                case "3": Visitor.registration(); break;
                case "4": begin();break;
            }
        }
    }
}
