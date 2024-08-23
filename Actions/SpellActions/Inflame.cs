public class Inflame : Action {

    public Inflame() {
        this.name = "Inflame";
        this.description = "Gain 2 Strength.";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 2;
        this.hasLimitedUses = true;
        this.uses = 1;
        this.targetting = TargetCategory.NONE;
    }

    // For now, nothing special.
    public override bool canUse() {
        return base.canUse();
    }

    public override bool use(Entity? target, Modifier? modifier) {
        // Apply the Whirl status effect
        owner.recieveStatusEffect(new Strength(magicNumber, owner));
        return base.use(target, modifier);
    }
}