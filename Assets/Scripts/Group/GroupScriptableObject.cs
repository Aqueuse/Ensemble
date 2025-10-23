using System.Collections.Generic;
using UnityEngine;

namespace Group {
    [CreateAssetMenu(fileName = "Group", menuName = "IA/AI Group")]
    public class GroupScriptableObject : ScriptableObject {
        public Color groupColor;
        public List<Member.Member> members;
    }
}