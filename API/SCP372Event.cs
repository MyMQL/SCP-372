using Exiled.Events.Features;

namespace SCP372Plugin.API
{
    public static class SCP372Event
    {
        // Event fired when SCP-372 is assigned
        public static event CustomEventHandler<SCP372AssignedEventArgs> SCP372Assigned;

        // Event fired when SCP-372 escapes
        public static event CustomEventHandler<SCP372EscapedEventArgs> SCP372Escaped;

        // Methods to trigger events
        public static void OnSCP372Assigned(SCP372AssignedEventArgs ev) => SCP372Assigned?.Invoke(ev);
        public static void OnSCP372Escaped(SCP372EscapedEventArgs ev) => SCP372Escaped?.Invoke(ev);
    }
}
