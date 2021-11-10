using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using VoicePlatform.Data.Entities;

#nullable disable

namespace VoicePlatform.Data.Contexts
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<ArtistCountry> ArtistCountries { get; set; }
        public virtual DbSet<ArtistProject> ArtistProjects { get; set; }
        public virtual DbSet<ArtistProjectFile> ArtistProjectFiles { get; set; }
        public virtual DbSet<ArtistVoiceDemo> ArtistVoiceDemos { get; set; }
        public virtual DbSet<ArtistVoiceStyle> ArtistVoiceStyles { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerProjectFile> CustomerProjectFiles { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectCountry> ProjectCountries { get; set; }
        public virtual DbSet<ProjectGender> ProjectGenders { get; set; }
        public virtual DbSet<ProjectUsagePurpose> ProjectUsagePurposes { get; set; }
        public virtual DbSet<ProjectVoiceStyle> ProjectVoiceStyles { get; set; }
        public virtual DbSet<UsagePurpose> UsagePurposes { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<VoiceDemo> VoiceDemos { get; set; }
        public virtual DbSet<VoiceStyle> VoiceStyles { get; set; }
        public virtual DbSet<ArtistToken> ArtistTokens { get; set; }
        public virtual DbSet<CustomerToken> CustomerToken { get; set; }
        public virtual DbSet<AdminToken> AdminToken { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Artist>(entity =>
            {
                entity.ToTable("Artist");

                entity.HasIndex(e => e.Username, "UQ__Artist__536C85E492204412")
                    .IsUnique();

                entity.HasIndex(e => e.Phone, "UQ__Artist__5C7E359E3D0B7733")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__Artist__A9D105349D3C2CC2")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Avatar).IsRequired();

                entity.Property(e => e.Bio)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.LastLoginTime).HasColumnType("datetime");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.HasOne(d => d.GenderNavigation)
                    .WithMany(p => p.Artists)
                    .HasForeignKey(d => d.Gender)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Artist__Gender__619B8048");

                entity.HasOne(d => d.UpdateByNavigation)
                    .WithMany(p => p.Artists)
                    .HasForeignKey(d => d.UpdateBy)
                    .HasConstraintName("FK__Artist__UpdateBy__628FA481");
            });

            modelBuilder.Entity<ArtistCountry>(entity =>
            {
                entity.HasKey(e => new { e.ArtistId, e.CountryId })
                    .HasName("PK__ArtistCo__C47D7D59514ECBC3");

                entity.ToTable("ArtistCountry");

                entity.HasOne(d => d.Artist)
                    .WithMany(p => p.ArtistCountries)
                    .HasForeignKey(d => d.ArtistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ArtistCou__Artis__6383C8BA");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.ArtistCountries)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ArtistCou__Count__6477ECF3");
            });

            modelBuilder.Entity<ArtistProject>(entity =>
            {
                entity.HasKey(e => new { e.ArtistId, e.ProjectId })
                    .HasName("PK__ArtistPr__3211C0BF127A0A09");

                entity.ToTable("ArtistProject");

                entity.Property(e => e.CanceledDate).HasColumnType("datetime");

                entity.Property(e => e.Comment).HasMaxLength(256);

                entity.Property(e => e.FinishedDate).HasColumnType("datetime");

                entity.Property(e => e.InvitedDate).HasColumnType("datetime");

                entity.Property(e => e.JoinedDate).HasColumnType("datetime");

                entity.Property(e => e.RequestedDate).HasColumnType("datetime");

                entity.Property(e => e.ReviewDate).HasColumnType("datetime");

                entity.HasOne(d => d.Artist)
                    .WithMany(p => p.ArtistProjects)
                    .HasForeignKey(d => d.ArtistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ArtistPro__Artis__656C112C");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ArtistProjects)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ArtistPro__Proje__66603565");
            });

            modelBuilder.Entity<ArtistProjectFile>(entity =>
            {
                entity.ToTable("ArtistProjectFile");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Comment)
                    .HasMaxLength(256);

                entity.Property(e => e.Url).IsRequired();

                entity.Property(e => e.Status).IsRequired();

                entity.HasOne(d => d.CreateByNavigation)
                    .WithMany(p => p.ArtistProjectFiles)
                    .HasForeignKey(d => d.CreateBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ArtistPro__Creat__6754599E");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ArtistProjectFiles)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ArtistPro__Proje__68487DD7");
            });

            modelBuilder.Entity<ArtistVoiceDemo>(entity =>
            {
                entity.HasKey(e => new { e.ArtistId, e.VoiceDemoId })
                    .HasName("PK__ArtistVo__F97D9A3E5BED99C3");

                entity.ToTable("ArtistVoiceDemo");

                entity.HasOne(d => d.Artist)
                    .WithMany(p => p.ArtistVoiceDemos)
                    .HasForeignKey(d => d.ArtistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ArtistVoi__Artis__693CA210");

                entity.HasOne(d => d.VoiceDemo)
                    .WithMany(p => p.ArtistVoiceDemos)
                    .HasForeignKey(d => d.VoiceDemoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ArtistVoi__Voice__6A30C649");
            });

            modelBuilder.Entity<ArtistVoiceStyle>(entity =>
            {
                entity.HasKey(e => new { e.ArtistId, e.VoiceStyleId })
                    .HasName("PK__ArtistVo__262BCF6EC63E626F");

                entity.ToTable("ArtistVoiceStyle");

                entity.HasOne(d => d.Artist)
                    .WithMany(p => p.ArtistVoiceStyles)
                    .HasForeignKey(d => d.ArtistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ArtistVoi__Artis__6B24EA82");

                entity.HasOne(d => d.VoiceStyle)
                    .WithMany(p => p.ArtistVoiceStyles)
                    .HasForeignKey(d => d.VoiceStyleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ArtistVoi__Voice__6C190EBB");
            });
            
            modelBuilder.Entity<ArtistToken>(entity =>
            {
                entity.ToTable("ArtistToken");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.Artist)
                    .WithMany(p => p.ArtistTokens)
                    .HasForeignKey(d => d.ArtistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ArtistToken_Artist1");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("Country");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.HasIndex(e => e.Username, "UQ__Customer__536C85E415D6A19B")
                    .IsUnique();

                entity.HasIndex(e => e.Phone, "UQ__Customer__5C7E359E5F50667D")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__Customer__A9D1053424A06F7F")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Avatar).IsRequired();

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.LastLoginTime).HasColumnType("datetime");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.HasOne(d => d.GenderNavigation)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.Gender)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Customer__Gender__6D0D32F4");

                entity.HasOne(d => d.UpdateByNavigation)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.UpdateBy)
                    .HasConstraintName("FK__Customer__Update__6E01572D");
            });

            modelBuilder.Entity<CustomerProjectFile>(entity =>
            {
                entity.ToTable("CustomerProjectFile");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Comment)
                    .HasMaxLength(256);

                entity.Property(e => e.Url).IsRequired();

                entity.Property(e => e.Status).IsRequired();

                entity.HasOne(d => d.CreateByNavigation)
                    .WithMany(p => p.CustomerProjectFiles)
                    .HasForeignKey(d => d.CreateBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CustomerP__Creat__6EF57B66");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.CustomerProjectFiles)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CustomerP__Proje__6FE99F9F");
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.ToTable("Gender");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("Project");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ProjectDeadline).HasColumnType("datetime");

                entity.Property(e => e.ResponseDeadline).HasColumnType("datetime");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<ProjectCountry>(entity =>
            {
                entity.HasKey(e => new { e.ProjectId, e.CountryId })
                    .HasName("PK__ProjectC__9717A8F99AD216D5");

                entity.ToTable("ProjectCountry");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.ProjectCountries)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProjectCo__Count__70DDC3D8");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectCountries)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProjectCo__Proje__71D1E811");
            });

            modelBuilder.Entity<ProjectGender>(entity =>
            {
                entity.HasKey(e => new { e.ProjectId, e.GenderId })
                    .HasName("PK__ProjectG__12F8F06FAF32F84D");

                entity.ToTable("ProjectGender");

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.ProjectGenders)
                    .HasForeignKey(d => d.GenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProjectGe__Gende__72C60C4A");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectGenders)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProjectGe__Proje__73BA3083");
            });

            modelBuilder.Entity<ProjectUsagePurpose>(entity =>
            {
                entity.HasKey(e => new { e.ProjectId, e.UsagePurposeId })
                    .HasName("PK__ProjectU__5FEFB91281D42971");

                entity.ToTable("ProjectUsagePurpose");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectUsagePurposes)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProjectUs__Proje__74AE54BC");

                entity.HasOne(d => d.UsagePurpose)
                    .WithMany(p => p.ProjectUsagePurposes)
                    .HasForeignKey(d => d.UsagePurposeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProjectUs__Usage__75A278F5");
            });

            modelBuilder.Entity<ProjectVoiceStyle>(entity =>
            {
                entity.HasKey(e => new { e.ProjectId, e.VoiceStyleId })
                    .HasName("PK__ProjectV__75411ACE4D4BBA47");

                entity.ToTable("ProjectVoiceStyle");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectVoiceStyles)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProjectVo__Proje__76969D2E");

                entity.HasOne(d => d.VoiceStyle)
                    .WithMany(p => p.ProjectVoiceStyles)
                    .HasForeignKey(d => d.VoiceStyleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProjectVo__Voice__778AC167");
            });

            modelBuilder.Entity<UsagePurpose>(entity =>
            {
                entity.ToTable("UsagePurpose");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.Username, "UQ__User__536C85E4E29FB533")
                    .IsUnique();

                entity.HasIndex(e => e.Phone, "UQ__User__5C7E359E45FC8A21")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__User__A9D1053445DF918E")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Avatar).IsRequired();

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.LastLoginTime).HasColumnType("datetime");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.HasOne(d => d.GenderNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Gender)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__User__Gender__787EE5A0");

                entity.HasOne(d => d.UpdateByNavigation)
                    .WithMany(p => p.InverseUpdateByNavigation)
                    .HasForeignKey(d => d.UpdateBy)
                    .HasConstraintName("FK__User__UpdateBy__797309D9");
            });

            modelBuilder.Entity<VoiceDemo>(entity =>
            {
                entity.ToTable("VoiceDemo");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Url).IsRequired();
            });

            modelBuilder.Entity<VoiceStyle>(entity =>
            {
                entity.ToTable("VoiceStyle");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<ArtistToken>(entity =>
            {
                entity.ToTable("ArtistToken");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.HasOne(d => d.Artist)
                .WithMany(p => p.ArtistTokens)
                .HasForeignKey(d => d.ArtistId)
                .HasConstraintName("FK__Artist__Token__797309D9");
            });

            modelBuilder.Entity<CustomerToken>(entity =>
            {
                entity.ToTable("CustomerToken");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.HasOne(d => d.Customer)
                .WithMany(p => p.CustomerToken)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Customer__Token__797759D9");
            });

            modelBuilder.Entity<AdminToken>(entity =>
            {
                entity.ToTable("AdminToken");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.HasOne(d => d.User)
                .WithMany(p => p.AdminToken)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__User__Token__797279D9");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
