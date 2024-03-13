using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EventsApi.Entities
{
	public class Events
	{

        public Events() 
        {
            Speakers = new List<EventSpeakers>();
            IsDeleted = false;
        }
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid  Id { get; set; }

		[MaxLength(255)]
		public string  Title { get; set; }

        public string  Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public List<EventSpeakers> Speakers { get; set; }

        public bool  IsDeleted { get; set; }


        public void Update(string title, string descrption, DateTime starteDate, DateTime endDate)

        {
            Title = title;
            Description = descrption;
            StartDate = starteDate;
            EndDate = endDate;


        }
        public void Delete()
        {
            IsDeleted = true;
        }

    }
}
