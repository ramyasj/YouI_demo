using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouI_demo.DAL
{
    /// <summary>
    /// Represents the data to be processed from the the csv file
    /// </summary>
    public class Person
    {
        /// <summary>
        /// gets or sets the first Name of a person
        /// </summary>
        public string firstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of a person
        /// </summary>
        public string lastName { get; set; }
        
        /// <summary>
        /// Gets or sets the frequency of occurence of first names
        /// </summary>
        public int firstNameOccurencces { get; set; }

        /// <summary>
        /// Gets or sets the frequency of occurence of last names
        /// </summary>
        public int lastNameOccurencces { get; set; }

        /// <summary>
        /// Gets or sets the person's address
        /// </summary>
        public string address { get; set; }


    }
}
