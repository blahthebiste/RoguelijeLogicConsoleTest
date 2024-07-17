
// All actions extend this class.
public class Action {
    
    public ActionType actionType;
    public EquipmentItem? equippedItem;
    public bool hasLimitedUses;
	// Negative 1 means unlimited uses.
	public int uses = -1; // The number of times this action can be used per combat. Usually reserved for spells.
    
    // Whether this action can be used right now. Most actions should override this.
    public bool canUse() {
		if(hasLimitedUses) {
			return uses > 0;
		}
		else {
			return true;
		}
	}
    
    // The meat and potatoes of the action.
    // Each action should override this. Modifier often null.
    public void use(Modifier? modifier) {
		if(hasLimitedUses) {
			uses--;
		}
	}
}