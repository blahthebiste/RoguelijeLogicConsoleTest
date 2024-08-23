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
    
    public override string ToString() {
        string str = this.name + " ("+this.actionType.ToString()+"): "+this.description;
        if(this.modifier != null) {
            str += " Modifier: "+this.modifier.ToString();
        }
        return str;
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
    public virtual void use(Entity entityToUseAction, Entity? target, Action hoveredAction) {
        if(!canBeUsedBy(entityToUseAction)) return;
        else {
            if(numberMatchingActions(entityToUseAction) == 1) {
                Action? actionToUse = autoSelectAction(entityToUseAction);
                if(actionToUse != null) {
                    if(actionToUse.use(target, this.modifier)) CardManager.discardCard(this);
                }
            }
            else {
                // Need to select specific action, there are multiple (or 0) options.
                if(actionCanBeUsed(hoveredAction)) {
                    if(hoveredAction.use(target, this.modifier)) CardManager.discardCard(this);
                }
                else {
                    return; // Hovered action is not usable, do nothing.
                }
            }
        }
    }
    
    // Use the action; this version always uses autoselection.
    public virtual void use(Entity entityToUseAction, Entity? target) {
        if(!canBeUsedBy(entityToUseAction)) return;
        else {
            if(numberMatchingActions(entityToUseAction) == 1) {
                Action? actionToUse = autoSelectAction(entityToUseAction);
                if(actionToUse != null) {
                     // Good, use the action.
                    if(actionToUse.use(target, this.modifier)) CardManager.discardCard(this);
                }
            }
            else {
                // Invalid number of matching actions.
                Console.WriteLine("ERROR: cannot use this overload of ActionCard.use; entity does not have exactly 1 matching action");
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
    public bool canBeUsedBy(Entity entityToUseAction) {
        if(entityToUseAction.exhausted) return false;
        if(!entityToUseAction.playerControlled) return false;
        return true;
    }
    
    // Attempts to smooth out selection. If there is more than 1 valid action, returns null.
    // Otherwise, returns the usable action that matches this card type.
    public Action? autoSelectAction(Entity entityToUseAction) {
        List<Action> matchingActions = new List<Action>();
        foreach(Action action in entityToUseAction.ActionList) {
            if(actionCanBeUsed(action)) {
                matchingActions.Add(action);
            }
        }
        if(matchingActions.Count == 1) return matchingActions[0];
        return null; // More or less than 1 option, return null for no valid auto target
    }

    // Counts how many usable actions on the specified entity match this action card
    public int numberMatchingActions(Entity entityToUseAction) {
        int matches = 0;
        foreach(Action action in entityToUseAction.ActionList) {
            if(actionCanBeUsed(action)) {
                matches++;
            }
        }
        return matches;
    }
}
