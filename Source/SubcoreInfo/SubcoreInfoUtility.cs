using SubcoreInfo.Comps;
using System.Collections.Generic;
using Verse;

namespace SubcoreInfo
{
    internal static class SubcoreInfoUtility
    {

        public static void CopySubcoreInfo(ThingWithComps src, ThingWithComps dst)
        {
            IEnumerable<CompBase> srcComps = src.GetComps<CompBase>();
            IEnumerable<CompBase> dstComps = dst.GetComps<CompBase>();

            foreach (CompBase srcComp in srcComps)
            {
                foreach (CompBase dstComp in dstComps)
                {
                    dstComp.Copy(srcComp);
                }
                srcComp.Reset();
            }
        }
    }
}