using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiraNexus.Products.Migrations
{
    /// <inheritdoc />
    public partial class Correcao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "tenants",
                type: "char(36)",
                maxLength: 36,
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldMaxLength: 36,
                oldDefaultValue: new Guid("dc91b5a9-eff1-4d2c-9ea0-c3d00cee2ab4"))
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "products",
                type: "char(36)",
                maxLength: 36,
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldMaxLength: 36,
                oldDefaultValue: new Guid("6b352fd0-b0ac-4052-a8de-b6a7b612d612"))
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "categories",
                type: "char(36)",
                maxLength: 36,
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldMaxLength: 36,
                oldDefaultValue: new Guid("cdfe0edc-1c25-454c-a51b-9937cc2e6b44"))
                .OldAnnotation("Relational:Collation", "ascii_general_ci");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "tenants",
                type: "char(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: new Guid("dc91b5a9-eff1-4d2c-9ea0-c3d00cee2ab4"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldMaxLength: 36)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "products",
                type: "char(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: new Guid("6b352fd0-b0ac-4052-a8de-b6a7b612d612"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldMaxLength: 36)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                table: "categories",
                type: "char(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: new Guid("cdfe0edc-1c25-454c-a51b-9937cc2e6b44"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldMaxLength: 36)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");
        }
    }
}
