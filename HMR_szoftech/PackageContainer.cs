﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMR_szoftech
{
    static class PackageContainer
    {
        static private List<Package> packageList = new List<Package>();

        static public void readDatas()
        {
            StreamReader sr = new StreamReader("packagedatas.txt");
            string sor;
            string[] adatok;
            Package newPackage;
            while (!sr.EndOfStream)
            {
                sor = sr.ReadLine();
                adatok = sor.Split(';');
                string roomType = adatok[0];
                int numberOfGuests = Convert.ToInt32(adatok[1]);
                int packagePrice = Convert.ToInt32(adatok[2]);
                string startDate = adatok[3];
                string endDate = adatok[4];
                newPackage = new Package(roomType, numberOfGuests, packagePrice, startDate, endDate);
                packageList.Add(newPackage);
            }
            sr.Close();
        }

        public static void listPackages()
        {
            Console.Clear();
            Console.WriteLine("Elérhető csomagjaink: ");
            for (int i = 0; i < packageList.Count; i++)
            {
                Console.WriteLine($"{i+1}.: szoba típusa: {packageList[i].getRoomType()}, vendégek száma: {packageList[i].getNumberOfGuests()}, csomagár: {packageList[i].getPackagePrice()}, dátum: {packageList[i].getStartDate()}-{packageList[i].getEndDate()}");
            }
        }

        public static int numberOfPackages()
        {
            return packageList.Count();
        }

        public static Package getPackage(int idx)
        {
            return packageList[idx];
        }

        public static void deletePackage(int idx)
        {
            packageList.RemoveAt(idx);
            writePackages();
        }

        public static void writePackages()
        {
            StreamWriter sw=new StreamWriter("packagedatas.txt");
            for(int i=0;i<packageList.Count; i++)
            {
                string sor = Convert.ToString(packageList[i].getRoomType() + ";" + packageList[i].getNumberOfGuests() + ";" + packageList[i].getPackagePrice() + ";" + packageList[i].getStartDate() + ";" + packageList[i].getEndDate());
                sw.WriteLine(sor);
            }
            sw.Close();
        }

        public static void addPackage(Package newPackage)
        {
            packageList.Add(newPackage);
        }
    }
}