﻿using MyEverNote.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEverNote.DataAccessLayer.EntityFramework
{
    public class DatabaseContext:DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<EverNoteUser> EverNoteUsers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Liked> Likes { get; set; }
        public DatabaseContext()    // ilk burası çalışıcak
        {
            Database.SetInitializer(new MyInitialization());
        }
    }
}
