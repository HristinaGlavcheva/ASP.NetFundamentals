using static Homies.Data.DataConstants;

namespace Homies.ViewModels
{
    public class EventsAllViewModel
    {
        public EventsAllViewModel()
        {
            
        }

        public EventsAllViewModel(
            int id,
            string name,
            DateTime start,
            string type,
            string organiser)
        {
            Id = id;
            Name = name;
            Start = start.ToString(DateTimeFormat);
            Type = type;
            Organiser = organiser;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Start { get; set; }

        public string Type { get; set; }

        public string Organiser { get; set; }
    }
}
