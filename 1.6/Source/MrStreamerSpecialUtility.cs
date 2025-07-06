using Steamworks;
using SubcoreInfo.Comps;
using System.Collections.Generic;
using Verse;

namespace SubcoreInfo;

[StaticConstructorOnStartup]
public static class MrStreamerSpecialUtility
{
    static MrStreamerSpecialUtility()
    {
        if (cSteamIDs.Contains(SteamUser.GetSteamID()))
        {
            Enabled = true;
        }
        if (Enabled)
        {
            Log.Message("Mr Streamer Special mode enabled.");
        }
    }

    public static readonly List<CSteamID> cSteamIDs = [
        new CSteamID(76561198013667370), // synthe0n
        new CSteamID(76561198035612258), // Alstyr
        new CSteamID(76561198028227150), // Mr Streamer
    ];

    public static bool Enabled
    {
        get => SubcoreInfoSettings.mrStreamerSpecial;
        set => SubcoreInfoSettings.mrStreamerSpecial = value;
    }

    public static List<CompInfoBase> Subcores = [
        new CompInfoBase {
            TitleName = "Grignr",
            PawnName = new NameTriple("Grignr", "Grignr", "Grignrson"),
            FactionName = "The Streamers",
            IdeoName = "The Streamers",
        },
        new CompInfoBase {
            TitleName = "Feldoh",
            PawnName = new NameTriple("Dexter", "Feldoh", "Feldoh"),
            FactionName = "The Streamersd ",
            IdeoName = "The Streamers",
        }
    ];

    public static CompInfoBase RandomSubcore => Subcores.RandomElement();
}
