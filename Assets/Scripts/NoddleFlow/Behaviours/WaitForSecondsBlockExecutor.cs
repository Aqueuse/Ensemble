using System.Threading.Tasks;

namespace Behaviours {
    public class WaitForSecondsBlockExecutor : AiBlockExecutor {
        public string secondsVariableUuid;
        
        public override async Task Execute(GraphExecutor graphExecutor) {
            if (graphExecutor.runtimeGraph.intData.TryGetValue(secondsVariableUuid, out var dataExecutor)) {
                await Task.Delay(dataExecutor * 1000);
            }
            
            await graphExecutor.runtimeGraph.executors[outputUuidTrigger].Execute(graphExecutor);
        }
    }
}