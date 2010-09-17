using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JustGiving.Api.Sdk.Model
{
    [Serializable]
    public enum ActivityType
    {
        NotSet = 0,
        Running_Marathons = 1,
        Treks = 2,
        Walks = 3,
        Cycling = 4,
        Swimming = 5,
        Birthday = 6,
        Wedding = 7,
        OtherCelebration = 8,
        Christening = 9,
        InMemory = 10,
        Anniversaries = 11,
        Triathlons = 12,
        Parachuting_Skydives = 13,
        OtherSportingEvents = 14,
        NewYearsResolutions = 15,
        Christmas = 16,
    }
}
