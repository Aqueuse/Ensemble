using System.Collections.Generic;
using UnityEngine;

namespace Group {
    [CreateAssetMenu]
    public class GroupScriptableObject : ScriptableObject {
        public Color groupColor;
        public List<Member.Member> members;
    }
}