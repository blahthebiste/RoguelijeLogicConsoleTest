

// Responsible for managing player hand, draw pile, discard pile, and master deck during combat.
public static class CardManager {
	public static List<ActionCard> DrawPile = new List<ActionCard>();
	public static List<ActionCard> DiscardPile = new List<ActionCard>();
	public static List<ActionCard> Hand = new List<ActionCard>();
	
		
	public static List<ActionCard> copyMasterDeck() {
		List<ActionCard> copiedDeck = new List<ActionCard>();
		foreach(ActionCard card in CurrentRun.MasterDeck) {
			copiedDeck.Add(card.makeCopy());
		}
		return copiedDeck;		
	}
	
	public static void beginCombat() {
		DrawPile = copyMasterDeck();
		// Add character cards to drawpile:
		foreach(PlayerCharacter hero in CurrentRun.Party) {
			DrawPile.Add(hero.personalCard);
		}
		DiscardPile = new List<ActionCard>();
		Hand = new List<ActionCard>();
		randomizeDrawPileOrder();
		drawHand();
	}
	
	public static void drawHand() {
		for(int i = 0; i < CurrentRun.DrawPerTurn; i++) {
			drawCard();
		}
	}
	
	public static void drawCard(int number = 1) {
		for(int i = 0; i < number; i++){
			if(DrawPile.Count == 0) {
				if(DiscardPile.Count == 0)  {
					// Both draw and discard are empty; do nothing
					return;
				}
				// Discard pile has cards; reshuffle them into your draw pile.
				reshuffle();
			}
			Hand.Add(DrawPile[0]);
			DrawPile.RemoveAt(0);
		}
	}

	// Draws a card of the specified type from the draw pile. If the draw pile does not contain that card, triggers a reshuffle.
	public static void drawCardOfType(ActionType cardType) {
		if(DrawPile.Count == 0 && DiscardPile.Count == 0) {
			// Both draw and discard are empty; do nothing
			return;
		}
		// Search the current draw pile first
		foreach(ActionCard card in DrawPile)
		{
			if(card.actionType == cardType)
			{
				Hand.Add(card);
				DrawPile.Remove(card);
				Console.WriteLine("DEBUG: Drew "+card.name+".");
				return;
			}
		}
		// Didn't find it in the draw pile, lets check the discard:
		bool foundMatchInDiscardPile = false;
		foreach(ActionCard card in DiscardPile)
		{
			if(card.actionType == cardType)
			{
				foundMatchInDiscardPile = true;
				Console.WriteLine("DEBUG: Found matching card, "+card.name+", in discard pile. Reshuffling.");
				break;
			}
		}
		if(foundMatchInDiscardPile)
		{
			reshuffle();
			// Just use recursion, it's fun and I'm lazy.
			// Could be a problem if a card is ever stuck in the discard pile after every reshuffle, but that is not yet a thing.
			drawCardOfType(cardType);
		}
		else
		{ // No match found anywhere.
			Console.WriteLine("Failed to find card of type, "+cardType+", in draw or discard pile.");
		}
			
	}
	
	// Puts a card into the discard pile from the hand.
	// Used whenever a card is played, and at end of turn.
	public static void discardCard(ActionCard card) {
		DiscardPile.Add(card);
		Hand.Remove(card);
	}

	// Prompts the player to choose a card in their discard pile.
	// Puts the chosen card into their hand.
	// Returns false if the discard pile was empty, or the player cancelled.
	public static bool retrieveCard() {
		if(DiscardPile.Count == 0) {
			Console.WriteLine("Your discard pile is empty!");
			return false;
		}
		while(true) {
			Console.WriteLine("Choose a card to return to your hand:");
			int cardCount = 0;
			foreach(ActionCard card in DiscardPile) {
                cardCount++;
				Console.WriteLine("\t[Card "+cardCount+"] "+card.ToString()+"\n");
			}
			Console.Write("\n> ");
            string? cmd = Console.ReadLine();
            if(cmd == null) continue;
            if(cmd.ToLower().Trim() == "exit" || cmd.ToLower().Trim() == "quit" || cmd.ToLower().Trim() == "cancel" || cmd.ToLower().Trim() == "back") {
                Console.WriteLine("");
                Console.WriteLine("Cancelling card retrieval.");
                return false;
            }
            if(int.TryParse(cmd.ToLower().Trim(), out int cardSelection)) {
				if(cardSelection <= cardCount && cardSelection > 0) {
					ActionCard chosenCard = DiscardPile[cardSelection - 1];
					Console.WriteLine("Returned "+chosenCard.name+" to your hand.");
					DiscardPile.Remove(chosenCard);
					Hand.Add(chosenCard);
					return true;
				}
                else {
                    Console.WriteLine("Must enter a number between 1 and "+cardCount+".\n");
                }
			}
		}

	}

	public static void discardHand() {
		foreach(ActionCard card in Hand) {
			DiscardPile.Add(card); // Put all cards from hand into discard pile
		}
		Hand = new List<ActionCard>(); // Reset hand
	}
	
	public static void reshuffle() {
		foreach(ActionCard card in DiscardPile) {
			DrawPile.Add(card); // Put all cards from discard pile into draw pile
		}
		DiscardPile = new List<ActionCard>(); // Reset discard pile
		randomizeDrawPileOrder();
	}
	
	// Source: https://stackoverflow.com/a/69220421/5086634
	// public static void randomizeCardOrder(List<ActionCard> cards) {
	// 	int n = cards.Count;
	// 	while (n > 1)
	// 	{
	// 		n--;
	// 		int k = CurrentRun.rng.Next(n + 1);
	// 		(cards[k], cards[n]) = (cards[n], cards[k]);
	// 	}
	// }

	public static void randomizeDrawPileOrder() {
		CurrentRun.Shuffle(DrawPile);
	}
}