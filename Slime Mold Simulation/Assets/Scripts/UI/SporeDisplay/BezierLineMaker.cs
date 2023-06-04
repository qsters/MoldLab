using System;
using UnityEngine;

namespace UI
{
    public class BezierLineMaker : MonoBehaviour
    {
        private static BezierCurve[] Curves;

        public static BezierLineMaker singleton;
        [SerializeField] private LineRenderer referenceLine;
        [SerializeField] private LineRenderer displayLine;
        [SerializeField] private GameObject testObject;
        [SerializeField] public int segmentCount;
        [SerializeField] public float smoothingLength;

        private void Awake()
        {
            if (singleton == null)
            {
                singleton = this;
            }
            else
            {
                Debug.LogWarning("BEZIERE LINE NOT SINGLETON");
            }
        }

        public void UpdateLine()
        {
            Curves = new BezierCurve[referenceLine.positionCount - 1];

            for (var i = 0; i < Curves.Length; i++) Curves[i] = new BezierCurve();

            for (var i = 0; i < Curves.Length; i++)
            {
                var position = referenceLine.GetPosition(i);
                var lastPosition = i == 0 ? referenceLine.GetPosition(0) : referenceLine.GetPosition(i - 1);
                var nextPosition = referenceLine.GetPosition(i + 1);

                var lastDirection = (position - lastPosition).normalized;
                var nextDirection = (nextPosition - position).normalized;

                var startTangent = (lastDirection + nextDirection) * smoothingLength;
                var endTangent = (nextDirection + lastDirection) * -1 * smoothingLength;

                Curves[i].Points[0] = position; // Start Position (P0)

                Curves[i].Points[1] = position + startTangent; // Start Tangent (P1)
                Curves[i].Points[2] = nextPosition + endTangent; // End Tangent (P2)
                Curves[i].Points[3] = nextPosition; // End Position (P3)
            }

            // Manually setting end handles
            Curves[Curves.Length - 1].Points[2] = Curves[Curves.Length - 1].Points[3];
            Curves[Curves.Length - 1].Points[1] = Curves[Curves.Length - 1].Points[3];

            // manually setting second curve
            Curves[1].Points[1] = Curves[1].Points[0];
            Curves[1].Points[2] = Curves[1].Points[3] + Vector3.up;

            // manually setting first curve
            // Curves[0].Points[1] = (Curves[0].Points[0] + Curves[0].Points[3]) / 2.0f + Vector3.right;
            // Curves[0].Points[2] = Curves[0].Points[3];


            var newLine = displayLine;

            referenceLine.enabled = false;
            newLine.positionCount = segmentCount * (Curves.Length - 1);

            // Handles Debugger
            // int colorIndex = 1;
            // for (int i = 0; i < 1; i++)
            // {
            //     for (int j = 0; j < 4; j++)
            //     {
            //         var circle = GameObject.Instantiate(testObject, Curves[i].Points[j] + referenceLine.gameObject.GetComponent<Transform>().position, Quaternion.Euler(0f, 0f, 0f));
            //         var color = Color.red;
            //         if (j == 0 || j == 3)
            //         {
            //             color = Color.blue;
            //         }
            //         else if (j == 1)
            //         {
            //             color = Color.green;
            //         }
            //
            //         if (i == 5 && j == 1)
            //         {
            //             color = Color.cyan;
            //         }
            //
            //         circle.GetComponent<SpriteRenderer>().color = color;
            //
            //         colorIndex++;
            //     }
            // }

            var index = 0;
            for (var i = 1; i < Curves.Length; i++)
            {
                var segments = Curves[i].GetSegments(segmentCount);

                for (var j = 0; j < segments.Length; j++)
                {
                    newLine.SetPosition(index, segments[j]);
                    index++;
                }
            }
        }

        private void EnsureCurvesMatchLineRendererPositions()
        {
            if (Curves == null || Curves.Length != referenceLine.positionCount - 1)
            {
                Curves = new BezierCurve[referenceLine.positionCount - 1];
                for (var i = 0; i < Curves.Length; i++) Curves[i] = new BezierCurve();
            }
        }
    }
}

// Taken from https://www.youtube.com/watch?v=u0yZb1xIyLA
[Serializable]
public class BezierCurve
{
    public Vector3[] Points;

    public BezierCurve()
    {
        Points = new Vector3[4];
    }

    public BezierCurve(Vector3[] Points)
    {
        this.Points = Points;
    }

    public Vector3 StartPosition => Points[0];

    public Vector3 EndPosition => Points[3];

    // Equations from: https://en.wikipedia.org/wiki/B%C3%A9zier_curve
    public Vector3 GetSegment(float Time)
    {
        Time = Mathf.Clamp01(Time);
        var time = 1 - Time;
        return time * time * time * Points[0]
               + 3 * time * time * Time * Points[1]
               + 3 * time * Time * Time * Points[2]
               + Time * Time * Time * Points[3];
    }

    public Vector3[] GetSegments(int Subdivisions)
    {
        var segments = new Vector3[Subdivisions];

        float time;
        for (var i = 0; i < Subdivisions; i++)
        {
            time = (float)i / Subdivisions;
            segments[i] = GetSegment(time);
        }

        return segments;
    }
}