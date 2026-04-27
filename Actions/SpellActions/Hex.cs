public class Hex : Action {

    public Hex() {
        this.name = "Hex";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 8;
        this.hasLimitedUses = true;
        this.uses = 1;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.SINGLE_ENEMY;
        this.description = "Apply "+magicNumber+" Curse.";
    }

    public override bool useOnTarget(Entity? target, Modifier? modifier) {
        // Not affected by spell power or charged.
        // Apply the curse to the target.
        target!.AddStatusEffect(new Curse(magicNumber, target));
        return true;
    }
}