public class Block : Action {

    public Block() {
        this.name = "Block";
        this.actionType = ActionType.DEFEND;
        this.block = 6;
        this.targetting = TargetCategory.SELF;
        this.description = "Generate "+block+" Block.";
    }


    public override bool useOnce() {
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        // Generate Block.
        Battlefield.addBlock(properties.block, this.owner);
        return true;
    }
}