public class LeatherBoots : EquipmentItem {


    public LeatherBoots() {
        this.name = "Leather Boots";
        this.description = "Your Skill action draws a card.";
        this.slot = ActionType.SKILL;
        this.price = 65;
        this.tier = 1;
    }

    // Triggers whenever the action that the item is equipped to is used
    public override Action onUseEquippedAction(Action actionBeingUsed) {
        Console.WriteLine("Drawing a card due to Leather boots!");
        CardManager.drawCard(1);
        return base.onUseEquippedAction(actionBeingUsed);
    }

}