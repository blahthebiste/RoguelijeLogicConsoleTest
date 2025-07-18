public class SubtlePoison : Action {

    public SubtlePoison() {
        this.name = "Subtle Poison";
        this.description = "Apply 2 Poison.";
        this.actionType = ActionType.SKILL;
        this.targetting = TargetCategory.SINGLE_ENEMY;
        this.magicNumber = 2;
    }


    public override bool useOnTarget(Entity? target, Modifier? modifier) {
        // Apply the Poison status effect
        target!.AddStatusEffect(new Poison(magicNumber, target));
        return true;
    }
}