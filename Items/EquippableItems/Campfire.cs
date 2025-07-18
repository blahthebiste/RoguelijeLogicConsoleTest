public class Campfire : EquipmentItem {


    public Campfire() {
        this.name = "Campfire";
        this.description = "Your Rest action causes all allies to rest.";
        this.slot = ActionType.REST;
        this.price = 65;
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
        foreach (PlayerCharacter hero in Battlefield.PlayerSide.ToList())
        {
            // Make sure not to double up on the hero using the campfire!
            if (this.parentAction.owner == hero)
            {
                continue;
            }
            foreach (Action act in hero.ActionListMinusPassives.ToList())
            {
                // Match the first action that is type rest and targets self:
                if (act.actionType == ActionType.REST && act.targetting == TargetCategory.SELF)
                {
                    act.freeAction = true; // Let the heroes rest without exhausting themself
                    Console.WriteLine(hero.name + " rests at the Campfire!");
                    act.promptUse();
                    act.freeAction = false;
                    break;
                }
            }
        }
        return actionBeingUsed;
    }
}