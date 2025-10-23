using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Examples.MineurSimulator.Scripts {
    public class MineurSimulator : MonoBehaviour {
        [SerializeField] private List<Transform> caillouxTransformList;
        [SerializeField] private Transform targetTransform;

        public GraphExecutor nainIA;

        public void GoToMine() {
            targetTransform.position = ChooseRandomCaillou().position;

            Vector3 nainScale = nainIA.GetComponentInChildren<Transform>().localScale;
            nainScale.x = Math.Abs(nainScale.x);
            nainIA.GetComponentInChildren<Transform>().localScale = nainScale;
        
            //nainIA.TriggerEvent("go_to_mine");
        }
    
        private Transform ChooseRandomCaillou() {
            return caillouxTransformList[Random.Range(0, caillouxTransformList.Count)];
        }
    }
}
