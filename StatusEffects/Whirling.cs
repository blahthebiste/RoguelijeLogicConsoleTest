public class Whirling : StatusEffect {

    public Whirling(int amount) {
        this.amount = amount;
        this.name = "Whirling";
        this.description = "Next attack also hits adjacent targets.";
    }
}