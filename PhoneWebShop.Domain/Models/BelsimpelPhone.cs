using Newtonsoft.Json;
using System.Collections.Generic;

namespace PhoneWebShop.Domain.Models
{
    public class PageViewDetails
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("sub_type")]
        public string SubType { get; set; }

        [JsonProperty("provider")]
        public object Provider { get; set; }

        [JsonProperty("brand")]
        public object Brand { get; set; }

        [JsonProperty("is_business")]
        public bool IsBusiness { get; set; }

        [JsonProperty("is_retention")]
        public bool IsRetention { get; set; }

        [JsonProperty("is_simonly")]
        public bool IsSimonly { get; set; }
    }

    public class Properties
    {
        [JsonProperty("share_path")]
        public string SharePath { get; set; }

        [JsonProperty("api_path")]
        public string ApiPath { get; set; }

        [JsonProperty("api_params")]
        public string ApiParams { get; set; }

        [JsonProperty("prefix")]
        public string Prefix { get; set; }

        [JsonProperty("page_title")]
        public string PageTitle { get; set; }

        [JsonProperty("should_show_geld_lenen_kost_geld_banner")]
        public bool ShouldShowGeldLenenKostGeldBanner { get; set; }

        [JsonProperty("should_show_nummerbehoud_garantie_details")]
        public bool ShouldShowNummerbehoudGarantieDetails { get; set; }

        [JsonProperty("show_package_discount_block_in_results")]
        public bool ShowPackageDiscountBlockInResults { get; set; }

        [JsonProperty("base_url")]
        public string BaseUrl { get; set; }

        [JsonProperty("mode")]
        public string Mode { get; set; }

        [JsonProperty("page_view_details")]
        public PageViewDetails PageViewDetails { get; set; }
    }

    public class AlternateUrl
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }
    }

    public class Breadcrumb
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public class SeoData
    {
        [JsonProperty("should_index")]
        public bool ShouldIndex { get; set; }

        [JsonProperty("should_follow")]
        public bool ShouldFollow { get; set; }

        [JsonProperty("canonical")]
        public string Canonical { get; set; }

        [JsonProperty("alternate_urls")]
        public List<AlternateUrl> AlternateUrls { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("breadcrumbs")]
        public List<Breadcrumb> Breadcrumbs { get; set; }

        [JsonProperty("title")]
        public object Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("top_text")]
        public string TopText { get; set; }

        [JsonProperty("bottom_text")]
        public string BottomText { get; set; }

        [JsonProperty("internal_links")]
        public string InternalLinks { get; set; }
    }

    public class Header
    {
        [JsonProperty("heading")]
        public string Heading { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("total_results_string")]
        public string TotalResultsString { get; set; }

        [JsonProperty("usps")]
        public List<string> Usps { get; set; }

        [JsonProperty("section_heading")]
        public object SectionHeading { get; set; }
    }

    public class DisplayType
    {
        [JsonProperty("can_toggle")]
        public bool CanToggle { get; set; }

        [JsonProperty("show_header")]
        public bool ShowHeader { get; set; }
    }

    public class Option
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("hidden")]
        public bool Hidden { get; set; }

        [JsonProperty("disabled")]
        public bool Disabled { get; set; }

        [JsonProperty("selected")]
        public bool Selected { get; set; }

        [JsonProperty("default_selected_state")]
        public bool DefaultSelectedState { get; set; }

        [JsonProperty("result_count")]
        public int? ResultCount { get; set; }

        [JsonProperty("result_count_as_string")]
        public string ResultCountAsString { get; set; }

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }

        [JsonProperty("destination_url")]
        public string DestinationUrl { get; set; }

        [JsonProperty("colors")]
        public List<string> Colors { get; set; }
    }

    public class Parameter
    {
        [JsonProperty("parameter_identifier")]
        public string ParameterIdentifier { get; set; }

        [JsonProperty("parameter_location")]
        public string ParameterLocation { get; set; }

        [JsonProperty("expected_data_type")]
        public string ExpectedDataType { get; set; }

        [JsonProperty("intent")]
        public string Intent { get; set; }

        [JsonProperty("default_value")]
        public string DefaultValue { get; set; }
    }

    public class ExternalService
    {
        [JsonProperty("base_url")]
        public string BaseUrl { get; set; }

        [JsonProperty("endpoint")]
        public string Endpoint { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("parameters")]
        public List<Parameter> Parameters { get; set; }
    }

    public class Information
    {
        [JsonProperty("html_info_text")]
        public string HtmlInfoText { get; set; }
    }

    public class Filter
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("expanded")]
        public bool Expanded { get; set; }

        [JsonProperty("default_expanded_state")]
        public bool DefaultExpandedState { get; set; }

        [JsonProperty("display_type")]
        public DisplayType DisplayType { get; set; }

        [JsonProperty("hidden")]
        public bool Hidden { get; set; }

        [JsonProperty("can_have_active_filters")]
        public bool CanHaveActiveFilters { get; set; }

        [JsonProperty("parameter_id")]
        public string ParameterId { get; set; }

        [JsonProperty("options")]
        public List<Option> Options { get; set; }

        [JsonProperty("external_service")]
        public ExternalService ExternalService { get; set; }

        [JsonProperty("search_results")]
        public List<object> SearchResults { get; set; }

        [JsonProperty("values")]
        public List<double> Values { get; set; }

        [JsonProperty("display_values")]
        public List<string> DisplayValues { get; set; }

        [JsonProperty("min_value")]
        public int? MinValue { get; set; }

        [JsonProperty("max_value")]
        public double? MaxValue { get; set; }

        [JsonProperty("default_min_value")]
        public int? DefaultMinValue { get; set; }

        [JsonProperty("default_max_value")]
        public double? DefaultMaxValue { get; set; }

        [JsonProperty("selected_min_value")]
        public int? SelectedMinValue { get; set; }

        [JsonProperty("selected_max_value")]
        public double? SelectedMaxValue { get; set; }

        [JsonProperty("min_parameter_id")]
        public string MinParameterId { get; set; }

        [JsonProperty("max_parameter_id")]
        public string MaxParameterId { get; set; }

        [JsonProperty("amount_of_options_to_show_before_collapsing")]
        public int? AmountOfOptionsToShowBeforeCollapsing { get; set; }

        [JsonProperty("amount_of_products_before_filter")]
        public int? AmountOfProductsBeforeFilter { get; set; }

        [JsonProperty("information")]
        public Information Information { get; set; }
    }

    public class Section
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("default_enabled_state")]
        public bool DefaultEnabledState { get; set; }

        [JsonProperty("enabled")]
        public bool Enabled { get; set; }

        [JsonProperty("hidden")]
        public bool Hidden { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("filters")]
        public List<Filter> Filters { get; set; }
    }

    public class ImageMetadata
    {
        [JsonProperty("special_offer_types")]
        public List<string> SpecialOfferTypes { get; set; }

        [JsonProperty("image_label")]
        public string ImageLabel { get; set; }

        [JsonProperty("is_smart_choice_label_visible")]
        public bool IsSmartChoiceLabelVisible { get; set; }

        [JsonProperty("smart_choice_titles")]
        public object SmartChoiceTitles { get; set; }
    }

    public class Image
    {
        [JsonProperty("webp_url")]
        public string WebpUrl { get; set; }

        [JsonProperty("fallback_url")]
        public string FallbackUrl { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }
    }

    public class NumberOfReviews
    {
        [JsonProperty("raw")]
        public int Raw { get; set; }

        [JsonProperty("formatted")]
        public string Formatted { get; set; }
    }

    public class Value
    {
        [JsonProperty("raw")]
        public double Raw { get; set; }

        [JsonProperty("formatted")]
        public string Formatted { get; set; }
    }

    public class CameraScore
    {
        [JsonProperty("field_name")]
        public string FieldName { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("value")]
        public Value Value { get; set; }
    }

    public class ScreenScore
    {
        [JsonProperty("field_name")]
        public string FieldName { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("value")]
        public Value Value { get; set; }
    }

    public class SpeedScore
    {
        [JsonProperty("field_name")]
        public string FieldName { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("value")]
        public Value Value { get; set; }
    }

    public class BatteryScore
    {
        [JsonProperty("field_name")]
        public string FieldName { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("value")]
        public Value Value { get; set; }
    }

    public class PriceQualityScore
    {
        [JsonProperty("field_name")]
        public string FieldName { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("value")]
        public Value Value { get; set; }
    }

    public class ScoreFieldAverages
    {
        [JsonProperty("camera_score")]
        public CameraScore CameraScore { get; set; }

        [JsonProperty("screen_score")]
        public ScreenScore ScreenScore { get; set; }

        [JsonProperty("speed_score")]
        public SpeedScore SpeedScore { get; set; }

        [JsonProperty("battery_score")]
        public BatteryScore BatteryScore { get; set; }

        [JsonProperty("price_quality_score")]
        public PriceQualityScore PriceQualityScore { get; set; }
    }

    public class Score
    {
        [JsonProperty("raw")]
        public double Raw { get; set; }

        [JsonProperty("formatted")]
        public string Formatted { get; set; }
    }

    public class Stars
    {
        [JsonProperty("raw")]
        public double Raw { get; set; }

        [JsonProperty("formatted")]
        public string Formatted { get; set; }
    }

    public class Label
    {
        [JsonProperty("score")]
        public Score Score { get; set; }

        [JsonProperty("split_score")]
        public List<string> SplitScore { get; set; }

        [JsonProperty("stars_percentage")]
        public int? StarsPercentage { get; set; }

        [JsonProperty("stars")]
        public Stars Stars { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public class Review
    {
        [JsonProperty("number_of_reviews")]
        public NumberOfReviews NumberOfReviews { get; set; }

        [JsonProperty("score_field_averages")]
        public ScoreFieldAverages ScoreFieldAverages { get; set; }

        [JsonProperty("reviews_page_url")]
        public string ReviewsPageUrl { get; set; }

        [JsonProperty("label")]
        public Label Label { get; set; }
    }

    public class ShippingStatus
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("max_days")]
        public string MaxDays { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("shortname")]
        public string Shortname { get; set; }

        [JsonProperty("label")]
        public object Label { get; set; }
    }

    public class ShopStock
    {
        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("house_nr")]
        public string HouseNr { get; set; }

        [JsonProperty("house_nr_addition")]
        public string HouseNrAddition { get; set; }

        [JsonProperty("postal_code")]
        public string PostalCode { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("shipping_status")]
        public ShippingStatus ShippingStatus { get; set; }
    }

    public class Hardware
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("shortname")]
        public string Shortname { get; set; }

        [JsonProperty("url_segment_string")]
        public string UrlSegmentString { get; set; }

        [JsonProperty("url_path")]
        public string UrlPath { get; set; }

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }

        [JsonProperty("brand")]
        public string Brand { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("os")]
        public string Os { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("is_price_including_vat")]
        public bool IsPriceIncludingVat { get; set; }

        [JsonProperty("pretty_standalone_price")]
        public string PrettyStandalonePrice { get; set; }

        [JsonProperty("pretty_from_price")]
        public string PrettyFromPrice { get; set; }

        [JsonProperty("pretty_dimensions")]
        public string PrettyDimensions { get; set; }

        [JsonProperty("pretty_weight")]
        public string PrettyWeight { get; set; }

        [JsonProperty("pretty_screen_size")]
        public string PrettyScreenSize { get; set; }

        [JsonProperty("pretty_screen_resolution")]
        public string PrettyScreenResolution { get; set; }

        [JsonProperty("pretty_storage_size")]
        public string PrettyStorageSize { get; set; }

        [JsonProperty("pretty_extendable_storage_type")]
        public string PrettyExtendableStorageType { get; set; }

        [JsonProperty("shop_count")]
        public int ShopCount { get; set; }

        [JsonProperty("shop_available_count")]
        public int ShopAvailableCount { get; set; }

        [JsonProperty("sim_card_type")]
        public List<object> SimCardType { get; set; }

        [JsonProperty("has_outlets")]
        public bool HasOutlets { get; set; }

        [JsonProperty("review")]
        public Review Review { get; set; }

        [JsonProperty("shop_stock")]
        public List<ShopStock> ShopStock { get; set; }

        [JsonProperty("yesterday_sales_count")]
        public int YesterdaySalesCount { get; set; }

        [JsonProperty("yesterday_hourly_page_views")]
        public int YesterdayHourlyPageViews { get; set; }

        [JsonProperty("group_name")]
        public string GroupName { get; set; }
    }

    public class EarliestOnlineShippingStatus
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("max_days")]
        public int MaxDays { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("shortname")]
        public string Shortname { get; set; }

        [JsonProperty("label")]
        public object Label { get; set; }
    }

    public class Result
    {
        [JsonProperty("index")]
        public int Index { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("should_show_plus_package")]
        public bool ShouldShowPlusPackage { get; set; }

        [JsonProperty("pretty_name")]
        public string PrettyName { get; set; }

        [JsonProperty("image_metadata")]
        public ImageMetadata ImageMetadata { get; set; }

        [JsonProperty("images")]
        public List<Image> Images { get; set; }

        [JsonProperty("hardware")]
        public Hardware Hardware { get; set; }

        [JsonProperty("shipping_status")]
        public ShippingStatus ShippingStatus { get; set; }

        [JsonProperty("earliest_online_shipping_status")]
        public EarliestOnlineShippingStatus EarliestOnlineShippingStatus { get; set; }
    }

    public class AdditionalData
    {
        [JsonProperty("additional_request_data")]
        public List<object> AdditionalRequestData { get; set; }
    }

    public class ItemCount
    {
        [JsonProperty("parameter_id")]
        public string ParameterId { get; set; }

        [JsonProperty("options")]
        public List<Option> Options { get; set; }

        [JsonProperty("results_per_page")]
        public int ResultsPerPage { get; set; }

        [JsonProperty("total_results")]
        public int TotalResults { get; set; }

        [JsonProperty("total_results_formatted")]
        public string TotalResultsFormatted { get; set; }

        [JsonProperty("total_results_string")]
        public string TotalResultsString { get; set; }
    }

    public class Sorting
    {
        [JsonProperty("parameter_id")]
        public string ParameterId { get; set; }

        [JsonProperty("options")]
        public List<Option> Options { get; set; }
    }

    public class FirstPage
    {
        [JsonProperty("value")]
        public int Value { get; set; }

        [JsonProperty("url_path")]
        public string UrlPath { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class CurrentPage
    {
        [JsonProperty("value")]
        public int Value { get; set; }

        [JsonProperty("url_path")]
        public string UrlPath { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class NextPage
    {
        [JsonProperty("value")]
        public int Value { get; set; }

        [JsonProperty("url_path")]
        public string UrlPath { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class LastPage
    {
        [JsonProperty("value")]
        public int Value { get; set; }

        [JsonProperty("url_path")]
        public string UrlPath { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class ConveniencePages
    {
        [JsonProperty("first_page")]
        public FirstPage FirstPage { get; set; }

        [JsonProperty("current_page")]
        public CurrentPage CurrentPage { get; set; }

        [JsonProperty("next_page")]
        public NextPage NextPage { get; set; }

        [JsonProperty("last_page")]
        public LastPage LastPage { get; set; }
    }

    public class NavigationPage
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("value")]
        public int Value { get; set; }

        [JsonProperty("first_page")]
        public bool FirstPage { get; set; }

        [JsonProperty("last_page")]
        public bool LastPage { get; set; }

        [JsonProperty("current_page")]
        public bool CurrentPage { get; set; }

        [JsonProperty("url_path")]
        public string UrlPath { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public class Pagination
    {
        [JsonProperty("parameter_id")]
        public string ParameterId { get; set; }

        [JsonProperty("convenience_pages")]
        public ConveniencePages ConveniencePages { get; set; }

        [JsonProperty("navigation_pages")]
        public List<NavigationPage> NavigationPages { get; set; }
    }

    public class ResponseFormat
    {
        [JsonProperty("parameter_id")]
        public string ParameterId { get; set; }

        [JsonProperty("options")]
        public List<Option> Options { get; set; }
    }

    public class SpecificFilters
    {
        [JsonProperty("parameter_id")]
        public string ParameterId { get; set; }

        [JsonProperty("returned_filters")]
        public List<object> ReturnedFilters { get; set; }
    }

    public class RequestedBrandOutOfStock
    {
        [JsonProperty("parameter_id")]
        public string ParameterId { get; set; }

        [JsonProperty("messages")]
        public List<object> Messages { get; set; }
    }

    public class ExcludeVisibleAfterOptions
    {
        [JsonProperty("parameter_id")]
        public object ParameterId { get; set; }

        [JsonProperty("options")]
        public List<object> Options { get; set; }
    }

    public class ExcludeAllowedWithSubscriptionsAfterOptions
    {
        [JsonProperty("parameter_id")]
        public object ParameterId { get; set; }

        [JsonProperty("options")]
        public List<object> Options { get; set; }
    }

    public class Settings
    {
        [JsonProperty("item_count")]
        public ItemCount ItemCount { get; set; }

        [JsonProperty("sorting")]
        public Sorting Sorting { get; set; }

        [JsonProperty("pagination")]
        public Pagination Pagination { get; set; }

        [JsonProperty("response_format")]
        public ResponseFormat ResponseFormat { get; set; }

        [JsonProperty("specific_filters")]
        public SpecificFilters SpecificFilters { get; set; }

        [JsonProperty("requested_brand_out_of_stock")]
        public RequestedBrandOutOfStock RequestedBrandOutOfStock { get; set; }

        [JsonProperty("exclude_visible_after_options")]
        public ExcludeVisibleAfterOptions ExcludeVisibleAfterOptions { get; set; }

        [JsonProperty("exclude_allowed_with_subscriptions_after_options")]
        public ExcludeAllowedWithSubscriptionsAfterOptions ExcludeAllowedWithSubscriptionsAfterOptions { get; set; }
    }

    public class BelsimpelPhone
    {
        [JsonProperty("properties")]
        public Properties Properties { get; set; }

        [JsonProperty("seo_data")]
        public SeoData SeoData { get; set; }

        [JsonProperty("header")]
        public Header Header { get; set; }

        [JsonProperty("ab_test_data")]
        public List<object> AbTestData { get; set; }

        [JsonProperty("sections")]
        public List<Section> Sections { get; set; }

        [JsonProperty("results")]
        public List<Result> Results { get; set; }

        [JsonProperty("additional_data")]
        public AdditionalData AdditionalData { get; set; }

        [JsonProperty("active_filters")]
        public List<object> ActiveFilters { get; set; }

        [JsonProperty("settings")]
        public Settings Settings { get; set; }
    }
}
