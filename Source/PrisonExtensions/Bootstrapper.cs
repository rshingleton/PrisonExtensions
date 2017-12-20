
using HugsLib;
using Verse;

namespace PrisonExtensions
{
    public class Bootstrapper : ModBase
    {
        private const string ModId = "PrisonExtensions";
        internal const string HarmonyInstanceId = "Ihmb.PrisonExtensions";

        public override void WorldLoaded()
        {
            Log.Message("PrisonExtensions injected.");
//            var designationCategory = DefDatabase<DesignationCategoryDef>.GetNamed("Structure", false);
//            foreach (var d in designationCategory._resolvedDesignators())
//            {
//                Log.Message(d.ToString());
//            }
        }

//        private class DesignatorEntry {
//            public readonly Designator_SelectableThings designator;
//            public readonly KeyBindingDef key;
//            public DesignatorEntry(Designator_SelectableThings designator, KeyBindingDef key) {
//                this.designator = designator;
//                this.key = key;
//            }
//        }
        
        
        public override string ModIdentifier
        {
            get { return ModId; }
        }
    }
}