using Contracts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;


namespace Repository
{
    public class QualificationLevelRepository : RepositoryBase<QualificationLevel>, IQualificationLevelRepository
    {
        public QualificationLevelRepository(JobsOnLineContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
