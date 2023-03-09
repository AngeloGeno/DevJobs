
using Contracts;

using Entities.Models;
using Moq;
using DevJobsWeb.Controllers;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.Formula.Functions;

namespace Tests.Controllers
{
    public class ApplicantsControllerTest
    {
        private readonly Mock<IRepositoryWrapper> _mockRepo;
        private readonly ApplicantController _controller;

        public ApplicantsControllerTest()
        {
           _mockRepo = new Mock<IRepositoryWrapper>();
            _controller = new ApplicantController(_mockRepo.Object);
        }

        [Fact]
        public void Index_ActionExecutes_ReturnsViewForIndex()
        {
            var result = _controller.Index();
            Assert.IsType<ViewResult>(result);

        }

        [Fact]
        public void Create_InvaInvalidModelState_CreateApplicantNeverExecutes()
        {
            _controller.ModelState.AddModelError("Name","Name is Required");
            var applicant = new Applicant { LastName = "Smirth" };
            _controller.Create(applicant);

            _mockRepo.Verify(x => x.Applicant.CreateApplicant(It.IsAny<Applicant>()), Times.Never);
        }
        [Fact]
        public void Create_ActionExecutes_ReturnsViewForIndex()
        {
            var result = _controller.Create();
            Assert.IsType<ViewResult>(result);

        }

        [Fact]
        public void CreateModelStateValid_CreateEmployeeCalledOnce()
        { 
            Applicant? app = null;

            _mockRepo.Setup(r => r.Applicant.CreateApplicant(It.IsAny<Applicant>()))
                .Callback<Applicant>(x => app = x);

            var applicant = new Applicant
            {
                ApplicantId = 34,
                Name = "Test Appliant",
                LastName = "Test LastName",
                Gender = "Male",
                Address = "Address",
                QualificationLevelId = 1,
            };
                _controller.Create(applicant);

                _mockRepo.Verify(x => x.Applicant.CreateApplicant(It.IsAny<Applicant>()), Times.Once);
            Assert.Equal(app.ApplicantId, applicant.ApplicantId);
            Assert.Equal(app.Name, applicant.Name);
            Assert.Equal(app.LastName, applicant.LastName);
            Assert.Equal(app.Gender, applicant.Gender);
            Assert.Equal(app.Address, applicant.Address);
            Assert.Equal(app.QualificationLevelId, applicant.QualificationLevelId);

        }

        public void Edit_ActionExecutes_ReturnsViewForIndex(int id)
        {
            var result = _controller.Edit(id);
            Assert.IsType<ViewResult>(result);

        }
          //Review
        public void Edit_InvaInvalidModelState_CreateApplicantNeverExecutes(int id)
        {
            _controller.ModelState.AddModelError("Name", "Name is Required");
            var applicant = new Applicant { LastName = "Smirth" };
            _controller.Edit(id);

            _mockRepo.Verify(x => x.Applicant.UpdateApplicant(It.IsAny<Applicant>()), Times.Never);
        }
     
        




    }
}
