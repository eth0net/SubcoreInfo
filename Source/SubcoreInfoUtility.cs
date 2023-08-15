using SubcoreInfo.Comps;
using Verse;

namespace SubcoreInfo;

internal static class SubcoreInfoUtility
{
    public static void ScanPawnInfo(this ThingWithComps thing, Pawn pawn)
    {
        foreach (CompBase comp in thing.GetComps<CompBase>())
        {
            comp.Copy(pawn);
        }
    }

    public static void CopySubcoreInfo(ThingWithComps src, ThingWithComps dst, bool reset = true)
    {
        foreach (CompBase srcComp in src.GetComps<CompBase>())
        {
            foreach (CompBase dstComp in dst.GetComps<CompBase>())
            {
                dstComp.Copy(srcComp);
            }
            if (reset) srcComp.Reset();
        }
    }
}