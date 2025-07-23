
// The compendium simply organizes all information in the game.
// Players can access parts of it that pertain to content they have already seen.
// It is also used for conveniently accessing specific pools of things.
public static class Compendium
{
    public static void Initialize()
    {
        Cards.InitializeCards();
    }

    public static class Cards
    {
        // Here we have lists of every card in the game
        public static List<ActionCard> BasicCards = new List<ActionCard>();
        public static List<ActionCard> DualCards = new List<ActionCard>();
        public static List<ActionCard> MovementCards = new List<ActionCard>();
        public static List<ActionCard> SummonCards = new List<ActionCard>();
        public static List<ActionCard> AilmentCards = new List<ActionCard>();

        public static void InitializeCards()
        {
            // Basic cards
            BasicCards.Add(new BasicAttack());
            BasicCards.Add(new BasicDefend());
            BasicCards.Add(new BasicRest());
            BasicCards.Add(new BasicSkill());
            BasicCards.Add(new BasicSpell());


            // Dual cards
            DualCards.Add(new DualAttackDefend());
            DualCards.Add(new DualAttackSkill());
            DualCards.Add(new DualAttackSpell());
            DualCards.Add(new DualAttackRest());
            DualCards.Add(new DualDefendSkill());
            DualCards.Add(new DualDefendSpell());
            DualCards.Add(new DualDefendRest());
            DualCards.Add(new DualSkillSpell());
            DualCards.Add(new DualSkillRest());
            DualCards.Add(new DualSpellRest());

            // Movement cards
            MovementCards.Add(new Leap());
            MovementCards.Add(new Crouch());
            MovementCards.Add(new Strafe());

            // Ailment cards
            AilmentCards.Add(new Wound());

        }

        // Returns a copy of a random card of the given type, excluding the given card:
        public static ActionCard GetRandomCardOfType(ActionType type, ActionCard excludedCard)
        {
            List<ActionCard> possibleCards;
            switch (type)
            {
                case ActionType.ATTACK:
                case ActionType.DEFEND:
                case ActionType.SKILL:
                case ActionType.SPELL:
                case ActionType.REST:
                    possibleCards = BasicCards.ToList();
                    break;
                case ActionType.DUAL:
                    possibleCards = DualCards.ToList();
                    break;
                case ActionType.MOVEMENT:
                    possibleCards = MovementCards.ToList();
                    break;
                case ActionType.SUMMON:
                    possibleCards = SummonCards.ToList();
                    break;
                case ActionType.AILMENT:
                    possibleCards = AilmentCards.ToList();
                    break;
                default:
                    // Card type is passive, any, ultimate, or personal. Should never happen
                    Console.WriteLine("ERROR: card type was abnormal! Returning wound as placeholder");
                    return new Wound();
            }
            // Make sure to exclude the given card from possible results:
            foreach (ActionCard card in possibleCards.ToList())
            {
                if (card.name.ToLower().Trim() == excludedCard.name.ToLower().Trim())
                {
                    possibleCards.Remove(card);
                }
            }
            if (possibleCards.Count == 0)
            {
                Console.WriteLine("ERROR: no possible cards found! Returning wound as placeholder");
                return new Wound();
            }
            CurrentRun.Shuffle(possibleCards);
            return possibleCards[0];
        }

    }
    

}