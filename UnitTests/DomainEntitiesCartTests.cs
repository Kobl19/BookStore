using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class DomainEntitiesCartTests
    {
        [TestMethod]
        public void AddItem_AddNew_2Lines()
        {
            Book book1 = new Book { BookId=1,Name="Book1", Price=30};
            Book book2 = new Book { BookId = 2, Name = "Book2", Price = 20 };

            Cart cart = new Cart();

            cart.AddItem(book1, 2);
            cart.AddItem(book2, 5);
            cart.AddItem(book1, 8);


            List<CartLine> result = cart.lines.ToList();

            Assert.AreEqual(result.Count, 2);
            Assert.AreEqual(result[0].Books, book1);
            Assert.AreEqual(result[1].Books, book2);
            Assert.AreEqual(result[0].Quantity, 10);
            Assert.AreEqual(result[1].Quantity, 5);


        }
        [TestMethod]
        public void RemoveLine_Remove_1Line()
        {
            Book book1 = new Book { BookId = 1, Name = "Book1", Price = 30 };
            Book book2 = new Book { BookId = 2, Name = "Book2", Price = 20 };

            Cart cart = new Cart();

            cart.AddItem(book1, 2);
            cart.AddItem(book2, 5);
            cart.AddItem(book1, 8);
            cart.RemoveLine(book1);

            List<CartLine> result = cart.lines.ToList();

            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result[0].Books, book2);
            
            


        }
        [TestMethod]
        public void ComputeTotalValue_SumQuantity_Total()
        {
            Book book1 = new Book { BookId = 1, Name = "Book1", Price = 30 };
            Book book2 = new Book { BookId = 2, Name = "Book2", Price = 20 };

            Cart cart = new Cart();

            cart.AddItem(book1, 2);
            cart.AddItem(book2, 5);
            cart.AddItem(book1, 8);

            decimal result = cart.ComputeTotalValue();

            Assert.AreEqual(result, 400);
        }
        public void Clear_DeletedAll_Count0()
        {
            Book book1 = new Book { BookId = 1, Name = "Book1", Price = 30 };
            Book book2 = new Book { BookId = 2, Name = "Book2", Price = 20 };

            Cart cart = new Cart();

            cart.AddItem(book1, 2);
            cart.AddItem(book2, 5);
            cart.AddItem(book1, 8);
            cart.Clear();

            List<CartLine> result = cart.lines.ToList();

            Assert.AreEqual(result.Count, 0);
            




        }
    }
}
