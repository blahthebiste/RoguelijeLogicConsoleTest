
// All actions extend this class.
public class Action {
    
    public string name;
    public string description;
    public ActionType actionType;
    public EquipmentItem? equippedItem;
    public bool hasLimitedUses = false;
	public Entity owner; // The entity that is using the action

	public TargetCategory targetting = TargetCategory.NONE;
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
		owner = new Entity();
	}
    
    // Whether this action can be used right now. Most actions should override this.
    public virtual bool canUse() {
		if(hasLimitedUses && uses == 0) {
			return false;
		}
		// Iterate through effect list. If stunned, cannot use non-rest actions
		foreach(StatusEffect effect in owner.EffectList) {
			if(effect is Stun && this.actionType != ActionType.REST) {
				return false;
			}
		}
		return true;
	}
    
    // The meat and potatoes of the action.
    // Each action should override this. Modifier often null.
    public virtual void use(Entity? target, Modifier? modifier) {
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

	// Can this action target that entity?
	// Should be overridden, but the base version has useful basic targeting guidelines.
	public virtual bool CanTarget(Entity target) {
		switch(this.targetting)
		{
			case TargetCategory.NONE:
			case TargetCategory.ALL_ENEMIES:
			case TargetCategory.ALL_ALLIES:
			case TargetCategory.EVERYONE:
				return false;
			case TargetCategory.SELF:
				return target == owner;
			case TargetCategory.SINGLE_ENEMY:
				// If they are on different teams, they can target with this action
				return (owner.playerControlled != target.playerControlled);
			case TargetCategory.SINGLE_ALLY:
				// If they are on the same team, they can target with this action
				return (owner.playerControlled == target.playerControlled);
			case TargetCategory.SINGLE_ANY:
				// Always valid
				return true;
			default:
				Console.WriteLine("Action had no targetting set. This should never happen.");
				return false;

		}
	}

	public bool hasTarget() {
		if(this.targetting == TargetCategory.SINGLE_ALLY || this.targetting == TargetCategory.SINGLE_ENEMY || this.targetting == TargetCategory.SINGLE_ANY) {
			return true;
		}
		return false;
	}
}