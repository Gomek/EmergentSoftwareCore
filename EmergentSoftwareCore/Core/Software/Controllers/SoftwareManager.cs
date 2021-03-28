using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EmergentSoftwareCore
{
    public class SoftwareManager
    {
        public static Tuple<bool, IEnumerable<SoftwareSearch>> GetFilteredSoftwareSearch(string filter)
        {
            bool valid = true;
            int majorVersion = 0;
            int minorVersion = 0;
            int patch = 0;

            string[] filterElements = filter.Split(UtilityManager.GetVersionDelimiter());
            IEnumerable<SoftwareSearch> filteredSoftwareSearchList = new List<SoftwareSearch>();

            if (filter.Trim().Length > 0)
            {
                if (filterElements.Length > 0)
                {
                    if (!Int32.TryParse(filterElements[0], out majorVersion))
                    {
                        // Error Handling, check with business owner how they want handled
                        valid = false;
                    }
                }
                if (filterElements.Length > 1)
                {
                    if (!Int32.TryParse(filterElements[1], out minorVersion))
                    {
                        // Error Handling
                        valid = false;
                    }
                }
                if (filterElements.Length > 2)
                {
                    if (!Int32.TryParse(filterElements[2], out patch))
                    {
                        // Error Handling
                        valid = false;
                    }
                }
            }

            if (valid)
            {
                IEnumerable<SoftwareSearch> softwareSearchList = GetAllSoftwareSearch();
                filteredSoftwareSearchList = softwareSearchList.Where(item => item.MajorVersion > majorVersion)
                    .Union(softwareSearchList.Where(item => item.MajorVersion == majorVersion && item.MinorVersion > minorVersion))
                    .Union(softwareSearchList.Where(item => item.MajorVersion == majorVersion && item.MinorVersion == minorVersion && item.Patch > patch))
                    .OrderBy(item => item.MajorVersion).ThenBy(item => item.MinorVersion).ThenBy(item => item.Patch).ThenBy(item => item.Name);
            }

            return new Tuple<bool, IEnumerable<SoftwareSearch>>(valid, filteredSoftwareSearchList);
        }

        public static List<SoftwareSearch> GetAllSoftwareSearch()
        {
            List<SoftwareSearch> softwareSearchList = new List<SoftwareSearch>();
            string cacheKey = "GetSoftwareSearch";

            if (CacheUtilities.CacheContainsItem(cacheKey))
                softwareSearchList = (List<SoftwareSearch>)CacheUtilities.GetCacheItem(cacheKey);
            else
                softwareSearchList = GetAllSoftwareSearchCache(cacheKey);

            return softwareSearchList;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private static List<SoftwareSearch> GetAllSoftwareSearchCache(string cacheKey)
        {
            List<SoftwareSearch> softwareSearchList = new List<SoftwareSearch>();

            if (CacheUtilities.CacheContainsItem(cacheKey))
                softwareSearchList = (List<SoftwareSearch>)CacheUtilities.GetCacheItem(cacheKey);
            else
            {
                foreach (Software software in GetAllSoftware())
                    softwareSearchList.Add(new SoftwareSearch(software));

                CacheUtilities.AddCacheItem(cacheKey, softwareSearchList);
            }

            return softwareSearchList;
        }

        public static IEnumerable<Software> GetAllSoftware()
        {
            return new List<Software>
            {
                new Software
                {
                    Name = "MS Word",
                    Version = "13.2.1."
                },
                new Software
                {
                    Name = "AngularJS",
                    Version = "1.7.1"
                },
                new Software
                {
                    Name = "Angular",
                    Version = "8.1.13"
                },
                new Software
                {
                    Name = "React",
                    Version = "0.0.5"
                },
                new Software
                {
                    Name = "Vue.js",
                    Version = "2.6"
                },
                new Software
                {
                    Name = "Visual Studio",
                    Version = "2017.0.1"
                },
                new Software
                {
                    Name = "Visual Studio",
                    Version = "2019.1"
                },
                new Software
                {
                    Name = "Visual Studio Code",
                    Version = "1.35"
                },
                new Software
                {
                    Name = "Blazor",
                    Version = "0.7"
                },

                // For testing
             /*   new Software { Name = "Test", Version = "1.1.1" },
                new Software { Name = "Test", Version = "1.1.2" },
                new Software { Name = "Test", Version = "1.1.3" },
                new Software { Name = "Test", Version = "1.2.1" },
                new Software { Name = "Test", Version = "1.2.2" },
                new Software { Name = "Test", Version = "1.2.3" },
                new Software { Name = "Test", Version = "1.3.1" },
                new Software { Name = "Test", Version = "1.3.2" },
                new Software { Name = "Test", Version = "1.3.3" },
                new Software { Name = "TestCase", Version = "1.3.1" },
                new Software { Name = "TestCase", Version = "1.3.2" },
                new Software { Name = "TestCase", Version = "1.3.3" },
                new Software { Name = "TestCase", Version = "2.1.1" },
                new Software { Name = "TestCase", Version = "2.1.2" },
                new Software { Name = "TestCase", Version = "2.1.3" } */
            };
        }
    }
}
