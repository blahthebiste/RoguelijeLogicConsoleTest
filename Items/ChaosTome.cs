public class ChaosTome : Item {

    public int completedZones;

    // The only property chaos tomes have is their number
    public ChaosTome(int completedZones) {
        this.completedZones = completedZones;
    }

    // Consumes the tome; applies the upgrade to the run
    public void use() {
        switch(completedZones) {
            case 0:
                // Increase level cap from 1 to 2
                Console.WriteLine("Level cap increased from "+CurrentRun.LevelCap+" to "+ ++CurrentRun.LevelCap);
                break;
            case 1:
                // Increase party size to 4
                Console.WriteLine("Party size increased from "+CurrentRun.PartySize+" to "+ ++CurrentRun.PartySize);
                Console.WriteLine("(Draw per turn increased from "+CurrentRun.DrawPerTurn+" to "+ ++CurrentRun.DrawPerTurn+")");
                break;
            case 2:
                // Increase level cap from 2 to 3
                Console.WriteLine("Level cap increased from "+CurrentRun.LevelCap+" to "+ ++CurrentRun.LevelCap);
                break;
            case 3:
                // Increase party size to 5
                Console.WriteLine("Party size increased from "+CurrentRun.PartySize+" to "+ ++CurrentRun.PartySize);
                Console.WriteLine("(Draw per turn increased from "+CurrentRun.DrawPerTurn+" to "+ ++CurrentRun.DrawPerTurn+")");
                break;
            case 4:
                // Increase level cap from 2 to 3
                Console.WriteLine("Level cap increased from "+CurrentRun.LevelCap+" to "+ ++CurrentRun.LevelCap);
                break;
            case 5:
                // Increease level cap from 3 to 4?
                break;
            default:
                break;
        }
        // Remove the tome from inventory
        this.RemoveFromInventory();
    }

}