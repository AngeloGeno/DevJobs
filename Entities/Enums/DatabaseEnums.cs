using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Enums
{
    public class DatabaseEnums
    {
        public enum QualificationLevel
        {
           HigherCertificate = 1,
           Diploma = 2,
           Degree = 3,
           Masters = 4,

        }
        public enum ApplicationStatus
        {
           Submitted = 1,
           Processed = 2,
            Succesful = 3, 
            Rejected = 4,

        }


    }
}
