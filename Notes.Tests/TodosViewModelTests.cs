using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Notes.Data;
using Notes.Models;
using Notes.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Notes.Tests
{
    [TestClass]
    public class TodosViewModelTests
    {
        private TodosViewModel todoViewModel;
        private Mock<IRepository<Todo>> mockTodoRepository;

        [TestInitialize]
        public void Initialize()
        {
            mockTodoRepository = new Mock<IRepository<Todo>>();
            todoViewModel = new TodosViewModel(mockTodoRepository.Object);
        }

        [TestClass]
        public class WhenCreatingATodo : TodosViewModelTests
        {
            [TestMethod]
            public void ItIsSaved()
            {
                todoViewModel.CreateTodoCommand.Execute("test");

                mockTodoRepository.Verify(repo => repo.Save(It.Is<Todo>(todo => todo.Name == "test")));
            }

            [TestMethod]
            public void ItIsAddedToTodos()
            {
                todoViewModel.CreateTodoCommand.Execute("test");

                Assert.IsNotNull(todoViewModel.Todos.FirstOrDefault(todo => todo.Name == "test"));
            }
        }

        [TestClass]
        public class WhenDeletingATodo : TodosViewModelTests
        {
            [TestInitialize]
            new public void Initialize()
            {
                mockTodoRepository = new Mock<IRepository<Todo>>();
                mockTodoRepository.Setup(repo => repo.All()).Returns(new List<Todo> { new Todo("test") });
                todoViewModel = new TodosViewModel(mockTodoRepository.Object);
            }

            [TestMethod]
            public void ItIsDeleted()
            {
                todoViewModel.DeleteTodoCommand.Execute("test");

                mockTodoRepository.Verify(repo => repo.Delete(It.Is<Todo>(todo => todo.Name == "test")));
            }

            [TestMethod]
            public void ItIsRemovedFromTodos()
            {
                todoViewModel.DeleteTodoCommand.Execute("test");

                Assert.IsNull(todoViewModel.Todos.FirstOrDefault(todo => todo.Name == "test"));
            }
        }
    }
}
