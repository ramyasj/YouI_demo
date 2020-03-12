using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouI_demo.DAL;

namespace YouI_demo.BAL
{
    public class FileProcessing
    {

        /// <summary>
        /// Processes the data of the file provided from the path
        /// </summary>
        /// <param name="fPath">file path</param>
        public static string  ProcessDataAndGenerateFiles(string fPath)
        {

            List<Person> people = new List<Person>();
            string result = string.Empty;

            //Reads the file and fills the people list with details
            ReadFile(fPath, out people);

            // checking if the file is empty
            if (people != null)
            {
                try
                {

                    StringBuilder strPeopleNames = new StringBuilder("First Name\t" + "Last Name\t" + "Frequency of First names\t" + "Frequency of Last names\t" + Environment.NewLine);
                    foreach (var item in GetFrequencyOfOccurrences(people))
                    {
                        strPeopleNames.Append(item.firstName + "\t" + item.lastName + "\t" + item.firstNameOccurencces + "\t" + item.lastNameOccurencces + Environment.NewLine);
                    }

                    //Creating the first file with people names sorted in the order of frequency of firstname,lastname and firstname alphabets
                    using (StreamWriter firstFile = new StreamWriter("PeopleNamesSorted.txt"))
                    {
                        firstFile.Write(strPeopleNames);
                        firstFile.Close();
                    }

                    //LINQ to sort the addresses in the order of street name
                    var sortedAddressList = people.OrderBy(s => s.address.Substring(s.address.IndexOf(' ')));//.Sort();

                    StringBuilder strAddress = new StringBuilder("Address \n" + Environment.NewLine);
                    foreach (var item in sortedAddressList)
                    {
                        strAddress.Append(item.address + "\n" + Environment.NewLine);
                    }

                    //creating the second file with the address sorted in the order street names
                    using (StreamWriter secondFile = new StreamWriter("AddressSorted.txt"))
                    {
                        secondFile.Write(strAddress);
                        secondFile.Close();
                    }

                    result = "Files created.";
                }
                catch (Exception ex)
                {
                    result = ex.Message.ToString();
                }

            }
            else {
                result = "File is empty. Please provide a valid file.";
            }

            return result;
        }

        /// <summary>
        /// Gets the frequency of occurrence of the first name and second and sort them in the descending order of occurrence 
        /// of firstname and occurrence of last name and alphabetical order of first name
        /// </summary>
        /// <param name="people"></param>
        /// <returns>List of Person class objects</returns>
        private static List<Person> GetFrequencyOfOccurrences(List<Person> people)
        {
            // LINQ to get the number of occurences of first names and sorted in descending order of first name occurrences
            var firstNameFrequencies = from personName in people
                                       group personName by personName.firstName
                    into g
                                       orderby g.Count() descending
                                       select new Person() { firstName = g.Key, lastName = g.First().lastName, firstNameOccurencces = g.Count() };

            // LINQ to get the number of occurences of last names and sorted in descending order of first name occurrences and last name occurrences
            var orderedList = from personName in firstNameFrequencies
                              group personName by personName.lastName
                    into g
                              orderby g.Count() descending, g.First().firstName ascending
                              select new Person() { firstName = g.First().firstName, lastName = g.Key, lastNameOccurencces = g.Count(), firstNameOccurencces = g.First().firstNameOccurencces };

            return orderedList.ToList();
        }

        /// <summary>
        /// Reads the file from the given input path
        /// </summary>
        /// <param name="fPath">path of the file which has to be read</param>
        /// <param name="peopleNames">Output parameter that will filled if the file is found and processed</param>
        private static void ReadFile(string fPath, out List<Person> peopleNames)
        {
            peopleNames = null;

            try
            {
                var lines = File.ReadAllLines(fPath).Select(l => l.Split(','));
                List<Person> pNames = new List<Person>();

                foreach (var value in lines.Skip(1))
                {
                    Person person = new Person();
                    person.firstName = value[0];
                    person.lastName = value[1];
                    person.address = value[2];

                    //Assuming the occurencces is 0 for all the values                
                    person.lastNameOccurencces = 0;
                    person.firstNameOccurencces = 0;

                    pNames.Add(person);

                }

                peopleNames = pNames;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
