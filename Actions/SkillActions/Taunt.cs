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

    public override void use(Entity? target, Modifier? modifier) {
        // Go through all enemies, and for those that target allies, change the target
        foreach(Entity entity in Battlefield.EnemySide) {
            var enemy = entity as Enemy;
            if(enemy != null) enemy.setNextTarget(Battlefield.PlayerSide.IndexOf(owner));
        }
    }
}