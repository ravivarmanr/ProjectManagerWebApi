using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.Entity;

namespace ProjectManager.DataLayer
{
    public interface IProjectManagerDL
    {
        void AddUser(UserEntity user);

        UserEntity GetUser(int userId);

        void UpdateUser(UserEntity user);

        List<UserEntity> GetAllUsers();


        void AddProject(ProjectEntity project);

        ProjectEntity GetProject(int projectId);

        void UpdateProject(ProjectEntity project);

        List<ProjectEntity> GetAllProjects();


        void AddParentTask(ParentTaskEntity parentTask);


        void AddTask(TaskEntity task);

        TaskEntity GetTask(int taskId);

        void UpdateTask(TaskEntity task);

        List<TaskEntity> GetAllTasks();

        void EndTask(int taskId);

    }
}
