public class Leading : StatusEffect {


    public Leading(int amount, Entity owner) {
        this.amount = amount;
        this.name = "Leading";
        this.description = "Draw X additional cards at the start of each turn.";
        this.owner = owner;
        this.isDebuff = true;
    }

    // Draw cards at the start of every turn
    public override void startOfTurn() {
        if(this.owner != null){
            Console.WriteLine("Drawing "+this.amount+" additional cards due to "+this.name+"!");
            CardManager.drawCard(amount);
        }
    }
}