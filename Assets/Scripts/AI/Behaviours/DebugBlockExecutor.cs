using System.Threading.Tasks;
using Unity.GraphToolkit.Editor;
using UnityEngine;

namespace Behaviours {
    public class DebugBlockExecutor : AiBlockExecutor {
        public string messageVariableUuid;

        public override async Task Execute(GraphExecutor graphExecutor) {
            Debug.Log($"[Debug] {graphExecutor.runtimeGraph.stringData[messageVariableUuid]}");
            
            await graphExecutor.runtimeGraph.executors[outputUuidTrigger].Execute(graphExecutor);
        }
    }
}