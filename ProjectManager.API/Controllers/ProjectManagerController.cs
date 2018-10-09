using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProjectManager.API.Controllers
{
    public class ProjectManagerController : ApiController
    {

        [Route("GetProjects")]
        public IHttpActionResult GetProjects()
        {
            return Ok();
        }

        [Route("GetProjectById/{Id}")]
        public IHttpActionResult GetProject(int Id)
        {
            return Ok();
        }

        [Route("AddProject")]
        public IHttpActionResult AddProject()
        {
            return Ok();
        }

        [Route("GetTasks")]
        public IHttpActionResult GetTasks()
        {
            return Ok();
        }

        [Route("GetTaskById/{Id}")]
        public IHttpActionResult GetTask(int Id)
        {
            return Ok();
        }

        [Route("GetUsers")]
        public IHttpActionResult GetUsers()
        {
            return Ok();
        }

        [Route("GetUserById/{Id}")]
        public IHttpActionResult GetUserById(int Id)
        {
            return Ok();
        }


    }
}
