using System.Threading.Tasks;

namespace Behaviours {
    public class EndBlockExecutor : AiBlockExecutor {
        public override async Task Execute(GraphExecutor graphExecutor) {
            await Task.CompletedTask;
        }
    }
}