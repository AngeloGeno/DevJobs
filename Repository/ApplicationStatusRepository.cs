using Contracts;

using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ApplicationStatusRepository : RepositoryBase<ApplicationStatus>, IApplicationStatusRepository
    {
        public ApplicationStatusRepository(JobsOnLineContext repositoryContext) : base(repositoryContext)
        {
        }
    }
}
