public class Exposed : StatusEffect
{


    public Exposed(int amount, Entity owner)
    {
        this.amount = amount;
        this.name = "Exposed";
        this.description = "Take that much extra direct damage for 1 turn.";
        this.owner = owner;
    }

    // Amplify attacks
    public override Attack onReceiveAttack(Attack atk)
    {
        atk.damage += this.amount;
        return base.onAttack(atk);
    }

    // Clear at start turn
    public override void startOfTurn() {
        this.Remove();
    }
}