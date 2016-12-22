using System.Reflection;
using PrisonExtensions.Detouring;
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
            Log.Message("PrisonExtensions injected.");
        }

        private static void initSharedBedroom()
        {
            var source = typeof(ThoughtWorker_SharedBedroom).GetMethod("CurrentStateInternal", bindingFlags);
            var dest =
                typeof(ThoughtWorkers.ThoughtWorker_SharedBedroom).GetMethod("CurrentStateInternal", bindingFlags);
            Detour.TryDetourFromTo(source, dest);
        }
    }
}