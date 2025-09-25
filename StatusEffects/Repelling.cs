public class Repelling : StatusEffect {

    public Repelling(int amount, Entity owner) {
        this.amount = amount;
        this.name = "Repelling";
        this.description = "Stun enemies who attack you, for that many turns.";
        this.owner = owner;
    }

    public override Attack onReceiveAttack(Attack atk) {
        // Apply stun to the attacker
        atk.source.AddStatusEffect(new Stun(1, atk.source));
        return base.onAttack(atk);
    }

    // Decrement every turn
    public override void startOfTurn() {
        this.Decrease(1);
    }
}