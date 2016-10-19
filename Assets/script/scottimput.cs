using UnityEngine;
using System.Collections;

public class scottimput : MonoBehaviour {
	public float walkSpeed;
	public float jumpImpulse;
	public Transform groundCheckPoint;
	public LayerMask whatIsGround;
	private Rigidbody2D body;
	private Vector2 movement;

	private float horInput;
	private bool jumpImput;
	private bool weAreInTheGround;
	private bool facingRight;

	private Animator anim;
	// Use this for initialization
	void Start () {
		this.body = this.GetComponent<Rigidbody2D> ();
		this.movement = new Vector2 ();
		this.weAreInTheGround = false;
		this.anim = this.GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void Update () {
		this.horInput = Input.GetAxis ("Horizontal");
		this.jumpImput = Input.GetKey (KeyCode.Space);
		this.anim.SetFloat ("HorizontalSpeed", Mathf.Abs(horInput));
		this.anim.SetFloat ("VerticalSpeed", Mathf.Abs(this.body.velocity.y));
		if (Physics2D.OverlapCircle (this.groundCheckPoint.position, 0.02f, this.whatIsGround)) {
			this.weAreInTheGround = true;
		} else {
			this.weAreInTheGround = false;
		}	
		if((this.horInput>0)&&(facingRight)){
			this.Flip ();
			this.facingRight = false;
		}else if((this.horInput<0)&&(!facingRight)){
			this.Flip();
			this.facingRight =true;
		}
	}
	void FixedUpdate(){
		this.movement = this.body.velocity;

		this.movement.x = horInput * walkSpeed;
		if(this.jumpImput==true && this.weAreInTheGround==true){
			
			this.movement.y = jumpImpulse;	

		}
		this.body.velocity = this.movement;
	}
	void Flip(){
		Vector3 scale = this.transform.localScale;
		scale.x *= (-1);
		this.transform.localScale = scale;
	}
}
