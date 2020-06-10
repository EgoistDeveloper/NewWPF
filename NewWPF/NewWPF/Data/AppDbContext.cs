using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System.Windows.Media.Imaging;
using NewWPF.Models.ExampleModel;
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

            // default languge setting
            AppSettings.Add(new AppSetting
            {
                SettingName = "CurrentLang",
                Value = "en-Us",
                IsEditable = false,
                DefaultValue = "en-Us"
            });

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
                entity.Property(x => x.SpendTime)
                    .HasConversion(c => c.TimeSpanToTimeString(),
                        c => TimeSpan.Parse(c));
                entity.Property(x => x.Status)
                    .HasConversion(c => Convert.ToInt32(c),
                        c => Convert.ToBoolean(c));
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