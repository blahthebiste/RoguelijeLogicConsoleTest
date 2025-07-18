public class IceWall : Action
{

    public IceWall()
    {
        this.name = "Ice Wall";
        this.description = "Generate 18 Block.";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 18;
        this.hasLimitedUses = true;
        this.uses = 2;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.SELF;
    }


    public override bool useOnce(Modifier? modifier) {
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        int power = magicNumber;
        if (owner!.HasStatusEffect("Spell Power"))
        {
            StatusEffect spell_power = owner.GetStatusEffect("Spell Power")!;
            power += spell_power.amount;
        }
        if (owner!.HasStatusEffect("Charged"))
        {
            StatusEffect charge = owner.GetStatusEffect("Charged")!;
            power += charge.amount;
            owner.EffectList.Remove(charge);
        }
        // Generate Block. (Don't trigger onGainBlock since this is a spell? Too late)
        Console.WriteLine("Generated " + power + " Block.");
        // Generate Block.
        Battlefield.addBlock(power, this.owner);
        return true;
    }    
}