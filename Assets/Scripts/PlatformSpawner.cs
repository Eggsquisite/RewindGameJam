using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour {
    
    public GameObject platformPrefab;
    public Vector2 start;
    public Vector2 finish;
    public float platformSpeed = 3f;
    public float platformSpacing = 4f;

    private Vector2 dir;
    private List<GameObject> platforms;

    void Start() {
        
        dir = (finish - start).normalized;
        int noOfPlatforms = (int)((finish - start).magnitude / platformSpacing);
        platforms = new List<GameObject>(noOfPlatforms);
        platformSpacing = (finish - start).magnitude / noOfPlatforms;

        for (int i = 0; i < noOfPlatforms; i++) {
            platforms.Add(Instantiate(platformPrefab, start + (i*platformSpacing*dir), Quaternion.identity));
            Debug.Log("Platform created at: " + (start + (i*platformSpacing*dir)));
        }
    }

    void FixedUpdate() {
        if (!RewindManager.IsRewinding()) {
            for (int i = 0; i < platforms.Count; i++) {
                platforms[i].transform.Translate(dir*platformSpeed*Time.fixedDeltaTime);

                if (Vector2.Dot((finish - (Vector2)(platforms[i].transform.position)).normalized, dir) < 0) {
                    platforms[i].transform.position = start;
                }
            }
        }

        else {
            for (int i = 0; i < platforms.Count; i++) {
                platforms[i].transform.Translate(-dir*platformSpeed*Time.fixedDeltaTime*RewindManager.GetRewindRate());

                if (Vector2.Dot((start - (Vector2)(platforms[i].transform.position)).normalized, -dir) < 0) {
                    platforms[i].transform.position = finish;
                }
            }
        }
    }
}