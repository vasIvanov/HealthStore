﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HealthStore.Models.Products
{
    public class Plan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Days { get; set; }
        public string [] Exercices { get; set; }
        public double Price { get; set; }
    }
}
