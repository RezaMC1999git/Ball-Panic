using UnityEngine;
using UnityEngine.UI;

public class ShopMenuController : MonoBehaviour
{
	public static ShopMenuController instance;
	public Text coinText, scoreText, buyArrowsText, watchVideoText;
	public Button weaponsTabBtn, specialTabBtn, earnCoinsTabBtn, yesBtn;
	public GameObject weaponItemsPanel, specialItemsPanel, earnCoinsItemsPanel, coinShopPanel, buyArrowsPanel;

	void Awake()
	{
		MakeInstance();
	}

	// Use this for initialization
	void Start()
	{
		InitializeShopMenuController();
	}

	void MakeInstance()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	public void BuyDoubleArrows()
	{

		if (!GameController.instance.weapons[1])
		{

			if (GameController.instance.coins >= 7000)
			{

				buyArrowsPanel.SetActive(true);
				buyArrowsText.text = "Do You Want To Purchase Double Arrows?";
				yesBtn.onClick.RemoveAllListeners();
				yesBtn.onClick.AddListener(() => BuyArrow(1));

			}
			else
			{

				buyArrowsPanel.SetActive(true);
				buyArrowsText.text = "You Dont Have Enough Coins. Do You Want To Buy Coins?";
				yesBtn.onClick.RemoveAllListeners();
				yesBtn.onClick.AddListener(() => OpenCoinShop());

			}

		}

	}

	public void BuyStickyArrow()
	{

		if (!GameController.instance.weapons[2])
		{

			if (GameController.instance.coins >= 7500)
			{

				buyArrowsPanel.SetActive(true);
				buyArrowsText.text = "Do You Want To Purchase Sticky Arrow?";
				yesBtn.onClick.RemoveAllListeners();
				yesBtn.onClick.AddListener(() => BuyArrow(2));

			}
			else
			{

				buyArrowsPanel.SetActive(true);
				buyArrowsText.text = "You Dont Have Enough Coins. Do You Want To Buy Coins?";
				yesBtn.onClick.RemoveAllListeners();
				yesBtn.onClick.AddListener(() => OpenCoinShop());

			}

		}

	}

	public void BuyDoubleStickyArrows()
	{

		if (!GameController.instance.weapons[3])
		{

			if (GameController.instance.coins >= 8000)
			{

				buyArrowsPanel.SetActive(true);
				buyArrowsText.text = "Do You Want To Purchase Double Sticky Arrows?";
				yesBtn.onClick.RemoveAllListeners();
				yesBtn.onClick.AddListener(() => BuyArrow(3));

			}
			else
			{

				buyArrowsPanel.SetActive(true);
				buyArrowsText.text = "You Dont Have Enough Coins. Do You Want To Buy Coins?";
				yesBtn.onClick.RemoveAllListeners();
				yesBtn.onClick.AddListener(() => OpenCoinShop());

			}

		}

	}

	public void BuyArrow(int index)
	{
		GameController.instance.weapons[index] = true;
		GameController.instance.coins -= 7000;
		GameController.instance.Save();

		buyArrowsPanel.SetActive(false);
		coinText.text = "" + GameController.instance.coins;
	}

	void InitializeShopMenuController()
	{
		coinText.text = "" + GameController.instance.coins;
		scoreText.text = "" + GameController.instance.highScore;
	}

	public void OpenCoinShop()
	{

		if (buyArrowsPanel.activeInHierarchy)
		{
			buyArrowsPanel.SetActive(false);
		}

		coinShopPanel.SetActive(true);
	}

	public void CloseCoinShop()
	{
		coinShopPanel.SetActive(false);
	}

	public void OpenWeaponItemsPanel()
	{
		weaponItemsPanel.SetActive(true);
		specialItemsPanel.SetActive(false);
		earnCoinsItemsPanel.SetActive(false);
	}

	public void OpenSpecialItemsPanel()
	{
		specialItemsPanel.SetActive(true);
		weaponItemsPanel.SetActive(false);
		earnCoinsItemsPanel.SetActive(false);
	}

	public void OpenEarnCoinsItemsPanel()
	{
		earnCoinsItemsPanel.SetActive(true);
		specialItemsPanel.SetActive(false);
		weaponItemsPanel.SetActive(false);
	}

	public void PlayGame()
	{
		Application.LoadLevel("PlayerMenu");
	}

	public void GoToMainMenu()
	{
		Application.LoadLevel("MainMenu");
	}

	public void DontBuyArrows()
	{
		buyArrowsPanel.SetActive(false);
	}
}
