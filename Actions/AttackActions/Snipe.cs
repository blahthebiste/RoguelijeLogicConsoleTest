public class Snipe : Action {

    public Snipe()
    {
        this.name = "Snipe";
        this.actionType = ActionType.ATTACK;
        this.damage = 7;
        this.targetting = TargetCategory.SINGLE_ENEMY;
        this.ignoresTaunt = true;
        this.description = "Deal "+this.damage+" damage. Ignores Taunt.";
    }


    public override bool useOnTarget(Entity? target, Modifier? modifier) {
        int power = damage;
        if (target == null) {
            Console.WriteLine("ERROR: null target for action '"+this+"'.");
            return false;
        }
        if (this.owner == null)
        {
            Console.WriteLine("ERROR: null owner for action '" + this.name + "'.");
            return false;
        }
        if (owner!.HasStatusEffect("Deadly Aim"))
        {
            power *= 2;
        }
        // Deal damage to the target.
        Attack atk = new Attack(power + (modifier == null? 0 : modifier.damageMod), this.owner, target, this.hitsAbove, this.hitsBelow);
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