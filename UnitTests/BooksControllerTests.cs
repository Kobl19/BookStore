using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Domain.Abstract;
using System.Collections.Generic;
using Domain.Entities;
using WebUI.Controllers;
using System.Linq;
using System.Web.Mvc;
using WebUI.Models;
using WebUI.HtmlHelpers;

namespace UnitTests
{
    [TestClass]
    public class BooksControllerTests
    {
        [TestMethod]
        public void List_ElementsOnTheSecondPage_1elements()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.Books).Returns(new List<Book>
            {
                new Book{ BookId=1, Name="Book1"},
                new Book{ BookId=2, Name="Book2"},
                new Book{ BookId=3, Name="Book3"},
                new Book{ BookId=4, Name="Book4"},
                new Book{ BookId=5, Name="Book5"}
            });

            BooksController controller = new BooksController(mock.Object);
            controller.pageSize = 4;

            BooksListViewModel result = (BooksListViewModel)controller.List(null,2).Model;

            List<Book> books = result.Books.ToList();
            Assert.IsTrue(books.Count==1);
            Assert.AreEqual(books[0].Name, "Book5");


        }
        [TestMethod]
        public void PaginHelper_Generate_PageLinks()
        {
            HtmlHelper myHelper = null;
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                            + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                            + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
                            result.ToString());
        }

        [TestMethod]
        public void List_Send_ViewModel()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.Books).Returns(new List<Book>
            {
                new Book{ BookId=1, Name="Book1"},
                new Book{ BookId=2, Name="Book2"},
                new Book{ BookId=3, Name="Book3"},
                new Book{ BookId=4, Name="Book4"},
                new Book{ BookId=5, Name="Book5"}
            });

            BooksController controller = new BooksController(mock.Object);
            controller.pageSize = 4;

            BooksListViewModel result = (BooksListViewModel)controller.List(null,2).Model;

            PagingInfo pagingInfo = result.PagingInfo;
            Assert.AreEqual(pagingInfo.CurrentPage, 2);
            Assert.AreEqual(pagingInfo.ItemsPerPage, 4);
            Assert.AreEqual(pagingInfo.TotalItems, 5);
            Assert.AreEqual(pagingInfo.TotalPages, 2);

        }
        [TestMethod]
        public void List_Filter_Books()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.Books).Returns(new List<Book>
            {
                new Book{ BookId=1, Name="Book1", Genre="Genre1"},
                new Book{ BookId=2, Name="Book2", Genre="Genre2"},
                new Book{ BookId=3, Name="Book3", Genre="Genre3"},
                new Book{ BookId=4, Name="Book4", Genre="Genre2"},
                new Book{ BookId=5, Name="Book5", Genre="Genre1"}
            });

            BooksController controller = new BooksController(mock.Object);
            controller.pageSize = 4;

            List<Book>result = ((BooksListViewModel)controller.List("Genre2", 1).Model).Books.ToList();

            Assert.AreEqual(result.Count(), 2);
            Assert.IsTrue(result[0].Name=="Book2" && result[0].Genre=="Genre2");
            Assert.IsTrue(result[1].Name == "Book4" && result[1].Genre == "Genre2");
        }
    }
    
}
