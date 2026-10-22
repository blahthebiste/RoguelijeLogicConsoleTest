public class Whack : Action {

    public Whack() {
        this.name = "Whack";
        this.actionType = ActionType.ATTACK;
        this.damage = 3;
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