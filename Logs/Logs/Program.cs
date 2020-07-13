using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
namespace Logs
{
    /*
     * Author: René Bolf (rbolf.rene@gmail.com)
     * Project: Interview task in Safetica
     */
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Please enter the file path location");
            Console.WriteLine(@"for example: C:\Users\username\Desktop\Logs.txt");
            string fileFromConsole = Console.ReadLine();
            bool DoesFileExist = System.IO.File.Exists(fileFromConsole);
            if (DoesFileExist == true)
            {
                string file = File.ReadAllText(fileFromConsole);
                file = Regex.Replace(file, @"\r\n\r", ""); // if in file is a new line between messages this new line is replace with "", this is for finding substring in string (row 43)

                string dashes = "-------------------------------------------------------------------------------";
                string separator = dashes;
                List<string> listOfLogs = new List<string>(file.Split(separator, StringSplitOptions.RemoveEmptyEntries));

                var i = 0; // [0] element
                List<string> foundLogs = new List<string>();
                while (i < listOfLogs.Count)
                {
                    int counter = 1; // counter is 1, because we append first log into the found_logs list
                    foundLogs.Add(listOfLogs[i]); //append first log into the found logs list
                    string pattern = @"^[^<][a-zA-Z].+[^>]";
                    Regex rgx = new Regex(pattern, RegexOptions.Multiline);
                    var logMessage = rgx.Matches(listOfLogs[i]).Cast<Match>()
                                                             .Select(m => m.Value)
                                                             .ToList(); // convert MatchCollection to list

                    string stringLogMessage = string.Join("", logMessage); //convert list to string

                    var j = i + 1;
                    while (j < listOfLogs.Count)
                    {
                        if (listOfLogs[j].Contains(stringLogMessage))
                        {
                            counter += 1;
                            listOfLogs.RemoveAt(j);
                        }
                        else
                        {
                            j++;
                        }
                    }
                    string numberof = dashes + "\n" + "Number of occurrences: " + counter;
                    foundLogs.Insert(foundLogs.Count - 1, numberof);
                    listOfLogs.RemoveAt(i);
                    if (counter == 1)
                    {
                        foundLogs.RemoveAt(foundLogs.Count - 1); // remove log
                        foundLogs.RemoveAt(foundLogs.Count - 1); // remove dashes and number of occurrences

                    }
                    else if(counter == 2)
                    {
                        foundLogs.RemoveAt(foundLogs.Count - 1); // remove log
                        foundLogs.RemoveAt(foundLogs.Count - 1); // remove dashes and number of occurrences
                    }
                }
                foreach (var item in foundLogs)
                {
                    Console.WriteLine(item);
                }
            }
            else
            {
                Console.WriteLine("File does not exist, Please enter a valid file path");
            }
            
        }
    }
}
