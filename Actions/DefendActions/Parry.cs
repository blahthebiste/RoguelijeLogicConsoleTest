public class Parry : Action {

    public Parry() {
        this.name = "Parry";
        this.description = "Generate 4 Block.";
        this.actionType = ActionType.DEFEND;
        this.block = 4;
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