public class Campfire : EquipmentItem {


    public Campfire() {
        this.name = "Campfire";
        this.description = "Your Rest action also affects all allies.";
        this.slot = ActionType.REST;
        this.price = 65;
    }

    // Triggers whenever the action that the item is equipped to is used
    public override Action onUseEquippedAction(Action actionBeingUsed) {
        if(this.getOwner() != null) {
            foreach(StatusEffect eff in this.getOwner()!.EffectList) {
                if(eff.isDebuff) {
                    this.getOwner()!.EffectList.Remove(eff);
                }
            }
            Console.WriteLine("Dreamcatcher purged all debuffs!");
        }
        return actionBeingUsed;
    }
}