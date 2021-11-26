using CSVEmailSender.Models;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CSVEmailSender
{
    public class Helper
    {
        public string zipPath { get; set; }

        public string filePath { get; set; }

        public Helper()
        {

        }
        public void CreateCSVFile(UserData newUserdata)
        {
            string sTime = DateTime.Now.ToString("MMddyyyyhhmmssfff");

            filePath = MainAppPath.appPath + @"\Files\user_data" + sTime;
  
            using (StreamWriter writer = new StreamWriter(filePath + ".csv"))
            {
                writer.Write("Id,First Name,Last Name,Email,Phone,City,Street,Apartment Num");
                writer.WriteLine();
                writer.WriteLine($"{newUserdata.Id},{newUserdata.FirstName},{newUserdata.LastName},{newUserdata.Email},{newUserdata.Phone},{newUserdata.City},{newUserdata.Street},{newUserdata.ApartmentNum}");
            }


        }
        public bool CreateZipFile()
        {
            bool isFileZiped = false;

            zipPath = filePath + ".zip";
            using (ZipFile zip = new ZipFile())
            {
                zip.Password = "12345";
                zip.AddFile(filePath + ".csv", "");
                zip.Save(zipPath);
                isFileZiped = true;
            }
            return isFileZiped;
        }
    }
}
