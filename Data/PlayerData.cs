
        public class PlayerData : EntityData {
            
            // The card that is added to the deck during combat when this character is in the party.
            public string PersonalCard { get; set; }

            // The level of this character, used for determining whether they can level up.
            public int Level { get; set; }

            // A list of valid characters that this one can level-up into.
            public List<string> UpgradeList { get; set; }

            public PlayerData() : base() {
                PersonalCard = "Basic Attack";
                Level = 1;
                UpgradeList = new List<string>();
            }
        }