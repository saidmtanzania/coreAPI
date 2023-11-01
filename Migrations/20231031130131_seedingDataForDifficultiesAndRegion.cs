using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace coreAPI.Migrations
{
    /// <inheritdoc />
    public partial class seedingDataForDifficultiesAndRegion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("27ec554d-ef7c-49e2-ae6a-7f95205d7e40"), "HARD" },
                    { new Guid("ba8baf41-f687-42a1-adc0-883461b41cb7"), "MEDIUM" },
                    { new Guid("beb25260-3c11-4bde-b638-d91b415710e6"), "EASY" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("5636a3b1-e899-46e5-9fb3-a60a9e593fb7"), "TZ", "TANZANIA", "https://www.state.gov/wp-content/uploads/2018/11/Tanzania-e1555938157355-2501x1406.jpg" },
                    { new Guid("64a4ef3b-0b1f-406c-81ff-25af1f1ea478"), "DZ", "ALGERIA", "https://lp-cms-production.imgix.net/2023-10/iStock-985914532-RFC.jpg" },
                    { new Guid("97a6bd50-aa74-4dc5-b6de-1869db81d98f"), "KE", "KENYA", "https://destinationuganda.com/wp-content/uploads/2020/10/exploring-kampala-city-uganda-capital.jpg" },
                    { new Guid("eb6fa6ba-f718-48f7-90a2-736da9197a70"), "UG", "UGANDA", "https://a.travel-assets.com/findyours-php/viewfinder/images/res70/38000/38950-Nairobi.jpg" },
                    { new Guid("ed82e0ce-5945-41b3-9cbe-1ed90d23cb26"), "AO", "ANGOLA", "https://unhabitat.org/sites/default/files/styles/featured_image_header_sm_focal/public/2019/05/shutterstock_1116891344.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("27ec554d-ef7c-49e2-ae6a-7f95205d7e40"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("ba8baf41-f687-42a1-adc0-883461b41cb7"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("beb25260-3c11-4bde-b638-d91b415710e6"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("5636a3b1-e899-46e5-9fb3-a60a9e593fb7"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("64a4ef3b-0b1f-406c-81ff-25af1f1ea478"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("97a6bd50-aa74-4dc5-b6de-1869db81d98f"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("eb6fa6ba-f718-48f7-90a2-736da9197a70"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("ed82e0ce-5945-41b3-9cbe-1ed90d23cb26"));
        }
    }
}
