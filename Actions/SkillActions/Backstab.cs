public class Backstab : Action {

    public Backstab() {
        this.name = "Backstab";
        this.description = "Kill an enemy with 8 HP or less.";
        this.actionType = ActionType.SKILL;
        this.targetting = TargetCategory.SINGLE_ENEMY;
        this.magicNumber = 8;
    }

    // For now, nothing special.
    public override bool canUse(Entity? target, Modifier? modifier) {
        return base.canUse(target, modifier);
    }

    public override bool CanTarget(Entity target) {
        if(target.currentHP > 8) {
            Console.WriteLine("That target has too much HP!");
            return false;
        }
        return base.CanTarget(target);
    }

    public override bool use(Entity? target, Modifier? modifier) {
        if(base.use(target, modifier)) {
            // Kill them
            target!.die();
            return true;
        }
        return false;
    }
}