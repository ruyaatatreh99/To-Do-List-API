﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Model
{
    public class MyDataContext:DbContext
    {
        public MyDataContext(DbContextOptions<MyDataContext> options) :base(options) { } 
        
        public DbSet<Tasks> Tasks { get; set;}
        public DbSet<Catogry> Catogry { get; set; }
    }
}
