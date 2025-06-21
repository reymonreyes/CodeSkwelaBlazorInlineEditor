using Microsoft.EntityFrameworkCore;
using TableInlineEditor.Services.Models;

namespace TableInlineEditor.Services
{
    public interface IProductsDataService
    {
        void Create(Product product);
        void Delete(int id);
        Product? Get(int id);
        List<Product> GetAll();
        void Update(Product product);
    }

    public class ProductsDataService : IProductsDataService
    {
        public List<Product> GetAll()
        {
            var result = new List<Product>();
            using (var context = new ApplicationDbContext())
            {
                result = context.Products.Include(x => x.Category).Take(10).ToList();
            }

            return result;
        }

        public Product? Get(int id)
        {
            Product? result = null;

            using (var context = new ApplicationDbContext())
            {
                result = context.Products.FirstOrDefault(x => x.ProductID == id);
            }

            return result;
        }

        public void Create(Product product)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Products.Add(product);
                var id = context.SaveChanges();
            }
        }

        public void Update(Product product)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Update(product);
                context.SaveChanges();
            }            
        }

        public void Delete(int id)
        {
            //need to make adjustment on our sqlite Products table to prevent contraint errors, refer to !1 below
            using (var context = new ApplicationDbContext())
            {
                var product = Get(id);
                if (product != null)
                {
                    context.Products.Remove(product);
                    context.SaveChanges();
                }
            }
        }
    }    
}

/*
!1
ALTER TABLE Products RENAME TO ProductsTemp;
CREATE TABLE [Products](
   [ProductID]INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
   [ProductName]TEXT NOT NULL,
   [SupplierID]INTEGER,
   [CategoryID]INTEGER,
   [QuantityPerUnit]TEXT,
   [UnitPrice]NUMERIC DEFAULT 0,
   [UnitsInStock]INTEGER DEFAULT 0,
   [UnitsOnOrder]INTEGER DEFAULT 0,
   [ReorderLevel]INTEGER DEFAULT 0,
   [Discontinued]TEXT NOT NULL DEFAULT '0',
    CHECK ([UnitPrice]>=(0)),
    CHECK ([ReorderLevel]>=(0)),
    CHECK ([UnitsInStock]>=(0)),
    CHECK ([UnitsOnOrder]>=(0)),
	FOREIGN KEY ([CategoryID]) REFERENCES [Categories] ([CategoryID]) 
		ON DELETE CASCADE ON UPDATE NO ACTION,
	FOREIGN KEY ([SupplierID]) REFERENCES [Suppliers] ([SupplierID]) 
		ON DELETE CASCADE ON UPDATE NO ACTION
);
INSERT INTO Products SELECT * FROM ProductsTemp;
*/
