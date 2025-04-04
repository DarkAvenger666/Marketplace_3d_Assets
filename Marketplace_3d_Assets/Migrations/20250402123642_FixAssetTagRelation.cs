using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marketplace_3d_Assets.Migrations
{
    /// <inheritdoc />
    public partial class FixAssetTagRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "asset_bundle",
                columns: table => new
                {
                    Asset_Bundle_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Asset_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Profile_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Asset_Bundle_Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Creation_Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Modified_Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Discount_Percaentage = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asset_bundle", x => x.Asset_Bundle_Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "asset_file",
                columns: table => new
                {
                    Asset_File_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Asset_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    File_Type_Id = table.Column<int>(type: "int", nullable: false),
                    File_Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asset_file", x => x.Asset_File_Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "asset_status",
                columns: table => new
                {
                    Status_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asset_status", x => x.Status_Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "asset_type",
                columns: table => new
                {
                    Type_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asset_type", x => x.Type_Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cart_item",
                columns: table => new
                {
                    Cart_Item_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    User_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Asset_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Asset_Bundle_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cart_item", x => x.Cart_Item_Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "file_type",
                columns: table => new
                {
                    File_Type_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_file_type", x => x.File_Type_Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "moderation_request",
                columns: table => new
                {
                    Request_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Sending_Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Completion_Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Asset_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Is_Complited = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Comment = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    User_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_moderation_request", x => x.Request_Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "network_name",
                columns: table => new
                {
                    Network_Name_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_network_name", x => x.Network_Name_Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "post_image",
                columns: table => new
                {
                    Post_Image_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Post_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_post_image", x => x.Post_Image_Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "subscription",
                columns: table => new
                {
                    From_Whom_Profile_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    To_Whom_Profile_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subscription", x => new { x.From_Whom_Profile_Id, x.To_Whom_Profile_Id });
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tag",
                columns: table => new
                {
                    Tag_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tag", x => x.Tag_Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user_profile",
                columns: table => new
                {
                    Profile_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    User_Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Subscribers_Count = table.Column<int>(type: "int", nullable: false),
                    Subscriptions_Count = table.Column<int>(type: "int", nullable: false),
                    Creation_Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Modified_Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Gender = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Specialization = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    City = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    About = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_profile", x => x.Profile_Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user_role",
                columns: table => new
                {
                    Role_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_role", x => x.Role_Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "post_tag",
                columns: table => new
                {
                    Post_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Tag_Id = table.Column<int>(type: "int", nullable: false),
                    TagEntityTag_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_post_tag", x => new { x.Post_Id, x.Tag_Id });
                    table.ForeignKey(
                        name: "FK_post_tag_tag_TagEntityTag_Id",
                        column: x => x.TagEntityTag_Id,
                        principalTable: "tag",
                        principalColumn: "Tag_Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "asset",
                columns: table => new
                {
                    Asset_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type_Id = table.Column<int>(type: "int", nullable: false),
                    Upload_Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Modified_Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Status_Id = table.Column<int>(type: "int", nullable: false),
                    Count_Of_Copies_Sold = table.Column<int>(type: "int", nullable: false),
                    Profile_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserProfileProfile_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Asset_Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<float>(type: "float", nullable: false),
                    Count_Of_Views = table.Column<int>(type: "int", nullable: false),
                    Count_Of_Likes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asset", x => x.Asset_Id);
                    table.ForeignKey(
                        name: "FK_asset_asset_status_Status_Id",
                        column: x => x.Status_Id,
                        principalTable: "asset_status",
                        principalColumn: "Status_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_asset_asset_type_Type_Id",
                        column: x => x.Type_Id,
                        principalTable: "asset_type",
                        principalColumn: "Type_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_asset_user_profile_UserProfileProfile_Id",
                        column: x => x.UserProfileProfile_Id,
                        principalTable: "user_profile",
                        principalColumn: "Profile_Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "network_link",
                columns: table => new
                {
                    Profile_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Network_Name_Id = table.Column<int>(type: "int", nullable: false),
                    Profile_Link = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserProfileEntityProfile_Id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_network_link", x => new { x.Profile_Id, x.Network_Name_Id });
                    table.ForeignKey(
                        name: "FK_network_link_user_profile_UserProfileEntityProfile_Id",
                        column: x => x.UserProfileEntityProfile_Id,
                        principalTable: "user_profile",
                        principalColumn: "Profile_Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "post",
                columns: table => new
                {
                    Post_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Publication_Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Modified_Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Post_Text = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Profile_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Count_Of_Views = table.Column<int>(type: "int", nullable: false),
                    Count_Of_Likes = table.Column<int>(type: "int", nullable: false),
                    UserProfileEntityProfile_Id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_post", x => x.Post_Id);
                    table.ForeignKey(
                        name: "FK_post_user_profile_UserProfileEntityProfile_Id",
                        column: x => x.UserProfileEntityProfile_Id,
                        principalTable: "user_profile",
                        principalColumn: "Profile_Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "post_comment",
                columns: table => new
                {
                    Post_Comment_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Post_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Profile_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Comment_Text = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Publication_Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UserProfileEntityProfile_Id = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_post_comment", x => x.Post_Comment_Id);
                    table.ForeignKey(
                        name: "FK_post_comment_user_profile_UserProfileEntityProfile_Id",
                        column: x => x.UserProfileEntityProfile_Id,
                        principalTable: "user_profile",
                        principalColumn: "Profile_Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    User_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password_Hash = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Profile_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Phone = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Role_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.User_Id);
                    table.ForeignKey(
                        name: "FK_user_user_profile_Profile_Id",
                        column: x => x.Profile_Id,
                        principalTable: "user_profile",
                        principalColumn: "Profile_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_user_role_Role_Id",
                        column: x => x.Role_Id,
                        principalTable: "user_role",
                        principalColumn: "Role_Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "asset_comment",
                columns: table => new
                {
                    Asset_Comment_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Asset_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Profile_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Comment_Text = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Publication_Date = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asset_comment", x => x.Asset_Comment_Id);
                    table.ForeignKey(
                        name: "FK_asset_comment_asset_Asset_Id",
                        column: x => x.Asset_Id,
                        principalTable: "asset",
                        principalColumn: "Asset_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_asset_comment_user_profile_Profile_Id",
                        column: x => x.Profile_Id,
                        principalTable: "user_profile",
                        principalColumn: "Profile_Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "asset_image",
                columns: table => new
                {
                    Asset_Image_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Asset_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asset_image", x => x.Asset_Image_Id);
                    table.ForeignKey(
                        name: "FK_asset_image_asset_Asset_Id",
                        column: x => x.Asset_Id,
                        principalTable: "asset",
                        principalColumn: "Asset_Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "asset_tag",
                columns: table => new
                {
                    Asset_Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Tag_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_asset_tag", x => new { x.Asset_Id, x.Tag_Id });
                    table.ForeignKey(
                        name: "FK_asset_tag_asset_Asset_Id",
                        column: x => x.Asset_Id,
                        principalTable: "asset",
                        principalColumn: "Asset_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_asset_tag_tag_Tag_Id",
                        column: x => x.Tag_Id,
                        principalTable: "tag",
                        principalColumn: "Tag_Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_asset_Status_Id",
                table: "asset",
                column: "Status_Id");

            migrationBuilder.CreateIndex(
                name: "IX_asset_Type_Id",
                table: "asset",
                column: "Type_Id");

            migrationBuilder.CreateIndex(
                name: "IX_asset_UserProfileProfile_Id",
                table: "asset",
                column: "UserProfileProfile_Id");

            migrationBuilder.CreateIndex(
                name: "IX_asset_comment_Asset_Id",
                table: "asset_comment",
                column: "Asset_Id");

            migrationBuilder.CreateIndex(
                name: "IX_asset_comment_Profile_Id",
                table: "asset_comment",
                column: "Profile_Id");

            migrationBuilder.CreateIndex(
                name: "IX_asset_image_Asset_Id",
                table: "asset_image",
                column: "Asset_Id");

            migrationBuilder.CreateIndex(
                name: "IX_asset_tag_Tag_Id",
                table: "asset_tag",
                column: "Tag_Id");

            migrationBuilder.CreateIndex(
                name: "IX_network_link_UserProfileEntityProfile_Id",
                table: "network_link",
                column: "UserProfileEntityProfile_Id");

            migrationBuilder.CreateIndex(
                name: "IX_post_UserProfileEntityProfile_Id",
                table: "post",
                column: "UserProfileEntityProfile_Id");

            migrationBuilder.CreateIndex(
                name: "IX_post_comment_UserProfileEntityProfile_Id",
                table: "post_comment",
                column: "UserProfileEntityProfile_Id");

            migrationBuilder.CreateIndex(
                name: "IX_post_tag_TagEntityTag_Id",
                table: "post_tag",
                column: "TagEntityTag_Id");

            migrationBuilder.CreateIndex(
                name: "IX_user_Profile_Id",
                table: "user",
                column: "Profile_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_Role_Id",
                table: "user",
                column: "Role_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "asset_bundle");

            migrationBuilder.DropTable(
                name: "asset_comment");

            migrationBuilder.DropTable(
                name: "asset_file");

            migrationBuilder.DropTable(
                name: "asset_image");

            migrationBuilder.DropTable(
                name: "asset_tag");

            migrationBuilder.DropTable(
                name: "cart_item");

            migrationBuilder.DropTable(
                name: "file_type");

            migrationBuilder.DropTable(
                name: "moderation_request");

            migrationBuilder.DropTable(
                name: "network_link");

            migrationBuilder.DropTable(
                name: "network_name");

            migrationBuilder.DropTable(
                name: "post");

            migrationBuilder.DropTable(
                name: "post_comment");

            migrationBuilder.DropTable(
                name: "post_image");

            migrationBuilder.DropTable(
                name: "post_tag");

            migrationBuilder.DropTable(
                name: "subscription");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "asset");

            migrationBuilder.DropTable(
                name: "tag");

            migrationBuilder.DropTable(
                name: "user_role");

            migrationBuilder.DropTable(
                name: "asset_status");

            migrationBuilder.DropTable(
                name: "asset_type");

            migrationBuilder.DropTable(
                name: "user_profile");
        }
    }
}
