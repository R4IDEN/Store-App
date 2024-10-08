﻿
namespace Entities.Models
{
    public class Cart
    {
        public List<CartLine> Lines { get; set; }
        public Cart()
        {
            Lines = new List<CartLine>();
        }
        public virtual void AddItem(Product product, int quantity)
        {
            CartLine? line = Lines.Where(l => l.Product.ProductId == product.ProductId).FirstOrDefault();

            if (line is null) 
            {
                Lines.Add(new CartLine()
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
                line.Quantity += quantity;
        }

        //
        public virtual void RemoveLine(Product product) => Lines.RemoveAll(l => l.Product.ProductId == product.ProductId);

        public virtual void IncreaseQuantity(Product product)
        {
            var line = Lines.FirstOrDefault(l => l.Product.ProductId == product.ProductId);
            if(line!=null)
                line.Quantity++;
        }
        public virtual void DecreaseQuantity(Product product)
        {
            var line = Lines.FirstOrDefault(l => l.Product.ProductId == product.ProductId);
            
            if(line!=null)
            {
                if (line.Quantity == 1)
                    RemoveLine(product);
                else
                    line.Quantity--;
            }
        }
        
        public virtual void ClearCart() => Lines.Clear();

        //total value
        public decimal ComputeTotalValue() => Lines.Sum(e => e.Product.Price * e.Quantity);
        
    }
}
