public class RainOfArrows : Action {

    public RainOfArrows() {
        this.name = "Rain Of Arrows";
        this.actionType = ActionType.SKILL;
        this.targetting = TargetCategory.ALL_ENEMIES;
        this.damage = 3;
        this.description = "Deal "+damage+" damage to ALL enemies.";
    }

    public override bool useOnTarget(Entity? target, Modifier? modifier) {
        int power = damage;
        if (target == null) {
            Console.WriteLine("ERROR: null target for action '"+this+"'.");
            return false;
        }
        if (owner!.HasStatusEffect("Deadly Aim"))
        {
            power *= 2;
        }
        // Deal damage to the target.
        Attack atk = new Attack(power + (modifier == null? 0 : modifier.damageMod), this.owner!, target, this.hitsAbove, this.hitsBelow);
        Battlefield.performAttack(atk);
        return true;
    }

    // Decrement buffs after dealing all damage
    public override bool useOnce(Modifier? modifier){
        if (owner!.HasStatusEffect("Deadly Aim"))
        {
            owner!.GetStatusEffect("Deadly Aim")!.Decrease(1);
        }
        return true;
    }
}