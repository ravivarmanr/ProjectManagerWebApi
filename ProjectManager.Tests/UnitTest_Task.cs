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
    public class UnitTest_Task
    {
        private IProjectManagerDL _mockRepository;
        private List<TaskEntity> _taskList;
        private List<ParentTaskEntity> _parentTaskList;

        [SetUp]
        public void initialize()
        {
            // Initialize repository
            var mockRepo = new Mock<IProjectManagerDL>();

            //create sample data

            _parentTaskList = new List<ParentTaskEntity>()
            {
                new ParentTaskEntity{ParentId = 1, ParentTask = "Test project Creation", ParentStatus = "Y", AddDate=new DateTime(2018,9,9), UpdtDate=new DateTime(2018,9,9) },
                new ParentTaskEntity{ParentId = 2, ParentTask = "Project Testing", ParentStatus = "Y", AddDate=new DateTime(2018,9,9), UpdtDate=new DateTime(2018,9,9) },
                new ParentTaskEntity{ParentId = 3, ParentTask = "Repository Creation", ParentStatus = "Y", AddDate=new DateTime(2018,9,9), UpdtDate=new DateTime(2018,9,9) },
            };

            _taskList = new List<TaskEntity>()
            {
                new TaskEntity{TaskId=1, Task="Test project Creation", ParentId=1, ParentTask="Test project Creation", TaskPriority=1, TaskStatus="Y", StartDate= new DateTime(2018,9,9), EndDate=new DateTime(2018,9,9), ProjectId = 1, UserId = 2, AddDate=new DateTime(2018,9,9), UpdtDate=new DateTime(2018,9,9) },
                new TaskEntity{TaskId=2, Task="Setup Creation", ParentId=1, ParentTask="Test project Creation", TaskPriority=2, TaskStatus="Y", StartDate= new DateTime(2018,9,9), EndDate=new DateTime(2018,9,9), ProjectId = 2, UserId = 1, AddDate=new DateTime(2018,9,9), UpdtDate=new DateTime(2018,9,9) },
                new TaskEntity{TaskId=3, Task="Repository Creation", ParentId=1, ParentTask="Test project Creation", TaskPriority=2, TaskStatus="Y", StartDate= new DateTime(2018,9,9), EndDate=new DateTime(2018,9,9), ProjectId = 3, UserId = 4, AddDate=new DateTime(2018,9,9), UpdtDate=new DateTime(2018,9,9) },
                new TaskEntity{TaskId=4, Task="Test case creation", ParentId=3, ParentTask="Repository Creation", TaskPriority=3, TaskStatus="Y", StartDate= new DateTime(2018,9,9), EndDate=new DateTime(2018,9,10), ProjectId = 1, UserId = 1, AddDate=new DateTime(2018,9,9), UpdtDate=new DateTime(2018,9,9) },
                new TaskEntity{TaskId=5, Task="Test case execution", ParentId=3, ParentTask="Repository Creation", TaskPriority=4, TaskStatus="N", StartDate= new DateTime(2018,9,9), EndDate=new DateTime(2018,9,9), ProjectId = 2, UserId = 5, AddDate=new DateTime(2018,9,9), UpdtDate=new DateTime(2018,9,9) },
            };

            // Setup mocking behavior

            //Get the list of all the parent tasks
            mockRepo.Setup(p => p.GetAllParentTasks()).Returns(_parentTaskList);

            //Add the parent task
            mockRepo.Setup(p => p.AddParentTask(It.IsAny<ParentTaskEntity>()))
                .Callback((ParentTaskEntity parentTask) => _parentTaskList.Add(parentTask));

            
            //Get the list of all the tasks
            mockRepo.Setup(p => p.GetAllTasks()).Returns(_taskList);

            //Get the list of all the tasks by project Id
            mockRepo.Setup(p => p.GetAllTasks(It.IsAny<int>()))
                .Returns((int projectId) => _taskList.Where(p => p.ProjectId == projectId).ToList());


            //Get the one particular task
            mockRepo.Setup(p => p.GetTask(It.IsAny<int>()))
            .Returns((int id) => _taskList.Find(p => p.TaskId.Equals(id)));

            //Add the task
            mockRepo.Setup(p => p.AddTask(It.IsAny<TaskEntity>()))
                .Callback((TaskEntity task) => _taskList.Add(task));

            //Update the task
            mockRepo.Setup(p => p.UpdateTask(It.IsAny<TaskEntity>()))
                .Callback((TaskEntity updatedTask) =>
                {
                    var actualTask = _taskList.Find(p => p.TaskId.Equals(updatedTask.TaskId));

                    actualTask.Task = updatedTask.Task;
                    actualTask.ParentId = updatedTask.ParentId;
                    actualTask.TaskPriority = updatedTask.TaskPriority;
                    actualTask.StartDate = updatedTask.StartDate;
                    actualTask.EndDate = updatedTask.EndDate;
                    actualTask.TaskStatus = updatedTask.TaskStatus;
                    actualTask.UserId = updatedTask.UserId;
                }
                );

            //End the Task
            mockRepo.Setup(p => p.EndTask(It.IsAny<int>()))
                .Callback((int taskId) =>
                {
                    var actualTask = _taskList.Find(p => p.TaskId.Equals(taskId));

                    actualTask.TaskStatus = "N";
                }
                );

            _mockRepository = mockRepo.Object;

        }

        [Test]
        //Test Get All Parent Tasks
        public void ShouldReturnAllParentTask()
        {
            List<ParentTaskEntity> parentList = _mockRepository.GetAllParentTasks();

            Assert.IsTrue(parentList.Count() == 3);
            Assert.IsTrue(parentList.ElementAt(1).ParentId == 2);
            Assert.IsTrue(parentList.ElementAt(1).ParentTask == "Project Testing");
        }

        [Test]
        //Add the Parent Task
        public void ShouldAddParentTask()
        {
            var parentId= _parentTaskList.Count() + 1;
            var parentDetail = new ParentTaskEntity
            {
                ParentId = parentId,
                ParentTask = "Add New Parent",
                ParentStatus = "Y",
                AddDate = new DateTime(2018, 9, 10),
                UpdtDate = new DateTime(2018, 9, 11)
            };

            _mockRepository.AddParentTask(parentDetail);

            ParentTaskEntity addedParent = GetParentTask(parentId);
            Assert.IsTrue(_parentTaskList.Count() == 4);
            Assert.IsNotNull(addedParent);
            Assert.AreSame(addedParent.GetType(), typeof(ParentTaskEntity));
            Assert.AreEqual(parentId, addedParent.ParentId);
            Assert.IsTrue(parentDetail.ParentTask == addedParent.ParentTask);
        }

        [Test]
        //Test GetAll tasks to return all tasks
        public void ShouldReturnAllTask()
        {
            List<TaskEntity> taskList = _mockRepository.GetAllTasks();

            Assert.IsTrue(taskList.Count() == 5);
            Assert.IsTrue(taskList.ElementAt(0).Task == "Test project Creation");
            Assert.IsTrue(taskList.ElementAt(1).ParentId == 1);
            Assert.IsTrue(taskList.ElementAt(4).TaskStatus == "N");
            Assert.IsTrue(taskList.ElementAt(2).TaskPriority == 2);
            Assert.IsTrue(taskList.ElementAt(3).StartDate == new DateTime(2018, 9, 9));
            Assert.IsTrue(taskList.ElementAt(3).EndDate == new DateTime(2018, 9, 10));

        }

        [Test]
        //Test GetTask to return the task details of requested task id
        public void ShouldReturnSingleTask()
        {
            var taskId = 2;

            TaskEntity taskDetail = _mockRepository.GetTask(taskId);

            Assert.IsNotNull(taskDetail);
            Assert.IsTrue(taskDetail.Task== "Setup Creation");
            Assert.IsTrue(taskDetail.ParentTask== "Test project Creation");
            Assert.IsTrue(taskDetail.TaskPriority == 2);
            Assert.IsTrue(taskDetail.TaskStatus == "Y");
            Assert.IsTrue(taskDetail.StartDate == new DateTime(2018, 9, 9));
            Assert.IsTrue(taskDetail.EndDate == new DateTime(2018, 9, 9));
        }

        [Test]
        // Test Add Task
        public void ShouldAddTask()
        {
            var taskId = _taskList.Count() + 1;
            var taskDetail = new TaskEntity
            {
                TaskId = taskId,
                Task= "testing the Test case",
                ParentId = 3,
                TaskPriority = 5,
                TaskStatus = "Y",
                StartDate = new DateTime(2018, 9, 10),
                EndDate = new DateTime(2018, 9, 11)
            };

            _mockRepository.AddTask(taskDetail);

            TaskEntity addedTask = _mockRepository.GetTask(taskId);
            Assert.IsTrue(_taskList.Count() == 6);
            Assert.IsNotNull(addedTask);
            Assert.AreSame(addedTask.GetType(), typeof(TaskEntity));
            Assert.AreEqual(taskId, addedTask.TaskId);
            Assert.IsTrue(taskDetail.Task== addedTask.Task);
        }

        [Test]
        // Test update task functionality
        public void ShouldUpdateTask()
        {
            var taskId = 3;
            var taskDetail = new TaskEntity
            {
                TaskId = taskId,
                Task = "Update Repository Creation", //"Repository Creation",
                ParentId = 2, //1
                TaskPriority = 3, //2
                TaskStatus = "Y",
                StartDate = new DateTime(2018, 9, 10), //DateTime(2018, 9, 9),
                EndDate = new DateTime(2018, 10, 9) //DateTime(2018, 9, 9)
            };

            _mockRepository.UpdateTask(taskDetail);

            var UpdatedTask = _mockRepository.GetTask(taskId);

            Assert.IsTrue(UpdatedTask.Task== "Update Repository Creation");
            Assert.IsTrue(UpdatedTask.ParentId == 2);
            Assert.IsTrue(UpdatedTask.TaskPriority == 3);
            Assert.IsTrue(UpdatedTask.StartDate == new DateTime(2018, 9, 10));
            Assert.IsTrue(UpdatedTask.EndDate == new DateTime(2018, 10, 9));
        }

        [Test]
        //Test the end task functionality
        public void ShouldEndTask()
        {
            var taskId = 3;

            _mockRepository.EndTask(taskId);

            var updatedTask = _mockRepository.GetTask(taskId);

            Assert.IsTrue(updatedTask.TaskStatus == "N");
        }

        [TearDown]
        public void CleanUp()
        {
            _taskList.Clear();
            _parentTaskList.Clear();
        }

        private ParentTaskEntity GetParentTask(int ParentId)
        {
            return _parentTaskList.Where(p => p.ParentId == ParentId).Select(t => t).SingleOrDefault();
        }

    }
}
