using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMR_szoftech
{
    static class ReservationContainer
    {
        static private List<Reservation> reservationList=new List<Reservation>();

        static public Reservation getReservation(int idx)
        {
            return reservationList[idx];
        }

        static public Package getPackageFromReservation(int idx)
        {
            return reservationList[idx].getPackage();
        }

        static public int numberOfReservations()
        {
            return reservationList.Count;
        }

        static public void addReservation(Reservation newReservation)
        {
            reservationList.Add(newReservation);
        }

        static public void deleteReservation(int idx)
        {
            reservationList.RemoveAt(idx);
        }

        static public void readDatas()
        {
            StreamReader sr = new StreamReader("reservations.txt");
            string sor;
            string[] adatok;
            Reservation newReservation;
            while (!sr.EndOfStream)
            {
                sor = sr.ReadLine();
                adatok = sor.Split(';');
                string roomType = adatok[1];
                int numberOfGuests = Convert.ToInt32(adatok[2]);
                int packagePrice= Convert.ToInt32(adatok[3]);
                string startDate = adatok[4];
                string endDate = adatok[5];
                Package newPackage = new Package(roomType, numberOfGuests, packagePrice, startDate, endDate);
                Guest guest = GuestContainer.getGuestByID(adatok[0]);
                string date=Convert.ToString(newPackage.getStartDate()+"-"+newPackage.getEndDate());
                addReservation(new Reservation(newPackage, guest, date));
            }
            sr.Close();
        }

        public static void writeReservations()
        {
            StreamWriter sw = new StreamWriter("reservations.txt");
            for (int i = 0; i < reservationList.Count; i++)
            {
                string sor = Convert.ToString(reservationList[i].getGuest().getIdentityCardNumber() + ";" + reservationList[i].getPackage().getRoomType() + ";" + reservationList[i].getPackage().getNumberOfGuests()+";"+ reservationList[i].getPackage().getPackagePrice()+";"+ reservationList[i].getPackage().getStartDate()+";"+ reservationList[i].getPackage().getEndDate());
                sw.WriteLine(sor);
            }
            sw.Close();
        }
    }
}
