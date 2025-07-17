public class Cower : Action {

    public Cower() {
        this.name = "Cower";
        this.description = "Generate 3 Block.";
        this.actionType = ActionType.DEFEND;
        this.block = 3;
        this.targetting = TargetCategory.SELF;
    }

    public override bool useOnTarget(Entity? target, Modifier? modifier) {
        if (target == null) {
            Console.WriteLine("ERROR: null target for action '"+this+"'.");
            return false;
        }
        // Generate Block.
        Battlefield.addBlock(block, target);
        return true;
    }
}