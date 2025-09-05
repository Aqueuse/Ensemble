using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace IA {
    public class BehaviourSequence : MonoBehaviour {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private Animator animator;
        [SerializeField] private Transform spawnTransform;

        [Space(25)]
        public bool isLooping = true;
        public int currentIndex;

        // AIBlockType related variables
        private bool isFollowing;
        private bool animationEnded;
        private string waitingFor = "";
        private bool eventReceived;
        
        public List<AIBlock> blocks = new();
        public UnityEvent onSequenceComplete;
        
        private void Start() {
            if (NavMesh.SamplePosition(spawnTransform.position, out NavMeshHit hit, 2f, NavMesh.AllAreas)) {
                agent.Warp(hit.position);
                agent.SetDestination(hit.position);
                
                StartCoroutine(RunSequence());
            }
        }

        public void StopFollowing() {
            isFollowing = false;
        }
        
        public void OnAnimationEnd() {
            animationEnded = true;
        }
    
        public void TriggerEvent(string eventName) {
            Debug.Log("event received");
            waitingFor = eventName;
            eventReceived = true;
        }

        private IEnumerator RunSequence() {
            do {
                for (currentIndex = 0; currentIndex < blocks.Count; currentIndex++) {
                    var block = blocks[currentIndex];
                    
                    switch (block.type) {
                        case AIBlockType.GoToVector3:
                            agent.SetDestination(block.targetTransform.position);
                            
                            yield return new WaitUntil(() => !agent.pathPending && agent.remainingDistance < agent.stoppingDistance);
                            break;

                        case AIBlockType.FollowTransform:
                            isFollowing = true;
                            
                            if (block.targetTransform != null) {
                                while (isFollowing) {
                                    agent.SetDestination(block.targetTransform.position);

                                    if (eventReceived) {
                                        break;
                                    }

                                    yield return null;
                                }
                                
                            }
                            break;
                        
                        case AIBlockType.PlayAnimation:
                            if (animator != null && !string.IsNullOrEmpty(block.animationTrigger)) {
                                animator.SetTrigger(block.animationTrigger);
                                animationEnded = false;
                                yield return new WaitUntil(() => animationEnded);
                                animator.ResetTrigger(block.animationTrigger);
                            }

                            break;

                        case AIBlockType.WaitForEvent:
                            eventReceived = false;
                            waitingFor = block.eventName;
                            yield return new WaitUntil(() => eventReceived);
                            break;
                    }
                }
            } while (isLooping);

            // terminé (mode One-Time)
            onSequenceComplete?.Invoke();
        }
    }
}