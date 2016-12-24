using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PrisonExtensions.Detouring;
using PrisonExtensions.Detours;
using PrisonExtensions.JoyGivers;
using PrisonExtensions.ThingComps;
using RimWorld;
using Verse;

namespace PrisonExtensions
{
    [StaticConstructorOnStartup]
    public class Bootstrapper
    {
        public static BindingFlags bindingFlags = GenGeneric.BindingFlagsAll;

        static Bootstrapper()
        {
            initSharedBedroom();
            initPrisonerJoy();
            initWardens();
            InjectOverridesIntoPawns();
            Log.Message("PrisonExtensions injected.");
//            var designationCategory = DefDatabase<DesignationCategoryDef>.GetNamed("Structure", false);
//            foreach (var d in designationCategory._resolvedDesignators())
//            {
//                Log.Message(d.ToString());
//            }
        }

        private static void initSharedBedroom()
        {
            var source = typeof(ThoughtWorker_SharedBedroom).GetMethod("CurrentStateInternal", bindingFlags);
            var dest =
                typeof(ThoughtWorkers.ThoughtWorker_SharedBedroom).GetMethod("CurrentStateInternal", bindingFlags);
            Detour.TryDetourFromTo(source, dest);
        }

        private static void initPrisonerJoy()
        {
            var source = typeof(Pawn_NeedsTracker).GetMethod("ShouldHaveNeed", bindingFlags);
            var dest = typeof(Prisoner_NeedsTracker).GetMethod("ShouldHaveNeed", bindingFlags);
            Detour.TryDetourFromTo(source, dest);

            source = typeof(JoyGiver_TakeDrug).GetMethod("BestIngestItem", bindingFlags);
            dest = typeof(JoyGiver_PrisonerTakeDrug).GetMethod("BestIngestItem", bindingFlags);
            Detour.TryDetourFromTo(source, dest);
        }

        private static void initWardens()
        {
            var source = typeof(WorkGiver_Warden_DeliverFood).GetMethod("JobOnThing", bindingFlags);
            var dest = typeof(WorkerGivers.WorkGiver_Warden_DeliverFood).GetMethod("JobOnThing", bindingFlags);
            Detour.TryDetourFromTo(source, dest);
        }

        private static void InjectOverridesIntoPawns()
        {
            var thingDefsToInjectInto = DefDatabase<ThingDef>.AllDefs.Where(thingDef => (
                    (thingDef.inspectorTabs != null) &&
                    (thingDef.inspectorTabs.Contains(typeof(RimWorld.ITab_Pawn_Prisoner)))
                ))
                .ToList();

            foreach (var thingDef in thingDefsToInjectInto)
            {
                if (thingDef.comps == null)
                {
                    thingDef.comps = new List<CompProperties>();
                }
                if (!thingDef.HasComp(typeof(CompPrisoner)))
                {
                    var compProperties = new CompProperties();
                    compProperties.compClass = typeof(CompPrisoner);
                    thingDef.comps.Add(compProperties);
                }
            }
        }
    }
}