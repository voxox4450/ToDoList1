using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    internal class Database : DbContext
    {
        public Database()
        {
        }

        public DbSet<Note> Notes { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Priority> Priorities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note>()
                .HasKey(n => n.Id);

            modelBuilder.Entity<Note>()
                .Property(n => n.ContentText)
                .IsRequired()
                .HasMaxLength(150);

            modelBuilder.Entity<Note>()
                .Property(n => n.StartDate);

            modelBuilder.Entity<Note>()
                .Property(n => n.EndDate);

            modelBuilder.Entity<Note>()
                .Property(n => n.PriorityId);

            modelBuilder.Entity<Note>()
                .Property(n => n.StatusId);

            modelBuilder.Entity<Note>()
                .HasOne(n => n.Priority)
                .WithMany()
                .HasForeignKey(n => n.PriorityId);

            modelBuilder.Entity<Note>()
                .HasOne(n => n.Status)
                .WithMany()
                .HasForeignKey(n => n.StatusId);

            modelBuilder.Entity<Status>()
                .Property(x => x.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<Status>()
                .Property(x => x.Name)
                .HasMaxLength(30);

            modelBuilder.Entity<Priority>()
                .Property(x => x.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<Priority>()
                .Property(x => x.Name)
                .HasMaxLength(30);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=ACARS-0099;User=sa;Password=praktyki;Database=myDb;Trust Server Certificate=True;");
        }

        public List<Note> GetNotes()
        {
            using (var context = new Database())
            {
                return context.Notes.ToList();
            }
        }

        public List<Note> GetNotesWithDetails()
        {
            using (var context = new Database())
            {
                return context.Notes.Include(n => n.Priority).Include(n => n.Status).ToList();
            }
        }
    }
}