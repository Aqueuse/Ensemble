namespace IA {
    using System.Collections.Generic;
    using UnityEngine;

    namespace IA {
        [CreateAssetMenu(fileName = "AISequence", menuName = "IA/AI Sequence")]
        public class AISequence : ScriptableObject {
            [SerializeField] private List<AIBlock> blocks = new();
            [SerializeField] private bool isLooping = true;

            public List<AIBlock> Blocks => blocks;
            public bool IsLooping => isLooping;
        }
    }
}