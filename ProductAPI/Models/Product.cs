﻿namespace ProductAPI.Models
{
    public class Product
    {
        //[Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int UserId { get; set; }
    }
}
