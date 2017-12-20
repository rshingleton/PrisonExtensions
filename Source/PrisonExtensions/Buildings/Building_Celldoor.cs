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
            if (p.IsColonist || p.IsColonistPlayerControlled || p.IsPrisoner || p.IsPrisonerOfColony)
            {
                return true;
            }
            return false;
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