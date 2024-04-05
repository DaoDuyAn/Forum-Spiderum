using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialNetwork.Infrastructure.Migrations
{
    public partial class AddSlugField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0e48f9c1-912f-4268-b82f-05ec8b1c77e9"),
                column: "Slug",
                value: "game");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("6f906c89-37a4-4dfc-bd4f-2c2b6b3b17af"),
                column: "Slug",
                value: "quan-diem-tranh-luan");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("9d1b2b4a-932d-45e0-bf39-8acbf3f727a6"),
                column: "Slug",
                value: "sach");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("a7b7fd22-97f7-4ae0-8f11-7fe83c22d812"),
                column: "Slug",
                value: "the-thao");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d3f820ec-7e1d-4c57-bf2c-5212d8c5db65"),
                column: "Slug",
                value: "khoa-hoc-cong-nghe");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Categories");
        }
    }
}
