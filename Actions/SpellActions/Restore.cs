public class Restore : Action {

    public Restore() {
        this.name = "Restore";
        this.description = "Restore 6 HP. 6 uses.";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 6;
        this.hasLimitedUses = true;
        this.uses = 6;
        this.targetting = TargetCategory.SINGLE_ANY;
    }

    // For now, nothing special.
    public override bool canUse() {
        return base.canUse();
    }

    public override void use(Entity? target, Modifier? modifier) {
        // Apply the Whirl status effect
        target.recieveHealing(magicNumber);
        base.use(target, modifier);
    }
}