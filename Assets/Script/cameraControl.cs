using UnityEngine;
using System.Collections;

public class cameraControl : MonoBehaviour {

	public Transform bob;
	public GUIText playerText;
	public GUIText ausgabe;
	public int level;
	
	Quaternion zero = new Quaternion(0,0,0,0);
	GameObject[] levelArray = new GameObject[5];
	LevelInfo[] levelInfoArray = new LevelInfo[5];
	int lastLevelEnd = -20;
	int numberOfLevels = 18;
	int secureDistance;
	int zeroDistance;
	
	void Awake() {
		Application.targetFrameRate = 30;
		// PlayerPrefs.DeleteAll();
	}
	
	void Start () {
		
		// PlayerPrefs.SetInt("Level", 3); // where you wonna start?
		
		level = PlayerPrefs.GetInt("Level");
		if(level==0 || level>numberOfLevels) { PlayerPrefs.SetInt("Level", 1); PlayerPrefs.SetInt("Distance", 0); level=1;}
		secureDistance = PlayerPrefs.GetInt("Distance");
		zeroDistance = secureDistance;
		
		
		levelArray[0] = (GameObject) Instantiate(Resources.Load("Levels/Level1"), new Vector3(lastLevelEnd,-2), zero);
		levelInfoArray[0] = (LevelInfo) levelArray[0].GetComponent("LevelInfo");
		lastLevelEnd += levelInfoArray[0].levelDistance;
		
		levelArray[1] = (GameObject) Instantiate(Resources.Load("Levels/Level1"), new Vector3(lastLevelEnd,-2), zero);
		levelInfoArray[1] = (LevelInfo) levelArray[1].GetComponent("LevelInfo");
		lastLevelEnd += levelInfoArray[1].levelDistance;
		
		for(int i=2; i<5; i++)
		{
			try {	
					levelArray[i] = (GameObject) Instantiate(Resources.Load("Levels/Level"+(level+(i-2))), new Vector3(lastLevelEnd,-2), zero);
				} catch {
					levelArray[i] = (GameObject) Instantiate(Resources.Load("Levels/Level1"), new Vector3(lastLevelEnd,-2), zero);
				}
			levelInfoArray[i] = (LevelInfo) levelArray[i].GetComponent("LevelInfo");
			
			lastLevelEnd += levelInfoArray[i].levelDistance;
		}
		displayText(levelInfoArray[2].levelMessage);
	
	}
	
	void Update () {
		// Add Playertext
		Vector3 viewportPos = this.camera.WorldToViewportPoint(bob.transform.position);
		playerText.transform.position = new Vector2(viewportPos.x,viewportPos.y+0.08f);
		
		//Update Score
		
		int foot = Mathf.FloorToInt(bob.transform.position.x);
		ausgabe.text = (zeroDistance + foot) + " ft \nLevel " + level;
		
		if(foot>=levelArray[3].transform.position.x && bob.transform.position.y > -2)
		{
			level++;
			PlayerPrefs.SetInt("Level", level);
			secureDistance+=levelInfoArray[2].levelDistance;
			PlayerPrefs.SetInt("Distance", secureDistance);
			displayText(levelInfoArray[3].levelMessage);
			updateLevel();
		}
		
		if(foot<=levelArray[1].transform.position.x)
		{
			displayText("Deine Mudda läuft bei Super Mario nach links!");
		}
		
	}
	
	void FixedUpdate () {
		// camera Follow
		Vector3 goal = new Vector3(bob.position.x-10,bob.position.y+10,bob.position.z-10);
		transform.position = transform.position + (goal - transform.position)*0.08f;
	}
	
	void updateLevel() {
		Destroy(levelArray[0]);
		Destroy(levelInfoArray[0]);
		for(int i=0; i<4; i++)
		{
			levelArray[i]=levelArray[i+1];
			levelInfoArray[i]=levelInfoArray[i+1];
		}		
		try { levelArray[4] = (GameObject) Instantiate(Resources.Load("Levels/Level"+(level+2)), new Vector3(lastLevelEnd,-2), zero); }
		catch { levelArray[4] = (GameObject) Instantiate(Resources.Load("Levels/Level"+Mathf.Floor(Random.Range(1,numberOfLevels))), new Vector3(lastLevelEnd,-2), zero); }
		levelInfoArray[4] = (LevelInfo) levelArray[4].GetComponent("LevelInfo");
		lastLevelEnd += levelInfoArray[4].levelDistance;
	}
	
	void displayText(string text)
	{
		string[] lines = text.Split("#"[0]);
		playerText.text="";
		foreach(string line in lines)
		{
			playerText.text+= line;
			playerText.text+= "\n";
		}
	}
}
