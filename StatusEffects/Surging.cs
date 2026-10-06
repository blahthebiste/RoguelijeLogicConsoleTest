public class Surging : StatusEffect {
    public Surging(int amount, Entity owner) {
        this.amount = amount;
        this.name = "Surging";
        this.description = "Doubles damage/block/healing for the next X spell.";
        this.owner = owner;
    }

    // Actual code is done in a spell-by-spell basis?
}