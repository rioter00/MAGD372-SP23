# Systems

These systems are used for communication between different scripts. There are multiple ways to communicate information across scripts. The default method, to serialize a reference to the script, works but it also requires direct references to the scripts and they need to be set in the inspector in every scene. These systems on the other hand allow for each prefab to just be dropped in because they use asset files, not references to other prefabs.

This document will be changing a players score for context. Looking over the systems and the different applications of the systems that could be used for communication of the score between scripts.

## Event Bus

An event bus holds an event. The EventBus class is a ScriptableObject, so whenever you need a new event, you can create one in the project window:
`Right Click -> Create -> Events -> Event Bus`

### How to Use

Each event bus must be used in two ways otherwise it isn't doing anything.

---
 1. Event Caller

The event caller will be controlling when the event is activated. For instance, in a Score script, when points are collected:
```
public class Health : Monobehaviour 
{
	[SerializeField] private EventBus onScoreChange;
	private int score = 0;

	private void OnTriggerEnter(Collider other)
	{
		CollectPoints(1);
	}

	public void CollectPoints(int modification) {
		score += modification;
		ScoreEventArgs eventArgs = new ScoreEventArgs(score)
		onScoreChange.CallEvent(this, eventArgs);
	}
}

/* 
each eventbus uses a unique class for the arguments that inherit 
from EventArgs so the event handler can find which event is called
*/

public class ScoreEventArgs : EventArgs 
{
	// public variables to get any values needed by the event 
	public int Score { get; }
	
	// constructor to set the variables when created
	public ScoreEventArgs(int score) {
		Score = score;
	}
}
```
---
 2. Event Handler

The event handler will be actually managing what happens when the event is activated. Each EventBus requires at least one Event Handler. There will not be any errors if there is no event handler if invoked with the `?` as seen above.
```
public class UITextDisplay : Monobehaviour, IEventHandler 
{
	// EventBus from the Score class
	[SerializeField] private EventBus onScoreChange;
	// EventBus from Health class
	[SerializeField] private EventBus onDeath;

	private TextMeshProUGUI tmp;

	private void Awake() 
	{
		// get needed reference to component on the same gameobject
		tmp = GetComponent<TextMeshProUGUI>();
		/*
		multiple EventBuses can subscribe to the same handler, and 
		that handler will determine which event was called
		*/
		
		onScoreChange.Event += Handler;
		onDeath.Event += Handler;
	}

	// override from the IEventHandler interface, named generically
	private override void EventHandler(object sender, EventArgs e) 
	{
		/* 
		check which event was called through an 
		implicit downcasting of e
		*/
		
		if (e is ScoreEventArgs args)
		{
			ChangeScore(args);
			return;
		}
		
		if (e is DeathEventArgs args)
		{
			Die(args);
			return;
		}
	}

	private void ChangeScore(ScoreEventArgs args) {
		// Show new score on the text element
		tmp.text = args.Score.ToString();
	}

	private void Die(DeathEventArgs args)
	{
		// any logic for how player death affects score goes here
		tmp.text = "0";
	}
}
```

While the Health.cs is not explicitly shown, it can be inferred that the DeathEventArgs class does not contain any elements needed for the UI, though it might have elements being used somewhere else in another script that subscribed to the same event. 

If another event handler is subscribed to the onDeath event, it may need to know how long the cooldown is before respawning the player, so DeathEventArgs contains the following parameter:

`public int Cooldown { get; }`

Just because it exists within the arguments does not mean every implementation of the event needs to use the parameter in some way, which is why the Die() method above doesn't bother using the cooldown, as cooldown doesn't affect score at all.

However, if none of the implementations of an event are using the Cooldown variable, it would be appropriate to remove it from the DeathEventArgs class.

## Reference Variables

 If a value is used every frame in an Update or FixedUpdate method, then using a Reference Variable could be a better solution depending on the problem.
 
 Reference Variables are ScriptableObjects that contain data of the following types: bool, int, float, Vector2, and Vector3. To create a ReferenceVariable in the project window:
 `Right Click -> Create -> Variables -> Variable Type`

---
The variable's Value is shown in the inspector, and you can also click a box to reset the value on every playthrough. This can be used when you want the score to always start at whatever value you choose to reset to.

For setting the Value of a Variable Object, you simply serialize a reference to it, and set its value property:
```
public class Health : Monobehaviour 
{
	[SerializeField] private IntReference score;

	private void OnTriggerEnter(Collider other)
	{
		CollectPoints(1);
	}

	public void CollectPoints(int modification) {
		score.Value += modification;
	}
}
```

Similarly, to get the Value of a Variable Object, you serialize a reference to it and then get its Value property:
```
public class UITextDisplay : Monobehaviour, IEventHandler 
{
	[SerializeField] private IntReference score;
	[SerializeField] private EventBus onDeath;

	private TextMeshProUGUI tmp;

	private void Awake() 
	{
		// get needed reference to component on the same gameobject
		tmp = GetComponent<TextMeshProUGUI>();
		
		onDeath += EventHandler;
	}

	private override void EventHandler(object sender, EventArgs e) 
	{
		if (e is DeathEventArgs args) 
		{
			Die(args);
			return;
		}
	}

	private void Update() 
	{
		ChangeScore();
	}

	private void ChangeScore() {
		// Show new score on the text element
		tmp.text = score.Value.ToString();
	}

	public void Die(DeathEventArgs args)
	{
		// any logic for how player death affects score goes here
		tmp.text = "0";
	}
}
```

---
When serializing these primitive data types, don't use
```
[SerializeField] private bool booleanVal;
[SerializeField] private int integerNum;
[SerializeField] private float floatNum;
[SerializeField] private Vector2 vector2Val;
[SerializeField] private Vector3 vector3Val;
```
Instead, use the following data types:
```
[SerializeField] private BoolReference booleanVal;
[SerializeField] private IntReference integerNum;
[SerializeField] private FloatReference floatNum;
[SerializeField] private Vector2Reference vector2Val;
[SerializeField] private Vector3Reference vector3Val;
```
These types store their respective Variable objects on them, but can also store a constant value instead. By using these instead, if a player's speed is used by multiple scripts or multiple instances of a script, such as local multiplayer games might have multiple instances of a moving script, then by switching the type from Constant to Variable would allow for all player speeds to be modified at once by modifying the speed Variable Object (likely a FloatVariable). It's safer to use these for any serialized data fields so that no code needs to be modified if switching between them.
