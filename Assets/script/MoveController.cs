using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour {
	public Vector2 maxVel = new Vector2(3.0f, 3.0f);
    public Vector2 nextPos = Vector2.zero;
	public float movingAccel = 0.2f;
    public float movingDecel = 0.2f;
    public float gravity = 6.0f;
    public float maxFall = 10.0f;
    public bool falling = false;
    public bool grounded = false;
    public Vector2 movingIntensitive = Vector2.right;
    public float margin = 0.1f;
    public int verticalRay = 4;
    public int horizontalRay = 4;
    public bool manual = false;
	private GameState gameState;
    public Vector2 lastVel = Vector2.zero;
    private int layerMask;
    private Rect box;
    private Collider2D coldr;
	void Start () {
        layerMask = LayerMask.NameToLayer("ground");
        coldr = GetComponent<Collider2D>();
        if(coldr == null) {
            Debug.LogError("Can not find collider");
        }
	}
    void FixedUpdate () {		
        gameState = StateMgr.instance.State;	
		if (gameState != GameState.PLAYING) {
			return;
		}
        CalculateVel();
        box = new Rect(coldr.bounds.min.x,
                       coldr.bounds.min.y,
                       coldr.bounds.size.x,
                       coldr.bounds.size.y);
        if(!grounded) {
            lastVel = new Vector2(lastVel.x, Mathf.Max(lastVel.y - gravity, -maxFall));
        }

        if(lastVel.y < 0) {
            falling = true;
        }
        if (grounded || falling) {
            Vector2 startPoint = new Vector3(box.xMin + margin, box.center.y);//, transform.position.z);
            Vector2 endPoint = new Vector3(box.xMax - margin, box.center.y);//, transform.position.z);
            RaycastHit2D hitInfo;
            float distance = box.height / 2 + (grounded ? margin : Mathf.Abs(lastVel.y * Time.deltaTime));
            bool connected = false;
            for (int i = 0; i < verticalRay; i++) {
                float lerpAmount = (float)i / (float)verticalRay - 1;
                Vector2 origin = Vector2.Lerp(startPoint, endPoint, lerpAmount);
                Ray2D ray = new Ray2D(origin, Vector2.down);
                hitInfo = Physics2D.Raycast(origin, Vector2.down, distance);
                connected = (hitInfo.collider != null);
                if(connected) {
                    grounded = true;
                    falling = false;
                    if(manual)
                        transform.Translate(0.0f, (Vector2.down * (hitInfo.distance - box.height / 2)).y, 0.0f);
                    lastVel = new Vector2(lastVel.x, 0);
                    break;
                }
            }
            if(!connected) {
                grounded = false;
            }
        }

	}
    private void LateUpdate()
    {
        if (!manual)
        {
            lastVel = new Vector2(lastVel.x, 0);
        }
		transform.Translate(lastVel * Time.deltaTime);
    }
    void Natural() {
        
    }
    void CalculateVel() {
		Vector2 currMovingIntensitive = GetInputAxis();
		float currIntenX = currMovingIntensitive.x;
		float currIntenY = currMovingIntensitive.y;
		float lastIntenX = movingIntensitive.x;
		float lastIntenY = movingIntensitive.y;
		float currVelX = 0.0f;
		float currVelY = 0.0f;
		currVelX = CalculateMovingAmount(currIntenX, lastIntenX, lastVel.x, maxVel.x * currMovingIntensitive.normalized.x);
		currVelY = CalculateMovingAmount(currIntenY, lastIntenY, lastVel.y, maxVel.y * currMovingIntensitive.normalized.y);

		lastVel = new Vector2(currVelX, currVelY);

		movingIntensitive = currMovingIntensitive;
    }
    float CalculateMovingAmount(float currIntent, float lastIntent, float currVel, float maxAmount) {
        float result = 0.0f;
		if (currIntent * lastIntent < 0.0f)
		{
            Debug.Log("From " + currVel + " to " + (-maxAmount)); 
            result = Mathf.Lerp(currVel, -maxAmount, Time.deltaTime * movingDecel);
		}
        else 
		{
			result = Mathf.Lerp(currVel, maxAmount, Time.deltaTime * movingAccel);
		}
        return result;
    }
	Vector2 GetInputAxis () {
	#if UNITY_EDITOR
		return new Vector2 (
			Input.GetAxis("Horizontal"), 
			Input.GetAxis("Vertical")
		);
	#else
		return new Vector2 (
			Input.acceleration.x,
			Input.acceleration.y
		);
	#endif
	}
}
