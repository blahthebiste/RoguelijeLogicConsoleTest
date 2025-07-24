public class TwinSlash : Action {

    public TwinSlash() {
        this.name = "Twin Slash";
        this.description = "Deal 4 damage twice.";
        this.actionType = ActionType.ATTACK;
        this.damage = 4;
        this.magicNumber = 2; // Hit twice
        this.targetting = TargetCategory.SINGLE_ENEMY;
    }

    public override bool useOnTarget(Entity? target, Modifier? modifier) {
        if (target == null)
        {
            Console.WriteLine("ERROR: null target for action '" + this.name + "'.");
            return false;
        }
        // Deal damage to the target twice.
        for (int i = 0; i < magicNumber; i++)
        {
            Attack atk = new Attack(damage, this.owner!, target, this.hitsAbove, this.hitsBelow);
            Battlefield.performAttack(atk);
        }
        return true;
    }
}