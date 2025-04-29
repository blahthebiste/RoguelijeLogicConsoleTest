public class Harden : Action {

    public Harden() {
        this.name = "Harden";
        this.description = "Gain 1 Toughness.";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 1;
        this.hasLimitedUses = true;
        this.uses = 3;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.SELF;
    }

    public override bool useOnTarget(Entity? target, Modifier? modifier) {
        // Apply the damage resistance buff
        target!.AddStatusEffect(new Toughness(magicNumber, target));
        return true;
    }
}