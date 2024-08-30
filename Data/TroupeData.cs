public class TroupeData {
            public string Name { get; set; }
            public string Description { get; set; }
            public int Difficulty { get; set; }
            public List<string> EnemyList { get; set; }

            public TroupeData() {
                Name = "MISSING NAME";
                Description = "MISSING DESCRIPTION";
                EnemyList = new List<string>();
            }
        }