using System;
using System.Collections.Generic;
using System.Text;

namespace CSV.Models
{
    public class Constants
    {


        public readonly Student Student = new Student { StudentId = "200447599", FirstName = "KavirajSingh", LastName = "Jon" };
        public class Location
        {
            public readonly static string DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            public readonly static string ExePath = Environment.CurrentDirectory;

            public readonly static string ContentFolder = $"{ExePath}\\..\\..\\..\\Content\\Data";
            public readonly static string ImagesFolder = $"{ContentFolder}";
            public readonly static string DataFolder = $"{ContentFolder}";

            public const string InfoFile = "info.csv";
            public const string ImageFile = "myimage.jpg";

            /*public readonly static string StudentCSVFile = "C:/Users/aprat/Desktop/students.csv";
            public readonly static string StudentJSONFile = "C:/Users/aprat/Desktop/students.json";
            public readonly static string StudentXMLFile = "C:/Users/aprat/Desktop/students.xml";
*/
            public readonly static string StudentCSVFile = $"{DataFolder}\\students.csv";
            public readonly static string StudentJSONFile = $"{DataFolder}\\students.json";
            public readonly static string StudentXMLFile = $"{DataFolder}\\students.xml";

            public class FTP
            {
                public const string Username = @"bdat100119f\bdat1001";
                public const string Password = "bdat1001";

                public const string BaseUrl = "ftp://waws-prod-dm1-127.ftp.azurewebsites.windows.net/bdat1001-20914";

                public const string MyDirectory = "/200447599 KavirajSingh Jon";
                public const string CSVUploadLocation = BaseUrl + MyDirectory + "/students.csv";
                public const string JSONUploadLocation = BaseUrl + MyDirectory + "/students.json";
                public const string XMLUploadLocation = BaseUrl + MyDirectory + "/students.xml";

                public const int OperationPauseTime = 10000;
            }
        }
    }
}

