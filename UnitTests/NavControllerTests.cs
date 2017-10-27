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
    public class NavControllerTests
    {
        [TestMethod]
        public void Menu_Create_Categories()
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

            NavController target=new NavController(mock.Object);

            List<string> result = ((IEnumerable<string>)target.Menu(null).Model).ToList();

            Assert.AreEqual(result.Count(), 3);
            Assert.AreEqual(result[0], "Genre1");
            Assert.AreEqual(result[1], "Genre2");
            Assert.AreEqual(result[2], "Genre3");
        }
    }
}
