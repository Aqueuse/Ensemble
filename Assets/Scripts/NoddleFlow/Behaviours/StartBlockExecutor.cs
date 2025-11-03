using System.Threading.Tasks;
using UnityEngine;

namespace Behaviours {
    public class StartBlockExecutor : AiBlockExecutor {
        public override async Task Execute(GraphExecutor graphExecutor) {
            await Task.Delay(10);
            await graphExecutor.runtimeGraph.executors[outputUuidTrigger].Execute(graphExecutor);
        }
   }
}