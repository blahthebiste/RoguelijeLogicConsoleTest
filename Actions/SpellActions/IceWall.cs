public class IceWall : Action {

    public IceWall() {
        this.name = "Ice Wall";
        this.description = "Generate 18 Block.";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 18;
        this.hasLimitedUses = true;
        this.uses = 2;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.SELF;
    }

    public override bool useOnTarget(Entity? target, Modifier? modifier) {
        int power = magicNumber;
        if(owner!.HasStatusEffect("Spell Power")) {
            StatusEffect spell_power = owner.GetStatusEffect("Spell Power")!;
            power += spell_power.amount;
        }
        if(owner!.HasStatusEffect("Charged")) {
            StatusEffect charge = owner.GetStatusEffect("Charged")!;
            power += charge.amount;
            owner.EffectList.Remove(charge);
        }
        // Generate Block. (Don't trigger onGainBlock since this is a spell? Too late)
        Console.WriteLine("Generated "+power+" Block.");
        if (target == null)
        {
            Console.WriteLine("ERROR: null target for action '"+this+"'.");
            return false;
        }
        // Generate Block.
        Battlefield.addBlock(power, target);
        return true;
    }
}