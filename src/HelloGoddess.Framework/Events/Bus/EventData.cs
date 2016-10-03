using System;
using HelloGoddess.Infrastructure.Timing;

namespace HelloGoddess.Infrastructure.Events.Bus
{
    /// <summary>
    /// Implements <see cref="IEventData"/> and provides a base for event data classes.
    /// </summary>
    
    public abstract class EventData : IEventData
    {
        /// <summary>
        /// The time when the event occurred.
        /// </summary>
        public DateTime EventTime { get; set; }

        /// <summary>
        /// The object which triggers the event (optional).
        /// </summary>
        public object EventSource { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected EventData()
        {
            EventTime = Clock.Now;
        }
    }
}