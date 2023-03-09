
using Entities;
using Entities.Models;
using Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IApplicationRepository: IRepositoryBase<Application>
    {
        IEnumerable<Application> GetAllApplications();
        IEnumerable<ApplicationViewModel> GetApplicationsWithDetails();
        Application GetApplicationById(int id);
        void CreateApplication(Application application);
        void UpdateApplication(Application application);
        void DeleteApplication(Application application);
       // void CreateApplication(Application application);
    }
}
