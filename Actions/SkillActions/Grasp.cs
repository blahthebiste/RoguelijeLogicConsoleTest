public class Grasp : Action {

    public Grasp() {
        this.name = "Grasp";
        this.actionType = ActionType.SKILL;
        this.targetting = TargetCategory.SINGLE_ENEMY;
        this.magicNumber = 1;
        this.description = "Stun an enemy whose last action was not an attack.";
    }

    public override bool CanTarget(Entity target) {
        if(target.previousAction != null && target.previousAction.actionType == ActionType.ATTACK) {
            Console.WriteLine("The target just attacked! Grasp could not stun them.");
            return false;
        }
        return base.CanTarget(target);
    }

    public override bool useOnTarget(Entity? target, Modifier? modifier) {
        // Apply the Stun status effect
        target!.AddStatusEffect(new Stun(magicNumber, target));
        return true;
    }
}