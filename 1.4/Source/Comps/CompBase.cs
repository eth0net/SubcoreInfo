using Verse;

namespace SubcoreInfo.Comps;

public abstract class CompBase : ThingComp
{
    /// <summary>
    /// Copy the data from a pawn into this comp to ease data transfer.
    /// </summary>
    /// <param name="pawn"></param>
    public abstract void Copy(Pawn pawn);

    /// <summary>
    /// Copy the data from another comp into this one to ease data transfer.
    /// </summary>
    /// <param name="other"></param>
    public abstract void Copy(CompBase other);

    /// <summary>
    /// Reset clears all data in this comp.
    /// </summary>
    public abstract void Reset();
}
