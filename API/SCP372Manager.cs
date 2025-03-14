using System.Collections.Generic;
using MEC;

namespace SCP372Plugin
{
    public class SCP372Manager
    {

        public IEnumerator<float> MonitorVisibilityState()
        {
            while (true)
            {
                yield return Timing.WaitForSeconds(1f);
                VisibilityManager.Instance.MonitorState();
            }
        }
    }
}