public class Countering : StatusEffect {

    Strike strikeInstance;

    public Countering(int amount, Entity owner) {
        this.amount = amount;
        this.name = "Countering";
        this.description = "Strike enemies who attack you, for that many turns.";
        this.owner = owner;
        this.strikeInstance = new Strike();
        this.strikeInstance.owner = owner;
    }

    // Counter attacks with Strike
    public override Attack onReceiveAttack(Attack atk) {
        strikeInstance.use(atk.source, null);
        return base.onAttack(atk);
    }

    // Decrement every turn
    public override void endOfTurn() {
        this.Decrease(1);
    }
}