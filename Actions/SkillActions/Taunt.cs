public class Taunt : Action {

    public Taunt() {
        this.name = "Taunt";
        this.description = "Redirect all enemy attacks to me.";
        this.actionType = ActionType.SKILL;
        this.targetting = TargetCategory.NONE;
    }

    // For now, nothing special.
    public override bool canUse(Entity? target, Modifier? modifier) {
        return base.canUse(target, modifier);
    }

    public override bool use(Entity? target, Modifier? modifier) {
        if(base.use(target, modifier)) {
            // Go through all enemies, and for those that target allies, change the target
            foreach(Enemy enemy in Battlefield.EnemySide) {
                if(enemy != null) enemy.setNextTarget(Battlefield.PlayerSide.FindIndex(a => a.name == owner!.name));
            }
            return true;
        }
        return false;
    }
}