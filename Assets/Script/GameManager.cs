using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public float score;
	public Transform startingPoint;
	public float lerpSpeed;
	private PlayerController playerControllerScript;
	
	
	// Start is called before the first frame update
	void Start()
	{
		playerControllerScript =
			GameObject.Find("Player").GetComponent<PlayerController>();
		score = 0;
		
		playerControllerScript.gameOver = true;
		StartCoroutine(PlayIntro());
	}

	// Update is called once per frame
	void Update()
	{
		if(!playerControllerScript.gameOver)
		{
			if(playerControllerScript.doubleSpeed)
			{
				score += 2;
			}
			else
			{
				score++;
			}
			Debug.Log("Score: " + score);
		}

	}
	
	
	IEnumerator PlayIntro()
{
    Vector3 startPos = playerControllerScript.transform.position;
    Vector3 endPos = startingPoint.position;
    float journeyLength = Vector3.Distance(startPos, endPos);
    float startTime = Time.time;

    playerControllerScript.GetComponent<Animator>().SetFloat("Speed_Multiplier", 0.5f);

    while (true)
    {
        float distanceCovered = (Time.time - startTime) * lerpSpeed;
        float fractionOfJourney = distanceCovered / journeyLength;

        playerControllerScript.transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);

        if (fractionOfJourney >= 1)
            break;

        yield return null;
    }

    playerControllerScript.GetComponent<Animator>().SetFloat("Speed_Multiplier", 1.0f);
    playerControllerScript.gameOver = false;
}

}
