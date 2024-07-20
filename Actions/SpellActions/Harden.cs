public class Harden : Action {

    public Harden() {
        this.name = "Harden";
        this.description = "Gain 1 Toughness.";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 1;
        this.hasLimitedUses = true;
        this.uses = 3;
        this.targetting = TargetCategory.NONE;
    }

    // For now, nothing special.
    public override bool canUse() {
        return base.canUse();
    }

    public override void use(Entity? target, Modifier? modifier) {
        // Apply the Whirl status effect
        source.recieveStatusEffect(new Toughness(magicNumber));
        base.use(target, modifier);
    }
}