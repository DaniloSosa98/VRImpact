using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This Script is attached to each floater game object, where it manages the texture, display, and lifetime of the
/// floater. It handles the movement updates for drifting, and the opacity changes at the start and end of life,
/// including destruction.
///
/// Note, this class holds no intentionally configurable data. All configuration occurs in the FloaterManager.
/// Instead, this holds only instance data about the current state of the individual floater.
/// </summary>
public class EyeFloater : MonoBehaviour
{
	// used to manage the fade in/out effect
	private enum LifeState { Starting, Running, Ending }
	
	// a reference to the manager to keep track of min/max speeds, fade duration, etc
	private FloaterManager manager;
	// the texture used for the floater
	public Image texture;
	
	private LifeState curState; // denotes which direction to send alpha
	private Color curColor; // used for quick changes to alpha without re-fetching the color
	private Vector3 curSpeed; // screen units / second
	private float curAngSpeed; // degrees / second
	// this determines if, after fading out, should the floater move elsewhere and come back, or destroy itself?
	private bool respawnFloater = true;
	
	private RectTransform _transform; // cache for efficient access
	private RectTransform canvas; // canvas transform
	
	// Start is called before the first frame update
	private void Start()
	{
		// fetch components
		manager = GetComponentInParent<FloaterManager>();
		_transform = (RectTransform) transform;
		canvas = (RectTransform) GetComponentInParent<Canvas>().transform;
		
		// intent is to fade it in
		curState = LifeState.Starting;
		// set the texture
		texture.sprite = ProjectUtil.RandomFrom(manager.floaterTextures);
		// cache and set the initial color
		curColor = texture.color;
		curColor.a = 0;
		texture.color = curColor;
		// determine initial position
		Vector3 pos = _transform.localPosition;
		pos.x = Random.Range(canvas.offsetMin.x, canvas.offsetMax.x) * 3 / 4;
		pos.y = Random.Range(canvas.offsetMin.y, canvas.offsetMax.y) * 3 / 4;
		_transform.localPosition = pos;
		// determine initial speed
		curSpeed = Vector3.zero;
		curSpeed.x = Random.Range(-1, 1);
		curSpeed.y = Random.Range(-1, 1);
		curSpeed = curSpeed.normalized * Random.Range(manager.minSpeed, manager.maxSpeed);
		// determine initial angular speed
		curAngSpeed = Random.Range(manager.minAngSpeed, manager.maxAngSpeed);
	}
	
	// Update is called once per frame
	// here is where we update the opacity, position (including edge bounces and speed recalc), and rotation
	private void Update()
	{
		// update opacity if relevant
		if (curState != LifeState.Running)
		{
			// the fade amount this frame is the time passed over the total time expected, since alpha is 0 to 1
			float fadeAmount = Time.deltaTime / manager.fadeTime;
			// invert for ending
			if (curState == LifeState.Ending) fadeAmount *= -1;
			
			// apply fade
			curColor.a += fadeAmount;
			
			// manage state changes
			if (curColor.a >= 1) // opacity full, entrance completed
				curState = LifeState.Running;
			else if (curColor.a <= 0) // opacity 0, lifetime done
			{
				if(respawnFloater) RespawnFloater();
				else Destroy(gameObject);
			}
			
			// clamp and finalize
			curColor.a = Mathf.Clamp(curColor.a, 0, 1);
			texture.color = curColor;
		}
		
		// update position and physics
		
		Vector3 pos = _transform.localPosition;
		// movement
		pos += curSpeed * Time.deltaTime;
		// check for boundary break
		if (pos.x < canvas.offsetMin.x || pos.y < canvas.offsetMin.y
			|| pos.x > canvas.offsetMax.x || pos.y > canvas.offsetMax.y)
			curState = LifeState.Ending; // fade it out
		
		// finalize position
		_transform.localPosition = pos;
		// apply rotation
		_transform.Rotate(Vector3.forward, curAngSpeed * Time.deltaTime);
	}
	
	// called to begin fade out animation
	public void DestroyFloater()
	{
		respawnFloater = false;
		curState = LifeState.Ending;
	}
	
	// the floater reached the end of life at the edge of the screen; move it somewhere else and change it up again
	private void RespawnFloater()
	{
		// change the texture
		texture.sprite = ProjectUtil.RandomFrom(manager.floaterTextures);
		// move to a random point on the side of the screen
		Vector3 pos = _transform.localPosition;
		int side = Random.Range(0, 4);
		bool fromX = side % 2 == 0;
		bool positive = side / 2 == 0;
		if (fromX)
		{
			pos.x = positive ? canvas.offsetMax.x : canvas.offsetMin.x;
			pos.y = Random.Range(canvas.offsetMin.y, canvas.offsetMax.y);
		}
		else
		{
			pos.x = Random.Range(canvas.offsetMin.x, canvas.offsetMax.x);
			pos.y = positive ? canvas.offsetMax.y : canvas.offsetMin.y;
		}
		// commit position
		_transform.localPosition = pos;
		
		// determine speed and direction; point it in a 90-degree cone towards the center of the screen
		float mainAngle = 0;
		if (!fromX) mainAngle += 90;
		if (positive) mainAngle += 180;
		float angle = Random.Range(mainAngle - 45, mainAngle + 45);
		curSpeed.Set(Mathf.Cos(angle), Mathf.Sin(angle), 0);
		curSpeed *= Random.Range(manager.minSpeed, manager.maxSpeed);
		// recalc angular speed
		curAngSpeed = Random.Range(manager.minAngSpeed, manager.maxAngSpeed);
		
		// finally, update state
		curState = LifeState.Starting;
	}
}
