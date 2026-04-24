public class AscensionBook1 : Item {


    public AscensionBook1() {
        this.name = "Green Ascension Book";
        this.description = "This book shifts the reader through reality, allowing them to become a more powerful version of themself.";
    }

    // Levels up a level 1 character to level 2.
    // 1. Check if the character is level 1
    // 2. Generate options for level 2s they could upgrade to
    // 3. Let the player choose an option. Further steps are in CurrentRun.UpgradeHero().
    public void use(PlayerCharacter hero) {
        // First check if the character is a valid level
        if(hero.Level > 1)
        {
            Console.WriteLine("Cannot level up "+hero.name+"; they are already level "+hero.Level);
            return;
        }
        // Then check that their upgrade list is not empty:
        if(hero.UpgradeList == null || hero.UpgradeList.Count < 1)
        {
            Console.WriteLine("ERROR: Cannot level up "+hero.name+"; the have no valid characters to level up into!");
            return;
        }
        // Display the options
        while (true)
        {
            // Hero is level 1, lets fetch their options:
            Console.WriteLine("Which class should "+hero.name+" become? Enter its number:\n");
            int i = 0;
            foreach (string upgradeName in hero.UpgradeList)
            {
                i++;
                Console.WriteLine("\t["+i+"] " + upgradeName);
            }
            Console.WriteLine("");
            Console.Write("\n> ");
            string? cmd2 = Console.ReadLine();
            if (cmd2 == null) continue;
            if (int.TryParse(cmd2.ToLower().Trim(), out int heroSelection))
            {
                // If they entered a valid number for hero selection, perform the upgrade
                if (heroSelection <= hero.UpgradeList.Count && heroSelection > 0)
                {
                    CurrentRun.UpgradeHero(hero, hero.UpgradeList[heroSelection-1]);
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