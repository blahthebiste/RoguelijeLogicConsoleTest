public class Hex : Action {

    public Hex() {
        this.name = "Hex";
        this.description = "Apply 10 Curse.";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 10;
        this.hasLimitedUses = true;
        this.uses = 3;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.SINGLE_ENEMY;
    }

    public override bool useOnTarget(Entity? target, Modifier? modifier) {
        // Not affected by spell power or charged.
        // Apply the curse to the target.
        target!.AddStatusEffect(new Curse(magicNumber, target));
        return true;
    }
}