public class Bleed : StatusEffect {


    public Bleed(int amount, Entity owner) {
        this.amount = amount;
        this.name = "Bleed";
        this.description = "Lose this much HP at the end of each turn until you rest.";
        this.owner = owner;
        this.isDebuff = true;
    }

    // Remove HP at the end of every turn
    public override void endOfTurn() {
        if(this.owner != null){
            Console.WriteLine("Bleed saps "+this.amount+" HP from "+this.owner.name+"!");
            this.owner.changeHP(-this.amount);
        }
    }

    // Remove Bleed when resting
    public override Action onUseAction(Action actionBeingUsed) {
        if(owner != null && actionBeingUsed.actionType == ActionType.REST) {
            owner.RemoveStatusEffectByName(this.name);
        }
        return actionBeingUsed;
    }
}