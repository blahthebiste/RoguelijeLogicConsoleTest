public class Determined : StatusEffect
{


    public Determined(int amount, Entity owner)
    {
        this.amount = amount;
        this.name = "Determined";
        this.description = "HP cannot fall below 1 until next turn.";
        this.owner = owner;
    }

    // Prevent death
    public override int onHPChange(int HPdelta)
    {
        if(-HPdelta > owner.currentHP)
        {
            Console.WriteLine(owner.name+" was too angry to die!");
            HPdelta = -(owner.currentHP - 1);
        }
        return HPdelta;
    }

    // Clear at start turn
    public override void startOfTurn() {
        this.Remove();
    }
}