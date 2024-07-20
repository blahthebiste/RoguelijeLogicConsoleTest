

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
		DiscardPile = new List<ActionCard>();
		Hand = new List<ActionCard>();
		randomizeDrawPileOrder();
		drawOpeningHand();
	}
	
	public static void drawOpeningHand() {
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
	
	// Puts a card into the discard pile from the hand.
	// Used whenever a card is played, and at end of turn.
	public static void staticdiscardCard(ActionCard card) {
		DiscardPile.Add(card);
		Hand.Remove(card);
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
	public static void randomizeDrawPileOrder() {
		int n = DrawPile.Count;
		while (n > 1)
		{
			n--;
			int k = CurrentRun.rng.Next(n + 1);
			(DrawPile[k], DrawPile[n]) = (DrawPile[n], DrawPile[k]);
		}
	}

}