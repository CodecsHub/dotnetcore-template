using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetcore_api.Models
{
    public class UtilityAppSettings
    {
        // @todo:   Add regex, versioning and descriptive comment the same like Models input/output documentation

        /// <summary>
        /// The Primary or main connection string intended  for read, write and read/write
        /// </summary>
        public string DatabaseConnectionRead { get; set; }
        public string DatabaseConnectionWrite { get; set; }
        public string DatabaseConnectionReadWrite { get; set; }

        /// <summary>
        /// Details of the application itself
        /// </summary>
        // @todo:   Create web assembly info add like desktop information
        public string ApplicationName { get; set; }
        public int VersionMajor { get; set; }
        public int VersionMinor { get; set; }
        public int VersionPatch { get; set; }
        public string DateCreated { get; set; }
        public string DateUpdated { get; set; }

        public string VersionNumber
        {
            get
            {
                return $"{ VersionMajor }.{ VersionMinor }.{ VersionPatch }";
            }
        }
    }
}
