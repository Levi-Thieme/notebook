using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Notes.Data;
using Notes.Models;
using Notes.ViewModels;
using System.ComponentModel.Design;
using System.Linq;

namespace Notes.Tests
{
    [TestClass]
    public class NoteViewModelTests
    {
        private Mock<INoteRepository> mockNoteRepository;
        private NoteViewModel noteViewModel;

        [TestInitialize]
        public void Initialize()
        {
            mockNoteRepository = new Mock<INoteRepository>();
            noteViewModel = new NoteViewModel(mockNoteRepository.Object);
        }

        [TestClass]
        public class WhenCreatingANote : NoteViewModelTests
        {
            [TestMethod]
            public void AndTheNameIsInvalid_ItIsNotCreated()
            {
                var note = new Note { Name = "test" };
                noteViewModel.Notes.Add(note);

                Assert.IsFalse(noteViewModel.CreateNoteCommand.CanExecute(null));
                Assert.IsFalse(noteViewModel.CreateNoteCommand.CanExecute(""));
                Assert.IsFalse(noteViewModel.CreateNoteCommand.CanExecute(" "));
                Assert.IsFalse(noteViewModel.CreateNoteCommand.CanExecute("test"));
            }

            [TestMethod]
            public void ItIsSaved()
            {
                noteViewModel.CreateNoteCommand.Execute("test");

                mockNoteRepository.Verify(repo => repo.SaveNote(It.Is<Note>(note => note.Name == "test")), Times.Once);
            }

            [TestMethod]
            public void NewNoteNameIsCleared()
            {
                noteViewModel.CreateNoteCommand.Execute("test");

                Assert.AreEqual(string.Empty, noteViewModel.NewNoteName);
            }

            [TestMethod]
            public void TheNoteIsAddedToNotes()
            {
                noteViewModel.CreateNoteCommand.Execute("test");

                Assert.IsNotNull(noteViewModel.Notes.FirstOrDefault(n => n.Name == "test"));
            }

            [TestMethod]
            public void TheNameHasWhitespaceRemoved()
            {
                string name = "test  ";
                noteViewModel.CreateNoteCommand.Execute(name);

                Assert.IsNotNull(noteViewModel.Notes.FirstOrDefault(n => n.Name == name.Trim()));
            }
        }

        [TestClass]
        public class WhenDeletingANote : NoteViewModelTests
        {
            [TestMethod]
            public void IsIsDeleted()
            {
                var note = new Note();

                noteViewModel.DeleteNoteCommand.Execute(note);

                mockNoteRepository.Verify(repo => repo.DeleteNote(note), Times.Once);
            }

            [TestMethod]
            public void ItIsRemovedFromNotes()
            {
                var note = new Note { Name = "test" };
                noteViewModel.Notes.Add(note);

                noteViewModel.DeleteNoteCommand.Execute(note);

                Assert.IsNull(noteViewModel.Notes.FirstOrDefault(n => n.Name == "test"));
            }
        }
    }
}