using System.Collections.Generic;
using System.Reflection;
using RimWorld;
using Verse;

namespace PrisonExtensions.Injectors
{
    public static class Pawn_NeedsTrackerHelper
    {
        public static FieldInfo _needs_pawnField = typeof(Pawn_NeedsTracker).GetField(
            "pawn", BindingFlags.NonPublic | BindingFlags.Instance);

        public static Pawn _needs_pawn(this Pawn_NeedsTracker tracker)
        {
            return _needs_pawnField.GetValue(tracker) as Pawn;
        }
    }
}