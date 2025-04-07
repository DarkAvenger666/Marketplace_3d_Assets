using Microsoft.EntityFrameworkCore;
using Marketplace_3d_Assets.DataAccess.Entities;

namespace Marketplace_3d_Assets.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; } = null!;
        public DbSet<UserProfileEntity> UserProfiles { get; set; } = null!;
        public DbSet<SubscriptionEntity> Subscriptions { get; set; } = null!;
        public DbSet<NetworkNameEntity> NetworkNames { get; set; } = null!;
        public DbSet<NetworkLinkEntity> NetworkLinks { get; set; } = null!;
        public DbSet<PostEntity> Posts { get; set; } = null!;
        public DbSet<PostCommentEntity> PostComments { get; set; } = null!;
        public DbSet<AssetCommentEntity> AssetComments { get; set; } = null!;
        public DbSet<PostImageEntity> PostImages { get; set; } = null!;
        public DbSet<TagEntity> Tags { get; set; } = null!;
        public DbSet<PostTagEntity> PostTags { get; set; } = null!;
        public DbSet<AssetTagEntity> AssetTags { get; set; } = null!;
        public DbSet<AssetStatusEntity> AssetStatuses { get; set; } = null!;
        public DbSet<AssetTypeEntity> AssetTypes { get; set; } = null!;
        public DbSet<AssetEntity> Assets { get; set; } = null!;
        public DbSet<FileTypeEntity> FileTypes { get; set; } = null!;
        public DbSet<AssetFileEntity> AssetFiles { get; set; } = null!;
        public DbSet<AssetImageEntity> AssetImages { get; set; } = null!;
        public DbSet<ModerationRequestEntity> ModerationRequests { get; set; } = null!;
        public DbSet<AssetBundleEntity> AssetBundles { get; set; } = null!;
        public DbSet<UserRoleEntity> UserRoles { get; set; } = null!;
        public DbSet<CartItemEntity> CartItems { get; set; } = null!;
        public DbSet<AssetLikeEntity> AssetLikes { get; set; } = null!;
        public DbSet<ModerationRequestStatusEntity> ModerationRequestStatuses { get; set; } = null!;
        public ApplicationContext(DbContextOptions<ApplicationContext> options) 
            : base(options)
        {
            /*Database.EnsureCreated();*/
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NetworkLinkEntity>()
                .HasKey(e => new { e.Profile_Id, e.Network_Name_Id });

            modelBuilder.Entity<PostTagEntity>()
                .HasKey(e => new { e.Post_Id, e.Tag_Id});

            modelBuilder.Entity<AssetTagEntity>()
                .HasKey(e => new { e.Asset_Id, e.Tag_Id });

            modelBuilder.Entity<AssetTagEntity>()
                .HasOne(at => at.Asset)
                .WithMany(a => a.Asset_Tags)
                .HasForeignKey(at => at.Asset_Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AssetTagEntity>()
                .HasOne(at => at.Tag)
                .WithMany(t => t.Asset_Tags)
                .HasForeignKey(at => at.Tag_Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PostTagEntity>()
                .HasOne(pt => pt.Post)
                .WithMany(p => p.Post_Tags)
                .HasForeignKey(pt => pt.Post_Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PostTagEntity>()
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.Post_Tags)
                .HasForeignKey(pt => pt.Tag_Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SubscriptionEntity>()
                .HasKey(e => new { e.From_Whom_Profile_Id, e.To_Whom_Profile_Id });

            modelBuilder.Entity<UserEntity>()
                .HasOne(u => u.Profile)
                .WithOne(p => p.User)
                .HasForeignKey<UserEntity>(u => u.Profile_Id);

            modelBuilder.Entity<UserEntity>()
                .HasOne(u => u.Role) 
                .WithMany(r => r.Users)    
                .HasForeignKey(u => u.Role_Id);

            modelBuilder.Entity<AssetImageEntity>()
                .HasOne(ai => ai.Asset)
                .WithMany(a => a.Asset_Images)
                .HasForeignKey(ai => ai.Asset_Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AssetCommentEntity>()
                .HasOne(ac => ac.Asset)
                .WithMany(a => a.Asset_Comments)
                .HasForeignKey(ac => ac.Asset_Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AssetCommentEntity>()
                .HasOne(ac => ac.Profile)
                .WithMany(p => p.Asset_Comments)
                .HasForeignKey(ac => ac.Profile_Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AssetTypeEntity>()
                .HasMany(at => at.Assets)
                .WithOne(a => a.Type)
                .HasForeignKey(a => a.Type_Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AssetStatusEntity>()
                .HasMany(s => s.Assets)
                .WithOne(a => a.Status)
                .HasForeignKey(a => a.Status_Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserProfileEntity>()
                .HasMany(p => p.Network_Links)
                .WithOne(nl => nl.User_Profile)
                .HasForeignKey(nl => nl.Profile_Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserProfileEntity>()
                .HasMany(p => p.Asset_Comments)
                .WithOne(c => c.Profile)
                .HasForeignKey(c => c.Profile_Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserProfileEntity>()
                .HasMany(p => p.Post_Comments)
                .WithOne(c => c.Profile)
                .HasForeignKey(c => c.Profile_Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserProfileEntity>()
                .HasMany(profile => profile.Posts)
                .WithOne(post => post.Profile)
                .HasForeignKey(post => post.Profile_Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserProfileEntity>()
                .HasMany(profile => profile.Assets)
                .WithOne(a => a.Profile)
                .HasForeignKey(a => a.Profile_Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<AssetLikeEntity>()
                .HasKey(al => new { al.Asset_Id, al.Profile_Id });

            modelBuilder.Entity<AssetLikeEntity>()
                .HasOne(l => l.Asset)
                .WithMany(a => a.Asset_Likes)
                .HasForeignKey(l => l.Asset_Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AssetLikeEntity>()
                .HasOne(l => l.Profile)
                .WithMany(p => p.Asset_Likes)
                .HasForeignKey(l => l.Profile_Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ModerationRequestStatusEntity>()
                .HasMany(s => s.moderationRequests)
                .WithOne(mr => mr.Status)
                .HasForeignKey(mr => mr.Status_Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ModerationRequestEntity>()
                .HasOne(mr => mr.Asset)
                .WithMany(a => a.ModerationRequests)
                .HasForeignKey(mr => mr.Asset_Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
        /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         {
             optionsBuilder.UseMySQL();
         }*/
    }
}
