using Verse;

namespace SubcoreInfo.Comps;

public class CompInfoBase : CompBase
{
    /// <summary>
    /// Pawn tracks a reference to the pawn scanned.
    /// </summary>
    public Pawn Pawn;

    /// <summary>
    /// TitleName tracks the title of the pawn scanned.
    /// </summary>
    public string TitleName;

    /// <summary>
    /// PawnName tracks the name of the pawn scanned.
    /// </summary>
    public Name PawnName;

    /// <summary>
    /// FactionName tracks the name of the faction of the pawn scanned.
    /// </summary>
    public string FactionName;

    /// <summary>
    /// IdeoName tracks the name of the ideologion of the pawn scanned.
    /// </summary>
    public string IdeoName;

    /// <summary>
    /// Returns true if all info fields are null or empty.
    /// </summary>
    public bool IsBlank => !HasTitle && !HasFullName && !HasShortName && !HasFaction && !HasIdeo;

    public bool HasTitle => !string.IsNullOrEmpty(TitleName);
    public bool HasFullName => !string.IsNullOrEmpty(PawnName?.ToStringFull);
    public bool HasShortName => !string.IsNullOrEmpty(PawnName?.ToStringShort);
    public bool HasFaction => !string.IsNullOrEmpty(FactionName);
    public bool HasIdeo => !string.IsNullOrEmpty(IdeoName);

    /// <summary>
    /// PostExposeData is used to save our component state.
    /// </summary>
    public override void PostExposeData()
    {
        Scribe_References.Look(ref Pawn, "pawn");
        Scribe_Values.Look(ref TitleName, "titleName");
        Scribe_Deep.Look(ref PawnName, "patternName");
        Scribe_Values.Look(ref FactionName, "factionName");
        Scribe_Values.Look(ref IdeoName, "ideoName");
        base.PostExposeData();
    }

    /// <summary>
    /// Copy the data from a pawn into this comp to ease data transfer.
    /// </summary>
    /// <param name="pawn"></param>
    public override void Copy(Pawn pawn)
    {
        Pawn = pawn;
        TitleName = pawn?.royalty?.MainTitle()?.GetLabelCapFor(pawn);
        PawnName = pawn?.Name;
        FactionName = pawn?.Faction?.Name;
        IdeoName = pawn?.Ideo?.name;
    }

    /// <summary>
    /// Copy the data from another comp into this one to ease data transfer.
    /// </summary>
    /// <param name="other"></param>
    public override void Copy(CompBase other)
    {
        if (other is CompInfoBase comp)
        {
            Pawn = comp.Pawn;
            TitleName = comp.TitleName;
            PawnName = comp.PawnName;
            FactionName = comp.FactionName;
            IdeoName = comp.IdeoName;
        }
    }

    /// <summary>
    /// Reset clears all data in this comp.
    /// </summary>
    public override void Reset()
    {
        Pawn = null;
        TitleName = null;
        PawnName = null;
        FactionName = null;
        IdeoName = null;
    }
}
