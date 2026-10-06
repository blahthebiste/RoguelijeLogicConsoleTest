public class FrostBreath : Action {

    public FrostBreath() {
        this.name = "Frost Breath";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 6;
        this.targetting = TargetCategory.SINGLE_ENEMY;
        this.hitsAbove = true;
        this.hitsBelow = true;
        this.hasLimitedUses = true;
        this.uses = 2;
        this.maxUses = this.uses;
        this.description = "Deal "+magicNumber+" damage to an enemy and adjacent enemies. Apply Frost equal to unblocked damage.";
    }

    public override bool useOnTarget(Entity? target, Modifier? modifier) {
        int power = magicNumber;
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        if (target == null) {
            Console.WriteLine("ERROR: null target for action '"+this+"'.");
            return false;
        }
        if(owner!.HasStatusEffect("Spell Power")) {
            StatusEffect spell_power = owner.GetStatusEffect("Spell Power")!;
            power += spell_power.amount;
        }
        if(owner!.HasStatusEffect("Charged")) {
            StatusEffect charge = owner.GetStatusEffect("Charged")!;
            power += charge.amount;
            owner.EffectList.Remove(charge);
        }
        if (owner!.HasStatusEffect("Surging"))
        {
            StatusEffect charge = owner.GetStatusEffect("Surging")!;
            power *= 2;
            charge.Decrease(1);
        }
        // Deal damage to the target.
        Attack atk = new Attack(power, this.owner!, target, this.hitsAbove, this.hitsBelow);
        int finalDamage = target!.onReceiveAttack(atk).damage;
        // Apply frost based on final damage:
        if (finalDamage > 0)
        {
            Console.WriteLine(this.owner.name + "'s Frost Breath chills " + target.name + " to the bone -- Frost(" + finalDamage + ")!");
            target.AddStatusEffect(new Frost(finalDamage, target));
        }    
        return true;
    }
}