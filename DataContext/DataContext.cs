using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WinnerGenerator_Backend.Models;

namespace WinnerGenerator_Backend.DataContext;

public partial class DataContext : DbContext
{
    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Winner> Winners { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=WinnerGen;Integrated Security=False;User ID=SA;Password=Shtator03@;TrustServerCertificate=False;Encrypt=false;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Winner>(entity =>
        {
            entity.Property(e => e.ExcelFile).IsUnicode(false);
            entity.Property(e => e.GiveawayName).IsUnicode(false);
            entity.Property(e => e.Images).IsUnicode(false);
            entity.Property(e => e.InsertDateTime).HasColumnType("datetime");
            entity.Property(e => e.WinnersName).IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
