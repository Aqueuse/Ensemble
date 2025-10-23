using System;
using Behaviours;
using UnityEngine;

namespace Editor.AiNodes {
    [Serializable]
    public class EndNode : BaseNode {
        protected override void OnDefinePorts(IPortDefinitionContext context) {
            context.AddInputPort<bool>("TriggerIn").Build();
        }

        public override AiBlockExecutor ConvertToExecutor(AiRuntimeGraph aiRuntimeGraph) {
            var executor = ScriptableObject.CreateInstance<EndBlockExecutor>();
            executor.executorUuid = uuid;
            executor.name = typeof(EndBlockExecutor).ToString();
            
            return executor;
        }
    }
}