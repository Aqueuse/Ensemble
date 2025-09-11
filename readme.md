# Ensemble 🐒🐒🐒

A lightweight **sequencer for AI behaviours in Unity**, designed for clarity, flexibility, and modularity.
It avoids "spaghetti Update()" with if/else and instead lets you define **blocks of actions** that run in order.

---

## ✨ Features

* Define a list of **AIBlock ScriptableObjects** directly in the Unity inspector.
* Supports **looping sequences** or **one-time sequences** (`isLooping`).
* Includes core AI blocks:

    * **FollowTransform** → follow a target until an event stops it.
    * **GoToTransform** → move to a specific `AITarget` and stop when reached.
    * **PlayAnimation** → trigger an Animator state and wait until it ends.
    * **WaitForEvent** → pause until a custom event is triggered.
* Clean event system:

    * Use `TriggerEvent(string name)` to signal or stop behaviours.
* Modular: add your own blocks easily by extending `AIBlock`.

---

## 🚀 Usage

1. Add `BehaviourSequence` to your agent.
2. Configure the **list of blocks** in the inspector.
3. Choose **Looping** or **One-Time** via the `isLooping` checkbox.

### Example: Following the Player

```text
Blocks:
1. FollowTransform (target = player, stopOnEvent = go_fight_monster)
2. GoToTransform (target = monster)
[loop enabled]
→ Agent follows the player until "go_fight_monster" is triggered,
   then moves to the monster.
```

### Example: Activity

```text
Blocks:
1. GoToTransform (target = banana spot)
2. PlayAnimation (trigger = EatBanana)
[loop disabled]
→ Agent goes to the banana spot, plays an animation, then stops.
```

---

## 🎬 Animation Handling

*PlayAnimationBlock* triggers an Animator parameter (e.g. trigger `"EatBanana"`).
The sequence automatically waits until the animation finishes.

Done via Unity’s built-in *OnStateExit* (using a `StateMachineBehaviour`):

```csharp
public class NotifyOnExit : StateMachineBehaviour {
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        var currentBlock = animator.GetComponent<BehaviourSequence>().currentBlock;
        if (currentBlock is IA.Behaviours.PlayAnimationBlock playBlock) {
            playBlock.ended = true;
        }
    }
}
```

Attach `NotifyOnExit` to the relevant Animator states.

---

## 📡 Events

Events are central to this system:

* Use them to **stop a block** (`FollowTransform` with `stopOnEvent`)
* Or to **unblock waiting** (`WaitForEvent`).

Example from another script (UI Button, trigger, etc.):

```csharp
myAgent.GetComponent<BehaviourSequence>().TriggerEvent("go_fight_monster");
```

BehaviourSequence will then:

* Stop the current block if it matches its `stopOnEvent`.
* Or resume a waiting block if it was paused.

---

### 🔁 Loop vs One-Time

*Looping* (`isLooping = true`):

* When the sequence reaches the end, it restarts at the first block.
* Perfect for continuous behaviours (like patrolling or following).

*One-Time* (`isLooping = false`):

* When the sequence ends, it simply stops.
* Perfect for single-shot behaviours (like playing an animation or an activity).

---

## 🛠️ Extending

To add a new block type:

1. Create a new `ScriptableObject` class inheriting from `AIBlock`.
2. Implement the `IEnumerator Execute(AIContext ctx)` method.
3. Drop it into your sequence list in the inspector.

---

## ❤️ Contribute

This is a lightweight system, ideal for prototyping or simple AI.
If you like it, please buy me a kofi : [https://ko-fi.com/aqueuse](https://ko-fi.com/aqueuse) ☕

If you improve or extend it (new block types, bug fixes…), please open a PR! 🐒
