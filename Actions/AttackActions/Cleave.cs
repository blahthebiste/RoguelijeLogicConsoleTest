public class Cleave : Action {

    public Cleave() {
        this.name = "Cleave";
        this.actionType = ActionType.ATTACK;
        this.damage = 4;
        this.targetting = TargetCategory.SINGLE_ENEMY;
        this.hitsAbove = true;
        this.hitsBelow = true;
        this.description = "Deal "+damage+" damage to an enemy and adjacent enemies.";
    }

    public override bool useOnTarget(Entity? target) {
        if (target == null) {
            Console.WriteLine("ERROR: null target for action '"+this+"'.");
            return false;
        }
        // Deal damage to the target.
        Attack atk = new Attack(properties.damage, this.owner!, target, properties.hitsAbove, properties.hitsBelow);
        Battlefield.performAttack(atk);
        return true;
    }
    
    
}