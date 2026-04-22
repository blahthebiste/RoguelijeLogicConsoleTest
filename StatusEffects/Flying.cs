public class Flying : StatusEffect {

    
    public Flying(int amount, Entity owner) {
        this.amount = amount;
        this.name = "Flying";
        this.description = "Attacks ignore Taunt. Dodge the first attack every turn.";
        this.owner = owner;
    }

    // Ignore taunt with all actions -- logic in Action.cs
    
    // Apply Dodging effect each turn
    public override void endOfTurn() {
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
        }
        else
        {
            // Apply the Dodge status effect
            this.owner.AddStatusEffect(new Dodging(1, this.owner));    
        }
    }
}