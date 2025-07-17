public class Shop : RandomEvent {

    List<EquipmentItem> offeredItems;
    ActionCard? offeredCard;
    Modifier? offeredModifier; // NYI
    int cardPrice;
    int modifierPrice;

    Dictionary<int, object> ShopOffers;

    public Shop() {
        this.name = "Shop";
        this.description = "Purchase items, cards, and modifiers.";
        // Init offers
        offeredItems = new List<EquipmentItem>();
        ShopOffers = new Dictionary<int, object>();
    }

    // This function contains the bulk of the event code, where the player actually goes through it.
    public override void execute() {
        // Shuffle the pools
        CurrentRun.Shuffle(CurrentRun.DraftableCardPool);
        // TODO: CurrentRun.Shuffle(CurrentRun.ModifierPool);
        // CurrentRun.DraftableCardPool[0] is our offered card
        
        // Generate 3 items. getRandomItemFromPool always gives us an item, even if the pool was exhausted (Rubber Duck)
        while(offeredItems.Count < 3) {
            EquipmentItem itemOffer = CurrentRun.getRandomItemFromPool(1, true); // For now, offered items are removed from the pool
            if(itemOffer.price != null) { // Skip items with no price assigned (are we ok with them being removed from the pool?)
                offeredItems.Add(itemOffer);
                Console.WriteLine("Added "+itemOffer.name+" to shop");
            }
        }

        // Select offered card
        offeredCard = CurrentRun.DraftableCardPool[0];
        cardPrice = getRandomCardPrice(offeredCard);
        Console.WriteLine("Added "+offeredCard.name+" to shop");

        // Select offered Modifier:
        // TODO
        // Console.WriteLine("Added "+offeredModifier.name+" to shop");
        modifierPrice = CurrentRun.rng.Next(25, 50);

        // Display the items on offer
        while(true) {
            Console.WriteLine(this.name + ":\t\t\tYou have $"+CurrentRun.Money);
            Console.WriteLine("View an item for purchase by entering its number, or exit:\n");
            int itemCount = 0;
            ShopOffers = new Dictionary<int, object>(); // Reset offers
            // Display items
            foreach(EquipmentItem item in offeredItems) {
                itemCount++;
                Console.WriteLine("\t[Item "+itemCount+"] "+"($"+item.price+") "+item.ToString()+"\n");
                ShopOffers.Add(itemCount, item);
            }
            
            // Display card
            if(offeredCard != null) {
                itemCount++;
                Console.WriteLine("\t[Card "+itemCount+"] "+"($"+cardPrice+") "+offeredCard.ToString()+"\n");
                ShopOffers.Add(itemCount, offeredCard);
            }

            // Display Modifier
            if(offeredModifier != null) {
                itemCount++;
                Console.WriteLine("\t[Modifier "+itemCount+"] "+"($"+modifierPrice+") "+offeredModifier.ToString()+"\n");
                ShopOffers.Add(itemCount, offeredModifier);
            }

            if(itemCount <= 0) {
                // No more offers left
                Console.WriteLine("Shop is empty. Exiting shop.");
                return;
            }

            Console.Write("\n> ");
            string? cmd = Console.ReadLine();
            if(cmd == null) continue;
            if(cmd.ToLower().Trim() == "exit" || cmd.ToLower().Trim() == "quit") {
                Console.WriteLine("");
                Console.WriteLine("Exiting shop.");
                return;
            }
            if(int.TryParse(cmd.ToLower().Trim(), out int itemSelection)) {
                // If they entered a valid number, ask them if they wish to purchase:
                if(itemSelection <= itemCount && itemSelection > 0) {
                    object chosenOffer = ShopOffers[itemSelection];
                    if(chosenOffer is EquipmentItem) {
                        EquipmentItem chosenItem = (EquipmentItem)chosenOffer;
                        while(true) {
                            Console.WriteLine("Would you like to purchase "+chosenItem.name+" for "+chosenItem.price+"? [Y/N]\n");
                            Console.Write("\n> ");
                            string? cmd2 = Console.ReadLine();
                            if(cmd2 == null) continue;
                            if(cmd2.ToLower().Trim() == "n" || cmd2.ToLower().Trim() == "no") {
                                break;
                            }
                            if(cmd2.ToLower().Trim() == "y" || cmd2.ToLower().Trim() == "yes") {
                                if(CurrentRun.Money < chosenItem.price) {
                                    Console.WriteLine("You cannot afford that!");
                                    break;    
                                }
                                
                                CurrentRun.Inventory.Add(chosenItem);
                                CurrentRun.Money -= (int)chosenItem.price!;
                                Console.WriteLine("Got "+chosenItem.name);
                                offeredItems.Remove(chosenItem);
                                break;
                            }

                        }
                    }
                    if(chosenOffer is ActionCard) {
                        ActionCard chosenCard = (ActionCard)chosenOffer;
                        while(true) {
                            Console.WriteLine("Would you like to purchase "+chosenCard.name+" for "+cardPrice+"? [Y/N]\n");
                            Console.Write("\n> ");
                            string? cmd2 = Console.ReadLine();
                            if(cmd2 == null) continue;
                            if(cmd2.ToLower().Trim() == "n" || cmd2.ToLower().Trim() == "no") {
                                break;
                            }
                            if(cmd2.ToLower().Trim() == "y" || cmd2.ToLower().Trim() == "yes") {
                                if(CurrentRun.Money < cardPrice) {
                                    Console.WriteLine("You cannot afford that!");
                                    break;    
                                }
                                
                                CurrentRun.DraftCard(0);
                                CurrentRun.Money -= cardPrice;
                                Console.WriteLine("Got "+chosenCard.name);
                                offeredCard = null;
                                break;
                            }

                        }
                    }
                    if(chosenOffer is Modifier) {
                        Modifier chosenModifier = (Modifier)chosenOffer;
                        while(true) {
                            Console.WriteLine("Would you like to purchase "+chosenModifier.name+" for "+modifierPrice+"? [Y/N]\n");
                            Console.Write("\n> ");
                            string? cmd2 = Console.ReadLine();
                            if(cmd2 == null) continue;
                            if(cmd2.ToLower().Trim() == "n" || cmd2.ToLower().Trim() == "no") {
                                break;
                            }
                            if(cmd2.ToLower().Trim() == "y" || cmd2.ToLower().Trim() == "yes") {
                                if(CurrentRun.Money < modifierPrice) {
                                    Console.WriteLine("You cannot afford that!");
                                    break;    
                                }
                                
                                CurrentRun.Inventory.Add(chosenModifier);
                                CurrentRun.Money -= modifierPrice;
                                Console.WriteLine("Got "+chosenModifier.name);
                                offeredModifier = null;
                                break;
                            }

                        }
                    }
                }
                else {
                    Console.WriteLine("Must enter a number between 1 and "+itemCount+".\n");
                }
            }
            else {
                Console.WriteLine("Must enter a number between 1 and "+itemCount+", or 'exit' to leave the shop.\n");
            }
        }
    }

    // Eventually this will take into account the card's rarity, but for now just roll rng
    public int getRandomCardPrice(ActionCard card) {
        // TODO
        return CurrentRun.rng.Next(20, 41);
    }
}