public class Backstab : Action {

    public Backstab() {
        this.name = "Backstab";
        this.description = "Kill an enemy with 8 HP or less.";
        this.actionType = ActionType.SKILL;
        this.targetting = TargetCategory.SINGLE_ENEMY;
        this.magicNumber = 8;
    }

    // For now, nothing special.
    public override bool canUse() {
        return base.canUse();
    }

    public override bool CanTarget(Entity target) {
        if(target.currentHP > 8) {
            Console.WriteLine("That target has too much HP!");
            return false;
        }
        return base.CanTarget(target);
    }

    public override bool use(Entity? target, Modifier? modifier) {
        if(target == null) {
            Console.WriteLine("Invalid target!");
            return false;
        }
        if(owner == null) {
            Console.WriteLine("ERROR: no owner for action!");
            return false;
        }
        // Kill them
        target.die();
        return base.use(target, modifier);
    }
}