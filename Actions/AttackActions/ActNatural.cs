public class ActNatural : Action {

    public ActNatural() {
        this.name = "Act Natural";
        this.actionType = ActionType.ATTACK;
        this.damage = 0;
        this.targetting = TargetCategory.SINGLE_ENEMY;
        this.description = "Deal "+damage+" damage. It seems like they are not actually trying to hurt you...";
    }

    public override bool useOnTarget(Entity? target) {
        if (target == null)
        {
            Console.WriteLine("ERROR: null target for action '" + this.name + "'.");
            return false;
        }
        // Deal damage to the target.
        Attack atk = new Attack(properties, this.owner!, target);
        Battlefield.performAttack(atk);
        return true;
    }

    
}