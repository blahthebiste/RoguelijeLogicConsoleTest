public class TimeTurner : EquipmentItem {


    public TimeTurner()
    {
        this.name = "Time Turner";
        this.description = "Use this action for free at the start of battle.";
        this.slot = ActionType.ANY;
        this.price = 130;
        this.tier = 2;
    }


    public override void startOfCombat(){
        if(this.getOwner() != null && this.parentAction != null) {
            Console.WriteLine("Time Turner activates!");
            this.parentAction.promptUse();
        }
    }

}