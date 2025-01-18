public class CardDraft : Event {

    public CardDraft() {
        this.name = "Card Draft";
        this.description = "Choose 1 of 3 cards to add to your collection.";
    }

    // This function contains the bulk of the event code, where the player actually goes through it.
    public override void execute() {
        // Shuffle the pool so that the first 3 are random:
        CurrentRun.Shuffle(CurrentRun.DraftableCardPool);
        // Display the first 3
        while(true) {
            Console.WriteLine(this.name);
            Console.WriteLine("Choose one of the following cards to add to your deck, by entering its number:\n");
            Console.WriteLine("\t[1] "+CurrentRun.DraftableCardPool[0]!.ToString());
            Console.WriteLine("\t[2] "+CurrentRun.DraftableCardPool[1]!.ToString());
            Console.WriteLine("\t[3] "+CurrentRun.DraftableCardPool[2]!.ToString());
            Console.WriteLine("");
            Console.Write("\n> ");
            string? cmd2 = Console.ReadLine();
            if(cmd2 == null) continue;
            if(int.TryParse(cmd2.ToLower().Trim(), out int cardSelection)) {
                // If they entered a valid number for card selection, add it to their collection:
                if(cardSelection <= 3 && cardSelection > 0) {
                    CurrentRun.DraftCard(cardSelection-1);
                    return;
                }
                else {
                    Console.WriteLine("Must enter a number between 1 and 3.\n");
                }
            }
            else {
                Console.WriteLine("Must enter a number between 1 and 3.\n");
            }
        }
    }
}