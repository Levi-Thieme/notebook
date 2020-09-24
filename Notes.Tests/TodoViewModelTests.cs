using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Notes.Data;
using Notes.Models;
using Notes.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Notes.Tests
{
    [TestClass]
    public class TodoViewModelTests
    {
        private const string parentTodo = "TodoToday";
        private TodoViewModel todoViewModel;
        private Mock<ITodoRepository> todoRepositoryMock;
        private Task task;

        [TestInitialize]
        public void Initialize()
        {
            task = new Task(parentTodo, "workout");
            var tasks = new List<Task> { task };
            todoRepositoryMock = new Mock<ITodoRepository>();
            todoRepositoryMock.Setup(repo => repo.GetTasks(parentTodo)).Returns(tasks);
            todoViewModel = new TodoViewModel(parentTodo, todoRepositoryMock.Object);
        }

        [TestClass]
        public class WhenDeletingATask : TodoViewModelTests
        {
            [TestMethod]
            public void ItIsDeletedFromTheDatabase()
            {
                todoViewModel.DeleteTaskCommand.Execute(task);

                todoRepositoryMock.Verify(repo => repo.DeleteTask(task), Times.Once);
            }

            [TestMethod]
            public void ItIsRemovedFromTasks()
            {
                todoViewModel.DeleteTaskCommand.Execute(task);

                Assert.IsFalse(todoViewModel.Tasks.Contains(task));
            }
        }

        [TestClass]
        public class WhenCreatingATask : TodoViewModelTests
        {
            private const string taskName = "newTask";

            [TestMethod]
            public void ItIsSaved()
            {
                todoViewModel.CreateTaskCommand.Execute(taskName);

                todoRepositoryMock.Verify(repo => repo.CreateTask(parentTodo, taskName), Times.Once);
            }

            [TestMethod]
            public void ItIsAddedToTasks()
            {
                todoViewModel.CreateTaskCommand.Execute(taskName);

                Assert.IsNotNull(todoViewModel.Tasks.FirstOrDefault(task => task.Name == "newTask" && task.TodoName == parentTodo));
            }

            [TestMethod]
            public void AndTheNameIsInvalid_ItDoesNotCreateTheTask()
            {
                todoViewModel.Tasks.Add(new Task(parentTodo, "existingTask"));

                Assert.IsFalse(todoViewModel.CreateTaskCommand.CanExecute("existingTask"));
                Assert.IsFalse(todoViewModel.CreateTaskCommand.CanExecute(null));
                Assert.IsFalse(todoViewModel.CreateTaskCommand.CanExecute(string.Empty));
                Assert.IsFalse(todoViewModel.CreateTaskCommand.CanExecute(" "));
            }
        }
    }
}
