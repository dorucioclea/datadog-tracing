namespace Be.Vlaanderen.Basisregisters.DataDog.Tracing
{
    using System;
    using System.Reactive.Subjects;

    /// <summary>
    /// For creating new traces.
    /// </summary>
    public class TraceSource : IObservable<Trace>, ISpanSource
    {
        private readonly Subject<Trace> _subject = new Subject<Trace>();

        public long TraceId => Arguments.TraceId;

        public long? ParentSpanId => Arguments.ParentSpanId;

        public TraceSourceArguments Arguments { get; }

        public TraceSource(long traceId) : this (new TraceSourceArguments(traceId)) { }

        public TraceSource(TraceSourceArguments args) => Arguments = args;

        /// <summary>
        /// Begins a new trace.
        /// </summary>
        /// <param name="name">
        /// The name of the trace. This will be the title of the trace when viewing the trace.
        /// </param>
        /// <param name="serviceName">This is the name of the service. e.g. my_appication, memcached, dynamodb, etc.</param>
        /// <param name="resource">The underlying resource. e.g. /home for web or GET for memcached. This shouldn't have too many unique combinations.</param>
        /// <param name="type">The category of the service. Typically web, db, or cache.</param>
        /// <returns>The new trace.</returns>
        public ISpan Begin(string name, string serviceName, string resource, string type)
            => new RootSpan(_subject)
            {
                TraceId = TraceId,
                SpanId = Util.NewSpanId(),
                Name = name,
                Resource = resource,
                Type = type,
                Service = serviceName,
                Start = Util.GetTimestamp(),
                ParentId = ParentSpanId
            };

        /// <summary>
        /// Subscribes to traces that originate from this source instance when they complete.
        /// </summary>
        /// <param name="observer">The observer.</param>
        /// <returns>An unsubscribe disposable.</returns>
        public IDisposable Subscribe(IObserver<Trace> observer) => _subject.Subscribe(observer);
    }
}
