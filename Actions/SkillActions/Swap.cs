public class Swap : Action {

    public Swap() {
        this.name = "Swap";
        this.actionType = ActionType.SKILL;
        this.targetting = TargetCategory.SINGLE_ALLY;
        this.description = "Swap positions with an ally (free action).";
        this.freeAction = true;
    }

    public override bool useOnTarget(Entity? target, Modifier? modifier) {
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        if (this.owner is not PlayerCharacter) {
            Console.WriteLine("ERROR: enemies cannot use action '"+this+"'.");
            return false;
        }
        if (target == null) {
            Console.WriteLine("ERROR: null target for action '"+this+"'.");
            return false;
        }
        int ownerIndex = Battlefield.PlayerSide.IndexOf(owner);
        int targetIndex = Battlefield.PlayerSide.IndexOf(target);
        if(ownerIndex == -1)
        {
            Console.WriteLine(owner.name+" not found.");
            return false;
        }
        if(targetIndex == -1)
        {
            Console.WriteLine("Target "+target.name+" not found.");
            return false;
        }
        // Perform the swap!
        (Battlefield.PlayerSide[ownerIndex], Battlefield.PlayerSide[targetIndex]) = (Battlefield.PlayerSide[targetIndex], Battlefield.PlayerSide[ownerIndex]);
        Console.WriteLine(owner.name+" swapped places with "+target.name+"!");
        return true;
    
    }
}