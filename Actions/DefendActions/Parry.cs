public class Parry : Action {

    public Parry() {
        this.name = "Parry";
        this.actionType = ActionType.DEFEND;
        this.block = 4;
        this.targetting = TargetCategory.SELF;
        this.description = "Generate "+block+" Block.";
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