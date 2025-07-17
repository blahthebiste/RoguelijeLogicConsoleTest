
        public class EnemyData {
            public string Name { get; set; }
            public string Description { get; set; }
            public int HP { get; set; }
            public List<string> ActionList { get; set; }

            public EnemyData() {
                Name = "MISSING NAME";
                Description = "MISSING DESCRIPTION";
                ActionList = new List<string>();
            }
        }