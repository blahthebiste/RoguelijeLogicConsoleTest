public class WearsOff : StatusEffect {

    string effToRemove;
    int delay;

    public WearsOff(int amount, Entity owner, string effToRemove, int delay = 0)
    {
        this.amount = amount;
        this.name = effToRemove + " Wears Off";
        this.description = "At the start of turn, " + this.amount + " " + effToRemove + " will wear off.";
        this.owner = owner;
        this.effToRemove = effToRemove;
        this.delay = delay;
    }


    // Decrement start of turn
    public override void startOfTurn() {
        if (delay > 0)
        {
            delay--;
            return;
        }
        if (owner.HasStatusEffect(effToRemove))
        {
            StatusEffect eff = owner.GetStatusEffect(effToRemove)!;
            if (eff != null) eff.Decrease(this.amount);
        }
        this.Decrease(this.amount);
    }
}