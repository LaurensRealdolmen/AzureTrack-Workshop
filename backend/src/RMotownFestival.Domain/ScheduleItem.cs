using System;

namespace RMotownFestival.Domain
{
    public class ScheduleItem
    {
        public int Id { get; set; }
        public Artist Artist { get; set; }
        public Stage Stage { get; set; }
        public DateTime Time { get; set; }
        private bool isFavorite;
        public bool IsFavorite
        {
            get { return isFavorite; }
            set
            {
                if (isFavorite != value)
                {
                    isFavorite = value;
                }
            }
        }
    }
}
