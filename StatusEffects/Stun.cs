public class Stun : StatusEffect {

    public Stun(int amount) {
        this.amount = amount;
        this.name = "Stun";
        this.description = "Cannot take actions other than Rest actions for that many turns.";
    }

    // Decrement every turn
    public override void endOfTurn() {
        owner.recieveStatusEffect(new Stun(-1));
    }
}