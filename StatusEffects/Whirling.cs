public class Whirling : StatusEffect {

    public Whirling(int amount, Entity owner) {
        this.amount = amount;
        this.name = "Whirling";
        this.description = "Next attack also hits adjacent targets.";
        this.owner = owner;
    }

    // Also hit adjacent targets (above and below)
    public override Attack onAttack(Attack atk) {
        atk.hitsAbove = true;
        atk.hitsBelow = true;
        this.Decrease(1); // Wears down by 1
        return base.onAttack(atk);
    }

}