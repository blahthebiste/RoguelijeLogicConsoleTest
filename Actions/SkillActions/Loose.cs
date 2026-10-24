public class Loose : Action {

    public Loose()
    {
        this.name = "Loose";
        this.actionType = ActionType.SKILL;
        this.damage = 7;
        this.targetting = TargetCategory.OPPOSING; // Targetting is handled automatically
        this.description = "Deal "+damage+" damage to the opposing enemy.";
    }

    public override bool useOnTarget(Entity? target) {
        if (target == null) {
            Console.WriteLine("ERROR: null target for action '"+this+"'.");
            return false;
        }
        // Deal damage to the target.
        Attack atk = new Attack(properties, this.owner!, target);
        // Not considered a direct attack, so do not trigger onAttack?
        target!.onReceiveAttack(atk);
        return true;
    }
    
    
}