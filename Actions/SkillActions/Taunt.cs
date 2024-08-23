public class Taunt : Action {

    public Taunt() {
        this.name = "Taunt";
        this.description = "Redirect all enemy attacks to me.";
        this.actionType = ActionType.SKILL;
        this.targetting = TargetCategory.NONE;
    }

    // For now, nothing special.
    public override bool canUse() {
        return base.canUse();
    }

    public override bool use(Entity? target, Modifier? modifier) {
        // Go through all enemies, and for those that target allies, change the target
        foreach(Enemy enemy in Battlefield.EnemySide) {
            if(enemy != null) enemy.setNextTarget(Battlefield.PlayerSide.FindIndex(a => a.name == owner.name));
        }
        return base.use(target, modifier);
    }
}