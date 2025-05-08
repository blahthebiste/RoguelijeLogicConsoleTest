public class HairTrigger : EquipmentItem {


    public HairTrigger() {
        this.name = "Hair Trigger";
        this.description = "Use this action for free upon death.";
        this.slot = ActionType.ANY;
        this.price = 65;
    }


    public override void onDeath(){
        if(this.getOwner() != null && this.parentAction != null) {
            Console.WriteLine("Hair Trigger activates!");
            this.parentAction.promptUse();
        }
    }

}