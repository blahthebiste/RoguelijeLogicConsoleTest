public class CardDraft : Encounter {

    public CardDraft() {
        this.name = "Card Draft";
        this.description = "Choose 1 of 3 cards to add to your collection.";
    }

    // This function contains the bulk of the Encounter code, where the player actually goes through it.
    public override void execute() {
        // Shuffle the pool so that the first 3 are random:
        CurrentRun.Shuffle(CurrentRun.DraftableCardPool);
        // Retrieve the first 3 that are different:
        // Store their original index.
        List<ActionCard> draftCards = new List<ActionCard>();
        for (int i = 0; i < CurrentRun.DraftableCardPool.Count; i++)
        {
            bool matchFound = false;
            // Compare card to any previously selected cards
            // TODO: compare modifiers too? Maybe not
            foreach (ActionCard prevCard in draftCards)
            {
                if (prevCard.name == CurrentRun.DraftableCardPool[i].name)
                { // Found duplicate card, skip
                    matchFound = true;
                    break;
                }
            }
            if (!matchFound) {
                // No match found, so add it
                draftCards.Add(CurrentRun.DraftableCardPool[i]);
                if (draftCards.Count == 3)
                { // Got enough options
                    break;
                }
            }
            
        }

        // Display the options
        while (true)
        {
            Console.WriteLine(this.name);
            Console.WriteLine("Choose one of the following cards to add to your deck, by entering its number:\n");
            Console.WriteLine("\t[1] " + draftCards[0]!.ToString());
            Console.WriteLine("\t[2] " + draftCards[1]!.ToString());
            Console.WriteLine("\t[3] " + draftCards[2]!.ToString());
            Console.WriteLine("");
            Console.Write("\n> ");
            string? cmd2 = Console.ReadLine();
            if (cmd2 == null) continue;
            if (int.TryParse(cmd2.ToLower().Trim(), out int cardSelection))
            {
                // If they entered a valid number for card selection, add it to their collection:
                if (cardSelection <= 3 && cardSelection > 0)
                {
                    CurrentRun.DraftCard(draftCards[cardSelection - 1]);
                    return;
                }
                else
                {
                    Console.WriteLine("Must enter a number between 1 and 3.\n");
                }
            }
            else
            {
                Console.WriteLine("Must enter a number between 1 and 3.\n");
            }
        }
    }
}