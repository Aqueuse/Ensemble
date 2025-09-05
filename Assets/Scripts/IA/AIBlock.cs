using UnityEngine;

namespace IA {
    [System.Serializable]
    public class AIBlock {
        public AIBlockType type;
        public Transform targetTransform;
        public string animationTrigger; // a trigger with a name in the animator of the agent
        public string eventName;
        public float waitSeconds;
    }
}