public class Backstab : Action {

    public Backstab() {
        this.name = "Backstab";
        this.actionType = ActionType.SKILL;
        this.targetting = TargetCategory.SINGLE_ENEMY;
        this.magicNumber = 8;
        this.description = "Kill an enemy with "+magicNumber+" HP or less.";
    }

    public override bool CanTarget(Entity target) {
        if(target.currentHP > magicNumber) {
            Console.WriteLine("That target has too much HP!");
            return false;
        }
        return base.CanTarget(target);
    }

    public override bool useOnTarget(Entity? target, Modifier? modifier) {
        // Kill them
        target!.die();
        return true;
    }
}