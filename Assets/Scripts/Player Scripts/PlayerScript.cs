using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
	public static PlayerScript instance;

	private float speed = 8.0f;
	private float maxVelocity = 4.0f;

	private bool canWalk;
	private bool moveLeft, moveRight;
	private float height;
	private Button shootBtn;
	private bool shootOnce, shootTwice;

	[SerializeField] private AnimationClip Clip;
	[SerializeField] private AudioClip shootClip;
	[SerializeField] private Rigidbody2D myRigidBody;
    [SerializeField] private Animator myAnimator;
	[SerializeField] private GameObject[] arrows;
	[SerializeField] private GameObject shield;
	private string arrow;
	public bool hasShield, isInvincible, singleArrow, doubleArrows,
		singleStickyArrow, doubleStickyArrows, shootFirstArrow, shootSecondArrow;
	public delegate void Explode(bool touchedGoldBall);
	public static event Explode explode;

	private void Awake()
    {
		InitializePlayer();
		if (instance == null)
			instance = this;
		float cameraHeight = Camera.main.orthographicSize;
		height = -cameraHeight - 0.8f;
		shootBtn = GameObject.FindGameObjectWithTag("ShootButton").GetComponent<Button>();
		shootBtn.onClick.AddListener(() => ShootTheArrow());
	}

    private void FixedUpdate()
    {
		PlayerWalkKeyboard();
		MoveThePlayer();
    }
	void InitializePlayer()
	{
		canWalk = true;

		switch (GameController.instance.selectedWeapon)
		{
			case 0:
				arrow = "Arrow";
				shootOnce = true;
				shootTwice = false;

				singleArrow = true;
				doubleArrows = false;
				singleStickyArrow = false;
				doubleStickyArrows = false;

				break;
			case 1:

				arrow = "Arrow";
				shootOnce = true;
				shootTwice = true;

				singleArrow = false;
				doubleArrows = true;
				singleStickyArrow = false;
				doubleStickyArrows = false;

				break;
			case 2:

				arrow = "StickyArrow";
				shootOnce = true;
				shootTwice = false;

				singleArrow = false;
				doubleArrows = false;
				singleStickyArrow = true;
				doubleStickyArrows = false;

				break;
			case 3:

				arrow = "StickyArrow";
				shootOnce = true;
				shootTwice = true;

				singleArrow = false;
				doubleArrows = false;
				singleStickyArrow = false;
				doubleStickyArrows = true;

				break;


		}

		Vector3 bottomBrick = GameObject.FindGameObjectWithTag("BottomBrick").transform.position;
		Vector3 temp = transform.position;

		switch (gameObject.name)
		{

			case "Homosapien(Clone)":
				temp.y = -3f;
				break;

			case "Joker(Clone)":
				temp.y = -3f;
				break;

			case "Spartan(Clone)":
				temp.y = -3f;
				break;

			case "Pirate(Clone)":
				temp.y = -3f;
				break;

			case "Player(Clone)":
				temp.y = -3f;
				break;

			case "Zombie(Clone)":
				temp.y = -3f;
				break;

		}   // switch
		transform.position = temp;


	}
	private void Update()
    {

    }
	public void PlayerShootOnce(bool shootOnce)
	{
		this.shootOnce = shootOnce;
		shootFirstArrow = false;
	}
	public void MoveThePlayerLeft()
	{
		moveLeft = true;
		moveRight = false;
	}

	public void MoveThePlayerRight()
	{
		moveLeft = false;
		moveRight = true;
	}

	public void PlayerShootTwice(bool shootTwice)
	{

		if (doubleArrows || doubleStickyArrows)
		{
			this.shootTwice = shootTwice;
		}
		shootSecondArrow = false;
	}
	public void ShootTheArrow()
    {
        if (GameplayController.instance.levelInProgress)
        {
			if (shootOnce)
			{

				if (arrow == "Arrow")
				{
					Instantiate(arrows[0], new Vector3(transform.position.x, height, 0), Quaternion.identity);
				}
				else if (arrow == "StickyArrow")
				{
					Instantiate(arrows[2], new Vector3(transform.position.x, height, 0), Quaternion.identity);
				}

				StartCoroutine(PlayerTheShootAnimation());
				shootOnce = false;
				shootFirstArrow = true;

			}
			else if (shootTwice)
			{

				if (arrow == "Arrow")
				{
					Instantiate(arrows[1], new Vector3(transform.position.x, height, 0), Quaternion.identity);
				}
				else if (arrow == "StickyArrow")
				{
					Instantiate(arrows[3], new Vector3(transform.position.x, height, 0), Quaternion.identity);
				}

				StartCoroutine(PlayerTheShootAnimation());
				shootTwice = false;
				shootSecondArrow = true;

			}
		}
    }
	public void StopMoving()
    {
		moveLeft = moveRight = false;
		myAnimator.SetBool("Walk", false);
    }
	IEnumerator PlayerTheShootAnimation()
	{
		canWalk = false;
		myAnimator.Play("PlayerShoot");
		shootBtn.interactable = false;
		AudioSource.PlayClipAtPoint (shootClip, transform.position);
		yield return new WaitForSeconds(Clip.length);
		myAnimator.SetBool("Shoot", false);
		shootBtn.interactable = true;
		canWalk = true;
	}

	public void DestroyShield()
	{
		StartCoroutine(SetPlayerInvisible());
		hasShield = false;
		shield.SetActive(false);
	}

	IEnumerator SetPlayerInvisible()
	{
		isInvincible = true;
		yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(3f));
		isInvincible = false;
	}
	void MoveRight()
	{

		float force = 0.0f;
		float velocity = Mathf.Abs(myRigidBody.velocity.x);

		if (canWalk)
		{
			// moving right

			if (velocity < maxVelocity)
			{
				force = speed;
			}

			Vector3 scale = transform.localScale;
			scale.x = 1.0f;
			transform.localScale = scale;

			myAnimator.SetBool("Walk", true);

		}

		myRigidBody.AddForce(new Vector2(force, 0));

	}
	void MoveThePlayer()
	{

		if (GameplayController.instance.levelInProgress)
		{

			if (moveLeft)
			{
				MoveLeft();
			}

			if (moveRight)
			{
				MoveRight();
			}

		}

	}

	void MoveLeft()
	{

		float force = 0.0f;
		float velocity = Mathf.Abs(myRigidBody.velocity.x);

		if (canWalk)
		{

			// moving right

			if (velocity < maxVelocity)
			{
				force = -speed;
			}

			Vector3 scale = transform.localScale;
			scale.x = -1.0f;
			transform.localScale = scale;

			myAnimator.SetBool("Walk", true);


		}

		myRigidBody.AddForce(new Vector2(force, 0));

	}
	void PlayerWalkKeyboard()
	{
		canWalk = true;
		float force = 0.0f;
		float velocity = Mathf.Abs(myRigidBody.velocity.x);

		float h = Input.GetAxis("Horizontal");

		if (canWalk)
		{
			if (h > 0)
			{
				if (velocity < maxVelocity)
				{
					force = speed;
				}
				Vector3 scale = transform.localScale;
				scale.x = 1.0f;
				transform.localScale = scale;
				myAnimator.SetBool("Walk", true);
			}
			else if (h < 0)
			{
				if (velocity < maxVelocity)
				{
					force = -speed;
				}
				Vector3 scale = transform.localScale;
				scale.x = -1.0f;
				transform.localScale = scale;
				myAnimator.SetBool("Walk", true);
			}
			else if (h == 0)
			{
				myAnimator.SetBool("Walk", false);
			}
			myRigidBody.AddForce(new Vector2(force, 0));
		}
	}
	void OnTriggerEnter2D(Collider2D target)
	{
		if (target.tag == "SingleArrow")
		{
			if (!singleArrow)
			{
				arrow = "Arrow";
				if (!shootFirstArrow)
				{
					shootOnce = true;
				}
				shootTwice = false;

				singleArrow = true;
				doubleArrows = false;
				singleStickyArrow = false;
				doubleStickyArrows = false;
			}
		}

		if (target.tag == "DoubleArrow")
		{
			if (!doubleArrows)
			{
				arrow = "Arrow";
				if (!shootFirstArrow)
				{
					shootOnce = true;
				}
				if (!shootSecondArrow)
				{
					shootTwice = true;
				}
				singleArrow = false;
				doubleArrows = true;
				singleStickyArrow = false;
				doubleStickyArrows = false;
			}
		}

		if (target.tag == "SingleStickyArrow")
		{
			if (!singleStickyArrow)
			{
				arrow = "StickyArrow";

				if (!shootFirstArrow)
				{
					shootOnce = true;
				}
				shootTwice = false;
				singleArrow = false;
				doubleArrows = false;
				singleStickyArrow = true;
				doubleStickyArrows = false;

			}

		}

		if (target.tag == "DoubleStickyArrow")
		{
			if (!doubleStickyArrows)
			{
				arrow = "StickyArrow";
				if (!shootFirstArrow)
				{
					shootOnce = true;
				}
				if (!shootSecondArrow)
				{
					shootTwice = true;
				}
				singleArrow = false;
				doubleArrows = false;
				singleStickyArrow = false;
				doubleStickyArrows = true;
			}
		}

		if (target.tag == "Watch")
		{
			GameplayController.instance.levelTime += Random.Range(10, 20);
		}

		if (target.tag == "Shield")
		{
			hasShield = true;
			shield.SetActive(true);
		}

		if (target.tag == "Dynamite")
		{
			if (explode != null)
			{
				explode(false);
			}
		}

	}
}
