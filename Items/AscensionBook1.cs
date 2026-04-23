public class AscensionBook1 : Item {


    public AscensionBook1() {
        this.name = "Green Ascension Book";
        this.description = "This book shifts the reader through reality, allowing them to become a more powerful version of themself.";
    }

    // Levels up a level 1 character to level 2.
    // 1. Check if the character is level 1
    // 2. Generate options for level 2s they could upgrade to
    // 3. Remove items from the level 1
    // 4. Remove them from the party
    // 5. Add the new level 2 into the party
    public void use(PlayerCharacter hero) {
        // TODO: level up logic
        if(hero.level > 1)
        {
            Console.WriteLine("Cannot level up "+hero.name+"; they are already level "+hero.level);
            return;
        }
    }
}