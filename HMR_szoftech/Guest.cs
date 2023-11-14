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
    }
}
