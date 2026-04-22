public class Locked : StatusEffect
{

    public Entity lockedTarget;

    public Locked(int amount, Entity owner, Entity target)
    {
        this.amount = amount;
        this.name = "Locked";
        this.description = "Can only target "+target.name+" for that many turns.";
        this.owner = owner;
        this.isDebuff = true;
        lockedTarget = target;
    }

    // Decrement every turn
    public override void endOfTurn() {
        this.Decrease(1);
    }
}