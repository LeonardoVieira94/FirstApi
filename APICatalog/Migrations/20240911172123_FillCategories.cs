using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalog.Migrations
{
    /// <inheritdoc />
    public partial class FillCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Categories(Name, ImageUrl) Values('Drinks', 'drinks.jpg')");
            mb.Sql("Insert into Categories(Name, ImageUrl) Values('FastFood', 'fastfood.jpg')");
            mb.Sql("Insert into Categories(Name, ImageUrl) Values('Dessert', 'dessert.jpg')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Categories");
        }
    }
}
