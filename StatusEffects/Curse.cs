public class Curse : StatusEffect
{

    public int countdown;
    public Curse(int amount, Entity owner)
    {
        this.amount = amount;
        this.name = "Curse";
        this.owner = owner;
        this.isDebuff = true;
        countdown = 3;
        this.description = "Take this much damage after "+countdown+" turns.";
    }

    // Count down at the start of every turn
    public override void endOfTurn()
    {
        if (this.owner != null)
        {
            countdown--;
            Console.WriteLine("Curse counts down -- " + countdown + " turns left!");
            if (countdown == 0)
            {
                Console.WriteLine(this.owner.name + "'s Curse damages them!");
                // Deal damage to the target.
                Attack atk = new Attack(this.amount, this.owner, this.owner);
                // Don't trigger onAttack for the owner, since it is a status effect
                this.owner.onReceiveAttack(atk);
                // Now clear the Curse.
                this.Decrease(this.amount);
            }
        }
    }

    public override void startOfTurn()
    {
        if (this.owner != null)
        if (countdown == 1) Console.WriteLine(this.owner.name + "'s Curse is about to damage them for " + this.amount + "!");
    }
}