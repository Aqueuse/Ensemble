# BehaviourSequence 🐒

A lightweight **sequencer for AI behaviours in Unity**, designed for clarity, flexibility, and shareability.  
It avoids "spaghetti Update()" with if/else and instead lets you define **blocks of actions** that run in order.

---

## ✨ Features

- Define a list of **blocks** directly in the Unity inspector.
- Supports **looping sequences** or **one-time sequences** (toggle with `isLooping`).
- Includes core AI blocks:
    - **FollowTransform** → follow a target transform (e.g. group anchor).
    - **GoToVector3** → move to a specific position.
    - **PlayAnimation** → trigger an Animator state and wait until it ends.
    - **WaitForEvent** → pause until a custom event is triggered.
- Clean event system:
    - Use `TriggerEvent(string name)` to unblock `WaitForEvent` actions.
- Modular: add your own blocks easily.

---

## 🚀 Usage

1. Add `BehaviourSequence` to your agent.
2. Configure the **list of blocks** in the inspector.
3. Choose **Looping** or **One-Time** via the `isLooping` checkbox.

### Example: Group Following

```text
Blocks:
1. FollowTransform (target = groupAnchor)
2. WaitForEvent (eventName = HasGroupMoved)
[loop enabled]
→ Agent follows the group, waits for a move event, then continues looping.
```

### Example: One-Time Activity

```text
Blocks:
1. GoToVector3 (target = activity spot)
2. PlayAnimation (trigger = Eat)
[loop disabled]
→ Agent goes to the activity, plays animation, then sequence ends.
The activity can then re-enable the default loop behaviour.
```

## 🎬 Animation Handling
*PlayAnimation* block triggers an Animator parameter (e.g. trigger `"Eat"`).

The sequence automatically waits until the animation finishes.

Done via Unity’s built-in *OnStateExit* (using a `StateMachineBehaviour`):

```csharp
public class NotifyOnExit : StateMachineBehaviour {
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        var seq = animator.GetComponent<BehaviourSequence>();
        if (seq != null) seq.OnAnimationEnd();
    }
}
```

Attach `NotifyOnExit` to the relevant Animator states.

## 📡 Events
Use the *WaitForEvent* block to pause until a signal is received.

Example:
In the sequence: `WaitForEvent (eventName = HasGroupMoved)`
From another script:

```csharp
myAgent.GetComponent<BehaviourSequence>().TriggerEvent("HasGroupMoved");
```

### 🔁 Loop vs One-Time

*Looping* (`isLooping = true`):
- When the sequence reaches the end, it restarts at the first block.
- Perfect for continuous behaviours (like following a group).

*One-Time* (`isLooping = false`):
- When the sequence ends, it invokes `onSequenceComplete` and stops.
- Perfect for activities (like eating a banana).

### 🛠️ Extending

To add a new block type:

1. Extend the `AIBlockType` enum.
2. Add fields in `AIBlock` if needed.
3. Handle it in the switch inside `BehaviourSequence.RunSequence()`.

## ❤️ Contribute

This is a lightweight system, ideal for prototyping or simple AI.

If you like it, please buy me a kofi : https://ko-fi.com/aqueuse ☕

If you improve or extend it (new block types, bug fixes…), please open a PR! 🐒