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

        List<ProjectEntity> GetAllProjects();

        ProjectEntity GetProject(int projId);
    }
}
