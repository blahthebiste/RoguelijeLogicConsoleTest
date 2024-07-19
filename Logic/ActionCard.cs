// All action cards extend this class.
public class ActionCard {
    public ActionType? actionType;
    public String? name;
    public String? description;
    public Modifier? modifier; // Each card can have 1 modifier.
    public Entity? owner; // Some cards are tied to specific characters.
    
    // Default constructor
    public ActionCard() {
        this.actionType = null;
        this.name = null;
        this.description = null;
        this.modifier = null;
        this.owner = null;
    }
    // Constructor with no owner
    public ActionCard(int actionCardID) {
        // TODO: get info from data
    }
    
    // Constructor from data
    public ActionCard(int actionCardID, Entity? owner) {
        // TODO: get info from data
    }
    
    // Returns an exact copy of this card
    public ActionCard makeCopy(){
        ActionCard newCopy = new ActionCard();
        newCopy.actionType = this.actionType;
        newCopy.name = this.name;
        newCopy.description = this.description;
        newCopy.modifier = this.modifier;
        newCopy.owner = this.owner;
        return newCopy;
    }
    
    // Use the action
    public virtual void use(Entity hoveredEntity, Action hoveredAction) {
        if(!canBeUsedOn(hoveredEntity)) return;
        else {
            Action? actionToUse = autoSelectAction(hoveredEntity);
            if(actionToUse == null) {
                // Need to select specific action, there are multiple (or 0) options.
                if(actionCanBeUsed(hoveredAction)) hoveredAction.use(hoveredEntity, null, this.modifier);
                else {
                    return; // Hovered action is not usable, do nothing.
                }
            }
            else {
                actionToUse.use(hoveredEntity, null, this.modifier); // Good, use the action.
            }
        }
    }
    
    // Whether this card can be played on that action.
    public bool actionCanBeUsed(Action hoveredAction) {
        if(!hoveredAction.canUse()) return false;
        if(hoveredAction.actionType == this.actionType) return true;
        else return false;
    }
    
    // Whether this card can be played on that entity.
    public bool canBeUsedOn(Entity hoveredEntity) {
        if(hoveredEntity.exhausted) return false;
        if(!hoveredEntity.playerControlled) return false;
        return true;
    }
    
    // Attempts to smooth out selection. If there is more than 1 valid action, returns null.
    // Otherwise, returns the usable action that matches this card type.
    public Action? autoSelectAction(Entity hoveredEntity) {
        List<Action> matchingActions = new List<Action>();
        foreach(Action action in hoveredEntity.ActionList) {
            if(actionCanBeUsed(action)) {
                matchingActions.Add(action);
            }
        }
        if(matchingActions.Count == 1) return matchingActions[0];
        return null; // More or less than 1 option, return null for no valid auto target
    }
}
