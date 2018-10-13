
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
        
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok();
        }

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
            var getAllUsers = _ProjectManagerService.GetAllUsers();
            return Ok(getAllUsers);
        }

        

        //[Route("GetProjects")]
        //public IHttpActionResult GetProjects()
        //{
        //    return Ok();
        //}

        //[Route("GetProjectById/{Id}")]
        //public IHttpActionResult GetProject(int Id)
        //{
        //    return Ok();
        //}

        //[Route("AddProject")]
        //public IHttpActionResult AddProject()
        //{
        //    return Ok();
        //}

        //[Route("GetTasks")]
        //public IHttpActionResult GetTasks()
        //{
        //    return Ok();
        //}

        //[Route("GetTaskById/{Id}")]
        //public IHttpActionResult GetTask(int Id)
        //{
        //    return Ok();
        //}

        


    }
}
