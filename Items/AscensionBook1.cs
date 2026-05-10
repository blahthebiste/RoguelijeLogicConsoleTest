public class AscensionBook1 : Item {


    public AscensionBook1() {
        this.name = "Green Ascension Book";
        this.description = "This book shifts the reader through reality, allowing them to become a more powerful version of themself.";
        this.type = "Tome";
    }

    // Levels up a level 1 character to level 2.
    // 1. Check if the character is level 1
    // 2. Generate options for level 2s they could upgrade to
    // 3. Let the player choose an option. Further steps are in CurrentRun.UpgradeHero().
    public override void use() {
        PlayerCharacter? hero = null;
        var AllHeroes = CurrentRun.Party.Concat(CurrentRun.Bench).ToList();
        // Prompt for hero to upgrade:
        while(hero == null) {
			Console.WriteLine("Choose a hero to level up:");
             // search for valid heroes among party members and bench:
            int index = 0;
            foreach(PlayerCharacter pc in AllHeroes){
                // First check if the character is a valid level
                if(pc.Level > 1)
                {
                    Console.WriteLine("DEBUG: Cannot level up "+pc.name+"; they are already level "+pc.Level);
                    continue;
                }
                // Then check that their upgrade list is not empty:
                if(pc.UpgradeList == null || pc.UpgradeList.Count < 1)
                {
                    Console.WriteLine("DEBUG: Cannot level up "+pc.name+"; the have no valid characters to level up into!");
                    continue;
                }
                // Valid hero found. Print them out.
                Console.WriteLine("["+(++index)+"] "+pc.name);
            }
			Console.Write("\n> ");
            string? cmd = Console.ReadLine();
            if(cmd == null) continue;
            if(cmd.ToLower().Trim() == "exit" || cmd.ToLower().Trim() == "quit" || cmd.ToLower().Trim() == "cancel" || cmd.ToLower().Trim() == "back") {
                Console.WriteLine("");
                Console.WriteLine("Cancelling level up.");
                return;
            }
            if(int.TryParse(cmd.ToLower().Trim(), out int n))
            {
                if(n <= index && n > 0) {
                    // Valid hero selection.
                    hero = AllHeroes[n-1];
                    Console.WriteLine("DEBUG: Selected "+hero.name+".");
                    break;
                }
                else {
                    Console.WriteLine("Invalid number. Must be between 1-"+index+".");
                    continue;
                }
            }
        }
        
        // Display the options
        while (true)
        {
            // Fetch possible levelup choices:
            Console.WriteLine("Which class should "+hero.name+" become? Enter its number:\n");
            int i = 0;
            foreach (string upgradeName in hero.UpgradeList!)
            {
                i++;
                Console.WriteLine("\t["+i+"] " + upgradeName);
            }
            Console.WriteLine("");
            Console.Write("\n> ");
            string? cmd2 = Console.ReadLine();
            if (cmd2 == null) continue;
            if(cmd2.ToLower().Trim() == "cancel" || cmd2.ToLower().Trim() == "back") {
                Console.WriteLine("");
                use(); // Use recursion to go back to selecting which hero to level up. Surely nothing can go wrong...
                return;
            }
            if (int.TryParse(cmd2.ToLower().Trim(), out int heroSelection))
            {
                // If they entered a valid number for hero selection, perform the upgrade
                if (heroSelection <= hero.UpgradeList.Count && heroSelection > 0)
                {
                    CurrentRun.UpgradeHero(hero, hero.UpgradeList[heroSelection-1]);
                    // Now remove this book from the inventory:
                    CurrentRun.Inventory.Remove(this);
                    return;
                }
                else
                {
                    Console.WriteLine("Must enter a number between 1 and "+hero.UpgradeList.Count+".\n");
                }
            }
            else
            {
                Console.WriteLine("Must enter a number between 1 and "+hero.UpgradeList.Count+".\n");
            }
        }
    }
}