using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class MoveController : MonoBehaviour {
	public StateMgr stateMgr;
	public Vector2 speedCoef = new Vector2(3.0f, 3.0f);
	[HideInInspector]
	public Vector2 currVel = Vector2.zero;
	[HideInInspector]
	public Vector2 lastVel = Vector2.zero;
//	[HideInInspector]
	public bool grounded = false;
	public bool walled = false;

	public float movingAccel = 0.2f;
    public float movingDecel = 0.2f;
    public float gravity = 6.0f;
    public float padding = 0.005f;
    public int verticalRay = 3;
    public int horizontalRay = 3;
    public bool manual = false;
	public float antiMoveCoef = 0.2f;

	public LayerMask layerMask;

	public Vector2 movingIntensitive = Vector2.right;
	private GameState gameState;  
//	private Rect box;
	private BoxCollider2D coll;
	private Vector3 pos;
	private Vector2 c;
	private Vector2 s;
	private Vector2 newPos;
	void Start () {
		coll = GetComponent<BoxCollider2D>();
        if(coll == null) {
            Debug.LogError("Can not find collider");
        }
		c = coll.offset * 0.5f;
		s = coll.size;
	}
    void FixedUpdate () {		
		if (stateMgr) {
			gameState = stateMgr.State;	
		} else {
			gameState = GameState.PLAYING;
		}
		if (gameState != GameState.PLAYING) {
			return;
		}        
		CalculateNewPos();
	}
    private void LateUpdate()
    {        
		transform.Translate(newPos.x, newPos.y, 0);
    }

    void CalculateNewPos() {		
		movingIntensitive = GetInputAxis();
		pos = transform.position;
		float targetSpeed = movingIntensitive.x * speedCoef.x;
		if(grounded) {
			currVel.y = 0;
		}
		if (walled || !grounded) {
			Debug.Log ("Wall - Hoope");
			currVel.x *= antiMoveCoef;
			targetSpeed *= antiMoveCoef;
			currVel.y -= gravity * Time.deltaTime / antiMoveCoef;
		} else {
			currVel.y -= gravity * Time.deltaTime;
		}
		currVel.x = CalculateMovingAmount (currVel.x, targetSpeed, movingAccel);

		newPos = ApplyVel (currVel.x, currVel.y);
    }
	Vector2 ApplyVel(float vx, float vy) {
		Vector2 newPos;  
		float delX = vx * Time.deltaTime;
		float delY = vy * Time.deltaTime;

		float dirY = Mathf.Sign (delY);
		float dirX = Mathf.Sign (delX);
		grounded = false;
		Ray2D ray;
		RaycastHit2D hit;
		for (int i = 0; i < verticalRay; i++) {
			float posX = (pos.x + c.x - s.x / 2) + (s.x / 2) / (verticalRay - 1) * i;
			float posY = pos.y + c.x -(s.y / 2) + (s.y / 2) * dirY;
			Vector2 org = new Vector2 (posX, posY);
			ray = new Ray2D (org, new Vector2(0, dirY));
			Debug.DrawRay(ray.origin, ray.direction * (Mathf.Abs (delY) + padding));
			hit = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Abs (delY) + padding, layerMask);
			if (hit.collider != null) {
				float dist = hit.distance;
//				Debug.Log ("castLen " + Mathf.Abs (delY) + padding + " ; Dist: " + dist);
				if (dist > padding) {
//					Debug.Log ("Shinking " + dist);
					delY = (dist - padding) * (dirY);
				} else {
//					Debug.Log ("Standing " + dist);
					delY = 0;
				}
				grounded = true;
				break;
			}
		}
		walled = false;
		int count = 0;
		for (int i = 0; i < horizontalRay; i++) {
			float posX = pos.x + c.x + s.y/2 * dirX;
			float posY = (pos.y + c.y - s.y / 2) + s.y / (horizontalRay - 1) * i;
			Vector2 org = new Vector2 (posX, posY);
			ray = new Ray2D (org, new Vector2(dirX, 0));
			Debug.DrawRay(ray.origin, ray.direction * delX);
			hit = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Abs (delX) + padding, layerMask);
			if (hit.collider != null) {				
				count++;
				if (count > 2) {
					float dist = hit.distance;
					if (dist > padding) {
						delX = (dist - padding) * dirY;
					} else {
						delX = 0;
					}
					walled = true;
					break;
				}
			}
		}
		newPos = new Vector2 (delX, delY);
		return newPos;
	}
    public static float CalculateMovingAmount(float curAmount, float targetAmount, float accel) {
		float result = .0f;
		result = Mathf.Lerp (curAmount, targetAmount, accel);
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
