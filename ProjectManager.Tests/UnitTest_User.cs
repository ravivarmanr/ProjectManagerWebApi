using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Moq;
using ProjectManager.BusinessLayer;
using ProjectManager.DataLayer;
using ProjectManager.Entity;


namespace ProjectManager.Tests
{
    [TestFixture]
    public class UnitTest_User
    {

        private IProjectManagerDL _mockRepository;
        private List<UserEntity> _userList;

        [SetUp]
        public void initialize()
        {
            //Initialize repository
            var mockRepo = new Mock<IProjectManagerDL>();

            //create sample user data
            _userList = new List<UserEntity>()
            {
                new UserEntity{UserId=1, FirstName="Ravi", LastName="Ramm", EmpId=101131, UserStatus="Y", AddDate= new DateTime(2018,9,9), UpdtDate= new DateTime(2018,9,9)},
                new UserEntity{UserId=2, FirstName="Raja", LastName="Suresh", EmpId=101132, UserStatus="Y", AddDate= new DateTime(2018,9,9), UpdtDate= new DateTime(2018,9,9)},
                new UserEntity{UserId=3, FirstName="Sanjay", LastName="Vikram", EmpId=101133, UserStatus="Y", AddDate= new DateTime(2018,9,9), UpdtDate= new DateTime(2018,9,9)},
                new UserEntity{UserId=4, FirstName="Suresh", LastName="Hari", EmpId=101134, UserStatus="Y", AddDate= new DateTime(2018,9,9), UpdtDate= new DateTime(2018,9,9)},
                new UserEntity{UserId=5, FirstName="Bharath", LastName="Vinayak", EmpId=101135, UserStatus="N", AddDate= new DateTime(2018,9,9), UpdtDate= new DateTime(2018,9,9)}
            };

            //Setup mocking behavior

            //Get the list of all the Active Users
            mockRepo.Setup(u => u.GetAllUsers()).Returns(_userList);

            //Get the user by sending User ID
            mockRepo.Setup(u => u.GetUser(It.IsAny<int>()))
                .Returns((int id) => _userList.Find(u => u.UserId.Equals(id)));

            //Add the user
            mockRepo.Setup(u => u.AddUser(It.IsAny<UserEntity>()))
                .Callback((UserEntity user) => _userList.Add(user));

            //Update the user
            mockRepo.Setup(u => u.UpdateUser(It.IsAny<UserEntity>()))
                .Callback((UserEntity updatedUser) =>
                {
                    var actualUser = _userList.Find(u => u.UserId.Equals(updatedUser.UserId));

                    actualUser.FirstName = updatedUser.FirstName;
                    actualUser.LastName = updatedUser.LastName;
                    actualUser.EmpId = updatedUser.EmpId;
                    actualUser.UserStatus = updatedUser.UserStatus;
                    actualUser.UpdtDate = new DateTime();
                }
                );

            _mockRepository = mockRepo.Object;

        }

        [Test]
        //Test to get all users to return all users
        public void ShouldReturnAllUsers()
        {
            List<UserEntity> userList = _mockRepository.GetAllUsers();

            Assert.IsTrue(userList.Count() == 5);
            Assert.IsTrue(userList.ElementAt(0).UserId == 1);
            Assert.IsTrue(userList.ElementAt(1).FirstName == "Raja");
            Assert.IsTrue(userList.ElementAt(1).LastName == "Suresh");
            Assert.IsTrue(userList.ElementAt(3).UserStatus == "Y");
            Assert.IsTrue(userList.ElementAt(3).EmpId == 101134);

        }

        [Test]
        //Test Get User to return single user details
        public void ShouldReturnSingleUser()
        {
            var userId = 3;

            UserEntity userDetail = _mockRepository.GetUser(userId);

            Assert.IsNotNull(userDetail);
            Assert.IsTrue(userDetail.FirstName == "Sanjay");
            Assert.IsTrue(userDetail.LastName == "Vikram");
            Assert.IsTrue(userDetail.EmpId == 101133);
            Assert.IsTrue(userDetail.UserStatus == "Y");
        }

        [Test]
        //Test to add the user
        public void ShouldAddUser()
        {
            var userId = _userList.Count() + 1;
            var userDetail = new UserEntity
            {
                UserId = userId,
                FirstName = "Subash",
                LastName = "Mani",
                EmpId = 101136,
                UserStatus = "Y",
                AddDate = new DateTime()
            };

            _mockRepository.AddUser(userDetail);

            UserEntity addedUser = _mockRepository.GetUser(userId);
            Assert.IsTrue(_userList.Count() == 6);
            Assert.IsNotNull(addedUser);
            Assert.AreSame(addedUser.GetType(), typeof(UserEntity));
            Assert.AreEqual(userId, addedUser.UserId);
            Assert.IsTrue(userDetail.FirstName == addedUser.FirstName);
            Assert.IsTrue(userDetail.LastName == addedUser.LastName);
            Assert.IsTrue(userDetail.EmpId == addedUser.EmpId);

        }

        [TearDown]
        public void CleanUp()
        {
            _userList.Clear();
        }

    }
}
