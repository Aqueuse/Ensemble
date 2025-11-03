using System;
using System.Collections.Generic;
using Behaviours;
using Editor.FlowNoddles.variables;
using Unity.GraphToolkit.Editor;
using UnityEngine;

namespace Editor.FlowNoddles {
    [Serializable]
    public class EndNode : BaseNode {
        protected override void OnDefinePorts(IPortDefinitionContext context) {
            context.AddInputPort<bool>("DestroyYourself").Build();

            context.AddInputPort<bool>("TriggerIn").Build();
        }

        public override AiBlockExecutor ConvertToExecutor(AiRuntimeGraph aiRuntimeGraph) {
            var executor = ScriptableObject.CreateInstance<EndBlockExecutor>();
            executor.executorUuid = uuid;
            executor.name = typeof(EndBlockExecutor).ToString();
            
            var destroyUuidNode = (BoolVariableNode)GetInputPortByName("DestroyYourself").firstConnectedPort.GetNode();
            
            destroyUuidNode.GetNodeOptionByName("uuid").TryGetValue(value: out string destroyUuid);
            destroyUuidNode.GetNodeOptionByName("value").TryGetValue(value: out bool destroyUuidValue);

            executor.destroyYourselfUuid = destroyUuid;
            
            aiRuntimeGraph.boolData.TryAdd(destroyUuid, destroyUuidValue);
            
            return executor;
        }
    }
}