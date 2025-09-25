public class RainOfArrows : Action {

    public RainOfArrows() {
        this.name = "Rain Of Arrows";
        this.description = "Deal 3 damage to ALL enemies.";
        this.actionType = ActionType.SKILL;
        this.targetting = TargetCategory.ALL_ENEMIES;
        this.damage = 3;
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