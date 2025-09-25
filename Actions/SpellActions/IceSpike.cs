public class IceSpike : Action {

    public IceSpike() {
        this.name = "IceSpike";
        this.description = "Deal 6 damage, plus any Frost on the target.";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 6;
        this.hasLimitedUses = true;
        this.uses = 3;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.SINGLE_ENEMY;
    }

    public override bool useOnTarget(Entity? target, Modifier? modifier) {
        if (target == null) {
            Console.WriteLine("ERROR: null target for action '"+this+"'.");
            return false;
        }
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        int power = magicNumber;
        if(owner.HasStatusEffect("Spell Power")) {
            StatusEffect spell_power = owner.GetStatusEffect("Spell Power")!;
            power += spell_power.amount;
        }
        if(owner.HasStatusEffect("Charged")) {
            StatusEffect charge = owner.GetStatusEffect("Charged")!;
            power += charge.amount;
            owner.EffectList.Remove(charge);
        }
        // Ice Spike gets boosted by frost on the target:
        if(target.HasStatusEffect("Frost")) {
            StatusEffect frost = target.GetStatusEffect("Frost")!;
            power += frost.amount;
        }
        // Deal damage to the target.
        if (modifier != null) power += modifier.damageMod;
        Attack atk = new Attack(power, this.owner, target);
        //atk = owner.onAttack(atk); // Don't trigger onAttack for the owner, since it is a spell?
        target.onReceiveAttack(atk);
        return true;
    }
}