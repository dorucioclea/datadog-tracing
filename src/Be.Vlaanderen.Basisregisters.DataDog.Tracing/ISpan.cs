namespace Be.Vlaanderen.Basisregisters.DataDog.Tracing
{
    using System;

    /// <summary>
    /// Encapsulates an APM trace or a span within a trace.
    /// </summary>
    public interface ISpan : ISpanSource, IDisposable
    {
        /// <summary>
        /// Gets the trace id on the span.
        /// </summary>
        long TraceId { get; }
        
        /// <summary>
        /// Gets the id on the span.
        /// </summary>
        long SpanId { get; }

        /// <summary>
        /// Gets the parent id on the span.
        /// </summary>
        long? ParentId { get; }

        /// <summary>
        /// Gets or sets the resource name on the span.
        /// </summary>
        string Resource { get; set; }

        /// <summary>
        /// Sets metadata on the span. This is any additional information you might want to see when looking at the trace.
        /// e.g. keys or queries.
        /// </summary>
        /// <param name="name">The key of the metadata tag.</param>
        /// <param name="value">The value.</param>
        void SetMeta(string name, string value);

        /// <summary>
        /// Sets an error on the span along with the message, stack, and exception type.
        /// </summary>
        /// <param name="ex">The exception.</param>
        void SetError(Exception ex);
    }
}
