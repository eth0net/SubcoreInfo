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
            PawnName = new NameTriple("Grignr", "Grignr", "Grignrson"),
        },
        new CompInfoBase {
            PawnName = new NameTriple("Dexter", "Feldoh", "Feldoh"),
        }
    ];

    public static CompInfoBase RandomSubcore => Subcores.RandomElement();
}
