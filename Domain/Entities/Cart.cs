using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Cart
    {
        
        public List<CartLine> lineCollection = new List<CartLine>();
        public IEnumerable<CartLine> lines { get { return lineCollection; } }
        public void AddItem(Book book, int quantity)
        {
            CartLine line = lineCollection
                .Where(b => b.Books.BookId == book.BookId)
                .FirstOrDefault();
            if (line==null)
            {
                lineCollection.Add( new CartLine { Books = book, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public void RemoveLine(Book book)
        {
            lineCollection.RemoveAll(b => b.Books.BookId == book.BookId);
        }
        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(b => b.Quantity*b.Books.Price);
        }
        public void Clear()
        {
            lineCollection.Clear();
        }
    }
    public class CartLine
    {
        public Book Books { get; set; }
        public int Quantity { get; set; }
    }
}
