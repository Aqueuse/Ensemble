using System.Threading.Tasks;

namespace Behaviours {
    public class EndBlockExecutor : AiBlockExecutor {
        public string destroyYourselfUuid;
        
        public override async Task Execute(GraphExecutor graphExecutor) {
            await Task.CompletedTask;

            if (graphExecutor.runtimeGraph.boolData[destroyYourselfUuid]) {
                Destroy(graphExecutor.gameObject);
            }
        }
    }
}