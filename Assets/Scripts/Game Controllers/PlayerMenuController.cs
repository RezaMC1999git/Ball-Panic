using UnityEngine;
using UnityEngine.UI;

public class PlayerMenuController : MonoBehaviour
{

	public Text scoreText, coinText;
	public bool[] players;
	public bool[] weapons;
	public Image[] priceTags;
	public Image[] weaponIcons;
	public Sprite[] weaponArrows;
	public int selectedWeapon;
	public int selectedPlayer;
	public GameObject buyPlayerPanel;
	public Button yesBtn;
	public Text buyPlayerText;
	public GameObject coinShop;

    private void Start()
    {
		InitializePlayerMenuController();

	}
    void InitializePlayerMenuController()
	{

		scoreText.text = "" + GameController.instance.highScore;
		coinText.text = "" + GameController.instance.coins;

		players = GameController.instance.players;
		weapons = GameController.instance.weapons;

		selectedPlayer = GameController.instance.selectedPlayer;
		selectedWeapon = GameController.instance.selectedWeapon;

		for (int i = 0; i < weaponIcons.Length; i++)
		{
			weaponIcons[i].gameObject.SetActive(false);
		}

		for (int i = 1; i < players.Length; i++)
		{
			if (players[i] == true)
			{
				priceTags[i - 1].gameObject.SetActive(false);
			}
		}

		weaponIcons[selectedPlayer].gameObject.SetActive(true);
		weaponIcons[selectedPlayer].sprite = weaponArrows[selectedWeapon];

	}
	public void Player1Button()
	{
		if (selectedPlayer != 0)
		{
			selectedPlayer = 0;
			selectedWeapon = 0;

			weaponIcons[selectedPlayer].gameObject.SetActive(true);
			weaponIcons[selectedPlayer].sprite = weaponArrows[selectedWeapon];

			for (int i = 0; i < weaponIcons.Length; i++)
			{
				if (i == selectedPlayer)
					continue;

				weaponIcons[i].gameObject.SetActive(false);
			}
			GameController.instance.selectedPlayer = selectedPlayer;
			GameController.instance.selectedWeapon = selectedWeapon;
			GameController.instance.Save();
		}
		else
		{
			selectedWeapon++;
			if (selectedWeapon == weapons.Length)
				selectedWeapon = 0;
			bool foundWeapon = true;
			while (foundWeapon)
			{

				if (weapons[selectedWeapon] == true)
				{
					weaponIcons[selectedPlayer].sprite = weaponArrows[selectedWeapon];
					GameController.instance.selectedWeapon = selectedWeapon;
					GameController.instance.Save();
					foundWeapon = false;
				}
				else
				{
					selectedWeapon++;

					if (selectedWeapon == weapons.Length)
						selectedWeapon = 0;
				}

			}

		}

	}

	public void PiratePlayerButton()
	{

		if (players[1] == true)
		{

			if (selectedPlayer != 1)
			{

				selectedPlayer = 1;
				selectedWeapon = 0;

				weaponIcons[selectedPlayer].gameObject.SetActive(true);
				weaponIcons[selectedPlayer].sprite = weaponArrows[selectedWeapon];

				for (int i = 0; i < weaponIcons.Length; i++)
				{
					if (i == selectedPlayer)
						continue;

					weaponIcons[i].gameObject.SetActive(false);
				}

				GameController.instance.selectedPlayer = selectedPlayer;
				GameController.instance.selectedWeapon = selectedWeapon;
				GameController.instance.Save();

			}
			else
			{

				selectedWeapon++;

				if (selectedWeapon == weapons.Length)
					selectedWeapon = 0;

				bool foundWeapon = true;

				while (foundWeapon)
				{

					if (weapons[selectedWeapon] == true)
					{
						weaponIcons[selectedPlayer].sprite = weaponArrows[selectedWeapon];
						GameController.instance.selectedWeapon = selectedWeapon;
						GameController.instance.Save();
						foundWeapon = false;
					}
					else
					{
						selectedWeapon++;

						if (selectedWeapon == weapons.Length)
							selectedWeapon = 0;
					}

				}

			}

		}
		else
		{

			if (GameController.instance.coins >= 7000)
			{

				buyPlayerPanel.SetActive(true);
				buyPlayerText.text = "Do You Want To Purchase The Pirate?";
				yesBtn.onClick.RemoveAllListeners();
				yesBtn.onClick.AddListener(() => BuyPlayer(1));

			}
			else
			{

				buyPlayerPanel.SetActive(true);
				buyPlayerText.text = "You Dont Have Enough Coins. Do You Want To Purchase Coins?";
				yesBtn.onClick.RemoveAllListeners();
				yesBtn.onClick.AddListener(() => OpenCoinShop());

			}

		}

	} // pirate player

	public void ZombiePlayerButton()
	{

		if (players[2] == true)
		{

			if (selectedPlayer != 2)
			{

				selectedPlayer = 2;
				selectedWeapon = 0;

				weaponIcons[selectedPlayer].gameObject.SetActive(true);
				weaponIcons[selectedPlayer].sprite = weaponArrows[selectedWeapon];

				for (int i = 0; i < weaponIcons.Length; i++)
				{
					if (i == selectedPlayer)
						continue;

					weaponIcons[i].gameObject.SetActive(false);
				}

				GameController.instance.selectedPlayer = selectedPlayer;
				GameController.instance.selectedWeapon = selectedWeapon;
				GameController.instance.Save();

			}
			else
			{

				selectedWeapon++;

				if (selectedWeapon == weapons.Length)
					selectedWeapon = 0;

				bool foundWeapon = true;

				while (foundWeapon)
				{

					if (weapons[selectedWeapon] == true)
					{
						weaponIcons[selectedPlayer].sprite = weaponArrows[selectedWeapon];
						GameController.instance.selectedWeapon = selectedWeapon;
						GameController.instance.Save();
						foundWeapon = false;
					}
					else
					{
						selectedWeapon++;

						if (selectedWeapon == weapons.Length)
							selectedWeapon = 0;
					}

				}

			}

		}
		else
		{

			if (GameController.instance.coins >= 7000)
			{

				buyPlayerPanel.SetActive(true);
				buyPlayerText.text = "Do You Want To Purchase The Zombie?";
				yesBtn.onClick.RemoveAllListeners();
				yesBtn.onClick.AddListener(() => BuyPlayer(2));

			}
			else
			{

				buyPlayerPanel.SetActive(true);
				buyPlayerText.text = "You Dont Have Enough Coins. Do You Want To Purchase Coins?";
				yesBtn.onClick.RemoveAllListeners();
				yesBtn.onClick.AddListener(() => OpenCoinShop());

			}

		}

	} // Zombie player

	public void HomosapienPlayerButton()
	{

		if (players[3] == true)
		{

			if (selectedPlayer != 3)
			{

				selectedPlayer = 3;
				selectedWeapon = 0;

				weaponIcons[selectedPlayer].gameObject.SetActive(true);
				weaponIcons[selectedPlayer].sprite = weaponArrows[selectedWeapon];

				for (int i = 0; i < weaponIcons.Length; i++)
				{
					if (i == selectedPlayer)
						continue;

					weaponIcons[i].gameObject.SetActive(false);
				}

				GameController.instance.selectedPlayer = selectedPlayer;
				GameController.instance.selectedWeapon = selectedWeapon;
				GameController.instance.Save();

			}
			else
			{

				selectedWeapon++;

				if (selectedWeapon == weapons.Length)
					selectedWeapon = 0;

				bool foundWeapon = true;

				while (foundWeapon)
				{

					if (weapons[selectedWeapon] == true)
					{
						weaponIcons[selectedPlayer].sprite = weaponArrows[selectedWeapon];
						GameController.instance.selectedWeapon = selectedWeapon;
						GameController.instance.Save();
						foundWeapon = false;
					}
					else
					{
						selectedWeapon++;

						if (selectedWeapon == weapons.Length)
							selectedWeapon = 0;
					}

				}

			}

		}
		else
		{

			if (GameController.instance.coins >= 7000)
			{

				buyPlayerPanel.SetActive(true);
				buyPlayerText.text = "Do You Want To Purchase The Homosapien?";
				yesBtn.onClick.RemoveAllListeners();
				yesBtn.onClick.AddListener(() => BuyPlayer(3));

			}
			else
			{

				buyPlayerPanel.SetActive(true);
				buyPlayerText.text = "You Dont Have Enough Coins. Do You Want To Purchase Coins?";
				yesBtn.onClick.RemoveAllListeners();
				yesBtn.onClick.AddListener(() => OpenCoinShop());

			}

		}

	} // Homosapien player

	public void JokerPlayerButton()
	{

		if (players[4] == true)
		{

			if (selectedPlayer != 4)
			{

				selectedPlayer = 4;
				selectedWeapon = 0;

				weaponIcons[selectedPlayer].gameObject.SetActive(true);
				weaponIcons[selectedPlayer].sprite = weaponArrows[selectedWeapon];

				for (int i = 0; i < weaponIcons.Length; i++)
				{
					if (i == selectedPlayer)
						continue;

					weaponIcons[i].gameObject.SetActive(false);
				}

				GameController.instance.selectedPlayer = selectedPlayer;
				GameController.instance.selectedWeapon = selectedWeapon;
				GameController.instance.Save();

			}
			else
			{

				selectedWeapon++;

				if (selectedWeapon == weapons.Length)
					selectedWeapon = 0;

				bool foundWeapon = true;

				while (foundWeapon)
				{

					if (weapons[selectedWeapon] == true)
					{
						weaponIcons[selectedPlayer].sprite = weaponArrows[selectedWeapon];
						GameController.instance.selectedWeapon = selectedWeapon;
						GameController.instance.Save();
						foundWeapon = false;
					}
					else
					{
						selectedWeapon++;

						if (selectedWeapon == weapons.Length)
							selectedWeapon = 0;
					}

				}

			}

		}
		else
		{

			if (GameController.instance.coins >= 7000)
			{

				buyPlayerPanel.SetActive(true);
				buyPlayerText.text = "Do You Want To Purchase The Joker?";
				yesBtn.onClick.RemoveAllListeners();
				yesBtn.onClick.AddListener(() => BuyPlayer(4));

			}
			else
			{

				buyPlayerPanel.SetActive(true);
				buyPlayerText.text = "You Dont Have Enough Coins. Do You Want To Purchase Coins?";
				yesBtn.onClick.RemoveAllListeners();
				yesBtn.onClick.AddListener(() => OpenCoinShop());

			}

		}

	} // Joker player

	public void SpartanPlayerButton()
	{

		if (players[5] == true)
		{

			if (selectedPlayer != 5)
			{

				selectedPlayer = 5;
				selectedWeapon = 0;

				weaponIcons[selectedPlayer].gameObject.SetActive(true);
				weaponIcons[selectedPlayer].sprite = weaponArrows[selectedWeapon];

				for (int i = 0; i < weaponIcons.Length; i++)
				{
					if (i == selectedPlayer)
						continue;

					weaponIcons[i].gameObject.SetActive(false);
				}

				GameController.instance.selectedPlayer = selectedPlayer;
				GameController.instance.selectedWeapon = selectedWeapon;
				GameController.instance.Save();

			}
			else
			{

				selectedWeapon++;

				if (selectedWeapon == weapons.Length)
					selectedWeapon = 0;

				bool foundWeapon = true;

				while (foundWeapon)
				{

					if (weapons[selectedWeapon] == true)
					{
						weaponIcons[selectedPlayer].sprite = weaponArrows[selectedWeapon];
						GameController.instance.selectedWeapon = selectedWeapon;
						GameController.instance.Save();
						foundWeapon = false;
					}
					else
					{
						selectedWeapon++;

						if (selectedWeapon == weapons.Length)
							selectedWeapon = 0;
					}

				}

			}

		}
		else
		{

			if (GameController.instance.coins >= 7000)
			{

				buyPlayerPanel.SetActive(true);
				buyPlayerText.text = "Do You Want To Purchase The Spartan?";
				yesBtn.onClick.RemoveAllListeners();
				yesBtn.onClick.AddListener(() => BuyPlayer(5));

			}
			else
			{

				buyPlayerPanel.SetActive(true);
				buyPlayerText.text = "You Dont Have Enough Coins. Do You Want To Purchase Coins?";
				yesBtn.onClick.RemoveAllListeners();
				yesBtn.onClick.AddListener(() => OpenCoinShop());

			}

		}

	}
	public void CloseCoinShop()
	{
		coinShop.SetActive(false);
	}

	public void OpenCoinShop()
	{

		if (buyPlayerPanel.activeInHierarchy)
		{
			buyPlayerPanel.SetActive(false);
		}

		coinShop.SetActive(true);

	}

	public void DontBuyPlayerOrCoins()
	{
		buyPlayerPanel.SetActive(false);
	}

	public void BuyPlayer(int index)
	{

		GameController.instance.players[index] = true;
		GameController.instance.coins -= 7000;
		GameController.instance.Save();
		InitializePlayerMenuController();

		buyPlayerPanel.SetActive(false);

	}
	public void GoToLevelMenu()
	{
		Application.LoadLevel("LevelMenu");
	}

	public void GoToMainMenu()
	{
		Application.LoadLevel("MainMenu");
	}
}
