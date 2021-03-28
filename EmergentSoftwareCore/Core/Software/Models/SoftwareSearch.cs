using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergentSoftwareCore
{
    public class SoftwareSearch
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public int MajorVersion { get; set; }
        public int MinorVersion { get; set; }
        public int Patch { get; set; }



        public SoftwareSearch(Software software)
        {
            Name = software.Name;
            Version = software.Version;

            int number;
            string[] searchTermElements = Version.Split(UtilityManager.GetVersionDelimiter());

            if (searchTermElements.Length > 0)
            {
                if (Int32.TryParse(searchTermElements[0], out number))
                    MajorVersion = number;
                else
                {
                    // Error Handling, check with business owner how they want handled
                }
            }
            if (searchTermElements.Length > 1)
            {
                if (Int32.TryParse(searchTermElements[1], out number))
                    MinorVersion = number;
                else
                {
                    // Error Handling
                }
            }
            if (searchTermElements.Length > 2)
            {
                if (Int32.TryParse(searchTermElements[2], out number))
                    Patch = number;
                else
                {
                    // Error Handling
                }
            }
        }
    }
}
