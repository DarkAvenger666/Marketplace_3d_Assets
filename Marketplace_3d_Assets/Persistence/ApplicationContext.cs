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
            modelBuilder.Entity<SubscriptionEntity>()
                .HasKey(e => new { e.From_Whom_Profile_Id, e.To_Whom_Profile_Id });
        }
        /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         {
             optionsBuilder.UseMySQL();
         }*/
    }
}
