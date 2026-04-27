public class Zap : Action {

    public Zap() {
        this.name = "Zap";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 9;
        this.hasLimitedUses = true;
        this.uses = 3;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.SINGLE_ENEMY;
        this.description = "Deal "+magicNumber+" damage.";
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
        // Deal damage to the target.
        if(modifier != null) power += modifier.damageMod;
        Attack atk = new Attack(power, this.owner!, target!);
        //atk = owner.onAttack(atk); // Don't trigger onAttack for the owner, since it is a spell?
        target!.onReceiveAttack(atk);
        return true;
    }
}