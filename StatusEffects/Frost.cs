public class Frost : StatusEffect
{


    public Frost(int amount, Entity owner)
    {
        this.amount = amount;
        this.name = "Frost";
        this.description = "Decreases attack damage and block until you rest.";
        this.owner = owner;
    }

    // Frost saps attack damage
    public override Attack onAttack(Attack atk)
    {
        atk.damage -= this.amount;
        return atk;
    }

    // ...and block gain.
    public override int onGainBlock(int block)
    {
        block -= this.amount;
        if (block < 0) block = 0;
        return block;
    }
    
    // Remove frost when resting
    public override Action onUseAction(Action actionBeingUsed) {
        if(owner != null && actionBeingUsed.actionType == ActionType.REST) {
            owner.RemoveStatusEffectByName(this.name);
        }
        return actionBeingUsed;
    }
}