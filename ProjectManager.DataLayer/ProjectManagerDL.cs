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

        public List<ProjectEntity> GetAllProjects()
        {
            var project = (from p in _DBContext.Projects
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
                               ManagerName = tProj.FirstName + ' ' + tProj.LastName
                           }
                           ).ToList();

            return project;
        }

        public ProjectEntity GetProject(int projectId)
        {
            var project = (from p in _DBContext.Projects
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
                               ManagerName = tProj.FirstName + ' ' + tProj.LastName
                           }
                           ).SingleOrDefault();

                return project;
        }


    }
}
