using System.Collections;
using UnityEngine;

namespace IA.Behaviours {
    [CreateAssetMenu(fileName = "BehaviourBlock", menuName = "IA/BehaviourBlocks/Wait For Event")]
    public class WaitForEventBlock : AIBlock {
        public override IEnumerator Execute(AIContext ctx)
        {
            yield return ctx.WaitForEvent(stopOnEvent);
        }
    }
}