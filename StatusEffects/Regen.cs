public class Regen : StatusEffect {


    public Regen(int amount, Entity owner) {
        this.amount = amount;
        this.name = "Regen";
        this.description = "Regain this much HP at the start of each turn.";
        this.owner = owner;
    }

    // Regain HP at the start of every turn
    public override void startOfTurn() {
        if(this.owner != null) {
            Console.WriteLine("Regen restores "+this.amount+" HP!");
            this.owner.changeHP(this.amount);
        }
    }
}