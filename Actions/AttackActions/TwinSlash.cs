public class TwinSlash : Action {

    public TwinSlash() {
        this.name = "Twin Slash";
        this.actionType = ActionType.ATTACK;
        this.damage = 4;
        this.magicNumber = 2; // Hit twice
        this.targetting = TargetCategory.SINGLE_ENEMY;
        this.description = "Deal "+damage+" damage twice.";
    }

    public override bool useOnTarget(Entity? target) {
        if (target == null)
        {
            Console.WriteLine("ERROR: null target for action '" + this.name + "'.");
            return false;
        }
        // Deal damage to the target twice.
        for (int i = 0; i < magicNumber; i++)
        {
            Attack atk = new Attack(properties.damage, this.owner!, target, properties.hitsAbove, properties.hitsBelow);
            Battlefield.performAttack(atk);
        }
        return true;
    }

    
}