using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoicePlatform.Utility.Enums
{
    public enum AdminFilterArtist
    {
        Status,
        Gender,
        Country,
        VoiceStyle
    }

    public enum AdminSortArtist
    {
        Name,
        Email,
        Gender,
        Status
    }

    public enum CustomerFilterArtist
    {
        Gender,
        Country,
        VoiceStyle,
        PriceMax,
        PriceMin
    }

    public enum SortRating
    {
        ReviewDate,
        Star
    }
}
