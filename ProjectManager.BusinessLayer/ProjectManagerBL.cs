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

        public List<UserEntity> GetAllUsers()
        {
            return _projDL.GetAllUsers();
        }

        public UserEntity GetUser(int userId)
        {
            return _projDL.GetUser(userId);
        }

        public void UpdateUser(UserEntity user)
        {
            _projDL.UpdateUser(user);
        }

    }
}
