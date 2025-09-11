using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace IA {
    public class AIContext {
        public NavMeshAgent Agent { get; }
        public Animator Animator { get; }
        public event System.Action<string> OnEvent;
        
        // Système d’évènements simple
        public string LastEventName { get; private set; }

        public void RaiseEvent(string name) {
            LastEventName = name;
            OnEvent?.Invoke(name);
        }

        // Aides d’attente
        public IEnumerator WaitUntilDestinationReached()
        {
            yield return new WaitUntil(() => !Agent.pathPending && Agent.remainingDistance <= Agent.stoppingDistance);
        }
    
        public IEnumerator WaitForEvent(string name)
        {
            LastEventName = null;
            yield return new WaitUntil(() => LastEventName == name);
        }

        public AIContext(NavMeshAgent agent, Animator animator)
        {
            Agent = agent; Animator = animator;
        }
    }
}