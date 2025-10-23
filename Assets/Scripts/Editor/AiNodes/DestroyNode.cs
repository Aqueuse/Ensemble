using System;
using Behaviours;
using UnityEngine;

namespace Editor.AiNodes {
    [Serializable]
    public class DestroyNode : BaseNode {
        protected override void OnDefinePorts(IPortDefinitionContext context) {
            context.AddInputPort<bool>("TriggerIn").Build();
        }

        public override AiBlockExecutor ConvertToExecutor(AiRuntimeGraph aiRuntimeGraph) {
            var executor = ScriptableObject.CreateInstance<DestroyBlockExecutor>();
            executor.executorUuid = uuid;
            executor.name = typeof(DestroyBlockExecutor).ToString();
            
            return executor;
        }
    }
}