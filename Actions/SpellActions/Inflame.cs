public class Inflame : Action {

    public Inflame() {
        this.name = "Inflame";
        this.description = "Gain 2 Strength.";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 2;
        this.hasLimitedUses = true;
        this.uses = 1;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.NONE;
    }

    // For now, nothing special.
    public override bool canUse(Entity? target, Modifier? modifier) {
        return base.canUse(target, modifier);
    }

    public override bool use(Entity? target, Modifier? modifier) {
        if(base.use(target, modifier)) {
            // Apply the strength buff
            owner!.AddStatusEffect(new Strength(magicNumber, owner));
            return true;
        }
        return false;
    }
}