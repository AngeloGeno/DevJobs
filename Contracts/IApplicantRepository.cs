using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Entities.Models;
using Entities.ViewModels;

namespace Contracts
{
    public interface IApplicantRepository:IRepositoryBase<Applicant>
    {
        IEnumerable<Applicant> GetAllApplicants();
        Applicant GetApplicantById(int applicantID);
        
        void CreateApplicant(Applicant applicant);

        void UpdateApplicant(Applicant applicant );
        void DeleteApplicant(Applicant applicant);
        public IEnumerable<ApplicantViewModel> ApplicantsWithQualificationName();

        ApplicantViewModel GetApplicantDeatailsByID(int applicantID);

    }

}
