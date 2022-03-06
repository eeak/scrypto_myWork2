using System;
using System.Collections.Generic;

namespace MyWork2
{
    public class DelayInsuranceInfo
    {
        public string paymentStatus { get; set; }
        public string price { get; set; }
        public object descriptionUrl { get; set; }
        public object insuranceUrl { get; set; }
    }

    public class RefusalInsuranceInfo
    {
        public string paymentStatus { get; set; }
        public string price { get; set; }
        public object descriptionUrl { get; set; }
        public object insuranceUrl { get; set; }
    }

    public class Acceptance
    {
        public DateTime date { get; set; }
        public string operationType { get; set; }
        public string operationAttr { get; set; }
        public string index { get; set; }
        public string indexTo { get; set; }
    }

    public class CustomsPassing
    {
        public DateTime date { get; set; }
        public string operationType { get; set; }
        public string operationAttr { get; set; }
        public string index { get; set; }
        public string indexTo { get; set; }
    }

    public class Arrived
    {
        public DateTime date { get; set; }
        public string operationType { get; set; }
        public string operationAttr { get; set; }
        public string index { get; set; }
        public string indexTo { get; set; }
    }

    public class ShipmentTripInfo
    {
        public Acceptance acceptance { get; set; }
        public CustomsPassing customsPassing { get; set; }
        public Arrived arrived { get; set; }
        public object hiddenInternalHistoryRecord { get; set; }
        public object expectedDeliveryDays { get; set; }
        public object startDeliveryDate { get; set; }
        public object expectedDeliveryDate { get; set; }
    }

    public class TrackingHistoryItemList
    {
        public DateTime date { get; set; }
        public string humanStatus { get; set; }
        public object humanOperationStatus { get; set; }
        public string operationType { get; set; }
        public string operationAttr { get; set; }
        public string countryId { get; set; }
        public string index { get; set; }
        public string cityName { get; set; }
        public string countryName { get; set; }
        public string countryNameGenitiveCase { get; set; }
        public string description { get; set; }
        public string weight { get; set; }
    }

    public class TrackingItem
    {
        public object tpoCustomPayment { get; set; }
        public List<object> customsPayments { get; set; }
        public string destinationCountryName { get; set; }
        public string destinationCountryNameGenitiveCase { get; set; }
        public string destinationCityName { get; set; }
        public string originCountryName { get; set; }
        public object originCityName { get; set; }
        public string mailRank { get; set; }
        public string mailCtg { get; set; }
        public string postMark { get; set; }
        public object insurance { get; set; }
        public object insuranceMoney { get; set; }
        public List<object> hiddenHistoryList { get; set; }
        public List<object> futurePathList { get; set; }
        public List<object> cashOnDeliveryEventsList { get; set; }
        public ShipmentTripInfo shipmentTripInfo { get; set; }
        public string sender { get; set; }
        public string recipient { get; set; }
        public string weight { get; set; }
        public string storageTime { get; set; }
        public string title { get; set; }
        public object liferayWebContentId { get; set; }
        public List<TrackingHistoryItemList> trackingHistoryItemList { get; set; }
        public string lastOperationTimezoneOffset { get; set; }
        public string globalStatus { get; set; }
        public string mailType { get; set; }
        public string mailTypeCode { get; set; }
        public string countryFromCode { get; set; }
        public string countryToCode { get; set; }
        public object customDuty { get; set; }
        public object cashOnDelivery { get; set; }
        public object indexFrom { get; set; }
        public string indexTo { get; set; }
        public object deliveryOrderDate { get; set; }
        public string commonStatus { get; set; }
        public string firstOperationDate { get; set; }
        public string lastOperationDate { get; set; }
        public object complexCode { get; set; }
        public object complexType { get; set; }
        public object complexDeliveryMethod { get; set; }
        public string barcode { get; set; }
        public object notificationBarcode { get; set; }
        public object notificationTitle { get; set; }
        public object sourceBarcode { get; set; }
        public object sourceTitle { get; set; }
        public object endStorageDate { get; set; }
        public object lastDayInOps { get; set; }
        public string lastOperationAttr { get; set; }
        public string lastOperationType { get; set; }
        public object id { get; set; }
        public string postmarkText { get; set; }
        public string mailTypeText { get; set; }
        public object customsPaymentStatus { get; set; }
        public object customsOperatorDuty { get; set; }
        public object customsOperatorDutyFee { get; set; }
        public object customsOperatorDutyFeeOnline { get; set; }
        public object customsPayment { get; set; }
        public string mailCtgText { get; set; }
        public string mailRankText { get; set; }
        public object returnRate { get; set; }
        public object redispatchRate { get; set; }
    }

    public class FormF22Params
    {
        public string senderAddress { get; set; }
        public string MailRankText { get; set; }
        public string MailCtgText { get; set; }
        public string WeightGr { get; set; }
        public string MailTypeText { get; set; }
        public string SendingType { get; set; }
        public object state { get; set; }
        public string PostId { get; set; }
        public string RecipientIndex { get; set; }
    }

    public class Response
    {
        public object userTrackingItemId { get; set; }
        public object userTitle { get; set; }
        public object itemAddedDate { get; set; }
        public object deleteDate { get; set; }
        public object lastOperationViewedTimestamp { get; set; }
        public object paymentStatus { get; set; }
        public object paymentSystem { get; set; }
        public DelayInsuranceInfo delayInsuranceInfo { get; set; }
        public RefusalInsuranceInfo refusalInsuranceInfo { get; set; }
        public object paymentDate { get; set; }
        public object parcelStatus { get; set; }
        public string euvStatus { get; set; }
        public object amount { get; set; }
        public TrackingItem trackingItem { get; set; }
        public object officeSummary { get; set; }
        public object postmanDeliveryInfo { get; set; }
        public object courierDeliveryInfo { get; set; }
        public FormF22Params formF22Params { get; set; }
    }

    public class Root
    {
        public List<Response> response { get; set; }
    }
}
