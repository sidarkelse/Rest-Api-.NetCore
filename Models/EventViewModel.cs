using EventsApi.Entities;
using System.ComponentModel.DataAnnotations;

namespace EventsApi.Models
{
	//Utilziado pra retorno
	public class EventViewModel
	{
		public Guid Id { get; set; }

		[MaxLength(255)]
		public string Title { get; set; }

		public string Description { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }

		public List<EventSpeakerViewModel> Speakers { get; set; }

	}
	public class EventSpeakerViewModel
	{
		public Guid Id { get; set; }
		public string? Name { get; set; }
		public string? TalkTitle { get; set; }
		public string? TalkDescription { get; set; }
		public string? LinkedInProfile { get; set; }

	}
}
