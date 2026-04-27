public class DeadlyBrew : Action {

    public DeadlyBrew() {
        this.name = "Deadly Brew";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 1;
        this.hasLimitedUses = true;
        this.uses = 1;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.ALL_ENEMIES;
        this.description = "Apply "+magicNumber+" Poison to ALL enemies.";
    }

    // This will apply 2 poison to the target. Will be run on each enemy.
    public override bool useOnTarget(Entity? target, Modifier? modifier)
    {
        // Not affected by spell power or charged.
        // Apply the poison to the target.
        target!.AddStatusEffect(new Poison(magicNumber, target));
        return true;
    }
}