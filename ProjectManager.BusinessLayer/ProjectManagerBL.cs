using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.DataLayer;
using ProjectManager.Entity;

namespace ProjectManager.BusinessLayer
{
    public class ProjectManagerBL : IProjectManagerBL
    {
        private IProjectManagerDL _projDL;

        public ProjectManagerBL (IProjectManagerDL projDL)
        {
            _projDL = projDL;
        }

        public void AddUser(UserEntity user)
        {
            _projDL.AddUser(user);
        }

        public UserEntity GetUser(int userId)
        {
            return _projDL.GetUser(userId);
        }

        public void UpdateUser(UserEntity user)
        {
            _projDL.UpdateUser(user);
        }

        public List<UserEntity> GetAllUsers()
        {
            return _projDL.GetAllUsers();
        }


        public void AddProject(ProjectEntity project)
        {
            _projDL.AddProject(project);
        }

        public ProjectEntity GetProject(int projectId)
        {
            return _projDL.GetProject(projectId);
        }

        public void UpdateProject(ProjectEntity project)
        {
            _projDL.UpdateProject(project);
        }

        public List<ProjectEntity> GetAllProjects()
        {
            return _projDL.GetAllProjects();
        }


        public void AddParentTask(ParentTaskEntity parentTask)
        {
            _projDL.AddParentTask(parentTask);
        }

        public List<ParentTaskEntity> GetAllParentTasks()
        {
            return _projDL.GetAllParentTasks();
        }

        public void AddTask(TaskEntity task)
        {
            _projDL.AddTask(task);
        }

        public TaskEntity GetTask(int taskId)
        {
            return _projDL.GetTask(taskId);
        }

        public void UpdateTask(TaskEntity task)
        {
            _projDL.UpdateTask(task);
        }

        public List<TaskEntity> GetAllTasks()
        {
            return _projDL.GetAllTasks();
        }

        public void EndTask(int taskId)
        {
            _projDL.EndTask(taskId);
        }

        public List<TaskEntity> GetAllTasks(int projectId)
        {
            return _projDL.GetAllTasks(projectId);
        }
    }
}
