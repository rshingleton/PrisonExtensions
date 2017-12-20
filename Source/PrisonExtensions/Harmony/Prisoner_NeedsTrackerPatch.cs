using System.Reflection;
using Harmony;
using RimWorld;
using Verse;

namespace PrisonExtensions.Detours
{
    internal static class Prisoner_NeedsTrackerPatch
    {
        /// <summary>
        /// Added Joy and Comfort to prisoners
        /// </summary>
        [HarmonyPatch(typeof(Pawn_NeedsTracker), "ShouldHaveNeed")]
        public class ShouldHaveNeed
        {
            private static readonly NeedDef defComfort = DefDatabase<NeedDef>.GetNamed("Comfort");
            private static readonly NeedDef defBeauty = DefDatabase<NeedDef>.GetNamed("Beauty");
            private static readonly NeedDef defSpace = DefDatabase<NeedDef>.GetNamed("Space");

            [HarmonyPrefix]
            public static bool Prefix(Pawn_NeedsTracker __instance, ref bool __result, NeedDef nd)
            {
                var pawn = Traverse.Create(__instance).Field("pawn").GetValue<Pawn>();

                if ((nd == NeedDefOf.Joy || nd == defComfort || nd == defBeauty || nd == defSpace) &&
                    (pawn.IsPrisonerOfColony || pawn.IsPrisoner)) // ADDED
                {
                    __result = true;
                    return false;
                }
                return true;
            }
        }
    }
}