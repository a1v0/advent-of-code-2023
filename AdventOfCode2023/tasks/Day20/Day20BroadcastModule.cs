namespace AdventOfCode2023;

public class BroadcastModule : BaseModule, IBaseModule
{
    public void IngestPulse(Pulse pulse)
    {
        /**
         * I'm not sure whether this is a best practice,
         * but I'm returning an error if this method is
         * called, because the broadcaster oughtn't receive
         * any pulses.
         **/
      throw new Exception("A Broadcaster object cannot ingest a pulse.");
    }
}