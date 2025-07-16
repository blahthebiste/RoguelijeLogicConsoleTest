public class PermaBlock : StatusEffect {


    public PermaBlock(int amount, Entity owner) {
        this.amount = amount;
        this.name = "Perma-Block";
        this.description = "Keep that much unused block each turn.";
        this.owner = owner;
    }

    // Actual code is in Battlefield

}