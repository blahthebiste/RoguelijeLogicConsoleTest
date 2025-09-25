public class Chill : Action {

    public Chill() {
        this.name = "Chill";
        this.description = "Apply 1 Frost to ALL enemies.";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 1;
        this.hasLimitedUses = true;
        this.uses = 3;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.ALL_ENEMIES;
    }

    // This will apply 1 frost to the target. Will be run on each enemy.
    public override bool useOnTarget(Entity? target, Modifier? modifier)
    {
        // Not affected by spell power or charged.
        // Apply the frost to the target.
        target!.AddStatusEffect(new Frost(magicNumber, target));
        return true;
    }
}