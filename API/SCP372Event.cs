using Exiled.Events.Features;

namespace SCP372Plugin.API
{
    public static class SCP372Event
    {
        /// <summary>
        /// Event fired when SCP-372 is assigned
        /// </summary>
        public static event CustomEventHandler<SCP372AssignedEventArgs> SCP372Assigned;

        /// <summary>
        /// Event fired when SCP-372 escapes
        /// </summary>
        public static event CustomEventHandler<SCP372EscapedEventArgs> SCP372Escaped;

        /// <summary>
        /// Event triggered when SCP-372 dies
        /// </summary>
        public static event CustomEventHandler<SCP372DiedEventArgs> SCP372Died;

        /// <summary>
        /// SCP372Assigned Invoke
        /// </summary>
        public static void OnSCP372Assigned(SCP372AssignedEventArgs ev) => SCP372Assigned?.Invoke(ev);
        /// <summary>
        /// SCP372Escaped Invoke
        /// </summary>
        public static void OnSCP372Escaped(SCP372EscapedEventArgs ev) => SCP372Escaped?.Invoke(ev);
        /// <summary>
        /// SCP372Died Invoke
        /// </summary>
        public static void OnSCP372Died(SCP372DiedEventArgs ev) => SCP372Died?.Invoke(ev);
    }
}