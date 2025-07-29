public class Mirror : Encounter {

    public Mirror() {
        this.name = "Mirror";
        this.description = "Copy a card in your collection.";
    }

    // This function contains the bulk of the Encounter code, where the player actually goes through it.
    public override void execute()
    {

        while (true)
        {
            Console.WriteLine(this.name);
            Console.WriteLine("Choose a card to get a copy of, by entering its number:");
            Console.WriteLine("");
            // Loop through master deck and collection:
            int i = 0;
            foreach (ActionCard card in CurrentRun.MasterDeck)
            {
                i++;
                Console.WriteLine("\t[" + i + "] " + card.ToString());
            }
            foreach (ActionCard card in CurrentRun.CardCollection)
            {
                i++;
                Console.WriteLine("\t[" + i + "] " + card.ToString());
            }
            Console.WriteLine("");
            Console.Write("\n> ");
            string? cmd = Console.ReadLine();
            if (cmd == null) continue;
            if (cmd.ToLower().Trim() == "exit" || cmd.ToLower().Trim() == "quit")
            {
                Console.WriteLine("");
                Console.WriteLine("Exiting Mirror.");
                return;
            }
            if (cmd.Split().Length != 1) continue;
            if (int.TryParse(cmd.ToLower().Trim(), out int cardSelection))
            {
                // If they entered a valid number, ask for confirmation:
                if (cardSelection <= i && cardSelection > 0)
                {
                    ActionCard selectedCard;
                    if (cardSelection < CurrentRun.MasterDeck.Count)
                    {
                        selectedCard = CurrentRun.MasterDeck[cardSelection - 1];
                    }
                    else
                    {
                        selectedCard = CurrentRun.CardCollection[cardSelection - CurrentRun.MasterDeck.Count - 1];
                    }

                    while (true)
                    {
                        Console.WriteLine("Duplicate " + selectedCard.name + "? [Y/N]\n");
                        Console.Write("\n> ");
                        string? cmd2 = Console.ReadLine();
                        if (cmd2 == null) continue;
                        if (cmd2.ToLower().Trim() == "n" || cmd2.ToLower().Trim() == "no")
                        {
                            break;
                        }
                        if (cmd2.ToLower().Trim() == "y" || cmd2.ToLower().Trim() == "yes")
                        { // Do duplication.
                            ActionCard copy = selectedCard.makeCopy();
                            CurrentRun.CardCollection.Add(copy);
                            Console.WriteLine("Added " + copy.name + " to collection.");
                            return;
                        }
                    }
                }
            }
        }
    }
}