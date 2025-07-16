public class Invisibility : StatusEffect {

    
    public Invisibility(int amount, Entity owner) {
        this.amount = amount;
        this.name = "Invisibility";
        this.description = "Cannot be targeted by enemies unless last alive for that many turns.";
        this.owner = owner;
    }

    // Decrement every turn
    public override void endOfTurn() {
        this.Decrease(1);
    }
}