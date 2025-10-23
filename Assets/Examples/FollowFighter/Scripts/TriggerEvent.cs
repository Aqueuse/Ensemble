using UnityEngine;

namespace Examples.FollowFighter.Scripts {
    public class TriggerEvent : MonoBehaviour {
        [SerializeField] private GraphExecutor sequenceExecutor;
        [SerializeField] private string eventName; // as defined in scriptableObject

        public void TriggerMe() {
            //if (sequenceExecutor!= null) sequenceExecutor.TriggerEvent(eventName);
        }
    }
}
