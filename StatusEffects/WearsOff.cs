public class WearsOff : StatusEffect {

    string effToRemove;

    public WearsOff(int amount, Entity owner, string effToRemove)
    {
        this.amount = amount;
        this.name = effToRemove+" Wears Off";
        this.description = "At the start of turn, " + this.amount + " " + effToRemove + " will wear off.";
        this.owner = owner;
        this.effToRemove = effToRemove;
    }


    // Decrement start of turn
    public override void startOfTurn() {
        if (owner.HasStatusEffect(effToRemove))
        {
            StatusEffect eff = owner.GetStatusEffect(effToRemove)!;
            if(eff != null) eff.Decrease(this.amount);
        }
        this.Decrease(this.amount);
    }
}