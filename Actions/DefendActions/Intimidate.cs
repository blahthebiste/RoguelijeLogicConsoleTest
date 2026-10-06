public class Intimidate : Action {

    public Intimidate() {
        this.name = "Intimidate";
        this.actionType = ActionType.DEFEND;
        this.magicNumber = 2;
        this.targetting = TargetCategory.ALL_ENEMIES;
        this.description = "ALL enemies lose "+magicNumber+" Strength for 1 turn.";
    }

    // This will apply 2 intimidated to the target. Will be run on each enemy.
    public override bool useOnTarget(Entity? target, Modifier? modifier)
    {
        // Not affected by spell power or charged.
        // Apply the intimidated effect to the target.
        target!.AddStatusEffect(new Intimidated(magicNumber, target));
        return true;
    }
}