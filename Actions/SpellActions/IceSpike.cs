public class IceSpike : Action {

    public IceSpike() {
        this.name = "Ice Spike";
        this.actionType = ActionType.SPELL;
        this.damage = 6;
        this.hasLimitedUses = true;
        this.uses = 3;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.SINGLE_ENEMY;
        this.description = "Deal "+damage+" damage, plus any Frost on the target.";
    }

    public override bool useOnTarget(Entity? target) {
        if (target == null) {
            Console.WriteLine("ERROR: null target for action '"+this+"'.");
            return false;
        }
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        int bonusDamage = 0;
        // Ice Spike gets boosted by frost on the target:
        if(target.HasStatusEffect("Frost")) {
            StatusEffect frost = target.GetStatusEffect("Frost")!;
            bonusDamage = frost.amount;
        }
        // Deal damage to the target.
        properties.damage += bonusDamage;
        Attack atk = new Attack(properties, this.owner!, target);
        target.onReceiveAttack(atk);
        return true;
    }
}