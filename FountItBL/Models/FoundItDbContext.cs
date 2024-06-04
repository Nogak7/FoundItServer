using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FountItBL.Models;

public partial class FoundItDbContext : DbContext
{
    public FoundItDbContext()
    {
    }

    public FoundItDbContext(DbContextOptions<FoundItDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Community> Communities { get; set; }

    public virtual DbSet<CommunityMember> CommunityMembers { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<PostComment> PostComments { get; set; }

    public virtual DbSet<PostStatus> PostStatuses { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=FoundItDB;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Community>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Communit__3214EC076FAF19F6");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Location).HasMaxLength(250);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("[Name]");

            entity.HasOne(d => d.ManagerNavigation).WithMany(p => p.Communities)
                .HasForeignKey(d => d.Manager)
                .HasConstraintName("FK_CommunitysToUsers");
        });

        modelBuilder.Entity<CommunityMember>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Communit__3214EC071EC566C5");

            entity.ToTable("CommunityMember");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.CommunityNavigation).WithMany(p => p.CommunityMembers)
                .HasForeignKey(d => d.Community)
                .HasConstraintName("FK_CommunityMember_Communitiys");

            entity.HasOne(d => d.UserNavigation).WithMany(p => p.CommunityMembers)
                .HasForeignKey(d => d.User)
                .HasConstraintName("FK_CommunityMember_ToUsers");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Post__3214EC0794C1512F");

            entity.ToTable("Post");

            entity.Property(e => e.Context).HasMaxLength(500);
            entity.Property(e => e.CreatingDate).HasColumnType("datetime");
            entity.Property(e => e.Location).HasMaxLength(250);
            entity.Property(e => e.Picture).HasMaxLength(200);
            entity.Property(e => e.Theme).HasMaxLength(50);

            entity.HasOne(d => d.CreatorNavigation).WithMany(p => p.Posts)
                .HasForeignKey(d => d.Creator)
                .HasConstraintName("FK_Post_User");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.Posts)
                .HasForeignKey(d => d.Status)
                .HasConstraintName("FK_PostToPostStatus");
        });

        modelBuilder.Entity<PostComment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PostComm__3214EC07790B1BBD");

            entity.ToTable("PostComment");

            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Postcomment1).HasColumnName("Postcomment");

            entity.HasOne(d => d.PostNavigation).WithMany(p => p.PostComments)
                .HasForeignKey(d => d.Post)
                .HasConstraintName("FK_PostComment_Post");

            entity.HasOne(d => d.Postcomment1Navigation).WithMany(p => p.InversePostcomment1Navigation)
                .HasForeignKey(d => d.Postcomment1)
                .HasConstraintName("FK_PostComment_PostComment");
        });

        modelBuilder.Entity<PostStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PostStat__3214EC071E814903");

            entity.ToTable("PostStatus");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Poststatus1)
                .HasMaxLength(250)
                .HasColumnName("Poststatus");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC272E77C3A5");

            entity.HasIndex(e => e.Email, "UC_Email").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(30);
            entity.Property(e => e.LastName).HasMaxLength(30);
            entity.Property(e => e.Pasword).HasMaxLength(30);
            entity.Property(e => e.ProfilePicture).HasMaxLength(200);
            entity.Property(e => e.UserName).HasMaxLength(30);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
