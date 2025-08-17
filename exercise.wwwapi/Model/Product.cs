﻿namespace exercise.wwwapi.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? Category { get; set; } = string.Empty;
        public int Price { get; set; }
    }
}
