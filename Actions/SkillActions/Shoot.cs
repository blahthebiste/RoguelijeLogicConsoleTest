public class Shoot : Action {

    public Shoot()
    {
        this.name = "Shoot";
        this.actionType = ActionType.SKILL;
        this.damage = 7;
        this.hasLimitedUses = true;
        this.uses = 6;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.SINGLE_ENEMY;
        this.description = "Deal "+this.damage+" damage.";
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