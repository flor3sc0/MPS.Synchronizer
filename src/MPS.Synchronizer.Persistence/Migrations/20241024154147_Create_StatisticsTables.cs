using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MPS.Synchronizer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Create_StatisticsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StatisticsIncomes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Уникальный идентификатор записи"),
                    incomeId = table.Column<int>(type: "integer", nullable: false, comment: "Номер поставки"),
                    number = table.Column<string>(type: "text", nullable: true, comment: "Номер УПД"),
                    date = table.Column<DateOnly>(type: "date", nullable: false, comment: "Дата поступления"),
                    lastChangeDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Дата и время обновления информации в сервисе. Это поле соответствует параметру dateFrom в запросе"),
                    supplierArticle = table.Column<string>(type: "text", nullable: true, comment: "Артикул продавца"),
                    techSize = table.Column<string>(type: "text", nullable: true, comment: "Размер товара (пример S, M, L, XL, 42, 42-43)"),
                    barcode = table.Column<string>(type: "text", nullable: true, comment: "Бар-код"),
                    quantity = table.Column<int>(type: "integer", nullable: false, comment: "Количество"),
                    totalPrice = table.Column<decimal>(type: "numeric", nullable: false, comment: "Цена из УПД"),
                    dateClose = table.Column<DateOnly>(type: "date", nullable: false, comment: "Дата принятия (закрытия) в WB"),
                    warehouseName = table.Column<string>(type: "text", nullable: true, comment: "Название склада"),
                    nmId = table.Column<long>(type: "bigint", nullable: false, comment: "Артикул WB"),
                    status = table.Column<string>(type: "text", nullable: true, comment: "Текущий статус поставки"),
                    SyncDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Дата и время синхронизации записи через WB-Api")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatisticsIncomes", x => x.Id);
                },
                comment: "Информация о поставке");

            migrationBuilder.CreateTable(
                name: "StatisticsOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Уникальный идентификатор записи"),
                    date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Дата и время заказа. Это поле соответствует параметру dateFrom в запросе, если параметр flag=1"),
                    lastChangeDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Дата и время обновления информации в сервисе. Это поле соответствует параметру dateFrom в запросе, если параметр flag=0 или не указан"),
                    warehouseName = table.Column<string>(type: "text", nullable: true, comment: "Склад отгрузки"),
                    countryName = table.Column<string>(type: "text", nullable: true, comment: "Страна"),
                    oblastOkrugName = table.Column<string>(type: "text", nullable: true, comment: "Округ"),
                    regionName = table.Column<string>(type: "text", nullable: true, comment: "Регион"),
                    supplierArticle = table.Column<string>(type: "text", nullable: true, comment: "Артикул продавца"),
                    nmId = table.Column<long>(type: "bigint", nullable: false, comment: "Артикул WB"),
                    barcode = table.Column<string>(type: "text", nullable: true, comment: "Баркод"),
                    category = table.Column<string>(type: "text", nullable: true, comment: "Категория"),
                    subject = table.Column<string>(type: "text", nullable: true, comment: "Предмет"),
                    brand = table.Column<string>(type: "text", nullable: true, comment: "Бренд"),
                    techSize = table.Column<string>(type: "text", nullable: true, comment: "Размер товара"),
                    incomeID = table.Column<long>(type: "bigint", nullable: false, comment: "Номер поставки"),
                    isSupply = table.Column<bool>(type: "boolean", nullable: false, comment: "Договор поставки"),
                    isRealization = table.Column<bool>(type: "boolean", nullable: false, comment: "Договор реализации"),
                    totalPrice = table.Column<decimal>(type: "numeric", nullable: false, comment: "Цена без скидок"),
                    discountPercent = table.Column<long>(type: "bigint", nullable: false, comment: "Скидка продавца"),
                    spp = table.Column<decimal>(type: "numeric", nullable: false, comment: "Скидка WB"),
                    finishedPrice = table.Column<decimal>(type: "numeric", nullable: false, comment: "Цена с учетом всех скидок, кроме суммы по WB Кошельку"),
                    priceWithDisc = table.Column<decimal>(type: "numeric", nullable: false, comment: "Цена со скидкой продавца (= totalPrice * (1 - discountPercent/100))"),
                    isCancel = table.Column<bool>(type: "boolean", nullable: false, comment: "Отмена заказа. true - заказ отменен"),
                    cancelDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Дата и время отмены заказа. Если заказ не был отменен, то '0001-01-01T00:00:00'"),
                    orderType = table.Column<string>(type: "text", nullable: true, comment: "Тип заказа: Клиентский/Возврат Брака/Принудительный возврат/Возврат обезлички/Возврат Неверного Вложения/Возврат Продавца/Возврат из Отзыва/АвтоВозврат МП/Недокомплект (Вина продавца)/Возврат КГТ"),
                    sticker = table.Column<string>(type: "text", nullable: true, comment: "Идентификатор стикера"),
                    gNumber = table.Column<string>(type: "text", nullable: true, comment: "Номер заказа"),
                    srid = table.Column<string>(type: "text", nullable: true, comment: "Уникальный идентификатор заказа. Примечание для использующих API Маркетплейс: srid равен rid в ответах методов сборочных заданий."),
                    SyncDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Дата и время синхронизации записи через WB-Api")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatisticsOrders", x => x.Id);
                },
                comment: "Информация о заказах");

            migrationBuilder.CreateTable(
                name: "StatisticsRealizationReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Уникальный идентификатор записи"),
                    realizationreport_id = table.Column<long>(type: "bigint", nullable: false, comment: "Номер отчёта"),
                    date_from = table.Column<DateOnly>(type: "date", nullable: false, comment: "Дата начала отчётного периода"),
                    date_to = table.Column<DateOnly>(type: "date", nullable: false, comment: "Дата конца отчётного периода"),
                    create_dt = table.Column<DateOnly>(type: "date", nullable: false, comment: "Дата формирования отчёта"),
                    currency_name = table.Column<string>(type: "text", nullable: true, comment: "Валюта отчёта"),
                    suppliercontract_code = table.Column<string>(type: "text", nullable: true, comment: "Договор"),
                    rrd_id = table.Column<long>(type: "bigint", nullable: false, comment: "Номер строки"),
                    gi_id = table.Column<long>(type: "bigint", nullable: false, comment: "Номер поставки"),
                    subject_name = table.Column<string>(type: "text", nullable: true, comment: "Предмет"),
                    nm_id = table.Column<long>(type: "bigint", nullable: false, comment: "Артикул WB"),
                    brand_name = table.Column<string>(type: "text", nullable: true, comment: "Бренд"),
                    sa_name = table.Column<string>(type: "text", nullable: true, comment: "Артикул продавца"),
                    ts_name = table.Column<string>(type: "text", nullable: true, comment: "Размер"),
                    barcode = table.Column<string>(type: "text", nullable: true, comment: "Баркод"),
                    doc_type_name = table.Column<string>(type: "text", nullable: true, comment: "Тип документа"),
                    quantity = table.Column<long>(type: "bigint", nullable: false, comment: "Количество"),
                    retail_price = table.Column<decimal>(type: "numeric", nullable: false, comment: "Цена розничная"),
                    retail_amount = table.Column<decimal>(type: "numeric", nullable: false, comment: "Сумма продаж (возвратов)"),
                    sale_percent = table.Column<long>(type: "bigint", nullable: false, comment: "Согласованная скидка"),
                    commission_percent = table.Column<decimal>(type: "numeric", nullable: false, comment: "Процент комиссии"),
                    office_name = table.Column<string>(type: "text", nullable: true, comment: "Склад"),
                    supplier_oper_name = table.Column<string>(type: "text", nullable: true, comment: "Обоснование для оплаты"),
                    order_dt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: "Дата заказа. Присылается с явным указанием часового пояса"),
                    sale_dt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: "Дата продажи. Присылается с явным указанием часового пояса"),
                    rr_dt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: "Дата операции. Присылается с явным указанием часового пояса"),
                    shk_id = table.Column<long>(type: "bigint", nullable: false, comment: "Штрих-код"),
                    retail_price_withdisc_rub = table.Column<decimal>(type: "numeric", nullable: false, comment: "Цена розничная с учетом согласованной скидки"),
                    delivery_amount = table.Column<long>(type: "bigint", nullable: false, comment: "Количество доставок"),
                    return_amount = table.Column<long>(type: "bigint", nullable: false, comment: "Количество возвратов"),
                    delivery_rub = table.Column<decimal>(type: "numeric", nullable: false, comment: "Стоимость логистики"),
                    gi_box_type_name = table.Column<string>(type: "text", nullable: true, comment: "Тип коробов"),
                    product_discount_for_report = table.Column<decimal>(type: "numeric", nullable: false, comment: "Согласованный продуктовый дисконт"),
                    supplier_promo = table.Column<decimal>(type: "numeric", nullable: false, comment: "Промокод"),
                    rid = table.Column<long>(type: "bigint", nullable: false, comment: "Уникальный идентификатор заказа"),
                    ppvz_spp_prc = table.Column<decimal>(type: "numeric", nullable: false, comment: "Скидка постоянного покупателя"),
                    ppvz_kvw_prc_base = table.Column<decimal>(type: "numeric", nullable: false, comment: "Размер кВВ без НДС, % базовый"),
                    ppvz_kvw_prc = table.Column<decimal>(type: "numeric", nullable: false, comment: "Итоговый кВВ без НДС, %"),
                    sup_rating_prc_up = table.Column<decimal>(type: "numeric", nullable: false, comment: "Размер снижения кВВ из-за рейтинга"),
                    is_kgvp_v2 = table.Column<decimal>(type: "numeric", nullable: false, comment: "Размер снижения кВВ из-за акции"),
                    ppvz_sales_commission = table.Column<decimal>(type: "numeric", nullable: false, comment: "Вознаграждение с продаж до вычета услуг поверенного, без НДС"),
                    ppvz_for_pay = table.Column<decimal>(type: "numeric", nullable: false, comment: "К перечислению продавцу за реализованный товар"),
                    ppvz_reward = table.Column<decimal>(type: "numeric", nullable: false, comment: "Возмещение за выдачу и возврат товаров на ПВЗ"),
                    acquiring_fee = table.Column<decimal>(type: "numeric", nullable: false, comment: "Возмещение издержек по эквайрингу. Издержки WB за услуги эквайринга: вычитаются из вознаграждения WB и не влияют на доход продавца"),
                    acquiring_percent = table.Column<decimal>(type: "numeric", nullable: false, comment: "Размер комиссии за эквайринг без НДС, %"),
                    acquiring_bank = table.Column<string>(type: "text", nullable: true, comment: "Наименование банка-эквайера"),
                    ppvz_vw = table.Column<decimal>(type: "numeric", nullable: false, comment: "Вознаграждение WB без НДС"),
                    ppvz_vw_nds = table.Column<decimal>(type: "numeric", nullable: false, comment: "НДС с вознаграждения WB"),
                    ppvz_office_id = table.Column<long>(type: "bigint", nullable: false, comment: "Номер офиса"),
                    ppvz_office_name = table.Column<string>(type: "text", nullable: true, comment: "Наименование офиса доставки"),
                    ppvz_supplier_id = table.Column<long>(type: "bigint", nullable: false, comment: "Номер партнера"),
                    ppvz_supplier_name = table.Column<string>(type: "text", nullable: true, comment: "Партнер"),
                    ppvz_inn = table.Column<string>(type: "text", nullable: true, comment: "ИНН партнера"),
                    declaration_number = table.Column<string>(type: "text", nullable: true, comment: "Номер таможенной декларации"),
                    bonus_type_name = table.Column<string>(type: "text", nullable: true, comment: "Обоснование штрафов и доплат"),
                    sticker_id = table.Column<string>(type: "text", nullable: true, comment: "Цифровое значение стикера, который клеится на товар в процессе сборки заказа по схеме 'Маркетплейс'"),
                    site_country = table.Column<string>(type: "text", nullable: true, comment: "Страна продажи"),
                    penalty = table.Column<decimal>(type: "numeric", nullable: false, comment: "Штрафы"),
                    additional_payment = table.Column<decimal>(type: "numeric", nullable: false, comment: "Доплаты"),
                    rebill_logistic_cost = table.Column<decimal>(type: "numeric", nullable: false, comment: "Возмещение издержек по перевозке"),
                    rebill_logistic_org = table.Column<string>(type: "text", nullable: true, comment: "Организатор перевозки"),
                    kiz = table.Column<string>(type: "text", nullable: true, comment: "Код маркировки"),
                    storage_fee = table.Column<decimal>(type: "numeric", nullable: false, comment: "Стоимость хранения"),
                    deduction = table.Column<decimal>(type: "numeric", nullable: false, comment: "Прочие удержания/выплаты"),
                    acceptance = table.Column<decimal>(type: "numeric", nullable: false, comment: "Стоимость платной приёмки"),
                    srid = table.Column<string>(type: "text", nullable: true, comment: "Уникальный идентификатор заказа.Примечание для использующих API Marketplace: srid равен rid в ответах методов сборочных заданий"),
                    report_type = table.Column<int>(type: "integer", nullable: false, comment: "Тип отчёта: 1 — стандартный, 2 — для уведомления о выкупе"),
                    SyncDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Дата и время синхронизации записи через WB-Api")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatisticsRealizationReports", x => x.Id);
                },
                comment: "Детализация к еженедельному отчёту реализации");

            migrationBuilder.CreateTable(
                name: "StatisticsSales",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Уникальный идентификатор записи"),
                    date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Дата и время продажи. Это поле соответствует параметру dateFrom в запросе, если параметр flag=1"),
                    lastChangeDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Дата и время обновления информации в сервисе. Это поле соответствует параметру dateFrom в запросе, если параметр flag=0 или не указан"),
                    warehouseName = table.Column<string>(type: "text", nullable: true, comment: "Склад отгрузки"),
                    countryName = table.Column<string>(type: "text", nullable: true, comment: "Страна"),
                    oblastOkrugName = table.Column<string>(type: "text", nullable: true, comment: "Округ"),
                    regionName = table.Column<string>(type: "text", nullable: true, comment: "Регион"),
                    supplierArticle = table.Column<string>(type: "text", nullable: true, comment: "Артикул продавца"),
                    nmId = table.Column<long>(type: "bigint", nullable: false, comment: "Артикул WB"),
                    barcode = table.Column<string>(type: "text", nullable: true, comment: "Баркод"),
                    category = table.Column<string>(type: "text", nullable: true, comment: "Категория"),
                    subject = table.Column<string>(type: "text", nullable: true, comment: "Предмет"),
                    brand = table.Column<string>(type: "text", nullable: true, comment: "Бренд"),
                    techSize = table.Column<string>(type: "text", nullable: true, comment: "Размер товара"),
                    incomeID = table.Column<long>(type: "bigint", nullable: false, comment: "Номер поставки"),
                    isSupply = table.Column<bool>(type: "boolean", nullable: false, comment: "Договор поставки"),
                    isRealization = table.Column<bool>(type: "boolean", nullable: false, comment: "Договор реализации"),
                    totalPrice = table.Column<decimal>(type: "numeric", nullable: false, comment: "Цена без скидок"),
                    discountPercent = table.Column<long>(type: "bigint", nullable: false, comment: "Скидка продавца"),
                    spp = table.Column<decimal>(type: "numeric", nullable: false, comment: "Скидка WB"),
                    paymentSaleAmount = table.Column<long>(type: "bigint", nullable: false, comment: "Оплачено с WB Кошелька"),
                    forPay = table.Column<decimal>(type: "numeric", nullable: false, comment: "К перечислению продавцу"),
                    finishedPrice = table.Column<decimal>(type: "numeric", nullable: false, comment: "Фактическая цена с учетом всех скидок (к взиманию с покупателя)"),
                    priceWithDisc = table.Column<decimal>(type: "numeric", nullable: false, comment: "Цена со скидкой продавца, от которой считается сумма к перечислению продавцу forPay (= totalPrice * (1 - discountPercent/100))"),
                    saleID = table.Column<string>(type: "text", nullable: true, comment: "Уникальный идентификатор продажи/возврата"),
                    orderType = table.Column<string>(type: "text", nullable: true, comment: "Тип заказа: Клиентский/Возврат Брака/Принудительный возврат/Возврат обезлички/Возврат Неверного Вложения/Возврат Продавца/Возврат из Отзыва/АвтоВозврат МП/Недокомплект (Вина продавца)/Возврат КГТ"),
                    sticker = table.Column<string>(type: "text", nullable: true, comment: "Идентификатор стикера"),
                    gNumber = table.Column<string>(type: "text", nullable: true, comment: "Номер заказа"),
                    srid = table.Column<string>(type: "text", nullable: true, comment: "Уникальный идентификатор заказа. Примечание для использующих API Маркетплейс: srid равен rid в ответах методов сборочных заданий"),
                    SyncDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Дата и время синхронизации записи через WB-Api")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatisticsSales", x => x.Id);
                },
                comment: "Информация о продаже или возврате товара");

            migrationBuilder.CreateTable(
                name: "StatisticsStocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Уникальный идентификатор записи"),
                    lastChangeDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Дата и время обновления информации в сервисе. Это поле соответствует параметру dateFrom в запросе"),
                    warehouseName = table.Column<string>(type: "text", nullable: true, comment: "Название склада"),
                    supplierArticle = table.Column<string>(type: "text", nullable: true, comment: "Артикул продавца"),
                    nmId = table.Column<long>(type: "bigint", nullable: false, comment: "Артикул WB"),
                    barcode = table.Column<string>(type: "text", nullable: true, comment: "Баркод"),
                    quantity = table.Column<long>(type: "bigint", nullable: false, comment: "Количество, доступное для продажи (сколько можно добавить в корзину)"),
                    inWayToClient = table.Column<long>(type: "bigint", nullable: false, comment: "В пути к клиенту"),
                    inWayFromClient = table.Column<long>(type: "bigint", nullable: false, comment: "В пути от клиента"),
                    quantityFull = table.Column<long>(type: "bigint", nullable: false, comment: "Полное (непроданное) количество, которое числится за складом (= quantity + в пути)"),
                    category = table.Column<string>(type: "text", nullable: true, comment: "Категория"),
                    subject = table.Column<string>(type: "text", nullable: true, comment: "Предмет"),
                    brand = table.Column<string>(type: "text", nullable: true, comment: "Бренд"),
                    techSize = table.Column<string>(type: "text", nullable: true, comment: "Размер"),
                    Price = table.Column<decimal>(type: "numeric", nullable: false, comment: "Цена"),
                    Discount = table.Column<decimal>(type: "numeric", nullable: false, comment: "Скидка"),
                    isSupply = table.Column<bool>(type: "boolean", nullable: false, comment: "Договор поставки (внутренние технологические данные)"),
                    isRealization = table.Column<bool>(type: "boolean", nullable: false, comment: "Договор реализации (внутренние технологические данные)"),
                    SCCode = table.Column<string>(type: "text", nullable: true, comment: "Код контракта (внутренние технологические данные)"),
                    SyncDateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, comment: "Дата и время синхронизации записи через WB-Api")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatisticsStocks", x => x.Id);
                },
                comment: "Информация о остатках товаров на складах WB");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsIncomes_date",
                table: "StatisticsIncomes",
                column: "date");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsIncomes_dateClose",
                table: "StatisticsIncomes",
                column: "dateClose");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsIncomes_incomeId",
                table: "StatisticsIncomes",
                column: "incomeId");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsIncomes_lastChangeDate",
                table: "StatisticsIncomes",
                column: "lastChangeDate");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsIncomes_nmId",
                table: "StatisticsIncomes",
                column: "nmId");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsIncomes_quantity",
                table: "StatisticsIncomes",
                column: "quantity");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsIncomes_totalPrice",
                table: "StatisticsIncomes",
                column: "totalPrice");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsOrders_cancelDate",
                table: "StatisticsOrders",
                column: "cancelDate");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsOrders_date",
                table: "StatisticsOrders",
                column: "date");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsOrders_discountPercent",
                table: "StatisticsOrders",
                column: "discountPercent");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsOrders_finishedPrice",
                table: "StatisticsOrders",
                column: "finishedPrice");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsOrders_incomeID",
                table: "StatisticsOrders",
                column: "incomeID");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsOrders_isCancel",
                table: "StatisticsOrders",
                column: "isCancel");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsOrders_isRealization",
                table: "StatisticsOrders",
                column: "isRealization");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsOrders_isSupply",
                table: "StatisticsOrders",
                column: "isSupply");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsOrders_lastChangeDate",
                table: "StatisticsOrders",
                column: "lastChangeDate");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsOrders_nmId",
                table: "StatisticsOrders",
                column: "nmId");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsOrders_priceWithDisc",
                table: "StatisticsOrders",
                column: "priceWithDisc");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsOrders_spp",
                table: "StatisticsOrders",
                column: "spp");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsOrders_totalPrice",
                table: "StatisticsOrders",
                column: "totalPrice");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_acceptance",
                table: "StatisticsRealizationReports",
                column: "acceptance");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_acquiring_fee",
                table: "StatisticsRealizationReports",
                column: "acquiring_fee");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_acquiring_percent",
                table: "StatisticsRealizationReports",
                column: "acquiring_percent");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_additional_payment",
                table: "StatisticsRealizationReports",
                column: "additional_payment");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_commission_percent",
                table: "StatisticsRealizationReports",
                column: "commission_percent");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_create_dt",
                table: "StatisticsRealizationReports",
                column: "create_dt");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_date_from",
                table: "StatisticsRealizationReports",
                column: "date_from");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_date_to",
                table: "StatisticsRealizationReports",
                column: "date_to");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_deduction",
                table: "StatisticsRealizationReports",
                column: "deduction");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_delivery_amount",
                table: "StatisticsRealizationReports",
                column: "delivery_amount");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_delivery_rub",
                table: "StatisticsRealizationReports",
                column: "delivery_rub");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_gi_id",
                table: "StatisticsRealizationReports",
                column: "gi_id");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_is_kgvp_v2",
                table: "StatisticsRealizationReports",
                column: "is_kgvp_v2");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_nm_id",
                table: "StatisticsRealizationReports",
                column: "nm_id");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_order_dt",
                table: "StatisticsRealizationReports",
                column: "order_dt");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_penalty",
                table: "StatisticsRealizationReports",
                column: "penalty");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_ppvz_for_pay",
                table: "StatisticsRealizationReports",
                column: "ppvz_for_pay");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_ppvz_kvw_prc",
                table: "StatisticsRealizationReports",
                column: "ppvz_kvw_prc");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_ppvz_kvw_prc_base",
                table: "StatisticsRealizationReports",
                column: "ppvz_kvw_prc_base");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_ppvz_office_id",
                table: "StatisticsRealizationReports",
                column: "ppvz_office_id");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_ppvz_reward",
                table: "StatisticsRealizationReports",
                column: "ppvz_reward");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_ppvz_sales_commission",
                table: "StatisticsRealizationReports",
                column: "ppvz_sales_commission");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_ppvz_spp_prc",
                table: "StatisticsRealizationReports",
                column: "ppvz_spp_prc");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_ppvz_supplier_id",
                table: "StatisticsRealizationReports",
                column: "ppvz_supplier_id");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_ppvz_vw",
                table: "StatisticsRealizationReports",
                column: "ppvz_vw");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_ppvz_vw_nds",
                table: "StatisticsRealizationReports",
                column: "ppvz_vw_nds");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_product_discount_for_report",
                table: "StatisticsRealizationReports",
                column: "product_discount_for_report");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_quantity",
                table: "StatisticsRealizationReports",
                column: "quantity");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_realizationreport_id",
                table: "StatisticsRealizationReports",
                column: "realizationreport_id");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_rebill_logistic_cost",
                table: "StatisticsRealizationReports",
                column: "rebill_logistic_cost");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_report_type",
                table: "StatisticsRealizationReports",
                column: "report_type");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_retail_amount",
                table: "StatisticsRealizationReports",
                column: "retail_amount");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_retail_price",
                table: "StatisticsRealizationReports",
                column: "retail_price");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_retail_price_withdisc_rub",
                table: "StatisticsRealizationReports",
                column: "retail_price_withdisc_rub");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_return_amount",
                table: "StatisticsRealizationReports",
                column: "return_amount");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_rid",
                table: "StatisticsRealizationReports",
                column: "rid");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_rr_dt",
                table: "StatisticsRealizationReports",
                column: "rr_dt");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_sale_dt",
                table: "StatisticsRealizationReports",
                column: "sale_dt");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_sale_percent",
                table: "StatisticsRealizationReports",
                column: "sale_percent");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_shk_id",
                table: "StatisticsRealizationReports",
                column: "shk_id");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_storage_fee",
                table: "StatisticsRealizationReports",
                column: "storage_fee");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_sup_rating_prc_up",
                table: "StatisticsRealizationReports",
                column: "sup_rating_prc_up");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsRealizationReports_supplier_promo",
                table: "StatisticsRealizationReports",
                column: "supplier_promo");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsSales_date",
                table: "StatisticsSales",
                column: "date");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsSales_discountPercent",
                table: "StatisticsSales",
                column: "discountPercent");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsSales_finishedPrice",
                table: "StatisticsSales",
                column: "finishedPrice");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsSales_forPay",
                table: "StatisticsSales",
                column: "forPay");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsSales_incomeID",
                table: "StatisticsSales",
                column: "incomeID");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsSales_isRealization",
                table: "StatisticsSales",
                column: "isRealization");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsSales_isSupply",
                table: "StatisticsSales",
                column: "isSupply");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsSales_lastChangeDate",
                table: "StatisticsSales",
                column: "lastChangeDate");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsSales_nmId",
                table: "StatisticsSales",
                column: "nmId");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsSales_paymentSaleAmount",
                table: "StatisticsSales",
                column: "paymentSaleAmount");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsSales_priceWithDisc",
                table: "StatisticsSales",
                column: "priceWithDisc");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsSales_spp",
                table: "StatisticsSales",
                column: "spp");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsSales_totalPrice",
                table: "StatisticsSales",
                column: "totalPrice");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsStocks_Discount",
                table: "StatisticsStocks",
                column: "Discount");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsStocks_inWayFromClient",
                table: "StatisticsStocks",
                column: "inWayFromClient");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsStocks_inWayToClient",
                table: "StatisticsStocks",
                column: "inWayToClient");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsStocks_isRealization",
                table: "StatisticsStocks",
                column: "isRealization");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsStocks_isSupply",
                table: "StatisticsStocks",
                column: "isSupply");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsStocks_lastChangeDate",
                table: "StatisticsStocks",
                column: "lastChangeDate");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsStocks_nmId",
                table: "StatisticsStocks",
                column: "nmId");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsStocks_Price",
                table: "StatisticsStocks",
                column: "Price");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsStocks_quantity",
                table: "StatisticsStocks",
                column: "quantity");

            migrationBuilder.CreateIndex(
                name: "IX_StatisticsStocks_quantityFull",
                table: "StatisticsStocks",
                column: "quantityFull");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatisticsIncomes");

            migrationBuilder.DropTable(
                name: "StatisticsOrders");

            migrationBuilder.DropTable(
                name: "StatisticsRealizationReports");

            migrationBuilder.DropTable(
                name: "StatisticsSales");

            migrationBuilder.DropTable(
                name: "StatisticsStocks");
        }
    }
}
