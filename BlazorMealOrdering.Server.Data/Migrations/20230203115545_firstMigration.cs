using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorMealOrdering.Server.Data.Migrations
{
    public partial class firstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "suppliers",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "public.uuid_generate_v4()"),
                    name = table.Column<string>(type: "character varying", maxLength: 100, nullable: false),
                    web_url = table.Column<string>(type: "character varying", maxLength: 500, nullable: false),
                    isactive = table.Column<bool>(type: "boolean", nullable: false),
                    createdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_supplier_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "public.uuid_generate_v4()"),
                    first_name = table.Column<string>(type: "character varying", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "character varying", maxLength: 100, nullable: false),
                    email_address = table.Column<string>(type: "character varying", maxLength: 100, nullable: false),
                    isactive = table.Column<bool>(type: "boolean", nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "public.uuid_generate_v4()"),
                    name = table.Column<string>(type: "character varying", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying", maxLength: 1000, nullable: false),
                    expire_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    create_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    supplier_id = table.Column<Guid>(type: "uuid", nullable: false),
                    createdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_id", x => x.id);
                    table.ForeignKey(
                        name: "fk_supplier_order_id",
                        column: x => x.supplier_id,
                        principalSchema: "public",
                        principalTable: "suppliers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_order_id",
                        column: x => x.create_user_id,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_items",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "public.uuid_generate_v4()"),
                    description = table.Column<string>(type: "character varying", maxLength: 1000, nullable: false),
                    create_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    createdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orderItem_id", x => x.id);
                    table.ForeignKey(
                        name: "fk_order_orderitem_id",
                        column: x => x.order_id,
                        principalSchema: "public",
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_orderitem_id",
                        column: x => x.create_user_id,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_order_items_create_user_id",
                schema: "public",
                table: "order_items",
                column: "create_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_items_order_id",
                schema: "public",
                table: "order_items",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_create_user_id",
                schema: "public",
                table: "orders",
                column: "create_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_supplier_id",
                schema: "public",
                table: "orders",
                column: "supplier_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order_items",
                schema: "public");

            migrationBuilder.DropTable(
                name: "orders",
                schema: "public");

            migrationBuilder.DropTable(
                name: "suppliers",
                schema: "public");

            migrationBuilder.DropTable(
                name: "users",
                schema: "public");
        }
    }
}
