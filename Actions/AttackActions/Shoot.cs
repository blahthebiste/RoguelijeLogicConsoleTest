public class Shoot : Action {

    public Shoot()
    {
        this.name = "Shoot";
        this.actionType = ActionType.ATTACK;
        this.damage = 5;
        this.targetting = TargetCategory.SINGLE_ENEMY;
        this.ignoresTaunt = true;
        this.description = "Deal "+this.damage+" damage. Ignores Taunt.";
    }


    public override bool useOnTarget(Entity? target, Modifier? modifier) {
        if (target == null) {
            Console.WriteLine("ERROR: null target for action '"+this+"'.");
            return false;
        }
        // Deal damage to the target.
        Attack atk = new Attack(damage + (modifier == null? 0 : modifier.damageMod), this.owner!, target, this.hitsAbove, this.hitsBelow);
        Battlefield.performAttack(atk);
        return true;
    }
}