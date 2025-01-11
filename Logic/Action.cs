
// All actions extend this class.
public class Action {
    
    public string name;
    public string description;
    public ActionType actionType;
    public EquipmentItem? equippedItem;
    public bool hasLimitedUses = false;
	public Entity? owner; // The entity that is using the action

	public TargetCategory targetting = TargetCategory.NONE;
	// Negative 1 means unlimited uses.
	public int uses = -1; // The number of times this action can be used per combat. Usually reserved for spells.
	public int maxUses = -1; // The most uses the action can gain.
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
    public virtual bool canUse(Entity? target, Modifier? modifier) {
		if(hasLimitedUses && uses == 0) {
			Console.WriteLine("No uses left.");
			return false;
		}
		if(owner == null) {
            Console.WriteLine("ERROR: no owner for action!");
            return false;
		}
		if(CurrentRun.requiresTarget(this) && target == null) {
            Console.WriteLine("Invalid target!");
            return false;
        }
		// Iterate through effect list. If stunned, cannot use non-rest actions
		foreach(StatusEffect effect in owner.EffectList) {
			if(effect is Stun && this.actionType != ActionType.REST) {
				Console.WriteLine("Cannot use non-rest actions while stunned!");
				return false;
			}
		}
		return true;
	}
    
    // The meat and potatoes of the action.
    // Each action should override this. Modifier often null.
    public virtual bool use(Entity? target, Modifier? modifier) {
		if(this.canUse(target, modifier)) {
			if(target == null) {
				Console.WriteLine(owner!.name+" used "+this.name+"!");
			}
			else {
				Console.WriteLine(owner!.name+" used "+this.name+" on "+target!.name+"!");
			}
			this.owner!.previousAction = this; // Update previous action.
			if(hasLimitedUses) {
				uses--;
				Console.WriteLine(this.uses+" use(s) remaining.");
			}
			owner!.exhausted = true;
			owner!.onUseAction(this); // Trigger event			
			return true;
		}
		else {

		}
		return false;
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
		if(owner == null) {
            Console.WriteLine("ERROR: no owner for action!");
            return false;
        }
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



	//==========================ITEM OPERATIONS=========================
	

    public void Unequip() {
		if(this.equippedItem == null) return;
        CurrentRun.Inventory.Add(this.equippedItem);
		this.equippedItem = null;
    }

	
	//=============================EVENTS============================
    public virtual void endOfCombat(){
        
    }
}