public class KillingWord : Action {

    public KillingWord() {
        this.name = "Killing Word";
        this.description = "Kill an enemy with 20 HP or less.";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 20;
        this.hasLimitedUses = true;
        this.uses = 1;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.SINGLE_ENEMY;
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