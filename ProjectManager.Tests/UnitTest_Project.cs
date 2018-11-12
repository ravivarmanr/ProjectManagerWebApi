using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Moq;
using ProjectManager.BusinessLayer;
using ProjectManager.DataLayer;
using ProjectManager.Entity;

namespace ProjectManager.Tests
{
    [TestFixture]
    public class UnitTest_Project
    {

        private IProjectManagerDL _mockRepository;
        private List<ProjectEntity> _projectList;

        [SetUp]
        public void initialize()
        {
            //Initialize repository
            var mockRepo = new Mock<IProjectManagerDL>();

            //create sample user data
            _projectList = new List<ProjectEntity>()
            {
                new ProjectEntity{ ProjectId=  1 , ProjectName ="Requirement Analysis"  , ProjectPriority = 2 , DateReqd =  "N" , StartDate = null , EndDate = null, ManagerId = 1 , ProjectStatus = "N" , AddDate = new DateTime(2018,9,9) ,UpdtDate =  new DateTime(2018,9,9) },
                new ProjectEntity{ ProjectId= 2 , ProjectName = "Creating Prototype" , ProjectPriority =  3 , DateReqd =  "Y" , StartDate = new DateTime(2018,9,9) , EndDate =  new DateTime(2018,9,15) , ManagerId =  1 , ProjectStatus =  "Y" , AddDate = new DateTime(2018,9,9) ,UpdtDate =  new DateTime(2018,9,9) },
                new ProjectEntity{ ProjectId= 3 , ProjectName = "Design Preparation" , ProjectPriority =  4 , DateReqd =  "Y" , StartDate = new DateTime(2018,9,10) , EndDate =  new DateTime(2018,9,20) , ManagerId =  1 , ProjectStatus =  "Y" , AddDate = new DateTime(2018,9,9) ,UpdtDate =  new DateTime(2018,9,9) },
                new ProjectEntity{ ProjectId= 4 , ProjectName = "New Project" , ProjectPriority =  11 , DateReqd =  "N" , StartDate =  null , EndDate =  null , ManagerId =  6 , ProjectStatus =  "Y" , AddDate = new DateTime(2018,9,9) ,UpdtDate =  new DateTime(2018,9,9) },
                new ProjectEntity{ ProjectId= 5 , ProjectName = "New Project 123" , ProjectPriority =  5 , DateReqd =  "Y" , StartDate = new DateTime(2018,9,15) , EndDate =  new DateTime(2018,9,25) , ManagerId =  5 , ProjectStatus =  "Y" , AddDate = new DateTime(2018,9,9) ,UpdtDate =  new DateTime(2018,9,9) }
            };

            //Setup mocking behavior

            //Get the list of all the Active Projects
            mockRepo.Setup(p => p.GetAllProjects()).Returns(_projectList);

            //Get the Project by project Id
            mockRepo.Setup(p => p.GetProject(It.IsAny<int>()))
                .Returns((int id) => _projectList.Find(p => p.ProjectId.Equals(id)));

            //Add the project
            mockRepo.Setup(p => p.AddProject(It.IsAny<ProjectEntity>()))
                .Callback((ProjectEntity project) => _projectList.Add(project));

            //Updated the project
            mockRepo.Setup(p => p.UpdateProject(It.IsAny<ProjectEntity>()))
                .Callback((ProjectEntity updatedProject) =>
                {
                    var actualProject = _projectList.Find(p => p.ProjectId.Equals(updatedProject.ProjectId));

                    actualProject.ProjectName = updatedProject.ProjectName;
                    actualProject.ProjectPriority = updatedProject.ProjectPriority;
                    actualProject.ManagerId = updatedProject.ManagerId;
                    actualProject.StartDate = updatedProject.StartDate;
                }
                );

            _mockRepository = mockRepo.Object;

        }

        [Test]
        //Test to GetAllProjects to return all the projects
        public void ShouldReturnAllProjects()
        {
            List<ProjectEntity> allProjects = _mockRepository.GetAllProjects();

            Assert.IsTrue(allProjects.Count() == 5);
            Assert.IsTrue(allProjects.ElementAt(0).ProjectId == 1);
            Assert.IsTrue(allProjects.ElementAt(1).ProjectName == "Creating Prototype");
            Assert.IsTrue(allProjects.ElementAt(2).ProjectPriority == 4);
            Assert.IsTrue(allProjects.ElementAt(2).DateReqd == "Y");
            Assert.IsTrue(allProjects.ElementAt(2).StartDate == new DateTime(2018, 9, 10));
        }

        [Test]
        //Test to get single Project
        public void ShouldReturnSingleProject()
        {
            var projectId = 2;

            ProjectEntity projectDetail = _mockRepository.GetProject(projectId);

            Assert.IsNotNull(projectDetail);
            Assert.IsTrue(projectDetail.ProjectName == "Creating Prototype");
            Assert.IsTrue(projectDetail.ProjectPriority == 3);
            Assert.IsTrue(projectDetail.ProjectStatus == "Y");
            Assert.IsTrue(projectDetail.DateReqd == "Y");
            Assert.IsTrue(projectDetail.StartDate == new DateTime(2018, 9, 9));
            Assert.IsTrue(projectDetail.EndDate == new DateTime(2018, 9, 15));

        }

        [Test]
        //Test to add the project
        public void ShouldAddProject()
        {
            var projectId = _projectList.Count() + 1;
            var projectDetail = new ProjectEntity
            {
                ProjectId = projectId,
                ProjectName = "New Project for Testing",
                ProjectPriority = 6,
                ProjectStatus = "Y",
                DateReqd = "Y",
                StartDate = new DateTime(2018, 10, 5),
                EndDate = new DateTime(2018, 12, 7),
                ManagerId = 3,
                AddDate = DateTime.Now
            };

            _mockRepository.AddProject(projectDetail);

            ProjectEntity addedProject = _mockRepository.GetProject(projectId);
            Assert.IsTrue(_projectList.Count() == 6);
            Assert.IsNotNull(addedProject);
            Assert.AreSame(addedProject.GetType(), typeof(ProjectEntity));
            Assert.AreEqual(addedProject.ProjectId, projectId);
            Assert.IsTrue(projectDetail.ProjectName == addedProject.ProjectName);
            Assert.IsTrue(projectDetail.ManagerId == addedProject.ManagerId);

        }

        [Test]
        //Test to update the project
        public void ShouldUpdateProject()
        {
            var projectId = 2;

            var projectDetail = new ProjectEntity
            {
                ProjectId = projectId,
                ProjectName = "Updated Project Prototype",
                StartDate = new DateTime(2018, 11, 25),
                ProjectPriority = 5,
                ManagerId = 3
            };

            _mockRepository.UpdateProject(projectDetail);

            var updatedProject = _mockRepository.GetProject(projectId);
            Assert.IsTrue(projectDetail.ProjectName == updatedProject.ProjectName);
            Assert.IsTrue(projectDetail.ManagerId == updatedProject.ManagerId);
            Assert.IsTrue(projectDetail.ProjectPriority == updatedProject.ProjectPriority);
            Assert.IsTrue(projectDetail.StartDate == updatedProject.StartDate);
        }


        [TearDown]
        public void CleanUp()
        {
            _projectList.Clear();
        }

    }
}
