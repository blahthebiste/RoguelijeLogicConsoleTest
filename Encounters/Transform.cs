public class Transform : Encounter {

    public Transform() {
        this.name = "Transform";
        this.description = "Transform a card in your collection into a random one of the same type.";
    }

    // This function contains the bulk of the Encounter code, where the player actually goes through it.
    public override void execute()
    {

        while (true)
        {
            Console.WriteLine(this.name);
            Console.WriteLine("Choose a card to transform, by entering its number:");
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
                Console.WriteLine("Exiting Forge.");
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
                        Console.WriteLine("Transform " + selectedCard.name + "? [Y/N]\n");
                        Console.Write("\n> ");
                        string? cmd2 = Console.ReadLine();
                        if (cmd2 == null) continue;
                        if (cmd2.ToLower().Trim() == "n" || cmd2.ToLower().Trim() == "no")
                        {
                            break;
                        }
                        if (cmd2.ToLower().Trim() == "y" || cmd2.ToLower().Trim() == "yes")
                        { // Do Transformation.
                            ActionType? type = selectedCard.actionType;
                            if (type == null) {
                                Console.WriteLine("ERROR: null ActionType for card '"+selectedCard.name+"'!");
                                return;
                            }
                            ActionCard newCard = Compendium.Cards.GetRandomCardOfType((ActionType)type, selectedCard);
                            if (CurrentRun.CardCollection.Contains(selectedCard))
                            {
                                CurrentRun.CardCollection.Remove(selectedCard);
                                CurrentRun.CardCollection.Add(newCard);
                            }
                            if (CurrentRun.MasterDeck.Contains(selectedCard))
                            {
                                CurrentRun.MasterDeck.Remove(selectedCard);  
                                CurrentRun.MasterDeck.Add(newCard);  
                            }
                            Console.WriteLine("Added " + newCard.name + " to collection.");
                            return;                            
                        }
                    }
                }
            }
        }
    }
}