public class WhiteFlag : EquipmentItem {


    public WhiteFlag() {
        this.name = "White Flag";
        this.description = "Your Rest action also gains 1 Piety.";
        this.slot = ActionType.REST;
        this.price = 130;
    }

    // Triggers whenever the action that the item is equipped to is used
    public override Action onUseEquippedAction(Action actionBeingUsed) {
        if(this.getOwner() != null) {
            // Apply the piety buff
            this.getOwner()!.AddStatusEffect(new Piety(1, this.getOwner()!));
            Console.WriteLine("White Flag gained 1 Piety!");
        }
        return actionBeingUsed;
    }
}