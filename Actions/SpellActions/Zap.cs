public class Zap : Action {

    public Zap() {
        this.name = "Zap";
        this.description = "Deal 9 damage.";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 9;
        this.hasLimitedUses = true;
        this.uses = 3;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.SINGLE_ENEMY;
    }

    public override bool use(Entity? target, Modifier? modifier) {
        if(base.use(target, modifier)) {
            if(owner!.HasStatusEffect("Spell Power")) {
                StatusEffect power = owner.GetStatusEffect("Spell Power")!;
                magicNumber += power.amount;
            }
            if(owner!.HasStatusEffect("Charged")) {
                StatusEffect charge = owner.GetStatusEffect("Charged")!;
                magicNumber += charge.amount;
                owner.EffectList.Remove(charge);
            }
            // Deal damage to the target.
            Attack atk = new Attack(magicNumber, this.owner!, target!);
            //atk = owner.onAttack(atk); // Don't trigger onAttack for the owner, since it is a spell?
            target!.onReceiveAttack(atk);
            return true;
        }
        return false;
    }
}