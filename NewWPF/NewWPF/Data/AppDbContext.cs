using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System.Windows.Media.Imaging;
using NewWPF.Models.Model;
using NewWPF.Helpers;
using NewWPF.Models.AppSetting;

namespace NewWPF.Data
{
    public sealed class AppDbContext : DbContext
    {
        public AppDbContext()
        {
            //Database.Migrate();
            if (!Database.EnsureCreated()) return;

            SaveChangesAsync();
        }

        #region Example

        public DbSet<Example> Examples { get; set; }

        #endregion

        #region AppSettings

        public DbSet<AppSetting> AppSettings { get; set; }

        #endregion


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());
            optionsBuilder.UseSqlite("Data Source=NewWPF.db;");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Example

            modelBuilder.Entity<Example>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.AddedDate)
                    .HasConversion(c => c.ToString("yyyy-MM-dd HH:mm:ss", Settings.CultureInfo),
                        c => DateTime.Parse(c));
                entity.Property(x => x.Image)
                    .HasConversion(c => c.ImageToByteArray(),
                        c => c.ByteArrayToBitmapImage());
                entity.Property(x => x.Width)
                    .HasConversion(c => c.ToString(CultureInfo.InvariantCulture),
                        c => Convert.ToDouble(c));
            });

            #endregion

            #region AppSetting

            modelBuilder.Entity<AppSetting>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.IsEditable)
                    .HasConversion(c => Convert.ToInt32(c),
                        c => Convert.ToBoolean(c));
            });

            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}