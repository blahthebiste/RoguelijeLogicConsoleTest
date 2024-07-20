public class Toughness : StatusEffect {

    public Toughness(int amount) {
        this.amount = amount;
        this.name = "Toughness";
        this.description = "Reduces incoming damage. Twice as effective immediately after using a Rest action.";
    }
}