using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace IA {
    public class BehaviourSequence : MonoBehaviour {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private Animator animator;
        [SerializeField] private bool isLooping = true;
        
        // Liste polymorphe
        [SerializeField] private List<AIBlock> blocks = new();

        public AIBlock currentBlock;
        private AIContext _ctx;

        private void Start() {
            _ctx = new AIContext(agent, animator);
            StartCoroutine(Run());
        }

        public void TriggerEvent(string name) {
            _ctx.RaiseEvent(name);
        }

        private IEnumerator Run() {
            do {
                foreach (var block in blocks) {
                    currentBlock = Instantiate(block);
                    if (currentBlock == null) continue;
                    
                    var exec = currentBlock.Execute(_ctx);
                    while (exec.MoveNext()) {
                        if (!string.IsNullOrEmpty(currentBlock.stopOnEvent) &&
                            _ctx.LastEventName == currentBlock.stopOnEvent) {
                            break;
                        }
                        
                        yield return exec.Current;
                    }
                }
            } while (isLooping);
        }
    }
}