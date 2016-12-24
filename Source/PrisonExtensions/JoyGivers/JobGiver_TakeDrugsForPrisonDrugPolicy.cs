using System;
using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI;

namespace PrisonExtensions.JoyGivers
{
    public class JoyGiver_PrisonerTakeDrug : JoyGiver_TakeDrug
    {
        private static List<ThingDef> takeableDrugs = new List<ThingDef>();

        protected override Thing BestIngestItem(Pawn pawn, Predicate<Thing> extraValidator)
        {
            if (pawn.IsPrisoner || pawn.IsPrisonerOfColony)
            {
                //Log.Message("No drugs in prison mofo");
                return null;
            }
            Predicate<Thing> predicate = (Thing t) => this.CanIngestForJoy(pawn, t) &&
                                                      (extraValidator == null || extraValidator(t));
            ThingContainer innerContainer = pawn.inventory.innerContainer;
            for (int i = 0; i < innerContainer.Count; i++)
            {
                if (predicate(innerContainer[i]))
                {
                    return innerContainer[i];
                }
            }
            takeableDrugs.Clear();
            DrugPolicy currentPolicy = pawn.drugs.CurrentPolicy;
            for (int j = 0; j < currentPolicy.Count; j++)
            {
                if (currentPolicy[j].allowedForJoy)
                {
                    takeableDrugs.Add(currentPolicy[j].drug);
                }
            }
            takeableDrugs.Shuffle<ThingDef>();
            for (int k = 0; k < takeableDrugs.Count; k++)
            {
                List<Thing> list = pawn.Map.listerThings.ThingsOfDef(takeableDrugs[k]);
                if (list.Count > 0)
                {
                    Predicate<Thing> validator = predicate;
                    Thing thing = GenClosest.ClosestThing_Global_Reachable(pawn.Position, pawn.Map, list,
                        PathEndMode.OnCell, TraverseParms.For(pawn, Danger.Deadly, TraverseMode.ByPawn, false), 9999f,
                        validator, null);
                    if (thing != null)
                    {
                        return thing;
                    }
                }
            }
            return null;
        }
    }
}