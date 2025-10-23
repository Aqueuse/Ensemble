﻿using System.Threading.Tasks;

namespace Behaviours {
    public class WaitForTriggerBlockExecutor : AiBlockExecutor {
        public string triggeringBoolUuid;

        public override async Task Execute(GraphExecutor graphExecutor) {
            await WaitUntil(graphExecutor);
            
            if (!string.IsNullOrEmpty(outputUuidTrigger) && graphExecutor.runtimeGraph.boolData[outputUuidTrigger])
                await graphExecutor.runtimeGraph.executors[outputUuidTrigger].Execute(graphExecutor);
        }

        private async Task WaitUntil(GraphExecutor graphExecutor, int sleep = 50) {
            while (!graphExecutor.runtimeGraph.boolData[triggeringBoolUuid]) {
                await Task.Delay(sleep);
            }
        }
    }
}