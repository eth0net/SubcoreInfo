using Verse;

namespace SubcoreInfo.Comps;

public class CompInfoBase : CompBase
{
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
    /// PostExposeData is used to save our component state.
    /// </summary>
    public override void PostExposeData()
    {
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
        TitleName = null;
        PawnName = null;
        FactionName = null;
        IdeoName = null;
    }
}