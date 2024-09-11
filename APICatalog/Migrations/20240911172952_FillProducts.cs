using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalog.Migrations
{
    /// <inheritdoc />
    public partial class FillProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Products(ProductName, ProductDescription, Price, ImageUrl, Inventory, RegDate, CategoryId) Values('Diet Coke','Soft Drink 350ml',5.45,'coke.jpg',50,now(),1)");
            mb.Sql("Insert into Products(ProductName, ProductDescription, Price, ImageUrl, Inventory, RegDate, CategoryId) Values('Tuna Sandwich','Tuna Sandwich with mayonnaise',8.50,'atum.jpg',10,now(),2)");
            mb.Sql("Insert into Products(ProductName, ProductDescription, Price, ImageUrl, Inventory, RegDate, CategoryId) Values('Pudding','Condensed milk pudding 100g',6.75,'pudding.jpg',20,now(),3)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Products");
        }
    }
}
