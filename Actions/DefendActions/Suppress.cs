public class Suppress : Action {

    public Suppress() {
        this.name = "Suppress";
        this.actionType = ActionType.DEFEND;
        this.magicNumber = 3;
        this.targetting = TargetCategory.SINGLE_ENEMY;
        this.description = "Apply -"+magicNumber+" Strength to an enemy for 1 turn.";
    }

    // This will apply 3 intimidated to the target. Will be run on each enemy.
    public override bool useOnTarget(Entity? target)
    {
        // Not affected by spell power or charged.
        // Apply the intimidated effect to the target.
        target!.AddStatusEffect(new Intimidated(magicNumber, target));
        return true;
    }
}