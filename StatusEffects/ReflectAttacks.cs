public class ReflectAttacks : StatusEffect {

    public ReflectAttacks(int amount, Entity owner) {
        this.amount = amount;
        this.name = "Reflect Attacks";
        this.description = "Attackers take damage as well, for that many turns.";
        this.owner = owner;
    }

    public override Attack onReceiveAttack(Attack atk) {
        // Make an equivalent attack on the attacker:
        Attack reflectedAtk = new Attack(atk.damage, atk.target, atk.source);
        Battlefield.performAttack(atk);
        return base.onAttack(atk);
    }

    // Decrement every turn
    public override void startOfTurn() {
        this.Decrease(1);
    }
}