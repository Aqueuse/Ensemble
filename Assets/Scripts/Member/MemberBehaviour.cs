using Group;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Member {
    public class MemberBehaviour : MonoBehaviour {
        [SerializeField] private SpriteRenderer memberSprite;
        [SerializeField] private NavMeshAgent navMeshAgent;

        private GroupBehaviour _groupBehaviour;
        
        public void Init(Color groupColor, GroupBehaviour groupBehaviour) {
            memberSprite.color = groupColor;
            
            _groupBehaviour = groupBehaviour;
            
            Vector3 spawnOffset = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
            Vector3 spawnTarget = transform.position + spawnOffset;
            navMeshAgent.Warp(spawnTarget);
        
            SetDestination(groupBehaviour.transform.position);
        }
    
        public void SetDestination(Vector3 destination) {
            navMeshAgent.SetDestination(destination);
        }
    
        public bool HasArrivedToDestination() {
            if (navMeshAgent.pathPending || navMeshAgent.remainingDistance > 0.5f)
                return false;

            // S'il croit être arrivé mais continue de glisser
            if (navMeshAgent.hasPath && navMeshAgent.velocity.sqrMagnitude > 0.05f)
                return false;

            return true;
        }
    }
}