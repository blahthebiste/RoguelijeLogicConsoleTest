public class TwinSlash : Action {

    public TwinSlash() {
        this.name = "Twin Slash";
        this.actionType = ActionType.ATTACK;
        this.damage = 4;
        this.magicNumber = 2; // Hit twice
        this.targetting = TargetCategory.SINGLE_ENEMY;
        this.description = "Deal "+damage+" damage twice.";
    }

    public override bool useOnTarget(Entity? target, Modifier? modifier) {
        int power = damage;
        if (target == null)
        {
            Console.WriteLine("ERROR: null target for action '" + this.name + "'.");
            return false;
        }
        if (owner!.HasStatusEffect("Deadly Aim"))
        {
            power *= 2;
        }
        // Deal damage to the target twice.
        for (int i = 0; i < magicNumber; i++)
        {
            Attack atk = new Attack(power + (modifier == null? 0 : modifier.damageMod), this.owner!, target, this.hitsAbove, this.hitsBelow);
            Battlefield.performAttack(atk);
        }
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