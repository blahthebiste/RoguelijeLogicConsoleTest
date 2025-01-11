public class Poison : StatusEffect {


    public Poison(int amount, Entity owner) {
        this.amount = amount;
        this.name = "Poison";
        this.description = "Lose this much HP at the start of each turn.";
        this.owner = owner;
    }

    // Remove HP at the start of every turn
    public override void startOfTurn() {
        if(this.owner != null) this.owner.changeHP(-this.amount);
    }

    // Remove poison when resting
    public override Action onUseAction(Action actionBeingUsed) {
        if(actionBeingUsed.actionType == ActionType.REST) {
            this.amount = 0; 
        }
        return actionBeingUsed;
    }
}