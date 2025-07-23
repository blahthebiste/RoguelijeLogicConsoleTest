// All action cards extend this class.
public class ActionCard {
    public ActionType? actionType;
    public String name = "MISSING NAME";
    public String description = "MISSING DESCRIPTION";
    public Modifier? modifier; // Each card can have 1 modifier.
    public Entity? owner; // Some cards are tied to specific characters.
    
    // Default constructor
    public ActionCard() {
        actionType = null;
        modifier = null;
        owner = null;
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
        string str = name + " ("+actionType.ToString()+"): "+description;
        if(modifier != null) {
            str += " Modifier: "+modifier.ToString();
        }
        return str;
    }

    // Returns an exact copy of this card
    public ActionCard makeCopy(){

        ActionCard? newCopy = DataRegistry.CardData.getCardByName(name);
        if (newCopy == null)
        {
            Console.WriteLine("ERROR: could not make copy of card; DataRegistry did not find card by name '"+name+"'!");
            return new BasicAttack();
        }
        newCopy.modifier = modifier;
        newCopy.owner = owner;
        return newCopy;
    }
    
    // Use the action
    public virtual void use(Entity entityToUseAction, Entity? target, Action hoveredAction) {
        if(!canBeUsedBy(entityToUseAction)) return;
        else {
            if(numberMatchingActions(entityToUseAction) == 1) {
                Action? actionToUse = autoSelectAction(entityToUseAction);
                if(actionToUse != null) {
                    if(actionToUse.use(target, modifier)) CardManager.discardCard(this);
                }
            }
            else {
                // Need to select specific action, there are multiple (or 0) options.
                if(actionCanBeUsed(hoveredAction)) {
                    if(hoveredAction.use(target, modifier)) CardManager.discardCard(this);
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
                    if(actionToUse.use(target, modifier)) CardManager.discardCard(this);
                }
            }
            else {
                // Invalid number of matching actions.
                Console.WriteLine("ERROR: cannot use this overload of ActionCard.use; entity does not have exactly 1 matching action");
            }
        }
    }
    
    // Whether this card can be played on that action.
    public virtual bool actionCanBeUsed(Action hoveredAction) {
        return hoveredAction.actionType == actionType || hoveredAction.actionType == ActionType.ANY;
    }
    
    // Whether this card can be played on that entity.
    public virtual bool canBeUsedBy(Entity entityToUseAction) {
        if(entityToUseAction.exhausted) {
            Console.WriteLine("Unit is exhausted and cannot act.");
            return false;
        }
        if(!entityToUseAction.playerControlled) {
            Console.WriteLine("Unit is not under player control.");
            return false;
        }
        return true;
    }
    
    // Attempts to smooth out selection. If there is more than 1 valid action, returns null.
    // Otherwise, returns the usable action that matches this card type.
    public virtual Action? autoSelectAction(Entity entityToUseAction) {
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
    public virtual int numberMatchingActions(Entity entityToUseAction) {
        Console.WriteLine("numberMatchingActions not overrided somehow?");
        int matches = 0;
        foreach(Action action in entityToUseAction.ActionList) {
            if(actionCanBeUsed(action)) {
                matches++;
            }
        }
        return matches;
    }
}
