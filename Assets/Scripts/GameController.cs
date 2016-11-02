using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	private int score;
	private bool gameOver;
	private bool restart;
	public Text scoreText;
//	public Text restartText;
	public Text gameOverText;
	public GameObject restartButton;

	// Use this for initialization
	void Start () {
		score = 0;
		UpdateScore ();
		StartCoroutine(SpawnWaves ());
		//restartText.text = "";
		gameOverText.text = "";
		restartButton.SetActive (false);
	}
	 
//	void Update()
//	{
//		if (restart && Input.GetKeyDown ("r")) {
//			SceneManager.LoadScene (SceneManager.GetActiveScene().name);
//		}
//	}
	IEnumerator SpawnWaves () {
		yield return new WaitForSeconds (startWait);
		while (true) {
			for (int i = 0; i <= hazardCount; i++) {
				GameObject hazard = hazards [Random.Range (0, hazards.Length)];
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
			if (gameOver) 
			{
				//restartText.text = "Press 'R' to Restart Game";
				restartButton.SetActive(true);
				restart = true;
				break;
			}
		}
	}

	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore()
	{
		scoreText.text = "Score: " + score;
	}	

	public void GameOver() 
	{
		gameOver = true;
		gameOverText.text = "Game Over!";
	}

	public void RestartGame() {
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}
}
