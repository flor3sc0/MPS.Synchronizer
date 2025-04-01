using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MPS.Synchronizer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Fields_to_StatisticsRealizationReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "assembly_id",
                table: "StatisticsRealizationReports",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                comment: "Номер сборочного задания");

            migrationBuilder.AddColumn<decimal>(
                name: "dlv_prc",
                table: "StatisticsRealizationReports",
                type: "numeric",
                nullable: false,
                defaultValue: 0m,
                comment: "Фиксированный коэффициент склада по поставке");

            migrationBuilder.AddColumn<DateTime>(
                name: "fix_tariff_date_from",
                table: "StatisticsRealizationReports",
                type: "timestamp without time zone",
                nullable: true,
                comment: "Дата начала действия фиксации");

            migrationBuilder.AddColumn<DateTime>(
                name: "fix_tariff_date_to",
                table: "StatisticsRealizationReports",
                type: "timestamp without time zone",
                nullable: true,
                comment: "Дата окончания действия фиксации");

            migrationBuilder.AddColumn<bool>(
                name: "is_legal_entity",
                table: "StatisticsRealizationReports",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                comment: "Признак B2B-продажи");

            migrationBuilder.AddColumn<string>(
                name: "payment_processing",
                table: "StatisticsRealizationReports",
                type: "text",
                nullable: true,
                comment: "Тип платежа за Эквайринг/Комиссии за организацию платежей");

            migrationBuilder.AddColumn<bool>(
                name: "srv_dbs",
                table: "StatisticsRealizationReports",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                comment: "Признак услуги платной доставки");

            migrationBuilder.AddColumn<string>(
                name: "trbx_id",
                table: "StatisticsRealizationReports",
                type: "text",
                nullable: true,
                comment: "Номер короба для платной приёмки");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_assembly_id",
                table: "StatisticsRealizationReports",
                column: "assembly_id");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_dlv_prc",
                table: "StatisticsRealizationReports",
                column: "dlv_prc");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_fix_tariff_date_from",
                table: "StatisticsRealizationReports",
                column: "fix_tariff_date_from");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_fix_tariff_date_to",
                table: "StatisticsRealizationReports",
                column: "fix_tariff_date_to");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_is_legal_entity",
                table: "StatisticsRealizationReports",
                column: "is_legal_entity");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_srv_dbs",
                table: "StatisticsRealizationReports",
                column: "srv_dbs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StatisticsRealizationReports_assembly_id",
                table: "StatisticsRealizationReports");

            migrationBuilder.DropIndex(
                name: "IX_StatisticsRealizationReports_dlv_prc",
                table: "StatisticsRealizationReports");

            migrationBuilder.DropIndex(
                name: "IX_StatisticsRealizationReports_fix_tariff_date_from",
                table: "StatisticsRealizationReports");

            migrationBuilder.DropIndex(
                name: "IX_StatisticsRealizationReports_fix_tariff_date_to",
                table: "StatisticsRealizationReports");

            migrationBuilder.DropIndex(
                name: "IX_StatisticsRealizationReports_is_legal_entity",
                table: "StatisticsRealizationReports");

            migrationBuilder.DropIndex(
                name: "IX_StatisticsRealizationReports_srv_dbs",
                table: "StatisticsRealizationReports");

            migrationBuilder.DropColumn(
                name: "assembly_id",
                table: "StatisticsRealizationReports");

            migrationBuilder.DropColumn(
                name: "dlv_prc",
                table: "StatisticsRealizationReports");

            migrationBuilder.DropColumn(
                name: "fix_tariff_date_from",
                table: "StatisticsRealizationReports");

            migrationBuilder.DropColumn(
                name: "fix_tariff_date_to",
                table: "StatisticsRealizationReports");

            migrationBuilder.DropColumn(
                name: "is_legal_entity",
                table: "StatisticsRealizationReports");

            migrationBuilder.DropColumn(
                name: "payment_processing",
                table: "StatisticsRealizationReports");

            migrationBuilder.DropColumn(
                name: "srv_dbs",
                table: "StatisticsRealizationReports");

            migrationBuilder.DropColumn(
                name: "trbx_id",
                table: "StatisticsRealizationReports");
        }
    }
}
