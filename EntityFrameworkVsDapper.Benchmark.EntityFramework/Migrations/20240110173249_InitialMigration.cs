using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EntityFrameworkVsDapper.Benchmark.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "brands",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    created_date_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_date_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_brands", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "materials",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    cost = table.Column<int>(type: "integer", nullable: false),
                    created_date_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_date_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_materials", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "styles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    brand_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    created_date_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_date_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_styles", x => x.id);
                    table.ForeignKey(
                        name: "fk_styles_brands_brand_id",
                        column: x => x.brand_id,
                        principalTable: "brands",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "benches",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    material_id = table.Column<int>(type: "integer", nullable: false),
                    style_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    cost = table.Column<int>(type: "integer", nullable: false),
                    height = table.Column<int>(type: "integer", nullable: false),
                    width = table.Column<int>(type: "integer", nullable: false),
                    depth = table.Column<int>(type: "integer", nullable: false),
                    created_date_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_date_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_benches", x => x.id);
                    table.ForeignKey(
                        name: "fk_benches_materials_material_id",
                        column: x => x.material_id,
                        principalTable: "materials",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_benches_styles_style_id",
                        column: x => x.style_id,
                        principalTable: "styles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_benches_id",
                table: "benches",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_benches_material_id",
                table: "benches",
                column: "material_id");

            migrationBuilder.CreateIndex(
                name: "ix_benches_style_id",
                table: "benches",
                column: "style_id");

            migrationBuilder.CreateIndex(
                name: "ix_brands_id",
                table: "brands",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_materials_id",
                table: "materials",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_styles_brand_id",
                table: "styles",
                column: "brand_id");

            migrationBuilder.CreateIndex(
                name: "ix_styles_id",
                table: "styles",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "benches");

            migrationBuilder.DropTable(
                name: "materials");

            migrationBuilder.DropTable(
                name: "styles");

            migrationBuilder.DropTable(
                name: "brands");
        }
    }
}
