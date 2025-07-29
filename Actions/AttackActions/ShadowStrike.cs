public class ShadowStrike : Action {

    public ShadowStrike() {
        this.name = "Shadow Strike";
        this.description = "Deal 4 damage. Apply 6 Curse.";
        this.actionType = ActionType.ATTACK;
        this.damage = 4;
        this.magicNumber = 6;
        this.targetting = TargetCategory.SINGLE_ENEMY;
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
        Attack atk = new Attack(damage, this.owner!, target, this.hitsAbove, this.hitsBelow);
        Battlefield.performAttack(atk);
        // Apply curse
        Console.WriteLine(this.owner.name + " applies " +magicNumber+ " Curse to " + target.name + "!");
        target.AddStatusEffect(new Curse(magicNumber, target)); 
        return true;
    }
}