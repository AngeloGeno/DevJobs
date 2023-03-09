
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;

using Repository;
using Entities.ViewModels;
using Microsoft.EntityFrameworkCore;
using Entities.Models;

namespace Repository
{
    public class ApplicantRepository : RepositoryBase<Applicant>, IApplicantRepository  
    {
        private readonly JobsOnLineContext _context;
        public ApplicantRepository(JobsOnLineContext repositoryContext) : base(repositoryContext)
        {
            _context = repositoryContext;
        }                                                       

        public IEnumerable<Applicant> GetAllApplicants()
        {
            return FindAll().OrderBy(a => a.Name).ToList();
            
        }

        public IEnumerable<ApplicantViewModel> ApplicantsWithQualificationName()
        {                           
            return (from b in _context.Applicants
                    join c in _context.QualificationLevels
                    on b.QualificationLevelId equals c.QualificationLevelId
                    select new ApplicantViewModel
                    {
                        Applicant = b,
                        QualificationLevelName = c.QualificationLevelName
                    })?.ToList();

        }
        public Applicant GetApplicantById(int applicantID)
        {

            return FindByCondition(applicant => applicant.ApplicantId.Equals(applicantID)).FirstOrDefault();

        }

        public ApplicantViewModel GetApplicantDeatailsByID(int applicantID)
        { var app = from b in _context.Applicants
                    join c in _context.QualificationLevels
                    on b.QualificationLevelId
                    equals c.QualificationLevelId where b.ApplicantId.Equals(applicantID) select new ApplicantViewModel { Applicant = b, QualificationLevelName = c.QualificationLevelName };

            return app.FirstOrDefault();
        }


        public void CreateApplicant(Applicant applicant)
        { 
             Create(applicant);
        }

        public void UpdateApplicant(Applicant applicant)
        {
            Update(applicant);
        }
        public void DeleteApplicant(Applicant applicant)
        {

            var app = (from b in _context.Applicants
                       where b.ApplicantId.Equals(applicant.ApplicantId)
                       select b)?.FirstOrDefault();

            var del = (from a in _context.Applications
                      where a.ApplicantId.Equals(app.ApplicantId)
                      select a)?.DefaultIfEmpty();

            //int deletedRecordsCount = del.ExecuteDelete();
            //Delete(app);

        }
    }
}
