public class Strength : StatusEffect {


    public Strength(int amount, Entity owner) {
        this.amount = amount;
        this.name = "Strength";
        this.description = "Increases attack damage and block.";
        this.owner = owner;
    }

    // Strength boosts attack damage
    public override Attack onAttack(Attack atk) {
        atk.damage += this.amount;
        return atk;
    }

    // ...and block gain.
    public override int onGainBlock(int block) {
        return block + this.amount;
    }
}