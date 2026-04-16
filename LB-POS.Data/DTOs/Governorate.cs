using System.Text.Json.Serialization;

namespace LB_POS.Data.DTOs
{
    public class Country
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name_ar")]
        public string NameAr { get; set; }

        [JsonPropertyName("name_en")]
        public string NameEn { get; set; }
        [JsonPropertyName("ISOCode")]
        public string ISOCode { get; set; }

        [JsonPropertyName("governorates")]
        public List<Governorate> Governorates { get; set; }
    }

    public class Governorate
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name_ar")]
        public string NameAr { get; set; }

        [JsonPropertyName("name_en")]
        public string NameEn { get; set; }

        [JsonPropertyName("subregions")]
        public List<Subregion> Subregions { get; set; }
    }

    public class Subregion
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } // كما لاحظت، هي نص (String) في ملفك

        [JsonPropertyName("name_ar")]
        public string NameAr { get; set; }

        [JsonPropertyName("name_long")]
        public string NameLong { get; set; }

        [JsonPropertyName("name_en")]
        public string NameEn { get; set; }
    }
}