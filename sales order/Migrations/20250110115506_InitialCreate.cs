using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace salesorder.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    COMCUSTOMERID = table.Column<long>(name: "COM_CUSTOMER_ID", type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CUSTOMERNAME = table.Column<string>(name: "CUSTOMER_NAME", type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.COMCUSTOMERID);
                });

            migrationBuilder.CreateTable(
                name: "SalesOrders",
                columns: table => new
                {
                    SOORDERID = table.Column<long>(name: "SO_ORDER_ID", type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ORDERNO = table.Column<string>(name: "ORDER_NO", type: "nvarchar(max)", nullable: false),
                    ORDERDATE = table.Column<DateTime>(name: "ORDER_DATE", type: "datetime2", nullable: false),
                    COMCUSTOMERID = table.Column<long>(name: "COM_CUSTOMER_ID", type: "bigint", nullable: false),
                    ADDRESS = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesOrders", x => x.SOORDERID);
                    table.ForeignKey(
                        name: "FK_SalesOrders_Customers_COM_CUSTOMER_ID",
                        column: x => x.COMCUSTOMERID,
                        principalTable: "Customers",
                        principalColumn: "COM_CUSTOMER_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SoItems",
                columns: table => new
                {
                    SOITEMID = table.Column<long>(name: "SO_ITEM_ID", type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SOORDERID = table.Column<long>(name: "SO_ORDER_ID", type: "bigint", nullable: false),
                    ITEMNAME = table.Column<string>(name: "ITEM_NAME", type: "nvarchar(max)", nullable: false),
                    QUANTITY = table.Column<int>(type: "int", nullable: false),
                    PRICE = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoItems", x => x.SOITEMID);
                    table.ForeignKey(
                        name: "FK_SoItems_SalesOrders_SO_ORDER_ID",
                        column: x => x.SOORDERID,
                        principalTable: "SalesOrders",
                        principalColumn: "SO_ORDER_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrders_COM_CUSTOMER_ID",
                table: "SalesOrders",
                column: "COM_CUSTOMER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SoItems_SO_ORDER_ID",
                table: "SoItems",
                column: "SO_ORDER_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SoItems");

            migrationBuilder.DropTable(
                name: "SalesOrders");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
