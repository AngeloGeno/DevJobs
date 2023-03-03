using Contracts;
using DevJobsWeb.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Controllers
{
    public class JobsControllerTest
    {
        private readonly Mock<IRepositoryWrapper> _mockRepo;
        private readonly JobController _controller;

        public JobsControllerTest()
        {
            _mockRepo = new Mock<IRepositoryWrapper>();
            _controller = new JobController(_mockRepo.Object);


        }

        [Fact]
        public void Index_ActionExecutes_ReturnsViewForIndex()
        {
            var result = _controller.Index();
            Assert.IsType<ViewResult>(result);

        }


    }
}
