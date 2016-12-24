using System.Reflection;
using RimWorld;
using Verse;

namespace PrisonExtensions.Utilities
{
    public static class PrisonUtility
    {
        public static void AddNeedJoy(Pawn pawn)
        {
            if (pawn.needs.joy == null)
            {
                var addNeed =
                    typeof(Pawn_NeedsTracker).GetMethod("AddNeed", BindingFlags.Instance | BindingFlags.NonPublic);
                addNeed.Invoke(pawn.needs, new object[] {DefDatabase<NeedDef>.GetNamed("Joy")});
            }
            pawn.needs.joy.CurLevel = Rand.Range(0, 0.5f);
        }

        public static void AddNeedComfort(Pawn pawn)
        {
            if (pawn.needs.comfort == null)
            {
                var addNeed =
                    typeof(Pawn_NeedsTracker).GetMethod("AddNeed", BindingFlags.Instance | BindingFlags.NonPublic);
                addNeed.Invoke(pawn.needs, new object[] {DefDatabase<NeedDef>.GetNamed("Comfort")});
            }
            pawn.needs.comfort.CurLevel = Rand.Range(0, 0.5f);
        }
    }
}