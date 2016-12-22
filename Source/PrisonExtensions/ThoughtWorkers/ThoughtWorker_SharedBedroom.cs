using RimWorld;
using Verse;

namespace PrisonExtensions.ThoughtWorkers
{
    public class ThoughtWorker_SharedBedroom : ThoughtWorker
    {
        protected override ThoughtState CurrentStateInternal(Pawn p)
        {
            if (p.IsPrisoner || p.IsPrisonerOfColony)
            {
//                if (Prefs.DevMode)
//                {
//                    Log.Message("You're a prisoner, stop crying");
//                }
                return false;
            }
//            if (Prefs.DevMode)
//            {
//                Log.Message("You've been hooked: " + Prefs.DevMode);
//            }
            return p.ownership.OwnedBed != null && p.ownership.OwnedRoom == null &&
                   !p.ownership.OwnedBed.GetRoom().PsychologicallyOutdoors;
        }
    }
}