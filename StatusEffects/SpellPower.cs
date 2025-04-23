public class SpellPower : StatusEffect {
    public SpellPower(int amount, Entity owner) {
        this.amount = amount;
        this.name = "Spell Power";
        this.description = "Increases damage/block/healing of spells.";
        this.owner = owner;
    }

    // Actual code is done in a spell-by-spell basis?
}