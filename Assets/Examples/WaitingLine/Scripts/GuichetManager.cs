using UI;
using UnityEngine;

namespace Examples.WaitingLine.Scripts {
    public class GuichetManager : MonoBehaviour {
        [SerializeField] private Transform ActiveApplicantTransform;

        [SerializeField] private Transform waitingLineContainer;
        [SerializeField] private SnakeLayoutGroup waitingLineLayoutGroup;
        
        private void AddToTheQueue(Transform newApplicant) {
            
        }

        private void RemoveFromTheQueue(Transform applicantToRemove) {
            
        }
        
        private void SubmitNextApplicant(Transform nextApplicant) {
            
        }

        public void AcceptApplicant(Transform applicant) {
            
        }

        public void RejectApplicant(Transform applicant) {
            
        }

        public void RejectAllApplicants() {
            
        }
    }
}
