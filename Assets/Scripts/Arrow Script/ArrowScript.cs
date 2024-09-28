using System.Collections;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
	private float arrowSpeed = 4.0f;
	private bool canShootStickyArrow;
    [SerializeField] AudioClip clip;
    private void Start()
    {
        canShootStickyArrow = true;
    }
    private void Update()
    {
        if (this.gameObject.tag == "FirstStickyArrow")
        {
            if (canShootStickyArrow)
            {
                ShootArrow();
            }
        }
        else if (this.gameObject.tag == "SecondStickyArrow")
        {
            if (canShootStickyArrow)
            {
                ShootArrow();
            }
        }
        else
        {
            ShootArrow();
        }
    }
    void ShootArrow()
	{
		Vector3 temp = transform.position;
		temp.y += arrowSpeed * Time.unscaledDeltaTime;
		transform.position = temp;
	}
    IEnumerator ResetStickyArrow()
    {

        yield return new WaitForSeconds(2.5f);

        if (this.gameObject.tag == "FirstStickyArrow")
        {
            PlayerScript.instance.PlayerShootOnce(true);
            this.gameObject.SetActive(false);
        }
        else if (this.gameObject.tag == "SecondStickyArrow")
        {
            PlayerScript.instance.PlayerShootTwice(true);
            this.gameObject.SetActive(false);
        }

    }
	void OnTriggerEnter2D(Collider2D target)
	{
		if (target.tag == "LargestBall" || target.tag == "LargeBall" || target.tag == "MediumBall" || target.tag == "SmallBall"
			|| target.tag == "SmalestBall")
		{
			if (gameObject.tag == "FirstArrow" || gameObject.tag == "FirstStickyArrow")
			{
				PlayerScript.instance.PlayerShootOnce(true);
			}
			else if (gameObject.tag == "SecondArrow" || gameObject.tag == "SecondStickyArrow")
			{
				PlayerScript.instance.PlayerShootTwice(true);
			}
			gameObject.SetActive(false);
		}

		if (target.tag == "TopBrick" || target.tag == "UnbreakableBrickTop" || target.tag == "UnbreakableBrickBottom"
			|| target.tag == "UnbreakableBrickLeft" || target.tag == "UnbreakableBrickRight"
			|| target.tag == "UnbreakableBrickBottomVertical")
		{
			if (this.gameObject.tag == "FirstArrow")
			{
				PlayerScript.instance.PlayerShootOnce(true);
				this.gameObject.SetActive(false);
			}
			else if (this.gameObject.tag == "SecondArrow")
			{
				PlayerScript.instance.PlayerShootTwice(true);
				this.gameObject.SetActive(false);
			}
			if (this.gameObject.tag == "FirstStickyArrow")
			{
				canShootStickyArrow = false;
				Vector3 targetPos = target.transform.position;
				Vector3 temp = transform.position;

				if (target.tag == "TopBrick")
				{
					targetPos.y += 1.6f;
				}
				else if (target.tag == "UnbreakableBrickTop" || target.tag == "UnbreakableBrickBottom" || target.tag == "UnbreakableBrickLeft"
						|| target.tag == "UnbreakableBrickRight")
				{
					targetPos.y -= 0.45f;
				}
				else if (target.tag == "UnbreakableBrickBottomVertical")
				{
					targetPos.y -= 0.68f;
				}
				temp.y = targetPos.y;
				transform.position = temp;
				AudioSource.PlayClipAtPoint(clip, transform.position);
				StartCoroutine("ResetStickyArrow");
			}
			else if (this.gameObject.tag == "SecondStickyArrow")
			{
				canShootStickyArrow = false;
				Vector3 targetPos = target.transform.position;
				Vector3 temp = transform.position;

				if (target.tag == "TopBrick")
				{
					targetPos.y += 1.6f;
				}
				else if (target.tag == "UnbreakableBrickTop" || target.tag == "UnbreakableBrickBottom" || target.tag == "UnbreakableBrickLeft"
						|| target.tag == "UnbreakableBrickRight")
				{
					targetPos.y -= 0.45f;
				}
				else if (target.tag == "UnbreakableBrickBottomVertical")
				{
					targetPos.y -= 0.68f;
				}

				temp.y = targetPos.y;
				transform.position = temp;
				AudioSource.PlayClipAtPoint(clip, transform.position);
				StartCoroutine("ResetStickyArrow");
			}
		}
		if (target.tag == "BrokenBrickTop" || target.tag == "BrokenBrickBottom" || target.tag == "BrokenBrickLeft"
			|| target.tag == "BrokenBrickRight")
		{
			BrickScript brick = target.gameObject.GetComponentInParent<BrickScript>();
			brick.StartCoroutine(brick.BreakTheBrick());
			if (gameObject.tag == "FirstArrow" || gameObject.tag == "FirstStickyArrow")
			{
				PlayerScript.instance.PlayerShootOnce(true);
			}
			else if (gameObject.tag == "SecondArrow" || gameObject.tag == "SecondStickyArrow")
			{
				PlayerScript.instance.PlayerShootTwice(true);
			}
			gameObject.SetActive(false);
		}
	}
}
