using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour {
    public GameObject platformPrefab;
    public Vector2 start;
    public Vector2 finish;

    public float platformSpeed = 3f;
    public float delayBetweenSpawns = 2f;

    public float platformSpacing = 3f;
    private float delayBetweenSpawnsRewind;

    private float timer = 0f;
    private Vector2 dir;
    private bool firstRewind = true;

    private List<GameObject> platforms;

    // Start is called before the first frame update
    void Start() {
        platforms = new List<GameObject>(30);
        dir = (finish - start).normalized;
        timer = delayBetweenSpawns; //spawns the first platform immediately
        delayBetweenSpawnsRewind = delayBetweenSpawns / RewindManager.GetMaxRewindRate();


        int noOfPlatforms = (int)((finish - start).magnitude / platformSpacing);
        platformSpacing = (finish - start).magnitude / noOfPlatforms;
        for (int i = 0; i < noOfPlatforms; i++) {
            platforms.Add(Instantiate(platformPrefab, start + (i*dir*platformSpacing), Quaternion.identity));
        }
    }


    void FixedUpdate() {
        if (!RewindManager.IsRewinding()) {
            if (!firstRewind) {
                timer *= RewindManager.GetMaxRewindRate();
                timer = delayBetweenSpawns - timer; //reverse
                firstRewind = true;
            }
            
            if (timer >= delayBetweenSpawns) {
                platforms.Add(Instantiate(platformPrefab, start, Quaternion.identity));
                timer = 0f;
            }


            for (int i = 0; i < platforms.Count; i++) {
                platforms[i].transform.Translate(dir*platformSpeed*Time.fixedDeltaTime);

                if (((Vector2) (platforms[i].transform.position) - finish).magnitude <= 0.05f) {
                    Destroy(platforms[i]);
                    platforms.RemoveAt(i);
                }
            }
            timer += Time.fixedDeltaTime;
        }

        else {
            if (firstRewind) { //flip the timer
                timer = delayBetweenSpawns - timer;
                timer /= RewindManager.GetMaxRewindRate(); //scales the current time
                firstRewind = false;
            }
            
            if (timer >= delayBetweenSpawns) {
                platforms.Add(Instantiate(platformPrefab, finish, Quaternion.identity));
                timer = 0f;
            } 
            
            for (int i = 0; i < platforms.Count; i++) {
                platforms[i].transform.Translate(-dir*platformSpeed*Time.fixedDeltaTime*RewindManager.GetRewindRate());

                if (((Vector2) (platforms[i].transform.position) - start).magnitude <= 0.05f) {
                    Destroy(platforms[i]);
                    platforms.RemoveAt(i);
                }
                
            }

            timer += Time.fixedDeltaTime * RewindManager.GetRewindRate();
        }
    }
}