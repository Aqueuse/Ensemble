using Group;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager> {
    public Transform spawnTransform;
    public Transform guichetTransform;
    public Transform guichetGroupWaitingTransform;
    public Transform arrivalTransform;

    public GameObject groupPrefab;
    public GameObject memberPrefab;

    public GroupScriptableObject groupScriptableObject;

    private void Start() {
        SpawnGroup();
    }

    public void SpawnGroup() {
        var group = Instantiate(groupPrefab, spawnTransform.position, spawnTransform.rotation, null);
        group.GetComponent<GroupBehaviour>().SpawnMembers(groupScriptableObject);
    }
}
