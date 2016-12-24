using System.Linq;
using RimWorld;
using Verse;

namespace PrisonExtensions.PlaceWorkers
{
    public class PlaceWorker_SecurityCamera : PlaceWorker
    {
        public override AcceptanceReport AllowsPlacing(BuildableDef checkingDef, IntVec3 loc, Rot4 rot,
            Thing thingToIgnore = null)
        {
            var r = GridsUtility.GetRoom(loc, Find.VisibleMap);
            if (r == null || r.TouchesMapEdge)
            {
                return (AcceptanceReport) "Security Camera must be placed in a walled room.";
            }
            if (!r.ContainedBeds.Any()) return (AcceptanceReport) true;

            var b = r.ContainedBeds.ElementAt(0);
            if (!b.ForPrisoners)
            {
                return (AcceptanceReport) "Security Camera can't be placed in a room with colonist beds.";
            }
            else
            {
                return (AcceptanceReport) true;
            }
        }
    }
}