using Domain.Abstract;
using Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebUI.Controllers;

namespace UnitTests
{
    [TestClass]
    public class AdminControllerTests
    {
        [TestMethod]
        public void Index_Contains_AllBooks()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.Books).Returns(new List<Book>
            {
                new Book{ BookId=1, Name="Book1"},
                new Book{ BookId=2, Name="Book2"},
                new Book{ BookId=3, Name="Book3"}
            });

            AdminController controller = new AdminController(mock.Object);


            List<Book> result = ((IEnumerable<Book>)controller.Index().ViewData.Model).ToList();

            
            Assert.IsTrue(result.Count == 3);
            Assert.AreEqual(result[0].Name, "Book1");
            Assert.AreEqual(result[1].Name, "Book2");
            Assert.AreEqual(result[2].Name, "Book3");


        }
        [TestMethod]
        public void EditGet_AddBookId_GetBooks()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.Books).Returns(new List<Book>
            {
                new Book{ BookId=1, Name="Book1"},
                new Book{ BookId=2, Name="Book2"},
                new Book{ BookId=3, Name="Book3"}
            });

            AdminController controller = new AdminController(mock.Object);


            Book book1 = controller.Edit(1).ViewData.Model as Book;
            Book book2 = controller.Edit(2).ViewData.Model as Book;
            Book book3 = controller.Edit(3).ViewData.Model as Book;


            Assert.AreEqual(1, book1.BookId);
            Assert.AreEqual(2, book2.BookId);
            Assert.AreEqual(3, book3.BookId);


        }
        [TestMethod]
        public void EditPost_Save_Changes()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            AdminController controller = new AdminController(mock.Object);

            Book book = new Book { Name = "Book1" };

            ActionResult result = controller.Edit(book);

            mock.Verify(m=>m.SaveBook(book));

            Assert.IsNotInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void EditPost_SaveInvalid_Changes()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            AdminController controller = new AdminController(mock.Object);

            Book book = new Book { Name = "Book1" };

            controller.ModelState.AddModelError("error", "error");

            ActionResult result = controller.Edit(book);

            mock.Verify(m => m.SaveBook(It.IsAny<Book>()), Times.Never());

            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
