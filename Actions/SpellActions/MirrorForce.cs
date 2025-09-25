public class MirrorForce : Action
{

    public MirrorForce()
    {
        this.name = "Mirror Force";
        this.description = "Generate 12 Block. Reflect attacks for 1 turn.";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 12;
        this.magicNumber2 = 1;
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
        if(modifier != null) power += modifier.blockMod;
        // Generate Block. (Don't trigger onGainBlock since this is a spell? Too late)
        Console.WriteLine("Generated " + power + " Block.");
        // Generate Block.
        Battlefield.addBlock(power, this.owner);
        // Add Reflect status:
        owner.AddStatusEffect(new ReflectAttacks(magicNumber2, owner));
        return true;
    }    
}