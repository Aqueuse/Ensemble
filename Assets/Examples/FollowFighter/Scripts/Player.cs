using UnityEngine;

namespace Examples.FollowFighter.Scripts {
    public class Player : MonoBehaviour {
        public float moveSpeed;
        [SerializeField] private GraphExecutor followerBehaviour;

        private Vector3 moveInput;
        private Vector3 move;
    
        private void Update() {
            moveInput.x = Input.GetAxisRaw("Horizontal"); // Gauche/Droite
            moveInput.y = Input.GetAxisRaw("Vertical"); // Haut/Bas

            move = new Vector3(moveInput.x, 0f, moveInput.y).normalized;
        
            transform.position += move * (moveSpeed * Time.fixedDeltaTime);

            if (Input.GetKeyDown(KeyCode.Space)) {
                //followerBehaviour.TriggerEvent("go_fight_cube");
            }
        }
    }
}