public abstract class StatusEffect : Events {
    public bool isDebuff = false;
    public bool hidden = false;
    public bool canBeZero = false;
    public int amount = 0;

    public string name = "MISSING NAME";
    public string description = "MISSING DESCRIPTION";

    public Entity owner; // The owner will always be set by the actual status effect constructor.

    protected StatusEffect()
    {
        Console.WriteLine("ERROR: creating generic status effect!");
        owner = new Entity();
    }

    // Useful for printing what would be shown to the player
    public override string ToString()
    {
        string effectString = name + "(" + amount + "): " + description;
        return effectString;
    }


    public virtual void Decrease(int amountDecrease) {
        amount -= amountDecrease;
        this.onAmountChanged(-amountDecrease);
    }

    
    // Trigger events and remove the effect from the entity
    public virtual void Remove() {
        onRemoved();
        if(owner != null) owner.EffectList.Remove(this);
    }

    //====================EVENTS====================
    // Some unique events for statuses, not included in the Events class

    // Run whenever a status effect is applied
    public virtual void onApplied()
    {

    }

    // Run whenever a status effect amount is modified
    // Probably the best time to remove effects with amount 0
    public virtual void onAmountChanged(int delta) {
        if(owner != null) {
            if(!canBeZero && amount == 0){
                Remove();
            }
        }

    }

    // Run whenever a status effect wears off
    public virtual void onRemoved() {

    }
    

}