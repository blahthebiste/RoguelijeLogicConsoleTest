public class Swipe : Action {

    public Swipe() {
        this.name = "Swipe";
        this.actionType = ActionType.ATTACK;
        this.damage = 2;
        this.targetting = TargetCategory.SINGLE_ENEMY;
        this.description = "Deal "+damage+" damage.";
    }

    public override bool useOnTarget(Entity? target) {
        if (target == null)
        {
            Console.WriteLine("ERROR: null target for action '" + this.name + "'.");
            return false;
        }
        // Deal damage to the target.
        Attack atk = new Attack(properties.damage, this.owner!, target, properties.hitsAbove, properties.hitsBelow);
        Battlefield.performAttack(atk);
        return true;
    }
    
    
}