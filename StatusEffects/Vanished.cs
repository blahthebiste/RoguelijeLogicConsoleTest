public class Vanished : StatusEffect
{


    public Vanished(int amount, Entity owner)
    {
        this.amount = amount;
        this.name = "Vanished";
        this.description = "Dodging all attacks for that many turns.";
        this.owner = owner;
    }

    // Dodge attacks
    public override Attack onReceiveAttack(Attack atk)
    {
        if(owner == null)
        {
            return base.onAttack(atk);
        }
        if(owner.HasStatusEffect("Impeded")) {
            Console.WriteLine(owner.name+" failed to dodge the attack, because they were Impeded!");
            return base.onAttack(atk);
        }
        if(atk.ignoresDodge)
        {
            Console.WriteLine(owner.name+" failed to dodge the attack, because the attack ignores dodge!");
            return base.onAttack(atk);
        }
        atk.damage = 0; // TODO: replace with proper dodge mechanic?
        return base.onAttack(atk);
    }

    // Decrement every turn
    public override void startOfTurn() {
        this.Decrease(1);
    }
}