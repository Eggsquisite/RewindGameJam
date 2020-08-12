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
        if (savePosition) rp = new RecordedPositions(transform.position);
        if (saveRotation) rr = new RecordedRotations(transform.rotation);
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
}

public class RecordedPositions {
    private List<Vector3> recordedPosition = new List<Vector3>(1000);
    private int pos = 0;

    public RecordedPositions(Vector3 firstPos) { Add(firstPos); }

    public void Add(Vector3 position) {
        if (pos == recordedPosition.Count) { recordedPosition.Add(position); pos++; }
        else recordedPosition.Insert(pos++, position);
    }
    
    public Vector3 RewindBy(int amount) {
        if ((pos - 1 - amount) >= 0) pos -= amount;
        //Debug.Log("Pos: " + pos);
        return recordedPosition[pos-1];
    }
}

public class RecordedRotations {
    private List<Quaternion> recordedRotation = new List<Quaternion>(1000);
    private int pos = 0;
    
    public RecordedRotations(Quaternion firstRot) { Add(firstRot); }
    
    public void Add(Quaternion rotation) {
        if (pos == recordedRotation.Count) { recordedRotation.Add(rotation); pos++; }
        else recordedRotation.Insert(pos++, rotation);
    }
    
    public Quaternion RewindBy(int amount) {
        //Debug.Log(pos + "   " + amount);
        if ((pos - 1 - amount) >= 0) pos -= amount;
        //Debug.Log(pos);
        return recordedRotation[pos-1];
    }
}