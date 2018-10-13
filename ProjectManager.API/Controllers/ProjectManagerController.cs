
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProjectManager.BusinessLayer;
using ProjectManager.Entity;

namespace ProjectManager.API.Controllers
{
    public class ProjectManagerController : ApiController
    {
        private IProjectManagerBL _ProjectManagerService;

        public ProjectManagerController(IProjectManagerBL ProjectManagerService)
        {
            _ProjectManagerService = ProjectManagerService;
        }

        //[HttpGet]
        //public IHttpActionResult Get()
        //{
        //    return Ok();
        //}

        [Route("AddUser")]
        public IHttpActionResult AddUser([FromBody]UserEntity user)
        {
            _ProjectManagerService.AddUser(user);
            return Ok();
        }

        [Route("GetUser/{UserId}")]
        public IHttpActionResult GetUser(int UserId)
        {
            var getUser = _ProjectManagerService.GetUser(UserId);
            return Ok(getUser);
        }

        [Route("UpdateUser")]
        public IHttpActionResult UpdateUser([FromBody]UserEntity user)
        {
            _ProjectManagerService.UpdateUser(user);
            return Ok();
        }

        [Route("GetAllUsers")]
        public IHttpActionResult GetAllUsers()
        {
            var allUsers = _ProjectManagerService.GetAllUsers();
            return Ok(allUsers);
        }

        [Route("AddProject")]
        public IHttpActionResult AddProject([FromBody]ProjectEntity project)
        {
            _ProjectManagerService.AddProject(project);
            return Ok();
        }

        [Route("GetProject/{projectId}")]
        public IHttpActionResult GetProject(int projectId)
        {
            var project = _ProjectManagerService.GetProject(projectId);
            return Ok(project);
        }

        [Route("UpdateProject")]
        public IHttpActionResult UpdateProject([FromBody]ProjectEntity project)
        {
            _ProjectManagerService.UpdateProject(project);
            return Ok();
        }

        [Route("GetAllProjects")]
        public IHttpActionResult GetAllProjects()
        {
            var allProjects = _ProjectManagerService.GetAllProjects();
            return Ok(allProjects);
        }


        [Route("AddParentTask")]
        public IHttpActionResult AddParentTask([FromBody]ParentTaskEntity parentTask)
        {
            _ProjectManagerService.AddParentTask(parentTask);
            return Ok();
        }


        [Route("AddTask")]
        public IHttpActionResult AddTask([FromBody]TaskEntity task)
        {
            _ProjectManagerService.AddTask(task);
            return Ok();
        }

        [Route("GetTask/{taskId}")]
        public IHttpActionResult GetTask(int taskId)
        {
            var Task = _ProjectManagerService.GetTask(taskId);
            return Ok(Task);
        }

        [Route("UpdateTask")]
        public IHttpActionResult UpdateTask([FromBody]TaskEntity task)
        {
            _ProjectManagerService.UpdateTask(task);
            return Ok();
        }

        [Route("GetAllTasks")]
        public IHttpActionResult GetAllTasks()
        {
            var allTasks = _ProjectManagerService.GetAllTasks();
            return Ok(allTasks);
        }
        
        [Route("EndTask/{taskId}")]
        public IHttpActionResult EndTask(int taskId)
        {
            _ProjectManagerService.EndTask(taskId);
            return Ok();
        }


    }
}
