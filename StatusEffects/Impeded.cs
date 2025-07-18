public class Impeded : StatusEffect {

    
    public Impeded(int amount, Entity owner) {
        this.amount = amount;
        this.name = "Impeded";
        this.description = "Cannot dodge for that many turns.";
        this.owner = owner;
    }

    // Decrement every turn
    public override void startOfTurn() {
        this.Decrease(1);
    }
}