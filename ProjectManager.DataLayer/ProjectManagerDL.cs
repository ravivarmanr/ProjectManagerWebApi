using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.Entity;

namespace ProjectManager.DataLayer
{
    public class ProjectManagerDL : IProjectManagerDL
    {
        ProjectManagerEntities _DBContext;

        public ProjectManagerDL()
        {
            _DBContext = new ProjectManagerEntities();
        }


        public void AddUser(UserEntity user)
        {
            var userParam = new UserData();

            var userId = _DBContext.UserDatas.Max(u => u.UserId) + 1;

            userParam.UserId = userId;
            userParam.FirstName = user.FirstName;
            userParam.LastName = user.LastName;
            userParam.EmpId = user.EmpId;
            userParam.UserStatus = user.UserStatus;
            userParam.AddDate = user.AddDate;
            userParam.UpdtDate = user.UpdtDate;

            _DBContext.UserDatas.Add(userParam);
            _DBContext.SaveChanges();

        }

        public UserEntity GetUser(int userId)
        {
            var user = (from u in _DBContext.UserDatas
                        where u.UserId == userId
                        select new UserEntity
                        {
                            UserId = u.UserId,
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            EmpId = u.EmpId,
                            UserStatus = u.UserStatus,
                            AddDate = u.AddDate,
                            UpdtDate = u.UpdtDate
                        }
                             ).SingleOrDefault();

            return user;

        }

        public void UpdateUser(UserEntity user)
        {
            var userParam = (from u in _DBContext.UserDatas
                             where u.UserId == user.UserId
                             select u
                             ).FirstOrDefault();

            userParam.UserId = user.UserId;
            userParam.FirstName = user.FirstName;
            userParam.LastName = user.LastName;
            userParam.EmpId = user.EmpId;
            userParam.UserStatus = user.UserStatus;
            //userParam.AddDate = user.AddDate;
            userParam.UpdtDate = user.UpdtDate;

            _DBContext.SaveChanges();
        }

        public List<UserEntity> GetAllUsers()
        {
            var user = (from u in _DBContext.UserDatas
                        select new UserEntity
                        {
                            UserId = u.UserId,
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            EmpId = u.EmpId,
                            UserStatus = u.UserStatus,
                            AddDate = u.AddDate,
                            UpdtDate = u.UpdtDate
                        }).ToList();

            return user;
        }

        public void AddProject(ProjectEntity project)
        {
            var projParam = new Project();
            var projectId = _DBContext.Projects.Max(p => p.ProjectId) + 1;

            projParam.ProjectId = projectId;
            projParam.ProjectName = project.ProjectName;
            projParam.ProjectPriority = project.ProjectPriority;
            projParam.DateReqd = project.DateReqd;
            projParam.StartDate = project.StartDate;
            projParam.EndDate = project.EndDate;
            projParam.ProjectStatus = project.ProjectStatus;
            projParam.AddDate = project.AddDate;
            projParam.UpdtDate = project.UpdtDate;
            
            _DBContext.Projects.Add(projParam);
            _DBContext.SaveChanges();

        }

        public ProjectEntity GetProject(int projectId)
        {
            var project = (from p in _DBContext.Projects
                           join u in _DBContext.UserDatas on p.ManagerId equals u.UserId
                           into proj
                           where p.ProjectId == projectId
                           from tProj in proj.DefaultIfEmpty()

                           select new ProjectEntity
                           {
                               ProjectId = p.ProjectId,
                               ProjectName = p.ProjectName,
                               ProjectPriority = p.ProjectPriority,
                               DateReqd = p.DateReqd,
                               StartDate = p.StartDate,
                               EndDate = p.EndDate,
                               ProjectStatus = p.ProjectStatus,
                               ManagerId = p.ManagerId,
                               ManagerName = tProj.FirstName + " " + tProj.LastName
                           }).SingleOrDefault();

            return project;
        }

        public void UpdateProject(ProjectEntity project)
        {
            var projParam = (from p in _DBContext.Projects
                             where p.ProjectId == project.ProjectId
                             select p).FirstOrDefault();

            projParam.ProjectId = project.ProjectId;
            projParam.ProjectName = project.ProjectName;
            projParam.ProjectPriority = project.ProjectPriority;
            projParam.DateReqd = project.DateReqd;
            projParam.StartDate = project.StartDate;
            projParam.EndDate = project.EndDate;
            projParam.ProjectStatus = project.ProjectStatus;
            //projParam.AddDate = project.AddDate;
            projParam.UpdtDate = project.UpdtDate;

            _DBContext.SaveChanges();

        }

        public List<ProjectEntity> GetAllProjects()
        {
            var allProjects = (from p in _DBContext.Projects
                           join u in _DBContext.UserDatas on p.ManagerId equals u.UserId
                           into proj
                           from tProj in proj.DefaultIfEmpty()

                           select new ProjectEntity
                           {
                               ProjectId = p.ProjectId,
                               ProjectName = p.ProjectName,
                               ProjectPriority = p.ProjectPriority,
                               DateReqd = p.DateReqd,
                               StartDate = p.StartDate,
                               EndDate = p.EndDate,
                               ProjectStatus = p.ProjectStatus,
                               ManagerId = p.ManagerId,
                               ManagerName = tProj.FirstName + " " + tProj.LastName
                           }
                           ).ToList();

            return allProjects;
        }


        public void AddParentTask(ParentTaskEntity parentTask)
        {
            var parentTaskParam = new ParentTask();
            var parentTaskId = -_DBContext.ParentTasks.Max(p => p.ParentId) + 1;

            parentTaskParam.ParentId = parentTaskId;
            parentTaskParam.ParentTask1 = parentTask.ParentTask;
            parentTaskParam.ParentStatus = parentTask.ParentStatus;
            parentTaskParam.AddDate = parentTask.AddDate;
            parentTaskParam.UpdtDate = parentTask.UpdtDate;
        }


        public void AddTask(TaskEntity task)
        {
            var taskParam = new Task();
            var taskId = _DBContext.Tasks.Max(t => t.TaskId) + 1;

            taskParam.TaskId = taskId;
            taskParam.Task1 = task.Task;
            taskParam.TaskPriority = task.TaskPriority;
            taskParam.StartDate = task.StartDate;
            taskParam.EndDate = task.EndDate;
            taskParam.TaskStatus = task.TaskStatus;
            taskParam.ProjectId = task.ProjectId;
            taskParam.ParentId = task.ParentId;
            taskParam.UserId = task.UserId;
            taskParam.AddDate = task.AddDate;
            taskParam.UpdtDate = task.UpdtDate;

            _DBContext.Tasks.Add(taskParam);
            _DBContext.SaveChanges();

        }

        public TaskEntity GetTask(int taskId)
        {

            var task = (from t in _DBContext.Tasks
                        join p in _DBContext.ParentTasks on t.ParentId equals p.ParentId
                        join u in _DBContext.UserDatas on t.UserId equals u.UserId
                        into projTask
                        where t.TaskId == taskId
                        from ptask in projTask.DefaultIfEmpty()

                        select new TaskEntity
                        {
                            TaskId = t.TaskId,
                            Task = t.Task1,
                            TaskPriority = t.TaskPriority,
                            StartDate = t.StartDate,
                            EndDate = t.EndDate,
                            TaskStatus = t.TaskStatus,
                            ProjectId = t.ProjectId,
                            ParentId = t.ParentId,
                            UserId = t.UserId,
                            AddDate = t.AddDate,
                            UpdtDate = t.UpdtDate,
                            ParentTask = p.ParentTask1,
                            UserName = ptask.FirstName + " " + ptask.LastName

                        }).SingleOrDefault();

            return task;
        }

        public void UpdateTask(TaskEntity task)
        {
            var taskParam = (from t in _DBContext.Tasks
                             where t.TaskId == task.TaskId
                             select t).FirstOrDefault();

            taskParam.TaskId = task.TaskId;
            taskParam.Task1 = task.Task;
            taskParam.TaskPriority = task.TaskPriority;
            taskParam.StartDate = task.StartDate;
            taskParam.EndDate = task.EndDate;
            taskParam.TaskStatus = task.TaskStatus;
            taskParam.ProjectId = task.ProjectId;
            taskParam.ParentId = task.ParentId;
            taskParam.UserId = task.UserId;
            //taskParam.AddDate = task.AddDate;
            taskParam.UpdtDate = task.UpdtDate;

            _DBContext.SaveChanges();
        }

        public List<TaskEntity> GetAllTasks()
        {
            var allTaskList = (from t in _DBContext.Tasks
                               join p in _DBContext.ParentTasks on t.ParentId equals p.ParentId
                               join u in _DBContext.UserDatas on t.UserId equals u.UserId
                               into projTask
                               from ptask in projTask.DefaultIfEmpty()

                               select new TaskEntity
                               {
                                   TaskId = t.TaskId,
                                   Task = t.Task1,
                                   TaskPriority = t.TaskPriority,
                                   StartDate = t.StartDate,
                                   EndDate = t.EndDate,
                                   TaskStatus = t.TaskStatus,
                                   ProjectId = t.ProjectId,
                                   ParentId = t.ParentId,
                                   UserId = t.UserId,
                                   AddDate = t.AddDate,
                                   UpdtDate = t.UpdtDate,
                                   ParentTask = p.ParentTask1,
                                   UserName = ptask.FirstName + " " + ptask.LastName

                               }).ToList();

            return allTaskList;
        }

        public void EndTask(int taskId)
        {

            var taskParam = (from t in _DBContext.Tasks
                             where t.TaskId == taskId
                             select t).FirstOrDefault();

            taskParam.EndDate = DateTime.Now;
            taskParam.UpdtDate = DateTime.Now;
            taskParam.TaskStatus = "N";

            _DBContext.SaveChanges();
        }
    }
}
