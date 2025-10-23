using System.Threading.Tasks;

namespace Behaviours {
    public class DestroyBlockExecutor : AiBlockExecutor {
        public override async Task Execute(GraphExecutor graphExecutor) {
            await Task.CompletedTask;
            Destroy(graphExecutor.gameObject);
        }
    }
}