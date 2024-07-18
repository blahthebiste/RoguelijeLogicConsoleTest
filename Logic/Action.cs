
// All actions extend this class.
public class Action {
    
    public string name;
    public string description;
    public ActionType actionType;
    public EquipmentItem? equippedItem;
    public bool hasLimitedUses = false;
	// Negative 1 means unlimited uses.
	public int uses = -1; // The number of times this action can be used per combat. Usually reserved for spells.
	public int damage = -1; // Some actions deal damage. -1 means they do not.
	public int block = -1; // Some actions gain block. -1 means they do not.
	public int healing = -1; // Some actions restore HP. -1 means they do not.
	public int magicNumber = -1; // Can be used for a variety of things. -1 means unused.
	public int magicNumber2 = -1; // Can be used for a variety of things. -1 means unused.
	public int magicNumber3 = -1; // Can be used for a variety of things. -1 means unused.


	public Action() {
		name = "MISSING NAME";
		description = "MISSING DESCRIPTION";
	}
    
    // Whether this action can be used right now. Most actions should override this.
    public virtual bool canUse() {
		if(hasLimitedUses) {
			return uses > 0;
		}
		else {
			return true;
		}
	}
    
    // The meat and potatoes of the action.
    // Each action should override this. Modifier often null.
    public virtual void use(Entity source, Entity? target, Modifier? modifier) {
		if(hasLimitedUses) {
			uses--;
		}
	}

	// Useful for printing what would be shown to the player
	public override string ToString() {
		string actionString = "("+this.actionType+") "+this.name + ": " + this.description;
		if(this.hasLimitedUses) {
			actionString += " " + this.uses + " use";
			if(this.uses > 1) {
				actionString += "s."; 
			}
			else {
				actionString += ".";
			}
		}
		return actionString;
	} 
}