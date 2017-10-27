namespace Domain.Concrete
{
    using Domain.Entities;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class EFDbContext : DbContext
    {
    
        public EFDbContext()
            : base("name=EFDbContext")
        {
        }

        public virtual DbSet<Book> Books { get; set; }
    }


    
}