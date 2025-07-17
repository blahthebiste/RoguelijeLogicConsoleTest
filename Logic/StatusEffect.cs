public class StatusEffect : Events {
    public bool isDebuff = false;
    public bool hidden = false;
    public bool canBeZero = false;
    public int amount = 0;

    public string name = "MISSING NAME";
    public string description = "MISSING DESCRIPTION";

    public Entity? owner; // The owner will always be set by the actual status effect constructor.

    // Useful for printing what would be shown to the player
	public override string ToString() {
		string effectString = this.name + "("+this.amount+"): " + this.description;
		return effectString;
	}


    public virtual void Decrease(int amountDecrease) {
        this.amount -= amountDecrease;
        this.onAmountChanged(-amountDecrease);
    }

    
    // Trigger events and remove the effect from the entity
    public virtual void Remove() {
        this.onRemoved();
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
            if(!this.canBeZero && this.amount == 0){
                this.Remove();
            }
        }

    }

    // Run whenever a status effect wears off
    public virtual void onRemoved() {

    }
    

}