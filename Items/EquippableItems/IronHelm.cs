public class IronHelm : EquipmentItem {


    public IronHelm() {
        this.name = "Iron Helm";
        this.description = "+1 Toughness.";
        this.slot = ActionType.ANY;
        this.price = 65;
    }


    // Each combat, gain +1 Toughness (it will be removed end of combat by the Battlefield logic)
    public override void startOfCombat(){
        if(this.getOwner() == null) {
            Console.WriteLine("ERROR: "+this.name+" has null owner!");
            return;
        }
        this.getOwner().AddStatusEffect(new Toughness(1, this.getOwner()));
    }

}