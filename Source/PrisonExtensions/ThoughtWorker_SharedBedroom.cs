using System;
using Verse;
using RimWorld;

namespace PrisonExtensions
{
    public class ThoughtWorker_SharedBedroom : ThoughtWorker
    {
        protected override ThoughtState CurrentStateInternal(Pawn p)
        {
            if (p.IsPrisoner || p.IsPrisonerOfColony)
            {
                Log.Message("You're a prisoner, stop crying");
                return false;
            }
            else
            {
                return p.ownership.OwnedBed != null && p.ownership.OwnedRoom == null &&
                       !p.ownership.OwnedBed.GetRoom().PsychologicallyOutdoors;
            }

        }
    }
}