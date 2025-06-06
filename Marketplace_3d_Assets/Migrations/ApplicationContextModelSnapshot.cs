﻿// <auto-generated />
using System;
using Marketplace_3d_Assets.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Marketplace_3d_Assets.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Marketplace_3d_Assets.DataAccess.Entities.AssetBundleEntity", b =>
                {
                    b.Property<Guid>("Asset_Bundle_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Asset_Bundle_Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("Asset_Id")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Creation_Date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Discount_Percaentage")
                        .HasColumnType("int");

                    b.Property<DateTime>("Modified_Date")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("Profile_Id")
                        .HasColumnType("char(36)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Asset_Bundle_Id");

                    b.ToTable("asset_bundle");
                });

            modelBuilder.Entity("Marketplace_3d_Assets.DataAccess.Entities.AssetCommentEntity", b =>
                {
                    b.Property<Guid>("Asset_Comment_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("Asset_Id")
                        .HasColumnType("char(36)");

                    b.Property<string>("Comment_Text")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("Profile_Id")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Publication_Date")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Asset_Comment_Id");

                    b.HasIndex("Asset_Id");

                    b.HasIndex("Profile_Id");

                    b.ToTable("asset_comment");
                });

            modelBuilder.Entity("Marketplace_3d_Assets.DataAccess.Entities.AssetEntity", b =>
                {
                    b.Property<Guid>("Asset_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Asset_Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Count_Of_Copies_Sold")
                        .HasColumnType("int");

                    b.Property<int>("Count_Of_Likes")
                        .HasColumnType("int");

                    b.Property<int>("Count_Of_Views")
                        .HasColumnType("int");

                    b.Property<DateTime>("Modified_Date")
                        .HasColumnType("datetime(6)");

                    b.Property<float>("Price")
                        .HasColumnType("float");

                    b.Property<Guid>("Profile_Id")
                        .HasColumnType("char(36)");

                    b.Property<int>("Status_Id")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Type_Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("Upload_Date")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Asset_Id");

                    b.HasIndex("Profile_Id");

                    b.HasIndex("Status_Id");

                    b.HasIndex("Type_Id");

                    b.ToTable("asset");
                });

            modelBuilder.Entity("Marketplace_3d_Assets.DataAccess.Entities.AssetFileEntity", b =>
                {
                    b.Property<Guid>("Asset_File_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("Asset_Id")
                        .HasColumnType("char(36)");

                    b.Property<string>("File_Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("File_Type_Id")
                        .HasColumnType("int");

                    b.HasKey("Asset_File_Id");

                    b.ToTable("asset_file");
                });

            modelBuilder.Entity("Marketplace_3d_Assets.DataAccess.Entities.AssetImageEntity", b =>
                {
                    b.Property<Guid>("Asset_Image_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("Asset_Id")
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Asset_Image_Id");

                    b.HasIndex("Asset_Id");

                    b.ToTable("asset_image");
                });

            modelBuilder.Entity("Marketplace_3d_Assets.DataAccess.Entities.AssetStatusEntity", b =>
                {
                    b.Property<int>("Status_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Status_Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Status_Id");

                    b.ToTable("asset_status");
                });

            modelBuilder.Entity("Marketplace_3d_Assets.DataAccess.Entities.AssetTagEntity", b =>
                {
                    b.Property<Guid>("Asset_Id")
                        .HasColumnType("char(36)");

                    b.Property<int>("Tag_Id")
                        .HasColumnType("int");

                    b.HasKey("Asset_Id", "Tag_Id");

                    b.HasIndex("Tag_Id");

                    b.ToTable("asset_tag");
                });

            modelBuilder.Entity("Marketplace_3d_Assets.DataAccess.Entities.AssetTypeEntity", b =>
                {
                    b.Property<int>("Type_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Type_Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Type_Id");

                    b.ToTable("asset_type");
                });

            modelBuilder.Entity("Marketplace_3d_Assets.DataAccess.Entities.CartItemEntity", b =>
                {
                    b.Property<Guid>("Cart_Item_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("Asset_Bundle_Id")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("Asset_Id")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("User_Id")
                        .HasColumnType("char(36)");

                    b.HasKey("Cart_Item_Id");

                    b.ToTable("cart_item");
                });

            modelBuilder.Entity("Marketplace_3d_Assets.DataAccess.Entities.FileTypeEntity", b =>
                {
                    b.Property<int>("File_Type_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("File_Type_Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("File_Type_Id");

                    b.ToTable("file_type");
                });

            modelBuilder.Entity("Marketplace_3d_Assets.DataAccess.Entities.ModerationRequestEntity", b =>
                {
                    b.Property<Guid>("Request_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("Asset_Id")
                        .HasColumnType("char(36)");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Completion_Date")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Is_Complited")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("Sending_Date")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("User_Id")
                        .HasColumnType("char(36)");

                    b.HasKey("Request_Id");

                    b.ToTable("moderation_request");
                });

            modelBuilder.Entity("Marketplace_3d_Assets.DataAccess.Entities.NetworkLinkEntity", b =>
                {
                    b.Property<Guid>("Profile_Id")
                        .HasColumnType("char(36)");

                    b.Property<int>("Network_Name_Id")
                        .HasColumnType("int");

                    b.Property<string>("Profile_Link")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Profile_Id", "Network_Name_Id");

                    b.ToTable("network_link");
                });

            modelBuilder.Entity("Marketplace_3d_Assets.DataAccess.Entities.NetworkNameEntity", b =>
                {
                    b.Property<int>("Network_Name_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Network_Name_Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Network_Name_Id");

                    b.ToTable("network_name");
                });

            modelBuilder.Entity("Marketplace_3d_Assets.DataAccess.Entities.PostCommentEntity", b =>
                {
                    b.Property<Guid>("Post_Comment_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Comment_Text")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("Post_Id")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("Profile_Id")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Publication_Date")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Post_Comment_Id");

                    b.HasIndex("Profile_Id");

                    b.ToTable("post_comment");
                });

            modelBuilder.Entity("Marketplace_3d_Assets.DataAccess.Entities.PostEntity", b =>
                {
                    b.Property<Guid>("Post_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("Count_Of_Likes")
                        .HasColumnType("int");

                    b.Property<int>("Count_Of_Views")
                        .HasColumnType("int");

                    b.Property<DateTime>("Modified_Date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Post_Text")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("Profile_Id")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Publication_Date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Post_Id");

                    b.HasIndex("Profile_Id");

                    b.ToTable("post");
                });

            modelBuilder.Entity("Marketplace_3d_Assets.DataAccess.Entities.PostImageEntity", b =>
                {
                    b.Property<Guid>("Post_Image_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("Post_Id")
                        .HasColumnType("char(36)");

                    b.HasKey("Post_Image_Id");

                    b.ToTable("post_image");
                });

            modelBuilder.Entity("Marketplace_3d_Assets.DataAccess.Entities.PostTagEntity", b =>
                {
                    b.Property<Guid>("Post_Id")
                        .HasColumnType("char(36)");

                    b.Property<int>("Tag_Id")
                        .HasColumnType("int");

                    b.Property<int?>("TagEntityTag_Id")
                        .HasColumnType("int");

                    b.HasKey("Post_Id", "Tag_Id");

                    b.HasIndex("TagEntityTag_Id");

                    b.ToTable("post_tag");
                });

            modelBuilder.Entity("Marketplace_3d_Assets.DataAccess.Entities.SubscriptionEntity", b =>
                {
                    b.Property<Guid>("From_Whom_Profile_Id")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("To_Whom_Profile_Id")
                        .HasColumnType("char(36)");

                    b.HasKey("From_Whom_Profile_Id", "To_Whom_Profile_Id");

                    b.ToTable("subscription");
                });

            modelBuilder.Entity("Marketplace_3d_Assets.DataAccess.Entities.TagEntity", b =>
                {
                    b.Property<int>("Tag_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Tag_Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Tag_Id");

                    b.ToTable("tag");
                });

            modelBuilder.Entity("Marketplace_3d_Assets.DataAccess.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("User_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password_Hash")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("Profile_Id")
                        .HasColumnType("char(36)");

                    b.Property<int>("Role_Id")
                        .HasColumnType("int");

                    b.HasKey("User_Id");

                    b.HasIndex("Profile_Id")
                        .IsUnique();

                    b.HasIndex("Role_Id");

                    b.ToTable("user");
                });

            modelBuilder.Entity("Marketplace_3d_Assets.DataAccess.Entities.UserProfileEntity", b =>
                {
                    b.Property<Guid>("Profile_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("About")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Creation_Date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Modified_Date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Specialization")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Subscribers_Count")
                        .HasColumnType("int");

                    b.Property<int>("Subscriptions_Count")
                        .HasColumnType("int");

                    b.Property<string>("User_Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Profile_Id");

                    b.ToTable("user_profile");
                });

            modelBuilder.Entity("Marketplace_3d_Assets.DataAccess.Entities.UserRoleEntity", b =>
                {
                    b.Property<int>("Role_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Role_Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Role_Id");

                    b.ToTable("user_role");
                });

            modelBuilder.Entity("Marketplace_3d_Assets.DataAccess.Entities.AssetCommentEntity", b =>
                {
                    b.HasOne("Marketplace_3d_Assets.DataAccess.Entities.AssetEntity", "Asset")
                        .WithMany("Asset_Comments")
                        .HasForeignKey("Asset_Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Marketplace_3d_Assets.DataAccess.Entities.UserProfileEntity", "Profile")
                        .WithMany("Asset_Comments")
                        .HasForeignKey("Profile_Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Asset");

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("Marketplace_3d_Assets.DataAccess.Entities.AssetEntity", b =>
                {
                    b.HasOne("Marketplace_3d_Assets.DataAccess.Entities.UserProfileEntity", "Profile")
                        .WithMany("Assets")
                        .HasForeignKey("Profile_Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Marketplace_3d_Assets.DataAccess.Entities.AssetStatusEntity", "Status")
                        .WithMany("Assets")
                        .HasForeignKey("Status_Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Marketplace_3d_Assets.DataAccess.Entities.AssetTypeEntity", "Type")
                        .WithMany("Assets")
                        .HasForeignKey("Type_Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Profile");

                    b.Navigation("Status");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Marketplace_3d_Assets.DataAccess.Entities.AssetImageEntity", b =>
                {
                    b.HasOne("Marketplace_3d_Assets.DataAccess.Entities.AssetEntity", "Asset")
                        .WithMany("Asset_Images")
                        .HasForeignKey("Asset_Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Asset");
                });

            modelBuilder.Entity("Marketplace_3d_Assets.DataAccess.Entities.AssetTagEntity", b =>
                {
                    b.HasOne("Marketplace_3d_Assets.DataAccess.Entities.AssetEntity", "Asset")
                        .WithMany("Asset_Tags")
                        .HasForeignKey("Asset_Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Marketplace_3d_Assets.DataAccess.Entities.TagEntity", "Tag")
                        .WithMany("Asset_Tags")
                        .HasForeignKey("Tag_Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Asset");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("Marketplace_3d_Assets.DataAccess.Entities.NetworkLinkEntity", b =>
                {
                    b.HasOne("Marketplace_3d_Assets.DataAccess.Entities.UserProfileEntity", "User_Profile")
                        .WithMany("Network_Links")
                        .HasForeignKey("Profile_Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User_Profile");
                });

            modelBuilder.Entity("Marketplace_3d_Assets.DataAccess.Entities.PostCommentEntity", b =>
                {
                    b.HasOne("Marketplace_3d_Assets.DataAccess.Entities.UserProfileEntity", "Profile")
                        .WithMany("Post_Comments")
                        .HasForeignKey("Profile_Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("Marketplace_3d_Assets.DataAccess.Entities.PostEntity", b =>
                {
                    b.HasOne("Marketplace_3d_Assets.DataAccess.Entities.UserProfileEntity", "Profile")
                        .WithMany("Posts")
                        .HasForeignKey("Profile_Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("Marketplace_3d_Assets.DataAccess.Entities.PostTagEntity", b =>
                {
                    b.HasOne("Marketplace_3d_Assets.DataAccess.Entities.TagEntity", null)
                        .WithMany("Post_Tags")
                        .HasForeignKey("TagEntityTag_Id");
                });

            modelBuilder.Entity("Marketplace_3d_Assets.DataAccess.Entities.UserEntity", b =>
                {
                    b.HasOne("Marketplace_3d_Assets.DataAccess.Entities.UserProfileEntity", "Profile")
                        .WithOne("User")
                        .HasForeignKey("Marketplace_3d_Assets.DataAccess.Entities.UserEntity", "Profile_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Marketplace_3d_Assets.DataAccess.Entities.UserRoleEntity", "Role")
                        .WithMany("Users")
                        .HasForeignKey("Role_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profile");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Marketplace_3d_Assets.DataAccess.Entities.AssetEntity", b =>
                {
                    b.Navigation("Asset_Comments");

                    b.Navigation("Asset_Images");

                    b.Navigation("Asset_Tags");
                });

            modelBuilder.Entity("Marketplace_3d_Assets.DataAccess.Entities.AssetStatusEntity", b =>
                {
                    b.Navigation("Assets");
                });

            modelBuilder.Entity("Marketplace_3d_Assets.DataAccess.Entities.AssetTypeEntity", b =>
                {
                    b.Navigation("Assets");
                });

            modelBuilder.Entity("Marketplace_3d_Assets.DataAccess.Entities.TagEntity", b =>
                {
                    b.Navigation("Asset_Tags");

                    b.Navigation("Post_Tags");
                });

            modelBuilder.Entity("Marketplace_3d_Assets.DataAccess.Entities.UserProfileEntity", b =>
                {
                    b.Navigation("Asset_Comments");

                    b.Navigation("Assets");

                    b.Navigation("Network_Links");

                    b.Navigation("Post_Comments");

                    b.Navigation("Posts");

                    b.Navigation("User")
                        .IsRequired();
                });

            modelBuilder.Entity("Marketplace_3d_Assets.DataAccess.Entities.UserRoleEntity", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
