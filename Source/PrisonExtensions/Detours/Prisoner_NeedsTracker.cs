using System.Reflection;
using PrisonExtensions.Injectors;
using RimWorld;
using Verse;

namespace PrisonExtensions.Detours
{
    internal class Prisoner_NeedsTracker : Pawn_NeedsTracker
    {
        internal bool ShouldHaveNeed(NeedDef nd)
        {
            var pawn = this._needs_pawn();
            if (pawn.RaceProps.intelligence < nd.minIntelligence)
            {
                return false;
            }
            if (nd == NeedDefOf.Joy && (pawn.IsPrisonerOfColony || pawn.IsPrisoner))
            {
                return true;
            }
            if (nd.colonistsOnly && (pawn.Faction == null || !pawn.Faction.IsPlayer))
            {
                return false;
            }
            if (nd.colonistAndPrisonersOnly && (pawn.Faction == null || !pawn.Faction.IsPlayer)
                && (pawn.HostFaction == null || pawn.HostFaction != Faction.OfPlayer))
            {
                return false;
            }
            if (nd.onlyIfCausedByHediff && !pawn.health.hediffSet.hediffs.Any((Hediff x) => x.def.causesNeed == nd))
            {
                return false;
            }

            if (nd == NeedDefOf.Food)
            {
                return pawn.RaceProps.EatsFood;
            }
            return nd != NeedDefOf.Rest || pawn.RaceProps.needsRest;
        }
    }
}