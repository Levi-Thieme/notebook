using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Notes.Data;
using Notes.Models;
using Notes.ViewModels;
using System.Linq;

namespace Notes.Tests
{
    [TestClass]
    public class TodoViewModelTests
    {
        private TodoViewModel todoViewModel;
        private Mock<IRepository<Todo>> mockTodoRepository;

        [TestInitialize]
        public void Initialize()
        {
            mockTodoRepository = new Mock<IRepository<Todo>>();
            todoViewModel = new TodoViewModel(mockTodoRepository.Object);
        }

        [TestClass]
        public class WhenCreatingATodo : TodoViewModelTests
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
    }
}
