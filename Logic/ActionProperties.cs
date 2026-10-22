// Struct to hold all data for an action which may be modified by modifiers.
// Modifiers modify this instead of the action's original values.
// This allows modifier effects to be applied each time an action is used, without
// permanently altering the base action.
public struct ActionProperties
{
    public ActionType actionType;
    public bool hasLimitedUses = false;
    public bool ignoresTaunt = false;
    public bool ignoresDodge = false;
    public bool hitsAbove = false;
    public bool hitsBelow = false;
    public bool freeAction = false; // The action does not exhaust the entity using it

    public TargetCategory targetting = TargetCategory.NONE;

    public int damage = -1; // Some actions deal damage. -1 means they do not.
    public int block = -1; // Some actions gain block. -1 means they do not.
    public int healing = -1; // Some actions restore HP. -1 means they do not.

    // This constructor copies all the base values from the supplied action:
    public ActionProperties(Action action)
    {
        this.actionType = action.actionType;
        this.hasLimitedUses = action.hasLimitedUses;
        this.ignoresTaunt = action.ignoresTaunt;
        this.ignoresDodge = action.ignoresDodge;
        this.hitsAbove = action.hitsAbove;
        this.hitsBelow = action.hitsBelow;
        this.freeAction = action.freeAction;
        this.targetting = action.targetting; // Yes, even target category can be changed
        this.damage = action.damage;
        this.damage = action.damage;
        this.block = action.block;
        this.healing = action.healing;

    }
}