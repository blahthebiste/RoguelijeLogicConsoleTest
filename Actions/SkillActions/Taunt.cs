public class Taunt : Action {

    public Taunt() {
        this.name = "Taunt";
        this.description = "Forces enemies to target me instead of my allies.";
        this.actionType = ActionType.SKILL;
        this.targetting = TargetCategory.NONE;
    }

    // For now, nothing special.
    public override bool canUse(Entity? target, Modifier? modifier) {
        return base.canUse(target, modifier);
    }

    public override bool use(Entity? target, Modifier? modifier) {
        if(base.use(target, modifier)) {
            // Apply taunting status effect, which updates enemy targets and the Battlefield.Taunters list accordingly:
            owner!.AddStatusEffect(new Taunting(1, owner!));
            return true;
        }
        return false;
    }
}