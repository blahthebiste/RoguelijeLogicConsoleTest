public class Obstruct : Action {

    public Obstruct() {
        this.name = "Obstruct";
        this.description = "Generate 10 Block.";
        this.actionType = ActionType.DEFEND;
        this.block = 10;
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