public class Forge : Encounter {

    public Forge() {
        this.name = "Forge";
        this.description = "Combine 2 items to get another.";
    }

    // This function contains the bulk of the Encounter code, where the player actually goes through it.
    public override void execute() {
        // Display the equipment items in the player's inventory
        List<EquipmentItem> equipmentInventory = new List<EquipmentItem>();
        foreach (Item item in CurrentRun.Inventory) {
            if (item is EquipmentItem)
            {
                equipmentInventory.Add((EquipmentItem)item);
            }
        }
        // Also show items equipped to heroes:
        foreach (PlayerCharacter hero in CurrentRun.Party) {
            foreach (Action act in hero.ActionList)
            {
                if (act.equippedItem != null)
                {
                    equipmentInventory.Add(act.equippedItem);
                }
            }
        }
        while (true)
        {
            Console.WriteLine(this.name);
            Console.WriteLine("Choose 2 items to combine, by entering their numbers:\n");
            for (int i = 0; i < equipmentInventory.Count; i++)
            {
                Console.WriteLine("\t[" + (i + 1) + " - Tier "+equipmentInventory[i]!.tier+"] " + equipmentInventory[i]!.ToString());
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
            if (cmd.Split().Length != 2) continue;
            if (int.TryParse(cmd.Split()[0].ToLower().Trim(), out int item1Selection) && int.TryParse(cmd.Split()[1].ToLower().Trim(), out int item2Selection))
            {
                // If they entered a valid number for 2 items, ask for confirmation:
                if (item1Selection <= equipmentInventory.Count && item1Selection > 0 && item2Selection <= equipmentInventory.Count && item2Selection > 0)
                {
                    EquipmentItem item1 = equipmentInventory[item1Selection - 1];
                    EquipmentItem item2 = equipmentInventory[item2Selection - 1];
                    while (true)
                    {
                        Console.WriteLine("Combine " + item1.name + " and " + item2.name + "? [Y/N]\n");
                        Console.Write("\n> ");
                        string? cmd2 = Console.ReadLine();
                        if (cmd2 == null) continue;
                        if (cmd2.ToLower().Trim() == "n" || cmd2.ToLower().Trim() == "no")
                        {
                            break;
                        }
                        if (cmd2.ToLower().Trim() == "y" || cmd2.ToLower().Trim() == "yes")
                        { // Do combining.
                          // If both items are the same tier, get an item 1 tier higher:
                            int newTier;
                            if (item1.tier == item2.tier)
                            {
                                newTier = item1.tier + 1;
                            }
                            else // If they are different tiers, use the higher tier:
                            {
                                newTier = Math.Max(item1.tier, item2.tier);
                            }
                            // Remove the old items:
                            if (item1.parentAction != null)
                            { //This item was equipped to a hero, unequip it:
                                item1.parentAction.Unequip();
                            }
                            if (item2.parentAction != null)
                            { //This item was equipped to a hero, unequip it:
                                item2.parentAction.Unequip();
                            }
                            // Now remove it from the inventory:
                            CurrentRun.Inventory.Remove(item1);
                            CurrentRun.Inventory.Remove(item2);
                            // Give new item:
                            EquipmentItem newItem = CurrentRun.getRandomItemFromPool(newTier, true);
                            Console.WriteLine("Got a random tier " + newTier + " item: " + newItem.name + "!\n");
                            CurrentRun.Inventory.Add(newItem);
                            return;
                        }

                    }
                }
                else
                {
                    Console.WriteLine("Must enter 2 numbers between 1 and " + CurrentRun.Inventory.Count + ".\n");
                }
            }
            else
            {
                Console.WriteLine("Must enter 2 numbers between 1 and " + CurrentRun.Inventory.Count + ".\n");
            }
        }
    }
}