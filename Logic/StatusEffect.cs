public class StatusEffect {
    public bool isDebuff = false;
    public bool hidden = false;
    public int amount = 0;

    public string name = "MISSING NAME";
    public string description = "MISSING DESCRIPTION";

    public Entity? owner; // The owner will always be set by the actual status effect constructor.

    public virtual void Decrease(int amountDecrease) {
        this.amount -= amountDecrease;
    }

    // Useful for printing what would be shown to the player
	public override string ToString() {
		string effectString = this.name + "("+this.amount+"): " + this.description;
		return effectString;
	}

//====================EVENTS====================
    public virtual void startOfTurn() {

    }

    public virtual void endOfTurn() {

    }
    
    public virtual Attack onAttack(Attack atk) {
        return atk;
    }
    
    public virtual Attack onReceiveAttack(Attack atk) {
        return atk;
    }

    public virtual int onLoseHP(int HPloss) {
        return HPloss;
    }

    public virtual int onGainBlock(int block) {
        return block;
    }
    

}