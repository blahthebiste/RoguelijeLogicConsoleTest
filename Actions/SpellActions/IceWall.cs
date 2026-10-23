public class IceWall : Action
{

    public IceWall()
    {
        this.name = "Ice Wall";
        this.actionType = ActionType.SPELL;
        this.block = 18;
        this.hasLimitedUses = true;
        this.uses = 2;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.SELF;
        this.description = "Generate "+block+" Block.";
    }


    public override bool useOnce() {
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        // Generate Block. (Don't trigger onGainBlock since this is a spell? Too late)
        Console.WriteLine("Generated " + block + " Block.");
        // Generate Block.
        Battlefield.addBlock((int)(properties.block*properties.blockMult), this.owner);
        return true;
    }    
}