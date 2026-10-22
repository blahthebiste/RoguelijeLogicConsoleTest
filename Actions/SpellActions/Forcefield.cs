public class Forcefield : Action
{

    public Forcefield()
    {
        this.name = "Forcefield";
        this.actionType = ActionType.SPELL;
        this.block = 25;
        this.hasLimitedUses = true;
        this.uses = 3;
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
        Battlefield.addBlock(properties.block, this.owner);
        return true;
    }    
}