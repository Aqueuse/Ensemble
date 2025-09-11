using System.Collections;
using UnityEngine;

namespace IA.Behaviours {
    /// <summary>
    /// AI block responsible for playing an animation via an Animator trigger, then waiting
    /// for the animation to finish before resuming the sequence.
    /// </summary>
    /// <remarks>
    /// Simple solution: a StateMachineBehaviour that notifies the context;
    /// Specifically, a <see cref="StateMachineBehaviour"/> (or any other notifier)
    /// is expected to signal the end of the animation by setting
    /// <c>isCompleted</c> to <c>true</c>. The block:
    /// 1) resets the trigger,
    /// 2) sets the trigger,
    /// 3) waits for the completion notification,
    /// 4) resets the trigger.
    /// </remarks>
    [CreateAssetMenu(fileName = "BehaviourBlock", menuName = "IA/BehaviourBlocks/Play Animation")]
    public class PlayAnimationBlock : AIBlock {
        [SerializeField] private string trigger;
        public bool ended;
        
        public override IEnumerator Execute(AIContext ctx)
        {
            if (ctx.Animator == null || string.IsNullOrEmpty(trigger)) yield break;

            ended = false;

            // Solution simple: StateMachineBehaviour qui notifie le contexte;
            ctx.Animator.ResetTrigger(trigger);
            ctx.Animator.SetTrigger(trigger);
            yield return new WaitUntil(() => ended);
        }
    }
}