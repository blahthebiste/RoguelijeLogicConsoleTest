public class Cower : Action {

    public Cower() {
        this.name = "Cower";
        this.description = "Generate 3 Block.";
        this.actionType = ActionType.DEFEND;
        this.block = 3;
        this.targetting = TargetCategory.SELF;
    }

    public override bool useOnce(Modifier? modifier) {
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        // Generate Block.
        Battlefield.addBlock(block + (modifier == null? 0 : modifier.blockMod), this.owner);
        return true;
    }
}