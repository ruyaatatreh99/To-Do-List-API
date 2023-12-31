
using ToDo.Model;
using ToDo.services;
using Moq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;
namespace ToDo_Test
{
    [TestClass]
    public class ToDoControllerTest
    {
        [TestMethod]
        public  void createtask_Test()
        {

            // Arrange
            var configuration = new ConfigurationBuilder()
    .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
    .AddJsonFile("appsettings.json")
    .Build();
            Tasks testtask = new Tasks
            {
                ID = 1,
                Title = "try test",
                Description = "First try",
                IsCompleted = 0,
                Category = "do"
            };
            var options = new DbContextOptionsBuilder<MyDataContext>()
                         .UseSqlServer(configuration.GetConnectionString("ConStr")).Options;

            // Acts
            Tasks tasks = new Tasks();
           var context = new MyDataContext(options);
            
                // Your test logic here, interact with the in-memory database
                tasks.Title = "try test";
                tasks.Description = "First try";
                tasks.IsCompleted = 0;
                tasks.Category = "do";
                context.Tasks.Add(tasks);
                context.SaveChanges();


            // Assert
            Assert.AreEqual(testtask.Title,tasks.Title);
            Assert.AreEqual(testtask.Description,tasks.Description);
            Assert.AreEqual(testtask.IsCompleted,tasks.IsCompleted);
            Assert.AreEqual(testtask.Category,tasks.Category);
            Assert.AreEqual(testtask.ID,tasks.ID);
        }

        [TestMethod]
        public void updatetask_Test()
        {

            // Arrange
            var configuration = new ConfigurationBuilder()
    .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
    .AddJsonFile("appsettings.json")
    .Build();
            Tasks testtask = new Tasks
            {
                ID = 1,
                Title = "try test",
                Description = "First try",
                IsCompleted = 1,
                Category = "done"
            };
            var options = new DbContextOptionsBuilder<MyDataContext>()
                         .UseSqlServer(configuration.GetConnectionString("ConStr")).Options;

            // Acts
            
            var context = new MyDataContext(options);
            var check = context.Tasks.FirstOrDefault(x => x.ID == testtask.ID);
            check.IsCompleted = 1;
            check.Category = "done";
            context.Tasks.Update(check);
            context.SaveChanges();


            // Assert
            Assert.AreEqual(testtask.Title,check.Title);
            Assert.AreEqual(testtask.Description,check.Description);
            Assert.AreEqual(testtask.IsCompleted,check.IsCompleted);
            Assert.AreEqual(testtask.Category,check.Category);
            Assert.AreEqual(testtask.ID,check.ID);
        }


        [TestMethod]
        public void viewtask_Test()
        {

            // Arrange
            var configuration = new ConfigurationBuilder()
    .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
    .AddJsonFile("appsettings.json")
    .Build();
            Tasks testtask = new Tasks
            {
                ID = 1,
                Title = "try test",
                Description = "First try",
                IsCompleted = 1,
                Category = "done"
            };
            var options = new DbContextOptionsBuilder<MyDataContext>()
                         .UseSqlServer(configuration.GetConnectionString("ConStr")).Options;

            // Acts

            var context = new MyDataContext(options);
            var check = context.Tasks.First(x => x.ID == testtask.ID);

            // Assert
            Assert.AreEqual(testtask.Title, check.Title);
            Assert.AreEqual(testtask.Description, check.Description);
            Assert.AreEqual(testtask.IsCompleted, check.IsCompleted);
            Assert.AreEqual(testtask.Category, check.Category);
            Assert.AreEqual(testtask.ID, check.ID);
        }
    }
}