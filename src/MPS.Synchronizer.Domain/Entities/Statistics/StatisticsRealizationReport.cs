using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using MPS.Synchronizer.Domain.Common;

namespace MPS.Synchronizer.Domain.Entities.Statistics;


/// <summary>
/// Детализация к еженедельному отчёту реализации
/// </summary>
[Comment("Детализация к еженедельному отчёту реализации")]
public class StatisticsRealizationReport : BaseSyncEntity
{
    /// <summary>
    /// Номер отчёта
    /// </summary>
    [Column("realizationreport_id")]
    [JsonPropertyName("realizationreport_id")]
    [Comment("Номер отчёта")]
    public long RealizationReportId { get; set; }

    /// <summary>
    /// Дата начала отчётного периода
    /// </summary>
    [Column("date_from")]
    [JsonPropertyName("date_from")]
    [Comment("Дата начала отчётного периода")]
    public DateOnly DateFrom { get; set; }

    /// <summary>
    /// Дата конца отчётного периода
    /// </summary>
    [Column("date_to")]
    [JsonPropertyName("date_to")]
    [Comment("Дата конца отчётного периода")]
    public DateOnly DateTo { get; set; }

    /// <summary>
    /// Дата формирования отчёта
    /// </summary>
    [Column("create_dt")]
    [JsonPropertyName("create_dt")]
    [Comment("Дата формирования отчёта")]
    public DateOnly CreateDate { get; set; }

    /// <summary>
    /// Валюта отчёта
    /// </summary>
    [Column("currency_name")]
    [JsonPropertyName("currency_name")]
    [Comment("Валюта отчёта")]
    public string CurrencyName { get; set; }

    /// <summary>
    /// Договор
    /// </summary>
    [Column("suppliercontract_code")]
    [JsonPropertyName("suppliercontract_code")]
    [Comment("Договор")]
    public string SupplierContractCode { get; set; }

    /// <summary>
    /// Номер строки
    /// </summary>
    [Column("rrd_id")]
    [JsonPropertyName("rrd_id")]
    [Comment("Номер строки")]
    [SkipIndexGeneration]
    public long RrdId { get; set; }

    /// <summary>
    /// Номер поставки
    /// </summary>
    [Column("gi_id")]
    [JsonPropertyName("gi_id")]
    [Comment("Номер поставки")]
    public long GiId { get; set; }

    /// <summary>
    /// Предмет
    /// </summary>
    [Column("subject_name")]
    [JsonPropertyName("subject_name")]
    [Comment("Предмет")]
    public string SubjectName { get; set; }

    /// <summary>
    /// Артикул WB
    /// </summary>
    [Column("nm_id")]
    [JsonPropertyName("nm_id")]
    [Comment("Артикул WB")]
    public long NmId { get; set; }

    /// <summary>
    /// Бренд
    /// </summary>
    [Column("brand_name")]
    [JsonPropertyName("brand_name")]
    [Comment("Бренд")]
    public string BrandName { get; set; }

    /// <summary>
    /// Артикул продавца
    /// </summary>
    [Column("sa_name")]
    [JsonPropertyName("sa_name")]
    [Comment("Артикул продавца")]
    public string SaName { get; set; }

    /// <summary>
    /// Размер
    /// </summary>
    [Column("ts_name")]
    [JsonPropertyName("ts_name")]
    [Comment("Размер")]
    public string TsName { get; set; }

    /// <summary>
    /// Баркод
    /// </summary>
    [Column("barcode")]
    [JsonPropertyName("barcode")]
    [Comment("Баркод")]
    public string Barcode { get; set; }

    /// <summary>
    /// Тип документа
    /// </summary>
    [Column("doc_type_name")]
    [JsonPropertyName("doc_type_name")]
    [Comment("Тип документа")]
    public string DocTypeName { get; set; }

    /// <summary>
    /// Количество
    /// </summary>
    [Column("quantity")]
    [JsonPropertyName("quantity")]
    [Comment("Количество")]
    public long Quantity { get; set; }

    /// <summary>
    /// Цена розничная
    /// </summary>
    [Column("retail_price")]
    [JsonPropertyName("retail_price")]
    [Comment("Цена розничная")]
    public decimal RetailPrice { get; set; }

    /// <summary>
    /// Сумма продаж (возвратов)
    /// </summary>
    [Column("retail_amount")]
    [JsonPropertyName("retail_amount")]
    [Comment("Сумма продаж (возвратов)")]
    public decimal RetailAmount { get; set; }

    /// <summary>
    /// Согласованная скидка
    /// </summary>
    [Column("sale_percent")]
    [JsonPropertyName("sale_percent")]
    [Comment("Согласованная скидка")]
    public long SalePercent { get; set; }

    /// <summary>
    /// Процент комиссии
    /// </summary>
    [Column("commission_percent")]
    [JsonPropertyName("commission_percent")]
    [Comment("Процент комиссии")]
    public decimal CommissionPercent { get; set; }

    /// <summary>
    /// Склад
    /// </summary>
    [Column("office_name")]
    [JsonPropertyName("office_name")]
    [Comment("Склад")]
    public string OfficeName { get; set; }

    /// <summary>
    /// Обоснование для оплаты
    /// </summary>
    [Column("supplier_oper_name")]
    [JsonPropertyName("supplier_oper_name")]
    [Comment("Обоснование для оплаты")]
    public string SupplierOperName { get; set; }

    /// <summary>
    /// Дата заказа.
    /// Присылается с явным указанием часового пояса
    /// </summary>
    [Column("order_dt")]
    [JsonPropertyName("order_dt")]
    [Comment("Дата заказа. Присылается с явным указанием часового пояса")]
    public DateTimeOffset OrderDt { get; set; }

    /// <summary>
    /// Дата продажи.
    /// Присылается с явным указанием часового пояса
    /// </summary>
    [Column("sale_dt")]
    [JsonPropertyName("sale_dt")]
    [Comment("Дата продажи. Присылается с явным указанием часового пояса")]
    public DateTimeOffset SaleDt { get; set; }

    /// <summary>
    /// Дата операции.
    /// Присылается с явным указанием часового пояса
    /// </summary>
    [Column("rr_dt")]
    [JsonPropertyName("rr_dt")]
    [Comment("Дата операции. Присылается с явным указанием часового пояса")]
    public DateTimeOffset RrDt { get; set; }

    /// <summary>
    /// Штрих-код
    /// </summary>
    [Column("shk_id")]
    [JsonPropertyName("shk_id")]
    [Comment("Штрих-код")]
    public long ShkId { get; set; }

    /// <summary>
    /// Цена розничная с учетом согласованной скидки
    /// </summary>
    [Column("retail_price_withdisc_rub")]
    [JsonPropertyName("retail_price_withdisc_rub")]
    [Comment("Цена розничная с учетом согласованной скидки")]
    public decimal RetailPriceWithDiscountRub { get; set; }

    /// <summary>
    /// Количество доставок
    /// </summary>
    [Column("delivery_amount")]
    [JsonPropertyName("delivery_amount")]
    [Comment("Количество доставок")]
    public long DeliveryAmount { get; set; }

    /// <summary>
    /// Количество возвратов
    /// </summary>
    [Column("return_amount")]
    [JsonPropertyName("return_amount")]
    [Comment("Количество возвратов")]
    public long ReturnAmount { get; set; }

    /// <summary>
    /// Стоимость логистики
    /// </summary>
    [Column("delivery_rub")]
    [JsonPropertyName("delivery_rub")]
    [Comment("Стоимость логистики")]
    public decimal DeliveryRub { get; set; }

    /// <summary>
    /// Тип коробов
    /// </summary>
    [Column("gi_box_type_name")]
    [JsonPropertyName("gi_box_type_name")]
    [Comment("Тип коробов")]
    public string GiBoxTypeName { get; set; }

    /// <summary>
    /// Согласованный продуктовый дисконт
    /// </summary>
    [Column("product_discount_for_report")]
    [JsonPropertyName("product_discount_for_report")]
    [Comment("Согласованный продуктовый дисконт")]
    public decimal ProductDiscountForReport { get; set; }

    /// <summary>
    /// Промокод
    /// </summary>
    [Column("supplier_promo")]
    [JsonPropertyName("supplier_promo")]
    [Comment("Промокод")]
    public decimal SupplierPromo { get; set; }

    /// <summary>
    /// Уникальный идентификатор заказа
    /// </summary>
    [Column("rid")]
    [JsonPropertyName("rid")]
    [Comment("Уникальный идентификатор заказа")]
    public long Rid { get; set; }

    /// <summary>
    /// Скидка постоянного покупателя
    /// </summary>
    [Column("ppvz_spp_prc")]
    [JsonPropertyName("ppvz_spp_prc")]
    [Comment("Скидка постоянного покупателя")]
    public decimal PpvzSppPercentage { get; set; }

    /// <summary>
    /// Размер кВВ без НДС, % базовый
    /// </summary>
    [Column("ppvz_kvw_prc_base")]
    [JsonPropertyName("ppvz_kvw_prc_base")]
    [Comment("Размер кВВ без НДС, % базовый")]
    public decimal PpvzKvwPercentageBase { get; set; }

    /// <summary>
    /// Итоговый кВВ без НДС, %
    /// </summary>
    [Column("ppvz_kvw_prc")]
    [JsonPropertyName("ppvz_kvw_prc")]
    [Comment("Итоговый кВВ без НДС, %")]
    public decimal PpvzKvwPercentage { get; set; }

    /// <summary>
    /// Размер снижения кВВ из-за рейтинга
    /// </summary>
    [Column("sup_rating_prc_up")]
    [JsonPropertyName("sup_rating_prc_up")]
    [Comment("Размер снижения кВВ из-за рейтинга")]
    public decimal SupRatingPercentageUp { get; set; }

    /// <summary>
    /// Размер снижения кВВ из-за акции
    /// </summary>
    [Column("is_kgvp_v2")]
    [JsonPropertyName("is_kgvp_v2")]
    [Comment("Размер снижения кВВ из-за акции")]
    public decimal IsKgvpV2 { get; set; }

    /// <summary>
    /// Вознаграждение с продаж до вычета услуг поверенного, без НДС
    ///  </summary>
    [Column("ppvz_sales_commission")]
    [JsonPropertyName("ppvz_sales_commission")]
    [Comment("Вознаграждение с продаж до вычета услуг поверенного, без НДС")]
    public decimal PpvzSalesCommission { get; set; }

    /// <summary>
    /// К перечислению продавцу за реализованный товар
    /// </summary>
    [Column("ppvz_for_pay")]
    [JsonPropertyName("ppvz_for_pay")]
    [Comment("К перечислению продавцу за реализованный товар")]
    public decimal PpvzForPay { get; set; }

    /// <summary>
    /// Возмещение за выдачу и возврат товаров на ПВЗ
    /// </summary>
    [Column("ppvz_reward")]
    [JsonPropertyName("ppvz_reward")]
    [Comment("Возмещение за выдачу и возврат товаров на ПВЗ")]
    public decimal PpvzReward { get; set; }

    /// <summary>
    /// Возмещение издержек по эквайрингу.
    /// Издержки WB за услуги эквайринга: вычитаются из вознаграждения WB и не влияют на доход продавца.
    /// </summary>
    [Column("acquiring_fee")]
    [JsonPropertyName("acquiring_fee")]
    [Comment("Возмещение издержек по эквайрингу. Издержки WB за услуги эквайринга: вычитаются из вознаграждения WB и не влияют на доход продавца")]
    public decimal AcquiringFee { get; set; }

    /// <summary>
    /// Размер комиссии за эквайринг без НДС, %
    /// </summary>
    [Column("acquiring_percent")]
    [JsonPropertyName("acquiring_percent")]
    [Comment("Размер комиссии за эквайринг без НДС, %")]
    public decimal AcquiringPercentage { get; set; }

    /// <summary>
    /// Наименование банка-эквайера
    /// </summary>
    [Column("acquiring_bank")]
    [JsonPropertyName("acquiring_bank")]
    [Comment("Наименование банка-эквайера")]
    public string AcquiringBank { get; set; }

    /// <summary>
    /// Вознаграждение WB без НДС
    /// </summary>
    [Column("ppvz_vw")]
    [JsonPropertyName("ppvz_vw")]
    [Comment("Вознаграждение WB без НДС")]
    public decimal PpvzVw { get; set; }

    /// <summary>
    /// НДС с вознаграждения WB
    /// </summary>
    [Column("ppvz_vw_nds")]
    [JsonPropertyName("ppvz_vw_nds")]
    [Comment("НДС с вознаграждения WB")]
    public decimal PpvzVwNds { get; set; }

    /// <summary>
    /// Номер офиса
    /// </summary>
    [Column("ppvz_office_id")]
    [JsonPropertyName("ppvz_office_id")]
    [Comment("Номер офиса")]
    public long PpvzOfficeId { get; set; }

    /// <summary>
    /// Наименование офиса доставки
    /// </summary>
    [Column("ppvz_office_name")]
    [JsonPropertyName("ppvz_office_name")]
    [Comment("Наименование офиса доставки")]
    public string PpvzOfficeName { get; set; }

    /// <summary>
    /// Номер партнера
    /// </summary>
    [Column("ppvz_supplier_id")]
    [JsonPropertyName("ppvz_supplier_id")]
    [Comment("Номер партнера")]
    public long PpvzSupplierId { get; set; }

    /// <summary>
    /// Партнер
    /// </summary>
    [Column("ppvz_supplier_name")]
    [JsonPropertyName("ppvz_supplier_name")]
    [Comment("Партнер")]
    public string PpvzSupplierName { get; set; }

    /// <summary>
    /// ИНН партнера
    /// </summary>
    [Column("ppvz_inn")]
    [JsonPropertyName("ppvz_inn")]
    [Comment("ИНН партнера")]
    public string PpvzInn { get; set; }

    /// <summary>
    /// Номер таможенной декларации
    /// </summary>
    [Column("declaration_number")]
    [JsonPropertyName("declaration_number")]
    [Comment("Номер таможенной декларации")]
    public string DeclarationNumber { get; set; }

    /// <summary>
    /// Обоснование штрафов и доплат
    /// </summary>
    [Column("bonus_type_name")]
    [JsonPropertyName("bonus_type_name")]
    [Comment("Обоснование штрафов и доплат")]
    public string BonusTypeName { get; set; }

    /// <summary>
    /// Цифровое значение стикера, который клеится на товар в процессе сборки заказа по схеме "Маркетплейс"
    /// </summary>
    [Column("sticker_id")]
    [JsonPropertyName("sticker_id")]
    [Comment("Цифровое значение стикера, который клеится на товар в процессе сборки заказа по схеме 'Маркетплейс'")]
    public string StickerId { get; set; }

    /// <summary>
    /// Страна продажи
    /// </summary>
    [Column("site_country")]
    [JsonPropertyName("site_country")]
    [Comment("Страна продажи")]
    public string SiteCountry { get; set; }

    /// <summary>
    /// Штрафы
    /// </summary>
    [Column("penalty")]
    [JsonPropertyName("penalty")]
    [Comment("Штрафы")]
    public decimal Penalty { get; set; }

    /// <summary>
    /// Доплаты
    /// </summary>
    [Column("additional_payment")]
    [JsonPropertyName("additional_payment")]
    [Comment("Доплаты")]
    public decimal AdditionalPayment { get; set; }

    /// <summary>
    /// Возмещение издержек по перевозке
    /// </summary>
    [Column("rebill_logistic_cost")]
    [JsonPropertyName("rebill_logistic_cost")]
    [Comment("Возмещение издержек по перевозке")]
    public decimal RebillLogisticCost { get; set; }

    /// <summary>
    /// Организатор перевозки
    /// </summary>
    [Column("rebill_logistic_org")]
    [JsonPropertyName("rebill_logistic_org")]
    [Comment("Организатор перевозки")]
    public string RebillLogisticOrg { get; set; }

    /// <summary>
    /// Код маркировки
    /// </summary>
    [Column("kiz")]
    [JsonPropertyName("kiz")]
    [Comment("Код маркировки")]
    public string Kiz { get; set; }

    /// <summary>
    /// Стоимость хранения
    /// </summary>
    [Column("storage_fee")]
    [JsonPropertyName("storage_fee")]
    [Comment("Стоимость хранения")]
    public decimal StorageFee { get; set; }

    /// <summary>
    /// Прочие удержания/выплаты
    /// </summary>
    [Column("deduction")]
    [JsonPropertyName("deduction")]
    [Comment("Прочие удержания/выплаты")]
    public decimal Deduction { get; set; }

    /// <summary>
    /// Стоимость платной приёмки
    /// </summary>
    [Column("acceptance")]
    [JsonPropertyName("acceptance")]
    [Comment("Стоимость платной приёмки")]
    public decimal Acceptance { get; set; }

    /// <summary>
    /// Уникальный идентификатор заказа
    /// </summary>
    [Column("srid")]
    [JsonPropertyName("srid")]
    [Comment("Уникальный идентификатор заказа.Примечание для использующих API Marketplace: srid равен rid в ответах методов сборочных заданий")]
    public string Srid { get; set; }

    /// <summary>
    /// Тип отчёта:
    /// 1 — стандартный,
    /// 2 — для уведомления о выкупе
    /// </summary>
    [Column("report_type")]
    [JsonPropertyName("report_type")]
    [Comment("Тип отчёта: 1 — стандартный, 2 — для уведомления о выкупе")]
    public int ReportType { get; set; }

    /// <summary>
    /// Фиксированный коэффициент склада по поставке
    /// </summary>
    [Column("dlv_prc")]
    [JsonPropertyName("dlv_prc")]
    [Comment("Фиксированный коэффициент склада по поставке")]
    public decimal DlvPrc { get; set; }

    /// <summary>
    /// Дата начала действия фиксации
    /// </summary>
    [Column("fix_tariff_date_from")]
    [JsonPropertyName("fix_tariff_date_from")]
    [Comment("Дата начала действия фиксации")]
    public DateTime? FixTariffDateFrom { get; set; }

    /// <summary>
    /// Дата окончания действия фиксации
    /// </summary>
    [Column("fix_tariff_date_to")]
    [JsonPropertyName("fix_tariff_date_to")]
    [Comment("Дата окончания действия фиксации")]
    public DateTime? FixTariffDateTo { get; set; }

    /// <summary>
    /// Тип платежа за Эквайринг/Комиссии за организацию платежей
    /// </summary>
    [Column("payment_processing")]
    [JsonPropertyName("payment_processing")]
    [Comment("Тип платежа за Эквайринг/Комиссии за организацию платежей")]
    public string PaymentProcessing { get; set; }

    /// <summary>
    /// Признак услуги платной доставки
    /// </summary>
    [Column("srv_dbs")]
    [JsonPropertyName("srv_dbs")]
    [Comment("Признак услуги платной доставки")]
    public bool SrvDbs { get; set; }

    /// <summary>
    /// Номер сборочного задания
    /// </summary>
    [Column("assembly_id")]
    [JsonPropertyName("assembly_id")]
    [Comment("Номер сборочного задания")]
    public long AssemblyId { get; set; }

    /// <summary>
    /// Признак B2B-продажи
    /// </summary>
    [Column("is_legal_entity")]
    [JsonPropertyName("is_legal_entity")]
    [Comment("Признак B2B-продажи")]
    public bool IsLegalEntity { get; set; }

    /// <summary>
    /// Номер короба для платной приёмки
    /// </summary>
    [Column("trbx_id")]
    [JsonPropertyName("trbx_id")]
    [Comment("Номер короба для платной приёмки")]
    public string TrbxId { get; set; }
}