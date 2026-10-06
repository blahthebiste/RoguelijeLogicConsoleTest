public class Lightning : Action {

    public Lightning() {
        this.name = "Lightning";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 9;
        this.hasLimitedUses = true;
        this.uses = 2;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.SINGLE_ENEMY;
        this.description = "Deal "+magicNumber+" damage. Excess damage chains to the next enemy.";
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
        if (owner!.HasStatusEffect("Surging"))
        {
            StatusEffect charge = owner.GetStatusEffect("Surging")!;
            power *= 2;
            charge.Decrease(1);
        }
        // Apply the overkill effect to the target:
        target!.AddStatusEffect(new ChainLightning(owner, target));
        // Deal damage to the target.
        if(modifier != null) power += modifier.damageMod;
        Attack atk = new Attack(power, this.owner!, target!);
        //atk = owner.onAttack(atk); // Don't trigger onAttack for the owner, since it is a spell?
        target!.onReceiveAttack(atk);
        return true;
    }

}