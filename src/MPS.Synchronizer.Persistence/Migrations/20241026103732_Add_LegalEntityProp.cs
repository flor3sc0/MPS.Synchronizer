using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MPS.Synchronizer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_LegalEntityProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LegalEntity",
                table: "StatisticsStocks",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                comment: "Юр. лицо которому принадлежит текущая запись, полученная через WB-Api");

            migrationBuilder.AddColumn<string>(
                name: "LegalEntity",
                table: "StatisticsSales",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                comment: "Юр. лицо которому принадлежит текущая запись, полученная через WB-Api");

            migrationBuilder.AddColumn<string>(
                name: "LegalEntity",
                table: "StatisticsRealizationReports",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                comment: "Юр. лицо которому принадлежит текущая запись, полученная через WB-Api");

            migrationBuilder.AddColumn<string>(
                name: "LegalEntity",
                table: "StatisticsOrders",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                comment: "Юр. лицо которому принадлежит текущая запись, полученная через WB-Api");

            migrationBuilder.AddColumn<string>(
                name: "LegalEntity",
                table: "StatisticsIncomes",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                comment: "Юр. лицо которому принадлежит текущая запись, полученная через WB-Api");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsStocks_LegalEntity",
                table: "StatisticsStocks",
                column: "LegalEntity");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsSales_LegalEntity",
                table: "StatisticsSales",
                column: "LegalEntity");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_LegalEntity",
                table: "StatisticsRealizationReports",
                column: "LegalEntity");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsOrders_LegalEntity",
                table: "StatisticsOrders",
                column: "LegalEntity");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsIncomes_LegalEntity",
                table: "StatisticsIncomes",
                column: "LegalEntity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StatisticsStocks_LegalEntity",
                table: "StatisticsStocks");

            migrationBuilder.DropIndex(
                name: "IX_StatisticsSales_LegalEntity",
                table: "StatisticsSales");

            migrationBuilder.DropIndex(
                name: "IX_StatisticsRealizationReports_LegalEntity",
                table: "StatisticsRealizationReports");

            migrationBuilder.DropIndex(
                name: "IX_StatisticsOrders_LegalEntity",
                table: "StatisticsOrders");

            migrationBuilder.DropIndex(
                name: "IX_StatisticsIncomes_LegalEntity",
                table: "StatisticsIncomes");

            migrationBuilder.DropColumn(
                name: "LegalEntity",
                table: "StatisticsStocks");

            migrationBuilder.DropColumn(
                name: "LegalEntity",
                table: "StatisticsSales");

            migrationBuilder.DropColumn(
                name: "LegalEntity",
                table: "StatisticsRealizationReports");

            migrationBuilder.DropColumn(
                name: "LegalEntity",
                table: "StatisticsOrders");

            migrationBuilder.DropColumn(
                name: "LegalEntity",
                table: "StatisticsIncomes");
        }
    }
}
