public class Devour : Action {

    public Devour() {
        this.name = "Devour";
        this.description = "Kill an enemy with 6 HP or less. Recover 6 HP.";
        this.actionType = ActionType.SKILL;
        this.targetting = TargetCategory.SINGLE_ENEMY;
        this.magicNumber = 6;
        this.healing = 6;
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
        // Restore HP
        owner!.ReceiveHealing(healing);
        return true;
    }
}