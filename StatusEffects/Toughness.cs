public class Toughness : StatusEffect {

    
    public Toughness(int amount, Entity owner) {
        this.amount = amount;
        this.name = "Toughness";
        this.description = "Reduces incoming damage. Twice as effective after using a Defend action.";
        this.owner = owner;
    }

    public override Attack onReceiveAttack(Attack atk) {
        if(owner == null) {
            Console.WriteLine("ERROR: no owner for action!");
            return atk;
        }
        atk.damage -= this.amount;
        if(this.owner != null && this.owner.previousAction != null) {
            if(this.owner.previousAction.actionType == ActionType.DEFEND) {
                atk.damage -= this.amount;
            }
        }
        if(atk.damage < 0) atk.damage = 0; // Cannot reduce damage to negative number.
        return atk;
    }
}