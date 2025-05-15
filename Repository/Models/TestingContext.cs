using Microsoft.EntityFrameworkCore;

namespace Repository.Models;

public class TestingContext : DbContext
{
   
    public TestingContext() : base()
    {

    }
    public TestingContext(DbContextOptions<TestingContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=Blog;Username=postgres; password=Tatva@123");

    public virtual DbSet<User> users {get; set;}
    public virtual DbSet<UserRole> userRole {get; set;}
    public virtual DbSet<Blog> blogs {get; set;}

    public virtual DbSet<Comment> comments {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
         modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.UserName).HasColumnName("username");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Isdeleted).HasDefaultValueSql("false").HasColumnName("isdeleted");
            entity.HasOne(d => d.UserroleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserRoleId)
                .HasConstraintName("users_UserRole_fkey");
        });

         modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("userroles_pkey");

            entity.ToTable("userroles");

            entity.Property(e => e.Isdeleted)
                .HasDefaultValueSql("false")
                .HasColumnName("isdeleted");
            entity.Property(e => e.RoleName)
                .HasMaxLength(150)
                .HasColumnName("role_name");
        });

         modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.BlogId).HasName("blogs_pkey");
 
            entity.ToTable("blogs");
 
            entity.Property(e => e.BlogContent)
                           .HasMaxLength(500).HasColumnName("blog_content");
            entity.Property(e => e.BlogTitle).HasMaxLength(500).HasColumnName("blog_title");
            entity.Property(e => e.BlogTag).HasMaxLength(500).HasColumnName("blog_TAG");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.IsDelete)
                .HasDefaultValueSql("false")
                .HasColumnName("isdeleted");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.HasOne(d => d.User).WithMany(p => p.Blogs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("blogs_User_fkey");
            
        });
 
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("comments_pkey");
 
            entity.ToTable("comments");
 
            entity.Property(e => e.CommentContent)
                .HasMaxLength(500)
                .HasColumnName("comment_content");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.IsDelete)
                .HasDefaultValueSql("false")
                .HasColumnName("isdeleted");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.HasOne(d => d.Blog).WithMany(p => p.Comments)
                .HasForeignKey(d => d.BlogId)
                .HasConstraintName("comments_blogs_fkey");
            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("comments_users_fkey");
           
        });

    }
}
