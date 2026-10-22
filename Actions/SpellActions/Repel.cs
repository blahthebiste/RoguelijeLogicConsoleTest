public class Repel : Action
{

    public Repel()
    {
        this.name = "Repel";
        this.actionType = ActionType.SPELL;
        this.block = 16;
        this.magicNumber = 1;
        this.hasLimitedUses = true;
        this.uses = 2;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.SELF;
        this.description = "Generate "+block+" Block. Stun attackers for "+magicNumber+" turn.";
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
        // Add Reflect status:
        owner.AddStatusEffect(new Repelling(magicNumber, owner));
        return true;
    }    
}