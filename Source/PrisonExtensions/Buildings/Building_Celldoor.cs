using RimWorld;
using Verse;
using Verse.AI;
using Verse.AI.Group;

namespace PrisonExtensions.Buildings
{
    //A door that does block rooms but also opens for prisoners.
    public class Building_Celldoor : Building_Door
    {
        private bool locked = false;

        public override bool PawnCanOpen(Pawn p)
        {
            Lord lord = p.GetLord();
            if (lord != null && lord.LordJob != null && lord.LordJob.CanOpenAnyDoor(p) || this.Faction == null)
                return true;
            return (GenAI.MachinesLike(this.Faction, p) || p.IsPrisonerOfColony); //also opens for prisoners
        }

        public bool isLocked()
        {
            return locked;
        }

        public void lockDoor()
        {
            this.locked = true;
        }

        public void unlockDoor()
        {
            this.locked = false;
        }
    }
}