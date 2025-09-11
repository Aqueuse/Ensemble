using System.Collections;
using System.Collections.Generic;
using IA.IA;
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
        private Coroutine _runningCoroutine;

        private void Start() {
            _ctx = new AIContext(agent, animator);
            _runningCoroutine = StartCoroutine(Run());
        }
        
        /// <summary>
        /// Stop current sequence and replace by a new one.
        /// </summary>
        public void SetSequence(AISequence aiSsequence) {
            if (_runningCoroutine != null) {
                StopCoroutine(_runningCoroutine);
            }
            blocks = aiSsequence.Blocks;
            isLooping = aiSsequence.IsLooping;
            _runningCoroutine = StartCoroutine(Run());
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
        
        public List<AIBlock> GetBlocks() => new (blocks);
    }
}