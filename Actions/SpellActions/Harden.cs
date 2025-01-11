public class Harden : Action {

    public Harden() {
        this.name = "Harden";
        this.description = "Gain 1 Toughness.";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 1;
        this.hasLimitedUses = true;
        this.uses = 3;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.NONE;
    }

    // For now, nothing special.
    public override bool canUse(Entity? target, Modifier? modifier) {
        return base.canUse(target, modifier);
    }

    public override bool use(Entity? target, Modifier? modifier) {
        if(base.use(target, modifier)) {
            // Apply the damage resistance buff
            owner!.AddStatusEffect(new Toughness(magicNumber, owner));
            return true;
        }
        return false;
    }
}