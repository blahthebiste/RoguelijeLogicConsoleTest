public class Piety : StatusEffect {

    
    public Piety(int amount, Entity owner) {
        this.amount = amount;
        this.name = "Piety";
        this.description = "Enemies with less HP than this will leave peacefully.";
        this.owner = owner;
    }

}