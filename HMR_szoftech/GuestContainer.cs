using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HMR_szoftech
{
    static class GuestContainer
    {
        static private List<Guest> guestList=new List<Guest>();

        static public void addGuest(Guest newGuest)
        {
            guestList.Add(newGuest);
        }

        static public void readDatas()
        {
            StreamReader sr = new StreamReader("guestdatas.txt");
            string sor;
            string[] adatok;
            Guest newGuest;
            while (!sr.EndOfStream)
            {
                sor = sr.ReadLine();
                adatok = sor.Split(';');
                int year = Convert.ToInt32(adatok[3]);
                int month = Convert.ToInt32(adatok[4]);
                int day = Convert.ToInt32(adatok[5]);
                newGuest=new Guest(adatok[0], adatok[1], "guest",  adatok[2], new DateTime(year, month, day));
                guestList.Add(newGuest);
            }
            sr.Close();
        }
    }
}
