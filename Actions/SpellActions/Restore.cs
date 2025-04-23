public class Restore : Action {

    public Restore() {
        this.name = "Restore";
        this.description = "Restore 6 HP.";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 6;
        this.hasLimitedUses = true;
        this.uses = 6;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.SINGLE_ANY;
    }

    // For now, nothing special.
    public override bool canUse(Entity? target, Modifier? modifier) {
        return base.canUse(target, modifier);
    }

    public override bool use(Entity? target, Modifier? modifier) {
        if(base.use(target, modifier)) {
            if(owner!.HasStatusEffect("SpellPower")) {
                StatusEffect power = owner.GetStatusEffect("SpellPower")!;
                magicNumber += power.amount;
            }
            if(owner!.HasStatusEffect("Charged")) {
                StatusEffect charge = owner.GetStatusEffect("Charged")!;
                magicNumber += charge.amount;
                owner.EffectList.Remove(charge);
            }
            // Apply healing
            target!.ReceiveHealing(magicNumber);
            return true;
        }
        return false;
    }
}