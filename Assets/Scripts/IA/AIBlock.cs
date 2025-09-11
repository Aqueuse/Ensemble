using System.Collections;
using UnityEngine;

namespace IA {
    [System.Serializable]
    public abstract class AIBlock : ScriptableObject {
        public string stopOnEvent;

        // Appelé par le séquenceur. Retourne un IEnumerator pour la coroutine.
        public abstract IEnumerator Execute(AIContext ctx);
    }
}