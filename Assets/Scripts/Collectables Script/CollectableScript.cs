using UnityEngine;

public class CollectableScript : MonoBehaviour
{

	private Rigidbody2D myRigidBody;
	private bool activeOnce = true;

	// Use this for initialization
	void Start()
	{
		myRigidBody = GetComponent<Rigidbody2D>();

		if (this.gameObject.tag != "InGameCollectable")
		{
			Invoke("DeactivateGameobject", Random.Range(5, 9));
		}

	}

	void DeactivateGameobject()
	{
		this.gameObject.SetActive(false);
	}

	void OnTriggerEnter2D(Collider2D target)
	{

		if (target.tag == "BottomBrick")
		{
			Vector3 temp = target.transform.position;
			temp.y += 2.75f;
			transform.position = new Vector2(transform.position.x, temp.y);
			myRigidBody.bodyType = RigidbodyType2D.Static;
		}

		if (target.tag == "Player")
		{
			if (this.gameObject.tag == "InGameCollectable")
			{
				if (GameController.instance.currentLevel == -1)
					GameController.instance.currentLevel = 0;
				GameController.instance.collectedItems[GameController.instance.currentLevel] = true;
				GameController.instance.Save();

				if (GameplayController.instance != null)
				{

					if (GameController.instance.currentLevel == 0)
					{
						GameplayController.instance.playerScore += 1 * 1000;
					}
					else
					{
						GameplayController.instance.playerScore += GameController.instance.currentLevel * 1000;
					}

				}


			}
			if(gameObject.tag != "InGameCollectable")
				this.gameObject.SetActive(false);
		}
	}
}
