public class Parry : Action {

    public Parry() {
        this.name = "Parry";
        this.actionType = ActionType.DEFEND;
        this.block = 4;
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