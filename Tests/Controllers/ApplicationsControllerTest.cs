using Contracts;
using DevJobsWeb.Controllers;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Entities;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Tests.Controllers
{
    public  class ApplicationControllerTest
    {
        private readonly Mock<IRepositoryWrapper> _mockRepo;
        private readonly ApplicationController _controller;

        public ApplicationControllerTest()
        {
            _mockRepo = new Mock<IRepositoryWrapper>();
            _controller = new ApplicationController(_mockRepo.Object);

            
        }

        [Fact]
        public void Index_ActionExecutes_ReturnsViewForIndex()
        {
            var result = _controller.Index();
            Assert.IsType<ViewResult>(result);

        }


    }
}
