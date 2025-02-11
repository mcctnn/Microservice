﻿using FirstMicroservice.Todos.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstMicroservice.Todos.WebAPI.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Todo> Todos { get; set; }
    }
}
