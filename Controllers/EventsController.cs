using AutoMapper;
using EventsApi.Entities;
using EventsApi.EventContext;
using EventsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventsApi.Controllers
{
	[Route("api/Events")]
	[ApiController]
	public class EventsController : Controller
	{
		private readonly EventContextDb _context;
		private readonly IMapper _mapper;
		public EventsController(EventContextDb context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		/// <summary>
		/// Obter Todos os Eventos
		/// </summary>
		/// <returns> Uma Coleção de Eventos</returns>
		/// <response code="200">Sucesso</response>


		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public IActionResult GetAll()
		{
			var Events = _context.Events.Where(d => !d.IsDeleted).ToList(); //buscando por eventos que não estejam cancelados/Removidos
			var viewModel = _mapper.Map<List<EventViewModel>>(Events);	
			return Ok(viewModel);

		}

		/// <summary>
		/// Obter um Evento
		/// </summary>
		/// <param name="id"> id = Identificador do Evento</param>
		/// <returns>Dados do Evento</returns>
		/// <response code="200">Sucesso</response>
		/// <response code="404">Não foi encontrado</response>
		/// 
		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public IActionResult GetById(Guid id)
		{
			var Event = _context.Events
				.Include(de => de.Speakers)
				.SingleOrDefault(d=> d.Id == id);
			if(Event == null)
			{
				return NotFound();
			}
			var viewModel = _mapper.Map<EventViewModel>(Event);
			return Ok(viewModel);
		}

		/// <summary>
		///Cadastrar um Evento/Criando Dados
		/// </summary>
		/// <remarks>
		/// Objeto Json
		/// </remarks>
		/// <param name="input">Dados do Evento</param>
		/// <returns> Objetos Recém-Criados</returns>
		/// <response code="201">Sucesso</response>

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		public IActionResult Post(EventInputModel input)
		{
			var Event = _mapper.Map<Events>(input);
			_context.Events.Add(Event);
			_context.SaveChanges();

			return CreatedAtAction(nameof(GetById),new { id = Event.Id }, Event);

		}
		/// <summary>
		/// Atualizar um Evento /Editar
		/// </summary>
		/// <remarks>
		///  Objeto  Json
		/// </remarks>
		/// <param name="id"></param>
		/// <param name="input"></param>
		/// <returns>Nada.</returns>
		/// <response code="404">Não encontrado.</response>
		/// <response code="204"> Sucesso</response>
		/// 

		[HttpPut("{id}")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		public IActionResult Update(Guid id, EventInputModel input)
		{
			var Event = _context.Events.SingleOrDefault(d => d.Id == id);
			if (Event == null)
			{
				return NotFound();
			}

			Event.Update(input.Title, input.Description, input.StartDate, input.EndDate);// se não for nulo, atualiza com esses dados input

			_context.Events.Update(Event);
			_context.SaveChanges();

			return NoContent();
		}

		/// <summary>
		/// Deletar um Evento
		/// </summary>
		/// <param name="id">Identificador de evento</param>
		/// <returns>Nada.</returns>
		/// <response code="204">Sucesso</response>
		/// <response code="404">não encontrado</response>

		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[HttpDelete("{id}")]
		public IActionResult DeleteById(Guid id)
		{
			var Event = _context.Events.SingleOrDefault(d => d.Id == id);
			if (Event == null)
			{
				return NotFound();
			}

			Event.Delete();
			_context.SaveChanges();
			return NoContent();	


		}
		/// <summary>
		/// Cadastrar Palestrantes
		/// </summary>
		/// <remarks>
		/// objeto json
		/// </remarks>
		/// <param name="id">Identificador do evento</param>
		/// <param name="input">Dados do Palestrante</param>
		/// 
		/// <returns>Nada.</returns>
		/// <response code="204">Sucesso</response>
		/// <response code="404">Evento não encontrado</response>

		[HttpPost("{id}/speaker")]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]

		public IActionResult PostSpeakers(Guid id, EventInputSpeakerModel input) //Palestrante
		{

			var speaker = _mapper.Map<EventSpeakers>(input);

			speaker.EventId = id; // chave de valor/estrangeira
			var Event = _context.Events.Any(d => d.Id == id);
			if (!Event)
			{
				return NotFound();
			}
			_context.eventSpeakers.Add(speaker);
			_context.SaveChanges();

			return NoContent();
		}

	}
}
