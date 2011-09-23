using System;
using System.Runtime.Serialization;

namespace JustGiving.Api.Sdk.Model.Event
{
	[DataContract(Name = "event", Namespace = "")]
	public class Event
	{
		[DataMember(Name = "name")]
		public string Name { get; set; }

		[DataMember(Name = "description")]
		public string Description { get; set; }

		/// <summary>
		/// JustGiving EventId, ignored during requests to the RegisterEvent API.
		/// </summary>
		[DataMember(Name = "id")]
		public int Id { get; set; }

		/// <summary>
		/// The date the event finishes
		/// </summary>
		[DataMember(Name = "completionDate")]
		public DateTime? CompletionDate { get; set; }

		/// <summary>
		/// The date the event expires and will subsequently become unavailable on the site
		/// </summary>
		[DataMember(Name = "expiryDate")]
		public DateTime? ExpiryDate { get; set; }

		/// <summary>
		/// The event start date
		/// </summary>
		[DataMember(Name = "startDate")]
		public DateTime? StartDate { get; set; }

		/// <summary>
		/// One of Running_Marathons, Treks, Walks, Cycling, Swimming, Birthday, 
		/// Wedding, OtherCelebration, Christening, InMemory, Anniversaries, Triathlons, 
		/// Parachuting_Skydives, OtherSportingEvents, NewYearsResolutions, Christmas.
		/// Select the nearest accurate eventType, defaulting back to OtherCelebration
		/// 
		/// Event names will be shown on fundraising pages an event type of: Running_Marathons
		/// Treks, Walks, Cycling, Swimming, Triathlons, Parachuting_Skydives, OtherSportingEvents.
		/// Other event types will be correctly linked, but not visually represented on the page.
		/// </summary>
		[DataMember(Name = "eventType")]
		public string EventType { get; set; }
	}
}
