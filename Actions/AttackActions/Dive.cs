public class Dive : Action {

    public Dive() {
        this.name = "Dive";
        this.actionType = ActionType.ATTACK;
        this.damage = 5;
        this.targetting = TargetCategory.ALL_ENEMIES;
        this.description = "Deal "+damage+" damage to ALL enemies. Stop flying.";
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

    public override bool useOnce(Modifier? modifier){
        if (owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        return owner.RemoveStatusEffectByName("Flying");
    }
}