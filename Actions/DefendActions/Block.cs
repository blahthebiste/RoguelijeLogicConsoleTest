public class Block : Action {

    public Block() {
        this.name = "Block";
        this.description = "Generate 6 Block.";
        this.actionType = ActionType.DEFEND;
        this.block = 6;
        this.targetting = TargetCategory.SELF;
    }


    public override bool useOnce(Modifier? modifier) {
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        // Generate Block.
        Battlefield.addBlock(block, this.owner);
        return true;
    }
}