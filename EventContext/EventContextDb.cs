using EventsApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventsApi.EventContext
{
	//conexão com banco de dados
	public class EventContextDb : DbContext
	{
		public EventContextDb(DbContextOptions<EventContextDb> options) : base(options)
		{

		}

		public DbSet<Events> Events { get; set; } //como se fosse tabelas
		public DbSet<EventSpeakers> eventSpeakers { get; set; }



		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Events>(e =>

			{
				e.HasKey(de => de.Id);//chave estrangeira

				e.Property(de => de.Title).IsRequired(false);//permitindo um valor nulo

				e.Property(de => de.Description)
				.HasMaxLength(200)
				.HasColumnType("varchar(200)");

				e.Property(de => de.StartDate)
				.HasColumnName("Start_Date");

				e.Property(de => de.EndDate)
				.HasColumnName("End_Date");


				e.HasMany(de => de.Speakers)
				.WithOne()
				.HasForeignKey(de => de.EventId);

			}
			);

			builder.Entity<EventSpeakers>(e =>
				{
					e.HasKey(de => de.Id);

				});
		}


	}
}