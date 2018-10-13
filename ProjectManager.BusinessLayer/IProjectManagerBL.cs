﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.Entity;

namespace ProjectManager.BusinessLayer
{
    public interface IProjectManagerBL
    {
        void AddUser(UserEntity user);

        UserEntity GetUser(int userId);

        void UpdateUser(UserEntity user);

        List<UserEntity> GetAllUsers();
        
    }
}
