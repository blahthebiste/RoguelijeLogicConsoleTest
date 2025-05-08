public class Grog : EquipmentItem {


    public Grog() {
        this.name = "Grog";
        this.description = "Reduce incoming debuffs by 1.";
        this.slot = ActionType.ANY;
        this.price = 65;
    }


    // Code is handled in Entity

}