public class Cover : Action {

    public Cover() {
        this.name = "Cover";
        this.actionType = ActionType.DEFEND;
        this.magicNumber = 5;
        this.targetting = TargetCategory.SINGLE_ALLY;
        this.description = "Select an ally. Deal "+magicNumber+" damage to all enemies who attack them this turn.";
    }

    // Only decrement 
    public override bool useOnce(Modifier? modifier) {
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        // Apply the Dodge status effect
        this.owner.AddStatusEffect(new Dodging(magicNumber, this.owner));
        return true;
    }

    public override bool useOnTarget(Entity? target, Modifier? modifier) {
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        if (target == null) {
            Console.WriteLine("ERROR: null target for action '"+this+"'.");
            return false;
        }
        // Apply the Cover status effect
        target.AddStatusEffect(new Covered(magicNumber, target, this.owner));
        return true;
    }
    
}