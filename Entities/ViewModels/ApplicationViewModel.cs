using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevJobsWeb;

namespace Entities.ViewModels
{
    public class ApplicationViewModel
    {
        public Application Application { get; set; }
        public string Name { get; set; } 
        public string Lastname { get; set; }    
        public string JobTitle { get; set; }    
        public string JobPosition   { get; set; }

        public string ApplicationStatus { get; set; }  
        
       



    }
}
