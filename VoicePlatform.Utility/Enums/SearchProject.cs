namespace VoicePlatform.Utility.Enums
{
    public enum CustomerFilter
    {
        UsagePurpose,
        Country,
        VoiceStyle,
        Gender,
        PriceMin,
        PriceMax,
        AgeMin,
        AgeMax,
        CreateDateMin,
        CreateDateMax
    }

    public enum AdminFilter
    {
        Status,
        PriceMin,
        PriceMax,
        CreateDate
    }

    public enum AdminSort
    {
        Name,
        Poster,
        Price,
        CreateDate,
        Status
    }
}
