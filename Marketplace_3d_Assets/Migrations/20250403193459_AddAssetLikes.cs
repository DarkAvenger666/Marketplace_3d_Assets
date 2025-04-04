using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marketplace_3d_Assets.Migrations
{
    /// <inheritdoc />
    public partial class AddAssetLikes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_asset_user_profile_UserProfileProfile_Id",
                table: "asset");

            migrationBuilder.DropForeignKey(
                name: "FK_network_link_user_profile_UserProfileEntityProfile_Id",
                table: "network_link");

            migrationBuilder.DropForeignKey(
                name: "FK_post_user_profile_UserProfileEntityProfile_Id",
                table: "post");

            migrationBuilder.DropForeignKey(
                name: "FK_post_comment_user_profile_UserProfileEntityProfile_Id",
                table: "post_comment");

            migrationBuilder.DropIndex(
                name: "IX_post_comment_UserProfileEntityProfile_Id",
                table: "post_comment");

            migrationBuilder.DropIndex(
                name: "IX_post_UserProfileEntityProfile_Id",
                table: "post");

            migrationBuilder.DropIndex(
                name: "IX_network_link_UserProfileEntityProfile_Id",
                table: "network_link");

            migrationBuilder.DropIndex(
                name: "IX_asset_UserProfileProfile_Id",
                table: "asset");

            migrationBuilder.DropColumn(
                name: "UserProfileEntityProfile_Id",
                table: "post_comment");

            migrationBuilder.DropColumn(
                name: "UserProfileEntityProfile_Id",
                table: "post");

            migrationBuilder.DropColumn(
                name: "UserProfileEntityProfile_Id",
                table: "network_link");

            migrationBuilder.DropColumn(
                name: "UserProfileProfile_Id",
                table: "asset");

            migrationBuilder.CreateIndex(
                name: "IX_post_comment_Profile_Id",
                table: "post_comment",
                column: "Profile_Id");

            migrationBuilder.CreateIndex(
                name: "IX_post_Profile_Id",
                table: "post",
                column: "Profile_Id");

            migrationBuilder.CreateIndex(
                name: "IX_asset_Profile_Id",
                table: "asset",
                column: "Profile_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_asset_user_profile_Profile_Id",
                table: "asset",
                column: "Profile_Id",
                principalTable: "user_profile",
                principalColumn: "Profile_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_network_link_user_profile_Profile_Id",
                table: "network_link",
                column: "Profile_Id",
                principalTable: "user_profile",
                principalColumn: "Profile_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_post_user_profile_Profile_Id",
                table: "post",
                column: "Profile_Id",
                principalTable: "user_profile",
                principalColumn: "Profile_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_post_comment_user_profile_Profile_Id",
                table: "post_comment",
                column: "Profile_Id",
                principalTable: "user_profile",
                principalColumn: "Profile_Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_asset_user_profile_Profile_Id",
                table: "asset");

            migrationBuilder.DropForeignKey(
                name: "FK_network_link_user_profile_Profile_Id",
                table: "network_link");

            migrationBuilder.DropForeignKey(
                name: "FK_post_user_profile_Profile_Id",
                table: "post");

            migrationBuilder.DropForeignKey(
                name: "FK_post_comment_user_profile_Profile_Id",
                table: "post_comment");

            migrationBuilder.DropIndex(
                name: "IX_post_comment_Profile_Id",
                table: "post_comment");

            migrationBuilder.DropIndex(
                name: "IX_post_Profile_Id",
                table: "post");

            migrationBuilder.DropIndex(
                name: "IX_asset_Profile_Id",
                table: "asset");

            migrationBuilder.AddColumn<Guid>(
                name: "UserProfileEntityProfile_Id",
                table: "post_comment",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "UserProfileEntityProfile_Id",
                table: "post",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "UserProfileEntityProfile_Id",
                table: "network_link",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "UserProfileProfile_Id",
                table: "asset",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_post_comment_UserProfileEntityProfile_Id",
                table: "post_comment",
                column: "UserProfileEntityProfile_Id");

            migrationBuilder.CreateIndex(
                name: "IX_post_UserProfileEntityProfile_Id",
                table: "post",
                column: "UserProfileEntityProfile_Id");

            migrationBuilder.CreateIndex(
                name: "IX_network_link_UserProfileEntityProfile_Id",
                table: "network_link",
                column: "UserProfileEntityProfile_Id");

            migrationBuilder.CreateIndex(
                name: "IX_asset_UserProfileProfile_Id",
                table: "asset",
                column: "UserProfileProfile_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_asset_user_profile_UserProfileProfile_Id",
                table: "asset",
                column: "UserProfileProfile_Id",
                principalTable: "user_profile",
                principalColumn: "Profile_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_network_link_user_profile_UserProfileEntityProfile_Id",
                table: "network_link",
                column: "UserProfileEntityProfile_Id",
                principalTable: "user_profile",
                principalColumn: "Profile_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_post_user_profile_UserProfileEntityProfile_Id",
                table: "post",
                column: "UserProfileEntityProfile_Id",
                principalTable: "user_profile",
                principalColumn: "Profile_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_post_comment_user_profile_UserProfileEntityProfile_Id",
                table: "post_comment",
                column: "UserProfileEntityProfile_Id",
                principalTable: "user_profile",
                principalColumn: "Profile_Id");
        }
    }
}
