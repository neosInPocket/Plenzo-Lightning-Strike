using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionController : MonoBehaviour
{
	[SerializeField] private string playString;

	public void ChangeScene()
	{
		SceneManager.LoadScene(playString);
	}
}
