public class Taunt : Action {

    public Taunt() {
        this.name = "Taunt";
        this.description = "Forces enemies to target me instead of my allies.";
        this.actionType = ActionType.SKILL;
        this.targetting = TargetCategory.SELF;
    }

    public override bool useOnTarget(Entity? target, Modifier? modifier) {
        // Apply taunting status effect, which updates enemy targets and the Battlefield.Taunters list accordingly:
        target!.AddStatusEffect(new Taunting(1, target!));
        return true;
    }
}