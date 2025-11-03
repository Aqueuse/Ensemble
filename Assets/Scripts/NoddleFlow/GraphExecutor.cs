using System.Linq;
using System.Threading.Tasks;
using Behaviours;
using UnityEngine;

public class GraphExecutor : MonoBehaviour {
    public AiRuntimeGraph runtimeGraph;
    
    private void Start() {
        _ = ExecuteGraph();
    }
    
    private async Task ExecuteGraph() {
        // récupérer le premier node = StartBlock
        foreach (var kvp in runtimeGraph.executors.Where(kvp => kvp.Value is StartBlockExecutor)) {
            await kvp.Value.Execute(this);
        }
    }
}