using Harmony;
using RimWorld;
using Verse;

namespace PrisonExtensions.Harmony
{
    public class ThoughtWorker_PrisonerPatch
    {
        public static class ThoughtWorker_Patch
        {
            /// <summary>
            /// Against rare error, when guest's ownership.OwnedBed == null 
            /// </summary>
            [HarmonyPatch(typeof(ThoughtWorker_PrisonBarracksImpressiveness), "CurrentStateInternal")]
            public class PrisonBarracksImpressiveness
            {
                [HarmonyPrefix]
                public static bool CurrentStateInternal(ref ThoughtState __result, Pawn p)
                {
                    if (p == null || p.ownership == null || p.ownership.OwnedBed == null)
                    {
                        __result = ThoughtState.Inactive;
                        return false;
                    }
                    return true;
                }
            }

            /// <summary>
            /// Against rare error, when guest's ownership.OwnedBed == null 
            /// </summary>
            [HarmonyPatch(typeof(ThoughtWorker_PrisonCellImpressiveness), "CurrentStateInternal")]
            public class PrisonCellImpressiveness
            {
                [HarmonyPrefix]
                public static bool CurrentStateInternal(ref ThoughtState __result, Pawn p)
                {
                    if (p == null || p.ownership == null || p.ownership.OwnedBed == null)
                    {
                        __result = ThoughtState.Inactive;
                        return false;
                    }
                    return true;
                }
            }

            /// <summary>
            /// Against rare error, when guest's ownership.OwnedBed == null 
            /// </summary>
            [HarmonyPatch(typeof(ThoughtWorker_PrisonCellImpressiveness), "CurrentStateInternal")]
            public class PrisonJoyState
            {
                [HarmonyPrefix]
                public static bool CurrentStateInternal(ref ThoughtState __result, Pawn p)
                {
                    if (p.needs.joy == null)
                    {
                        __result = ThoughtState.Inactive;
                        return false;
                    }
                    switch (p.needs.joy.CurCategory)
                    {
                        case JoyCategory.Empty:
                            __result = ThoughtState.ActiveAtStage(0);
                            return false;
                        case JoyCategory.VeryLow:
                            __result = ThoughtState.ActiveAtStage(1);
                            return false;
                        case JoyCategory.Low:
                            __result = ThoughtState.ActiveAtStage(2);
                            return false;
                        case JoyCategory.Satisfied:
                            __result = ThoughtState.Inactive;
                            return false;
                        case JoyCategory.High:
                            __result = ThoughtState.ActiveAtStage(3);
                            return false;
                        case JoyCategory.Extreme:
                            __result = ThoughtState.ActiveAtStage(4);
                            return false;
                        default:
                            return true;
                    }
                }
            }
        }
    }
}