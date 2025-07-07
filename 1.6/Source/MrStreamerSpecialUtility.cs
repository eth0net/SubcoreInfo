using Steamworks;
using SubcoreInfo.Comps;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace SubcoreInfo;

[StaticConstructorOnStartup]
public static class MrStreamerSpecialUtility
{
    private static CompInfoBase _currentSpecialComp;
    private static float _lastPaneClosedRealtime = -100000;
    private const float SpecialCompCooldownSeconds = 5;

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

    public static readonly List<CSteamID> cSteamIDs =
    [
        new(76561198013667370), // eth0net
        new(76561198035612258), // Alstyr
        new(76561198028227150), // Mr Streamer
    ];

    public static List<CompInfoBase> Subcores =
    [
        new()
        {
            TitleName = "Grignr",
            PawnName = new NameTriple(
                "Grignr",
                "Grignr",
                "Grignrson"
            ),
            FactionName = "The Streamers",
            IdeoName = "The Streamers",
        },
        new()
        {
            TitleName = "Feldoh",
            PawnName = new NameTriple(
                "Dexter",
                "Feldoh",
                "Feldoh"
            ),
            FactionName = "The Streamers",
            IdeoName = "The Streamers",
        }
    ];

    public static CompInfoBase RandomSubcore => Subcores.RandomElement();

    public static bool Enabled
    {
        get => SubcoreInfoSettings.mrStreamerSpecial;
        set => SubcoreInfoSettings.mrStreamerSpecial = value;
    }

    private static bool CooldownOver
    {
        get => Time.realtimeSinceStartup - _lastPaneClosedRealtime >= SpecialCompCooldownSeconds;
    }

    public static void NotifyInspectPaneOpened()
    {
        if (!Enabled) return;
        if (CooldownOver && Rand.Bool)
        {
            _currentSpecialComp = RandomSubcore;
        }
        else
        {
            _currentSpecialComp = null;
        }
    }

    public static void NotifyInspectPaneClosed()
    {
        if (_currentSpecialComp == null) return;
        _lastPaneClosedRealtime = Time.realtimeSinceStartup;
        _currentSpecialComp = null;
    }

    public static CompInfoBase GetDisplayComp(CompInfoBase normalComp)
    {
        if (!Enabled) return normalComp;
        return _currentSpecialComp ?? normalComp;
    }
}
