public class Strength : StatusEffect {

    public Strength(int amount) {
        this.amount = amount;
        this.name = "Strength";
        this.description = "Increases attack damage and block.";
    }
    public Strength(int amount, Entity owner) {
        this.amount = amount;
        this.name = "Strength";
        this.description = "Increases attack damage and block.";
        this.owner = owner;
    }
}