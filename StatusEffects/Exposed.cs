public class Exposed : StatusEffect
{


    public Exposed(int amount, Entity owner)
    {
        this.amount = amount;
        this.name = "Exposed";
        this.description = "Take double direct damage for that many turns.";
        this.owner = owner;
    }

    // Amplify attacks
    public override Attack onReceiveAttack(Attack atk)
    {
        atk.damage *= 2;
        return base.onAttack(atk);
    }

    // Decrement every turn
    public override void endOfTurn() {
        this.Decrease(1);
    }
}