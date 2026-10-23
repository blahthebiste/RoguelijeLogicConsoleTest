public class Obstruct : Action {

    public Obstruct() {
        this.name = "Obstruct";
        this.actionType = ActionType.DEFEND;
        this.block = 10;
        this.targetting = TargetCategory.SELF;
        this.description = "Generate "+block+" Block.";
    }


    public override bool useOnce() {
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        // Generate Block.
        Battlefield.addBlock((int)(properties.block*properties.blockMult), this.owner);
        return true;
    }
}