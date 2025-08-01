public class Campfire : EquipmentItem {


    public Campfire()
    {
        this.name = "Campfire";
        this.description = "Your Rest action causes all allies to rest.";
        this.slot = ActionType.REST;
        this.price = 65;
        this.tier = 1;
    }

    // Triggers whenever the action that the item is equipped to is used
    public override Action onUseEquippedAction(Action actionBeingUsed) {
        if (this.parentAction == null) {
            Console.WriteLine("ERROR: "+this.name+" has null parent action!");
            return actionBeingUsed;
        }
        if (this.parentAction.owner == null) {
            Console.WriteLine("ERROR: "+this.name+"'s parent action has null owner!");
            return actionBeingUsed;
        }
        // Loop through allies, trigger their first rest action
        foreach (Entity ally in Battlefield.PlayerSide.ToList())
        {
            // Make sure not to double up on the ally using the campfire!
            if (this.parentAction.owner == ally)
            {
                continue;
            }
            foreach (Action act in ally.ActionListMinusPassives.ToList())
            {
                // Match the first action that is type rest and targets self:
                if (act.actionType == ActionType.REST && act.targetting == TargetCategory.SELF)
                {
                    act.freeAction = true; // Let the allyes rest without exhausting themself
                    Console.WriteLine(ally.name + " rests at the Campfire!");
                    act.promptUse();
                    act.freeAction = false;
                    break;
                }
            }
        }
        return actionBeingUsed;
    }
}