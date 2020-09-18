using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLOscillationExample : MonoBehaviour
{
    public Material material;
    public int resolution = 64;
    [Header("Wave")]
    public float waveRevolutions = 6;
    [Header("circle")]
    [Range(1,10)]public float circleRadius = 2;
    [Header("Spiral")]
    public float spiralRadiusMax = 2;
    [Header("Wavecircle")]
    public float wavyCircleRadius = 3;
    public float wavyCircleWavyness = 64;
    public int wavyCircleWaveCount = 6;
    [Header("distortedCircle")]
    public float distortedCircleRadius = 10;
    public float distortedCircleRandomness = 0.5f;
    public int distortedCircleSeed = 0;


    // Update is called once per frame
    void OnRenderObject()
    {
        material.SetPass(0);

        //Draw a wave
        GL.Begin(GL.LINE_STRIP);
        for (int i = 0; i < resolution; i++)
        {
            float t = Mathf.InverseLerp(0, resolution - 1, i); //Normalized value of i
            float x = t * 10;
            float angle = t * Mathf.PI * 2 * waveRevolutions;
            float y = Mathf.Sin(angle) * 0.5f;

            GL.Vertex3(x, y, 0);
        }
        GL.End();

        //Draw a circle
        GL.Begin(GL.LINE_STRIP);
        for (int i = 0; i < resolution; i++)
        {
            float t = Mathf.InverseLerp(0, resolution - 1, i); //Normalized value of i
            float angle = t * Mathf.PI * 2;
            float x = Mathf.Cos(angle) * circleRadius;
            float y = Mathf.Sin(angle) * circleRadius;

            GL.Vertex3(x, y, 0);
        }
        GL.End();

        //Draw a Spiral
        GL.Begin(GL.LINE_STRIP);
        for (int i = 0; i < resolution; i++)
        {
            float t = Mathf.InverseLerp(0, resolution - 1, i); //Normalized value of i
            float angle = t * Mathf.PI * 2;
            float radius = spiralRadiusMax * t;
            float x = Mathf.Cos(angle) * radius*t;
            float y = Mathf.Sin(angle) * radius*t;

            GL.Vertex3(x, y, 0);

           
        }
        GL.End();
        //Draw a wavy circle
            GL.Begin(GL.LINE_STRIP);
            for (int i = 0; i < resolution; i++)
            {
            float t = Mathf.InverseLerp(0, resolution - 1, i); //Normalized value of i
            float circleAngle = t * Mathf.PI * 2;
            float waveAngle = t * Mathf.PI * 2 * wavyCircleWaveCount;
            float waveAmplitude = Mathf.Sin(waveAngle) * wavyCircleWavyness;
            float radius = wavyCircleRadius + waveAmplitude;
            float x = Mathf.Cos(circleAngle) * radius;
            float y = Mathf.Sin(circleAngle) * radius;

            GL.Vertex3(x, y, 0);
            }
            GL.End();


        //Draw a distorted circle
        GL.Begin(GL.LINE_STRIP);
        Random.InitState(distortedCircleSeed); //Ensure that random values are same across frames
        for (int i = 0; i < resolution; i++)
        {
            float t = Mathf.InverseLerp(0, resolution - 1, i); //Normalized value of i
            float angle = t * Mathf.PI * 2;
            float radius = distortedCircleRadius + Random.value * distortedCircleRandomness;
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            GL.Vertex3(x, y, 0);
        }
        GL.End();
    }
}
