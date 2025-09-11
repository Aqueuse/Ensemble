using System.Collections;
using IA.IA;
using UnityEngine;

namespace IA.Behaviours {
    [CreateAssetMenu(fileName = "WaitToSwitchBlock", menuName = "IA/BehaviourBlocks/Wait To Switch")]
    public class WaitToSwitchBlock : AIBlock {
        [SerializeField] private AISequence[] possibleSequences;
        [SerializeField] private bool isLooping;
        
        private bool _shouldSwitch;
        private int _targetIndex = -1;

        public override IEnumerator Execute(AIContext ctx) {
            _shouldSwitch = false;
            _targetIndex = -1;

            // Attente jusqu'à ce qu’on reçoive un ordre de switch
            yield return new WaitUntil(() => _shouldSwitch);

            if (_targetIndex >= 0 && _targetIndex < possibleSequences.Length) {
                var sequenceRunner = ctx.Agent.GetComponent<BehaviourSequence>();
                if (sequenceRunner != null) {
                    // On applique la séquence choisie
                    sequenceRunner.SetSequence(possibleSequences[_targetIndex]);
                }
            }
        }

        /// <summary>
        /// Appelé de l’extérieur (UI, manager, autre IA) pour déclencher le switch.
        /// </summary>
        public void TriggerSwitch(int index) {
            _targetIndex = index;
            _shouldSwitch = true;
        }
    }
}