using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code from https://github.com/dci05049/Verlet-Rope-Unity

public class RopeBridge : MonoBehaviour {
    private Transform StartPoint;
    private Transform EndPoint;

    [Range(1, 100)]
    [SerializeField] int numberOfSegments = 35;

    [Range(0.0f, 3.0f)]
    public float segmentLength = 0.25f;

    [Range(0.01f, 1f)]
    [SerializeField] float lineWidth = 0.1f;


    private LineRenderer lineRenderer;
    private List<RopeSegment> ropeSegments = new List<RopeSegment>();


    // Use this for initialization
    void Start() {
        this.lineRenderer = this.GetComponent<LineRenderer>();
        Vector3 ropeStartPoint = StartPoint.position;

        for (int i = 0; i < numberOfSegments; i++) {
            this.ropeSegments.Add(new RopeSegment(ropeStartPoint));
            ropeStartPoint.y -= segmentLength;
        }
    }

    // Update is called once per frame
    // void Update() {
    //     this.DrawRope();
    // }

    // private void FixedUpdate() {
    //     this.Simulate();
    // }

    public void SetStartPoint(Transform startPoint) {
        this.StartPoint = startPoint;
    }

    public void SetEndPoint(Transform endPoint) {
        this.EndPoint = endPoint;
    }

    public void UpdateEndPointPosition(Vector3 position) {
        EndPoint.position = position;
    }

    public void UpdateStartPointPosition(Vector3 position) {
        EndPoint.position = position;
    }

    public void Simulate() {
        // SIMULATION
        Vector2 forceGravity = new Vector2(0f, -0f);

        for (int i = 1; i < this.numberOfSegments; i++) {
            RopeSegment firstSegment = this.ropeSegments[i];
            Vector2 velocity = firstSegment.posNow - firstSegment.posOld;
            firstSegment.posOld = firstSegment.posNow;
            firstSegment.posNow += velocity;
            firstSegment.posNow += forceGravity * Time.fixedDeltaTime;
            this.ropeSegments[i] = firstSegment;
        }

        //CONSTRAINTS
        for (int i = 0; i < 50; i++) {
            this.ApplyConstraint();
        }
    }

    private void ApplyConstraint() {
        //Constrant to First Point 
        RopeSegment firstSegment = this.ropeSegments[0];
        firstSegment.posNow = this.StartPoint.position;
        this.ropeSegments[0] = firstSegment;


        //Constrant to Second Point 
        RopeSegment endSegment = this.ropeSegments[this.ropeSegments.Count - 1];
        endSegment.posNow = this.EndPoint.position;
        this.ropeSegments[this.ropeSegments.Count - 1] = endSegment;

        for (int i = 0; i < this.numberOfSegments - 1; i++) {
            RopeSegment firstSeg = this.ropeSegments[i];
            RopeSegment secondSeg = this.ropeSegments[i + 1];

            float dist = (firstSeg.posNow - secondSeg.posNow).magnitude;
            float error = Mathf.Abs(dist - this.segmentLength);
            Vector2 changeDir = Vector2.zero;

            if (dist > segmentLength) {
                changeDir = (firstSeg.posNow - secondSeg.posNow).normalized;
            } else if (dist < segmentLength) {
                changeDir = (secondSeg.posNow - firstSeg.posNow).normalized;
            }

            Vector2 changeAmount = changeDir * error;
            if (i != 0) {
                firstSeg.posNow -= changeAmount * 0.5f;
                this.ropeSegments[i] = firstSeg;
                secondSeg.posNow += changeAmount * 0.5f;
                this.ropeSegments[i + 1] = secondSeg;
            } else {
                secondSeg.posNow += changeAmount;
                this.ropeSegments[i + 1] = secondSeg;
            }
        }
    }

    public void SetRopeLength(float length) {
        segmentLength = (length / numberOfSegments);
    }

    public void Draw() {
        float lineWidth = this.lineWidth;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        Vector3[] ropePositions = new Vector3[this.numberOfSegments];
        for (int i = 0; i < this.numberOfSegments; i++) {
            ropePositions[i] = this.ropeSegments[i].posNow;
        }

        lineRenderer.positionCount = ropePositions.Length;
        lineRenderer.SetPositions(ropePositions);
    }

    public struct RopeSegment {
        public Vector2 posNow;
        public Vector2 posOld;

        public RopeSegment(Vector2 pos) {
            this.posNow = pos;
            this.posOld = pos;
        }
    }
}
