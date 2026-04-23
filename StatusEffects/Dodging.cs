public class Dodging : StatusEffect {


    public Dodging(int amount, Entity owner) {
        this.amount = amount;
        this.name = "Dodging";
        this.description = "Dodge the next X attacks.";
        this.owner = owner;
    }

    // Dodge attacks
    public override Attack onReceiveAttack(Attack atk) {
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
        this.Decrease(1); // Wears down by 1
        return base.onAttack(atk);
    }

}