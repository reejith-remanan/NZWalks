using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingdataforRegionsandDifficulties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("13875ec7-9f50-434a-83c4-b46a6f1b0f95"), "Hard" },
                    { new Guid("14ceba71-4b51-4777-9b17-46602cf66153"), "Bay Of Plenty" },
                    { new Guid("5aac2bfb-6f9a-4707-95cb-6c0cf6b5dc9f"), "Medium" },
                    { new Guid("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"), "Northland" },
                    { new Guid("845d118c-7a37-40f2-9d74-44e42202caf0"), "Easy" },
                    { new Guid("906cb139-415a-4bbb-a174-1a1faf9fb1f6"), "Nelson" },
                    { new Guid("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"), "Wellington" },
                    { new Guid("f077a22e-4248-4bf6-b564-c7cf4e250263"), "Southland" },
                    { new Guid("f7248fc3-2585-4efb-8d1d-1c555f4087f6"), "Auckland" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("13875ec7-9f50-434a-83c4-b46a6f1b0f95"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("14ceba71-4b51-4777-9b17-46602cf66153"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("5aac2bfb-6f9a-4707-95cb-6c0cf6b5dc9f"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("845d118c-7a37-40f2-9d74-44e42202caf0"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("906cb139-415a-4bbb-a174-1a1faf9fb1f6"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("f077a22e-4248-4bf6-b564-c7cf4e250263"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("f7248fc3-2585-4efb-8d1d-1c555f4087f6"));
        }
    }
}
