public class Bash : Action {

    public Bash() {
        this.name = "Bash";
        this.description = "Deal 4 damage.";
        this.actionType = ActionType.ATTACK;
        this.damage = 4;
        this.targetting = TargetCategory.SINGLE_ENEMY;
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