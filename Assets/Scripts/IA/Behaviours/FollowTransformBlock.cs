using System.Collections;
using UnityEngine;

namespace IA.Behaviours {
    [CreateAssetMenu(fileName = "BehaviourBlock", menuName = "IA/BehaviourBlocks/Follow Transform")]
    public class FollowTransformBlock : AIBlock {
        public TargetContext target;

        public override IEnumerator Execute(AIContext ctx) {
            if (target == null) {
                Debug.LogError("Target is not defined");
                yield break;
            }

            var associatedTargets = FindObjectsOfType<AITarget>(true);

            if (associatedTargets.Length == 0) {
                Debug.LogError(
                    "AITarget not found on scene. Please add one as a component of your target GameObject");
                yield break;
            }

            AITarget aiTarget = null;

            foreach (var associatedTarget in associatedTargets) {
                if (associatedTarget.targetContext == target) {
                    aiTarget = associatedTarget;
                    break;
                }
            }

            if (aiTarget == null) {
                Debug.LogError(
                    "target context not found on any AITarget on the scene. Have you forget to associate one ?");
                yield break;
            }

            while (true) {
                if (aiTarget != null) ctx.Agent.SetDestination(aiTarget.transform.position);
                yield return null;
            }
        }
    }
}