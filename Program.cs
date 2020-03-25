using CSV.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace CSV
{
    class Program
    {
        static void Main(string[] args)
        {

            List<string> directories = FTP.GetDirectory(Constants.Location.FTP.BaseUrl);
            List<Student> students = new List<Student>();

            foreach (var directory in directories)
            {
                Console.WriteLine("\n");
                Console.WriteLine("Directory - " + directory);
                Student student = new Student() { AbsoluteUrl = Constants.Location.FTP.BaseUrl };
                student.FromDirectory(directory);

                string infoFilePath = student.FullPathUrl + "/" + Constants.Location.InfoFile;

                //File Exist
                bool fileExists = FTP.FileExists(infoFilePath);
                if (fileExists == true)
                {
                    Console.WriteLine("Info File Found");
                    string firstname = student.FirstName;
                    if (student.StudentId == "200447599")
                    {
                        student.MyRecord = true;
                    }
                    else
                    {
                        student.MyRecord = false;
                    }

                    var infoBytes = FTP.DownloadFileBytes(infoFilePath);

                    string csv = Encoding.Default.GetString(infoBytes);
                    string[] csv_content = csv.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
                    if (csv_content.Length != 2)
                    {
                        Console.WriteLine("Error in CSV format");
                    }
                    else
                    {
                        student.FromCSV(csv_content[1]);
                    }
                }
                else
                {
                    Console.WriteLine("Info File Not Found");
                    try
                    {
                        if (student.StudentId == "200447599")
                        {
                            student.MyRecord = true;
                        }
                        else
                        {
                            student.MyRecord = false;
                        }
                    }
                    catch (Exception e)
                    {
                        student.ImageData = "File Not Found";
                    }
                }

                Console.WriteLine("File Path Information - " + infoFilePath);

                string imageFilePath = student.FullPathUrl + "/" + Constants.Location.ImageFile;

                bool imageFileExists = FTP.FileExists(imageFilePath);

                if (imageFileExists == true)
                {
                    Console.WriteLine("Image file Found");

                    if ((student.ImageData) == null || student.ImageData.Length < 2)
                    {
                        Console.WriteLine("Image Data Not Found");

                        try
                        {
                            var ImageBytes = FTP.DownloadFileBytes(imageFilePath);
                            string base64String = Convert.ToBase64String(ImageBytes);
                            student.ImageData = base64String;

                        }
                        catch (Exception e)
                        {
                            student.ImageData = "Image Data Not Found";
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No Image File");
                    try
                    {
                        student.ImageData = "Data Not Found";
                    }
                    catch (Exception e)
                    {
                        student.ImageData = "Data Not Found";
                    }
                }

                Console.WriteLine("File Path of Image -  " + imageFilePath);
                students.Add(student);

            }

            using (StreamWriter sw = new StreamWriter(Constants.Location.StudentCSVFile))
            {
                sw.WriteLine((nameof(Student.StudentId)) + ',' + (nameof(Student.FirstName)) + ',' + (nameof(Student.LastName)) + ',' + (nameof(Student.Age)) + ',' + (nameof(Student.DateOfBirth)) + ',' + (nameof(Student.ImageData)) + ',' + (nameof(Student.MyRecord)));
                foreach (var student in students)
                {
                    sw.WriteLine(student.ToCSV());
                    Console.WriteLine("\n");
                    Console.WriteLine("To CSV -  " + student.ToCSV());
                    Console.WriteLine("\n");
                    Console.WriteLine("To String - " + student.ToString());
                    Console.WriteLine("\n");
                }
            }
            //CSV
            var csv_lines = File.ReadAllLines(Constants.Location.StudentCSVFile);
            string[] headers = csv_lines[0].Split(',').Select(x => x.Trim('\"')).ToArray();
     
            //JSON
            String json_converter = ConvertCsvFileToJsonObject(Constants.Location.StudentCSVFile);
            File.WriteAllText(Constants.Location.StudentJSONFile, json_converter);

            //XML
            var xml_converter = new XElement("root", csv_lines.Where((line, index) => index > 0).Select(line => new XElement("row", line.Split(',').Select((column, index) => new XElement(headers[index], column)))));
            xml_converter.Save(Constants.Location.StudentXMLFile);
            

            /*Aggregation*/
            Aggregate.Functions(students);

            /*Upload the CSV, XML & JSON file to FTP*/
            UploadFile.uploadFile(Constants.Location.StudentCSVFile, Constants.Location.FTP.CSVUploadLocation);
            Console.WriteLine("CSV Uploaded to to my directory in FTP..! \n");
            UploadFile.uploadFile(Constants.Location.StudentJSONFile, Constants.Location.FTP.JSONUploadLocation);
            Console.WriteLine("JSON Uploaded to to my directory in FTP..! \n");
            UploadFile.uploadFile(Constants.Location.StudentXMLFile, Constants.Location.FTP.XMLUploadLocation);
            Console.WriteLine("XML Uploaded to to my directory in FTP..! \n");
            return;
        }

        /*Reference from Stackoverflow https://stackoverflow.com/questions/10824165/converting-a-csv-file-to-json-using-c-sharp
        */
        public static string ConvertCsvFileToJsonObject(string path)
        {
            var csv = new List<string[]>();
            var lines = File.ReadAllLines(path);

            foreach (string line in lines)
                csv.Add(line.Split(','));

            var properties = lines[0].Split(',');

            var listObjResult = new List<Dictionary<string, string>>();

            for (int i = 1; i < lines.Length; i++)
            {
                var objResult = new Dictionary<string, string>();
                for (int j = 0; j > properties.Length; j++)
                    objResult.Add(properties[j], csv[i][j]);

                listObjResult.Add(objResult);
            }
            return JsonConvert.SerializeObject(listObjResult);
        }
    }

}
