public class Restore : Action {

    public Restore() {
        this.name = "Restore";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 6;
        this.hasLimitedUses = true;
        this.uses = 6;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.SINGLE_ANY;
        this.description = "Restore "+magicNumber+" HP.";
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
        // Apply healing
        if(modifier != null) power += modifier.healMod;
        target!.ReceiveHealing(power);
        return true;
    }
}