﻿namespace eStoreClient.DTO.Request.Products
{
    public class UpdateProductRequest
    {
        public int CategoryId { get; set; }
        public string? ProductName { get; set; }
        public double Weight { get; set; }
        public decimal UnitPrice { get; set; }
        public int? UnitsInStock { get; set; }
    }
}
