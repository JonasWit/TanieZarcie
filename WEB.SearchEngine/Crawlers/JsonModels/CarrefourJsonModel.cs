using System;
using System.Collections.Generic;
using System.Text;

namespace WEB.SearchEngine.Crawlers.JsonModels
{
    public class CarrefourJsonModel
    {
        public string dataManager { get; set; }
        public Props props { get; set; }
        public string page { get; set; }
        public Query query { get; set; }
        public string buildId { get; set; }
        public Runtimeconfig runtimeConfig { get; set; }
        public string[] dynamicIds { get; set; }
    }

    public class Props
    {
        public bool isServer { get; set; }
        public Initialstate initialState { get; set; }
        public Initialprops initialProps { get; set; }
    }

    public class Initialstate
    {
        public Gtm gtm { get; set; }
        //public Cart cart { get; set; }
        public Products products { get; set; }
        public Payment payment { get; set; }
        public Shoppinglist shoppingList { get; set; }
        //public Auth auth { get; set; }
        //public Router router { get; set; }
        //public Account account { get; set; }
        public Shoppinglists shoppingLists { get; set; }
        public Product1 product { get; set; }
        //public Delivery delivery { get; set; }
        //public Session session { get; set; }
        public Categories categories { get; set; }
    }

    public class Gtm
    {
        public Ecommerceforcontentview ecommerceForContentView { get; set; }
        public Contentgrouping contentGrouping { get; set; }
    }

    public class Ecommerceforcontentview
    {
        public string currencyCode { get; set; }
        public Impression[] impressions { get; set; }
    }

    public class Impression
    {
        public string id { get; set; }
        public int position { get; set; }
        public string list { get; set; }
    }

    public class Contentgrouping
    {
        public string contentGroup1 { get; set; }
        public string contentGroup2 { get; set; }
        public string contentGroup3 { get; set; }
        public string contentGroup4 { get; set; }
        public string contentGroup5 { get; set; }
    }

    //public class Cart
    //{
    //    public Cart1 cart { get; set; }
    //    public object[] additionalServices { get; set; }
    //    public Paymentcommand paymentCommand { get; set; }
    //    public object[] pendingLines { get; set; }
    //    public object pendingPaymentId { get; set; }
    //    public object[] additionalServicePendingIds { get; set; }
    //    public object additionalPendingPaymentId { get; set; }
    //    public object[] orderInputAttributes { get; set; }
    //    public object pendingAddressId { get; set; }
    //    public object pendingInvoiceDataId { get; set; }
    //    public bool pendingCheckout { get; set; }
    //}

    //public class Cart1
    //{
    //    public Inputattributes inputAttributes { get; set; }
    //    public int necessaryAmount { get; set; }
    //    public Linesquantity linesQuantity { get; set; }
    //    public object[] lastFailedLines { get; set; }
    //    public Value value { get; set; }
    //    public object[] additionalServiceLines { get; set; }
    //    public Linesvalue linesValue { get; set; }
    //    public Linesandfreelinesvalue linesAndFreeLinesValue { get; set; }
    //    public Deliveryvalue deliveryValue { get; set; }
    //    public object[] lines { get; set; }
    //    public int numberOfLines { get; set; }
    //}

    public class Inputattributes
    {
    }

    public class Linesquantity
    {
    }

    public class Value
    {
        public Clientvalue clientValue { get; set; }
    }

    public class Clientvalue
    {
        public string grossString { get; set; }
    }

    public class Linesvalue
    {
        public Clientvalue1 clientValue { get; set; }
    }

    public class Clientvalue1
    {
    }

    public class Linesandfreelinesvalue
    {
        public Clientvalue2 clientValue { get; set; }
    }

    public class Clientvalue2
    {
    }

    public class Deliveryvalue
    {
    }

    public class Paymentcommand
    {
    }

    public class Products
    {
        public Data data { get; set; }
        public bool isPending { get; set; }
        public object error { get; set; }
    }

    public class Data
    {
        public int totalPages { get; set; }
        public Productlabelfacet[] productLabelFacets { get; set; }
        public Brandfacet[] brandFacets { get; set; }
        public bool promotion { get; set; }
        public bool fuzzy { get; set; }
        public int totalCount { get; set; }
        public Createdatefacet createDateFacet { get; set; }
        public object[] attributeFacets { get; set; }
        public Content[] content { get; set; }
        public object[] categoryFacets { get; set; }
        public Rootcategoryfacet[] rootCategoryFacets { get; set; }
    }

    public class Createdatefacet
    {
        public long date { get; set; }
        public int hits { get; set; }
        public bool selected { get; set; }
    }

    public class Productlabelfacet
    {
        public string label { get; set; }
        public int hits { get; set; }
        public bool selected { get; set; }
    }

    public class Brandfacet
    {
        public string brandId { get; set; }
        public string name { get; set; }
        public int hits { get; set; }
        public bool selected { get; set; }
    }

    public class Content
    {
        public string publishStatus { get; set; }
        //public Imagesmap imagesMap { get; set; }
        public bool active { get; set; }
        public string defaultCategoryName { get; set; }
        public string defaultCategorySlug { get; set; }
        public long createDate { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public string displayName { get; set; }
        public Actualsku actualSku { get; set; }
        public Product product { get; set; }
        public string url { get; set; }
        //public Image[] images { get; set; }
        //public Defaultimage defaultImage { get; set; }
        public string defaultCategoryId { get; set; }
        public string id { get; set; }
        public string accessType { get; set; }
        public string viewType { get; set; }
    }

    //public class Imagesmap
    //{
    //    public T5 T5 { get; set; }
    //    public T1 T1 { get; set; }
    //    public T8 T8 { get; set; }
    //    public T813 T813 { get; set; }
    //}

    //public class T5
    //{
    //    public string name { get; set; }
    //    public int width { get; set; }
    //    public int height { get; set; }
    //}

    //public class T1
    //{
    //    public string name { get; set; }
    //    public int width { get; set; }
    //    public int height { get; set; }
    //}

    //public class T8
    //{
    //    public string name { get; set; }
    //    public int width { get; set; }
    //    public int height { get; set; }
    //}

    //public class T813
    //{
    //    public string name { get; set; }
    //    public int width { get; set; }
    //    public int height { get; set; }
    //}

    public class Actualsku
    {
        public bool promotion { get; set; }
        public string grammageWithUnitString { get; set; }
        public float grammage { get; set; }
        public int realizationHours { get; set; }
        public int maxBuyQuantity { get; set; }
        public string status { get; set; }
        public Amount amount { get; set; }
        public string id { get; set; }
        public float buyQuantityStep { get; set; }
        public Minvalue minValue { get; set; }
        public float minBuyQuantity { get; set; }
        public float itemConverter { get; set; }
    }

    public class Amount
    {
        public int actualDiscountPercentValue { get; set; }
        public Actualgrosspriceobject actualGrossPriceObject { get; set; }
        public float actualGrossPrice { get; set; }
        public string actualGrossPriceString { get; set; }
        public float actualOldPrice { get; set; }
        public string actualOldPriceString { get; set; }
        public Actualoldpriceobject actualOldPriceObject { get; set; }
        public float actualDiscountValue { get; set; }
        public string actualDiscountValueString { get; set; }
    }

    public class Actualgrosspriceobject
    {
        public string digit { get; set; }
        public string separator { get; set; }
        public string _decimal { get; set; }
        public string currency { get; set; }
    }

    public class Actualoldpriceobject
    {
        public string digit { get; set; }
        public string separator { get; set; }
        public string _decimal { get; set; }
        public string currency { get; set; }
    }

    public class Minvalue
    {
        public float gross { get; set; }
        public string grossString { get; set; }
        public Valueobject valueObject { get; set; }
    }

    public class Valueobject
    {
        public string digit { get; set; }
        public string separator { get; set; }
        public string _decimal { get; set; }
        public string currency { get; set; }
    }

    public class Product
    {
        public float size { get; set; }
        public string publishStatus { get; set; }
        public string sellUnitString { get; set; }
        public string name { get; set; }
        public string grammageUnit { get; set; }
        public string code { get; set; }
        public string displayName { get; set; }
        public int weight { get; set; }
        public string id { get; set; }
        public string sizeWithUnitString { get; set; }
        public string[] labels { get; set; }
    }

    //public class Defaultimage
    //{
    //    public string name { get; set; }
    //    public int width { get; set; }
    //    public int height { get; set; }
    //}

    //public class Image
    //{
    //    public string name { get; set; }
    //    public int width { get; set; }
    //    public int height { get; set; }
    //}

    public class Rootcategoryfacet
    {
        public string categoryId { get; set; }
        public int hits { get; set; }
        public bool selected { get; set; }
    }

    public class Payment
    {
        public object[] data { get; set; }
        public bool isPending { get; set; }
        public object error { get; set; }
    }

    public class Shoppinglist
    {
        public Data1 data { get; set; }
        public bool isPending { get; set; }
        public object error { get; set; }
    }

    public class Data1
    {
    }

    //public class Auth
    //{
    //    public bool logged { get; set; }
    //}

    //public class Router
    //{
    //    public string prevLocation { get; set; }
    //    public string location { get; set; }
    //    public bool transition { get; set; }
    //    public Queryparams queryParams { get; set; }
    //}

    //public class Queryparams
    //{
    //}

    //public class Account
    //{
    //    public bool pendingInvoiceData { get; set; }
    //    public object[] addresses { get; set; }
    //    public object error { get; set; }
    //    public Data2 data { get; set; }
    //    public bool pendingAddresses { get; set; }
    //    public bool isPending { get; set; }
    //    public object[] removeAddressPendingIds { get; set; }
    //    public object[] removeInvoiceDataPendingIds { get; set; }
    //    public object[] invoiceData { get; set; }
    //    public object errorPendingData { get; set; }
    //    public object errorAddresses { get; set; }
    //}

    public class Data2
    {
    }

    public class Shoppinglists
    {
        public Data3 data { get; set; }
        public bool isPending { get; set; }
        public object error { get; set; }
    }

    public class Data3
    {
        public object[] content { get; set; }
        public int totalCount { get; set; }
    }

    public class Product1
    {
        //public Data4 data { get; set; }
        public bool isPending { get; set; }
        public string error { get; set; }
        public Upselling upSelling { get; set; }
    }

    //public class Data4
    //{
    //}

    public class Upselling
    {
        public object[] content { get; set; }
    }

    //public class Delivery
    //{
    //    public object selectedPickupLocationId { get; set; }
    //    public bool pendingLocations { get; set; }
    //    public object error { get; set; }
    //    public object selectedDeliveryId { get; set; }
    //    public object[] data { get; set; }
    //    public object errorSlots { get; set; }
    //    public bool isPending { get; set; }
    //    public object pendingSlotSelectionId { get; set; }
    //    public object selectedSlotId { get; set; }
    //    public Slots slots { get; set; }
    //    public object pickupLocationIdForSelectedSlot { get; set; }
    //    public object errorLocations { get; set; }
    //    public object[] listSlots { get; set; }
    //    public bool pendingSlots { get; set; }
    //    public object[] locations { get; set; }
    //}

    //public class Slots
    //{
    //    public object[] days { get; set; }
    //}

    //public class Session
    //{
    //    public string viewUuid { get; set; }
    //    public Branch branch { get; set; }
    //    public bool sessionExpired { get; set; }
    //    public bool userSelectedZone { get; set; }
    //    public bool userSelectedLocation { get; set; }
    //    public string storeUuid { get; set; }
    //    public string storeId { get; set; }
    //    public string xSession { get; set; }
    //    public string criteria { get; set; }
    //}

    //public class Branch
    //{
    //    public string publishStatus { get; set; }
    //    public string street { get; set; }
    //    public float lng { get; set; }
    //    public string houseNumber { get; set; }
    //    public string city { get; set; }
    //    public string name { get; set; }
    //    public string displayName { get; set; }
    //    public string zipCode { get; set; }
    //    public bool selling { get; set; }
    //    public string id { get; set; }
    //    public string uuid { get; set; }
    //    public float lat { get; set; }
    //    public string[] labels { get; set; }
    //}

    public class Categories
    {
        public Data5 data { get; set; }
        public object[] selectedPath { get; set; }
    }

    public class Data5
    {
        public string id { get; set; }
        public string name { get; set; }
        public Child[] children { get; set; }
    }

    public class Child
    {
        public Filesmap filesMap { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public string displayName { get; set; }
        public string url { get; set; }
        public File[] files { get; set; }
        public string id { get; set; }
        public string accessType { get; set; }
        public Child1[] children { get; set; }
        public string viewType { get; set; }
    }

    public class Filesmap
    {
        public Ec4Web ec4web { get; set; }
        public Ec4Icon ec4icon { get; set; }
        public MccIcon mccicon { get; set; }
        public Board board { get; set; }
    }

    public class Ec4Web
    {
        public string name { get; set; }
    }

    public class Ec4Icon
    {
        public string name { get; set; }
    }

    public class MccIcon
    {
        public string name { get; set; }
    }

    public class Board
    {
        public string name { get; set; }
    }

    public class File
    {
        public string name { get; set; }
    }

    public class Child1
    {
        public string id { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public string slug { get; set; }
        public string url { get; set; }
        public string accessType { get; set; }
        public string viewType { get; set; }
        public Child2[] children { get; set; }
        public string[] labels { get; set; }
        public Filesmap1 filesMap { get; set; }
        public File1[] files { get; set; }
    }

    public class Filesmap1
    {
        public MccIcon1 mccicon { get; set; }
        public Board1 board { get; set; }
    }

    public class MccIcon1
    {
        public string name { get; set; }
    }

    public class Board1
    {
        public string name { get; set; }
    }

    public class Child2
    {
        public string id { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public string slug { get; set; }
        public string url { get; set; }
        public string accessType { get; set; }
        public string viewType { get; set; }
        public Child3[] children { get; set; }
        public string[] labels { get; set; }
    }

    public class Child3
    {
        public string id { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public string slug { get; set; }
        public string url { get; set; }
        public string accessType { get; set; }
        public string viewType { get; set; }
        public string[] labels { get; set; }
        public Child4[] children { get; set; }
    }

    public class Child4
    {
        public string id { get; set; }
        public string name { get; set; }
        public string displayName { get; set; }
        public string slug { get; set; }
        public string url { get; set; }
        public string accessType { get; set; }
        public string viewType { get; set; }
    }

    public class File1
    {
        public string name { get; set; }
    }

    public class Initialprops
    {
        public Pageprops pageProps { get; set; }
        public string initWidth { get; set; }
        public string xSession { get; set; }
        public string cookies { get; set; }
    }

    public class Pageprops
    {
        public string pageType { get; set; }
        public string groupName { get; set; }
        public string groupDescription { get; set; }
        public string pageTitle { get; set; }
        public bool addDefaultTitle { get; set; }
        public string url { get; set; }
        public bool hideBrands { get; set; }
        public bool hideFilters { get; set; }
        public bool hideSort { get; set; }
        public bool hidePageSize { get; set; }
        public bool addAllToCart { get; set; }
        public string topBoxes { get; set; }
    }

    public class Query
    {
        public string slug { get; set; }
        public string initWidth { get; set; }
    }

    public class Runtimeconfig
    {
        public bool dev { get; set; }
        public string serverBaseUrl { get; set; }
        public string baseUrl { get; set; }
        public string apiPath { get; set; }
        public string apiUrl { get; set; }
        public string imagesUrl { get; set; }
        public string filesUrl { get; set; }
        public string staticFolder { get; set; }
        public string scidUrl { get; set; }
        public bool scid { get; set; }
        public string scidPasswordUrl { get; set; }
        public string scidLogoutUrl { get; set; }
        public string scidProfileUrl { get; set; }
        public string sentry { get; set; }
        public string snrDomain { get; set; }
        public bool recommendProgram { get; set; }
    }

}
