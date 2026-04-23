public class Favored : StatusEffect {

    public Favored(int amount, Entity owner) {
        this.amount = amount;
        this.name = "Favored";
        this.description = "Next X attacks cannot be blocked, dodged, or intercepted.";
        this.owner = owner;
    }

    public override Attack onAttack(Attack atk) {
        atk.ignoresBlock = true;
        atk.ignoresDodge = true;
        // Taunt ignoring code is in Action.cs
        this.Decrease(1); // Wears down by 1
        return base.onAttack(atk);
    }

}