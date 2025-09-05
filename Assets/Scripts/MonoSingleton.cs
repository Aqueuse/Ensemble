using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour {
    public static T instance;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this as T;
        }

        Init();
    }

    private void Init() { }

    private void OnApplicationQuit() {
        instance = null;
    }
}