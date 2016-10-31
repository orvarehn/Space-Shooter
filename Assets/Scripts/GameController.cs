using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	private int score;
	private bool gameOver;
	private bool restart;
	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;

	// Use this for initialization
	void Start () {
		score = 0;
		UpdateScore ();
		StartCoroutine(SpawnWaves ());
		restartText.text = "";
		gameOverText.text = "";
	}
	 
	void Update()
	{
		if (restart && Input.GetKeyDown ("r")) {
			SceneManager.LoadScene (SceneManager.GetActiveScene().name);
		}
	}
	IEnumerator SpawnWaves () {
		yield return new WaitForSeconds (startWait);
		while (true) {
			for (int i = 0; i <= hazardCount; i++) {
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazards, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
			if (gameOver) 
			{
				restartText.text = "Press 'R' to Restart Game";
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
		Debug.Log ("GAME OVER!!");
		gameOver = true;
		gameOverText.text = "Game Over!";
	}
}
