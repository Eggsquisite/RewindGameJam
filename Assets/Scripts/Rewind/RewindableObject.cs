using System.Collections.Generic;
using UnityEngine;

public class RewindableObject {

    private Transform transform;
    private RecordedPositions rp;
    private RecordedRotations rr;
    public bool savePosition, saveRotation;

    public RewindableObject(Transform transform, bool savePosition, bool saveRotation) {
        this.transform = transform;
        this.savePosition = savePosition;
        this.saveRotation = saveRotation;
        if (savePosition) rp = new RecordedPositions();
        if (saveRotation) rr = new RecordedRotations();
    }

    public void Add() {
        if (savePosition) rp.Add(transform.position);
        if (saveRotation) rr.Add(transform.rotation);
    }

    public Vector3 RewindPosition(int amount) {
        return rp.RewindBy(amount);
    }

    public Quaternion RewindRotation(int amount) {
        return rr.RewindBy(amount);
    }

    /*
    void Start() {
        if (saveRotation) recordedRotation = new List<Quaternion>(1000);
    }*/

    /*
    void Add() {
        if (position == recordedPosition.Count) {
            if (savePosition) recordedPosition.Add(transform.position);
            if (saveRotation) recordedRotation.Add(transform.rotation);
            position++;
        }
        else {
            if (savePosition) recordedPosition.Insert(position, transform.position);
            if (saveRotation) recordedRotation.Insert(position, transform.rotation);
            position++;
        }
    }*/
    
    /*void FixedUpdate() {
        if (!RewindManager.isRewinding) { //only record positions when not rewinding
            if (position == recordedPosition.Count) {
                recordedPosition.Add(gameObject.transform.position);
                if (saveRotation)recordedRotation.Add(gameObject.transform.rotation);
                position++;
            }
            else recordedPosition.Insert(position++, gameObject.transform.position);
        }
    }*/

    /*
    public TransformLite GetPreviousTransform() {
        if (position > 0) position--;
        if (savePosition && saveRotation) return new TransformLite(recordedPosition[position], recordedRotation[position]);
        else if (savePosition) return new TransformLite(recordedPosition[position]);
        else if (saveRotation) return new TransformLite(recordedRotation[position]);
        //else return null;
    }*/


    /*
    public Vector3 GetPreviousPosition() {
        if (position > 0) return recordedPosition[--position];
        else return recordedPosition[0];
    }

   //Only call this after calling GetPreviousPosition
    public Quaternion GetPreviousRotation() {
        if (!savePosition)
        return recordedRotation[position];
    }*/
}

public class RecordedPositions {
    private List<Vector3> recordedPosition = new List<Vector3>(1000);
    private int pos = 0;

    public void Add(Vector3 position) {
        if (pos == recordedPosition.Count) { recordedPosition.Add(position); pos++; }
        else recordedPosition.Insert(pos++, position);
    }
    
    public Vector3 RewindBy(int amount) {
        if ((pos - amount) >= 0) pos -= amount;
        return recordedPosition[pos];
    }
}

public class RecordedRotations {
    private List<Quaternion> recordedRotation = new List<Quaternion>(1000);
    private int pos = 0;
    
    public void Add(Quaternion rotation) {
        if (pos == recordedRotation.Count) { recordedRotation.Add(rotation); pos++; }
        else recordedRotation.Insert(pos++, rotation);
    }
    
    public Quaternion RewindBy(int amount) {
        if ((pos - amount) >= 0) pos -= amount;
        return recordedRotation[pos];
    }
}