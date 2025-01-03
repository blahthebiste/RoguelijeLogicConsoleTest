public class Charged : StatusEffect {
    public Charged(int amount, Entity owner) {
        this.amount = amount;
        this.name = "Charged";
        this.description = "Increases damage/block/healing for the next spell.";
        this.owner = owner;
    }

    // Actual code is done in a spell-by-spell basis?
}