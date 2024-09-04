public class Dodging : StatusEffect {


    public Dodging(int amount, Entity owner) {
        this.amount = amount;
        this.name = "Dodging";
        this.description = "Dodge the next attack.";
        this.owner = owner;
    }

    // Dodge attacks
    public override Attack onReceiveAttack(Attack atk) {
        atk.damage = 0; // TODO: replace with proper dodge mechanic
        this.Decrease(1); // Wears down by 1
        return base.onAttack(atk);
    }

}