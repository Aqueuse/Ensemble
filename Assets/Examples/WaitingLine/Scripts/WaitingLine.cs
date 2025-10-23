using UnityEngine;
using UnityEngine.AI;

namespace Examples.WaitingLine.Scripts {
    public class WaitingLine : MonoBehaviour {
        [SerializeField] private GameObject visitorPrefab;

        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Transform arrivalPoint;
        
        public void SpawnNewGroup() {
            for (int i = 0; i < 10; i++) {
                var visitor = Instantiate(visitorPrefab, spawnPoint.position, Quaternion.identity);
                var visitorAgent = visitor.GetComponent<NavMeshAgent>();
                
                if (NavMesh.SamplePosition(visitorAgent.transform.position, out NavMeshHit hit, 2, NavMesh.AllAreas)) {
                    visitorAgent.Warp(hit.position); // place directement l’agent sur le NavMesh
                }
            }
        }
    }
}
