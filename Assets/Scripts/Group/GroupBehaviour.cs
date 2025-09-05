using System.Collections.Generic;
using Member;
using UnityEngine;
using UnityEngine.AI;

namespace Group {
    public class GroupBehaviour : MonoBehaviour {
        [HideInInspector] public List<MemberBehaviour> members = new ();

        private GroupState groupState = GroupState.SPAWNING;
    
        public void SpawnMembers(GroupScriptableObject groupScriptableObject) {
            foreach (var unused in groupScriptableObject.members) {
                if (NavMesh.SamplePosition(GameManager.instance.spawnTransform.position, out NavMeshHit hit, 1.0f, NavMesh.AllAreas)) {
                
                    var member = Instantiate(GameManager.instance.memberPrefab, hit.position, Quaternion.identity).GetComponent<MemberBehaviour>();
                    member.Init(groupScriptableObject.groupColor, this);
                    members.Add(member);
                }
                else {
                    Debug.LogWarning($"Impossible de placer un membre du groupe {groupScriptableObject.name} sur le NavMesh !");
                }
            }
        
            InvokeRepeating(nameof(CheckGroupArrival), 0.1f, 0.2f);
        }
        
        public void CheckGroupArrival() {
            Debug.Log(groupState);
        
            if (groupState == GroupState.SPAWNING) {
                foreach (var memberBehaviour in members) {
                    if (!memberBehaviour.HasArrivedToDestination())
                        return;
                }

                groupState = GroupState.GO_TO_GUICHET;
                transform.position = GameManager.instance.guichetTransform.position;

                foreach (var memberBehaviour in members) {
                    memberBehaviour.SetDestination(transform.position);
                }
            
                return;
            }

            if (groupState == GroupState.GO_TO_GUICHET) {
                foreach (var memberBehaviour in members) {
                    if (!memberBehaviour.HasArrivedToDestination())
                        return;
                }

                groupState = GroupState.GUICHET_HANDLING;
            
                // we switch to Handing
            
                // after that we will go to arrival
            }

            if (groupState == GroupState.GO_TO_ARRIVAL) {
                foreach (var memberBehaviour in members) {
                    if (!memberBehaviour.HasArrivedToDestination())
                        return;
                }
            
                // everyone is here ? Okay, bye bye station !
                foreach (var memberBehaviour in members) {
                    Destroy(memberBehaviour.gameObject);
                }
        
                CancelInvoke(nameof(CheckGroupArrival));
                Destroy(gameObject);
            }
        }

        private void GuichetHandling() { // ¯\(°_o)/¯
        
            // check member count / for each activate one transform where the member will go and wait
        
            // the first member will be linked to the guichet for the interaction
        
            // when the first member has paid is ticket
            // move it to waiting tranform
            // remove the transform ???? each member would then just stick to his transform as he move forward
            // assign the new first member to the ticket 
            // rince and repeat
            
            // when the last member has paid is ticket, switch group state to go_to_arrival and quit
        }
    }
}