using System.Collections;
using UnityEngine;

namespace IA.Behaviours {
    [CreateAssetMenu(fileName = "BehaviourBlock", menuName = "IA/BehaviourBlocks/Go To Transform")]
    public class GoToTransformBlock : AIBlock {
        public TargetContext target;

        public override IEnumerator Execute(AIContext ctx) {
            if (target == null) {
                Debug.LogError("Target is not defined");
                yield return null;
            }

            var associatedTarget = FindObjectsOfType<AITarget>(true);

            if (associatedTarget.Length == 0) {
                Debug.LogError(
                    "AITarget not found on scene. Please add one as a component of your target GameObject");
                yield return null;
            }

            foreach (var aiTarget in associatedTarget) {
                if (aiTarget.targetContext == target) {
                    ctx.Agent.SetDestination(aiTarget.transform.position);
                    yield return null;
                }
            }

            yield return ctx.WaitUntilDestinationReached();
        }
    }
}