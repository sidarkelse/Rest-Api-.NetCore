using AutoMapper;
using EventsApi.Entities;
using EventsApi.Models;

namespace EventsApi.Mappers
{
	public class EventProfile : Profile
	{
		public EventProfile() 
		{
			CreateMap<Events, EventViewModel>();
			CreateMap<EventSpeakers,EventSpeakerViewModel>();

			CreateMap<EventInputModel,Events>();
			CreateMap<EventInputSpeakerModel, EventSpeakers>();
		}
	}
}
