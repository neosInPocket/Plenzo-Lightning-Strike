using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class GameManager : MonoBehaviour
{
	[SerializeField] private Image currentProgressFillImage;
	[SerializeField] private TMP_Text currentProgressText;
	[SerializeField] private GamePlayShow gamePlayShow;
	[SerializeField] private ResultCapture resultCapture;
	[SerializeField] private PortalsController portalsController;
	[SerializeField] private MeteorsSpawner meteorsSpawner;
	[SerializeField] private PortalBall portalBall;
	[SerializeField] private GameObject passScreen;
	private int rewardCoin;
	private int maximum;
	private int current;

	private void Start()
	{
		portalsController.Initialize();


		if (DataCaptureController.Capture.tutorialAvaliable)
		{
			DataCaptureController.Capture.tutorialAvaliable = false;
			DataCaptureController.Save();

			gamePlayShow.GamePlayShowEnd += OnGamePlayShowEnd;
			gamePlayShow.ShowGameplay();
		}
		else
		{
			OnGamePlayShowEnd(0);
		}

		GetLevelStatistics();
		RefreshBars();
	}

	private void OnGamePlayShowEnd(int currentIndex)
	{
		passScreen.gameObject.SetActive(true);
		Touch.onFingerDown += OnTouch;
	}

	private void OnTouch(Finger finger)
	{
		passScreen.gameObject.SetActive(false);
		Touch.onFingerDown -= OnTouch;
		meteorsSpawner.Enable(true);
		portalsController.SetEnabled(true);
		portalsController.StartAction();
		portalsController.SubscribeOnDeath(EndLoseGame);
		portalBall.SubscribeCrash(EndLoseGame);
		portalBall.SubscribeOnPortal(OnPortal);
	}

	private void EndLoseGame()
	{
		resultCapture.GetResultWindow(false, 0);

		portalBall.ClearAllSubscribers();
	}

	private void EndWinGame()
	{
		portalBall.ClearAllSubscribers();
		resultCapture.GetResultWindow(true, rewardCoin);

		DataCaptureController.Capture.goldCoins += rewardCoin;
		DataCaptureController.Capture.level += 1;
		DataCaptureController.Save();
	}

	private void OnPortal(Portal portal)
	{
		current++;

		if (current >= maximum)
		{
			EndWinGame();
		}

		RefreshBars();
	}

	private void RefreshBars()
	{
		currentProgressFillImage.fillAmount = (float)current / (float)maximum;
		currentProgressText.text = "current progress: " + current.ToString() + "/" + maximum.ToString();
	}

	private void GetLevelStatistics()
	{
		var x = (float)DataCaptureController.Capture.level;
		maximum = (int)(3 * Mathf.Sqrt(x));
		rewardCoin = (int)(3 * Mathf.Sqrt(x));
	}
}
