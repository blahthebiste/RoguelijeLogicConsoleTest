public class Stun : StatusEffect {


    public Stun(int amount, Entity owner) {
        this.amount = amount;
        this.name = "Stun";
        this.description = "Cannot take actions other than Rest actions for that many turns.";
        this.owner = owner;
    }

    // Decrement every turn
    public override void endOfTurn() {
        this.Decrease(1);
    }
}