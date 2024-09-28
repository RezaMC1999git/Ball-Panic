using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
	private bool hidden;
	private bool canTouchSettingsButton;
	private int infoIndex;
	[SerializeField] private Animator settingsButtonsAnim;
	[SerializeField] private Button musicBtn;
	[SerializeField] private Sprite[] musicBtnSprites;
	[SerializeField] private Button fbBtn;
	[SerializeField] private Sprite[] fbSprites;
	[SerializeField] private GameObject infoPanel;
	[SerializeField] private Image infoImage;
	[SerializeField] private Sprite[] infoSprites;

	void Start()
	{
		canTouchSettingsButton = true;
		hidden = true;

		if (GameController.instance.isMusicOn)
		{
			MusicController.instance.PlayBgMusic();
			musicBtn.image.sprite = musicBtnSprites[0];
		}
		else
		{
			MusicController.instance.StopBgMusic();
			musicBtn.image.sprite = musicBtnSprites[1];
		}
		infoIndex = 0;
		infoImage.sprite = infoSprites[infoIndex];
	}
	public void MusicButton()
	{
		if (GameController.instance.isMusicOn)
		{
			musicBtn.image.sprite = musicBtnSprites[1];
		    MusicController.instance.StopBgMusic();
			GameController.instance.isMusicOn = false;
			GameController.instance.Save();
		}
		else
		{
			musicBtn.image.sprite = musicBtnSprites[0];
		 	MusicController.instance.PlayBgMusic();
			GameController.instance.isMusicOn = true;
			GameController.instance.Save();
		}
	}
	public void SettingsButton()
	{
		StartCoroutine(DisableSettingsButtonWhilePlayingAnimation());
	}
	IEnumerator DisableSettingsButtonWhilePlayingAnimation()
	{
		if (canTouchSettingsButton)
		{
			if (hidden)
			{
				canTouchSettingsButton = false;
				settingsButtonsAnim.Play("SlideIn");
				hidden = false;
				yield return new WaitForSeconds(1.2f);
				canTouchSettingsButton = true;
			}
			else
			{
				canTouchSettingsButton = false;
				settingsButtonsAnim.Play("SlideOut");
				hidden = true;
				yield return new WaitForSeconds(1.2f);
				canTouchSettingsButton = true;
			}
		}
	}
	public void OpenInfoPanel()
	{
		infoPanel.SetActive(true);
	}

	public void CloseInfoPanel()
	{
		infoPanel.SetActive(false);
	}

	public void NextInfo()
	{
		infoIndex++;
		if (infoIndex == infoSprites.Length)
		{
			infoIndex = 0;
		}
		infoImage.sprite = infoSprites[infoIndex];
	}
	public void PlayButton()
	{
		MusicController.instance.PlayClickClip ();
		Application.LoadLevel("PlayerMenu");
	}
	public void GoToShopMenu()
    {
		MusicController.instance.PlayClickClip();
		Application.LoadLevel("ShopMenu");
    }
}
