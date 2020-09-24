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

            [TestMethod]
            public void ItSetsNewTodoNameToEmptyString()
            {
                todoViewModel.CreateTodoCommand.Execute("test");

                Assert.AreEqual(string.Empty, todoViewModel.NewTodoName);
            }

            [TestMethod]
            public void AndTheNameIsInvalid_ItDoesNotCreateTheTodo()
            {
                todoViewModel.Todos.Add(new Todo("existingTodo"));

                Assert.IsFalse(todoViewModel.CreateTodoCommand.CanExecute("existingTodo"));
                Assert.IsFalse(todoViewModel.CreateTodoCommand.CanExecute(null));
                Assert.IsFalse(todoViewModel.CreateTodoCommand.CanExecute(string.Empty));
                Assert.IsFalse(todoViewModel.CreateTodoCommand.CanExecute(" "));
            }
        }

        [TestClass]
        public class WhenDeletingATodo : TodosViewModelTests
        {
            private Todo TodoToDelete = new Todo { Name = "test" };

            [TestInitialize]
            new public void Initialize()
            {
                mockTodoRepository = new Mock<IRepository<Todo>>();
                mockTodoRepository.Setup(repo => repo.All()).Returns(new List<Todo> { TodoToDelete });
                todoViewModel = new TodosViewModel(mockTodoRepository.Object);
            }

            [TestMethod]
            public void ItIsDeleted()
            {
                todoViewModel.DeleteTodoCommand.Execute(TodoToDelete);

                mockTodoRepository.Verify(repo => repo.Delete(TodoToDelete));
            }

            [TestMethod]
            public void ItIsRemovedFromTodos()
            {
                todoViewModel.DeleteTodoCommand.Execute(TodoToDelete);

                Assert.IsFalse(todoViewModel.Todos.Contains(TodoToDelete));
            }
        }
    }
}
