using System.Collections.Generic;
using System.Linq;

namespace RMotownFestival.Domain
{
    public class Festival
    {
        public Schedule LineUp { get; set; }
        public IEnumerable<Artist> Artists { get; set; }
        public IEnumerable<Stage> Stages { get; set; }

        public Festival()
        {
            LineUp = new Schedule();
            Artists = Enumerable.Empty<Artist>();
            Stages = Enumerable.Empty<Stage>();
        }
    }
}
