using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleDB;
using VehicleRecords;


namespace TestInsertImage
{
    class Program
    {
        static void Main(string[] args)
        {
            int ImageNo = VehicleDBManager.InsertImageFromFile(@"C:\Users\geoff\Pictures\Da Boys.jpg", "image/jpeg");
            Console.WriteLine($"Inserted Image #{ImageNo}");
            Console.ReadLine();
        }
    }
}
