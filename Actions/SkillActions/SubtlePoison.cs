public class SubtlePoison : Action {

    public SubtlePoison() {
        this.name = "Subtle Poison";
        this.actionType = ActionType.SKILL;
        this.targetting = TargetCategory.SINGLE_ENEMY;
        this.magicNumber = 2;
        this.description = "Apply "+magicNumber+" Poison.";
    }


    public override bool useOnTarget(Entity? target, Modifier? modifier) {
        // Apply the Poison status effect
        target!.AddStatusEffect(new Poison(magicNumber, target));
        return true;
    }
}