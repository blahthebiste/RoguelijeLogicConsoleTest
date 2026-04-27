public class ShadowStrike : Action {

    public ShadowStrike() {
        this.name = "Shadow Strike";
        this.actionType = ActionType.ATTACK;
        this.damage = 4;
        this.magicNumber = 6;
        this.targetting = TargetCategory.SINGLE_ENEMY;
        this.description = "Deal "+damage+" damage. Apply "+magicNumber+" Curse.";
    }

    public override bool useOnTarget(Entity? target, Modifier? modifier) {
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        if (target == null) {
            Console.WriteLine("ERROR: null target for action '"+this+"'.");
            return false;
        }
        // Deal damage to the target.
        Attack atk = new Attack(damage + (modifier == null? 0 : modifier.damageMod), this.owner!, target, this.hitsAbove, this.hitsBelow);
        Battlefield.performAttack(atk);
        // Apply curse
        Console.WriteLine(this.owner.name + " applies " +magicNumber+ " Curse to " + target.name + "!");
        target.AddStatusEffect(new Curse(magicNumber, target)); 
        return true;
    }
}