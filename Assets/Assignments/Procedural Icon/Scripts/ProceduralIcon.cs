
using UnityEngine;

public class ProceduralIcon : MonoBehaviour
{
    public Material material = null;

    [Header("Activation")]
    [Range(0,1)]public float activation = 0.5f;
    [Header("Valence")]
    [Range(0,1)]public float valence = 0.5f;


    // Farve fra sort/rød til blå/grøn
    //wavyness i nogle kasser
    //softness
    //random?
    void OnRenderObject()
    {
        material.SetPass(0);

        float res = Mathf.Lerp(5, 12, valence);
        float lineCount = Mathf.Lerp(2, 20, activation);
        //GL.MultMatrix(Matrix4x4.Translate(new Vector3(i, y, 0)));
        //Color color = Color.HSVToRGB(Random.value, 1, 1);
        Color a = Color.black;
        Color b = Color.yellow;
        Color c = Color.blue;
        Color d = Color.red;
        Color e = Color.magenta;
        Color f = Color.blue;

        Color activationColor = Color.Lerp(c, d, activation);
        Color valenceColor = Color.Lerp(a, b, valence);
        Color valenceColor2 = Color.Lerp(f, e, valence);

        GLBackground(res, valenceColor);

        GLLines(lineCount, activationColor);

        GLShapes(res, lineCount);

        GLCircle(res, lineCount,valenceColor2);




        void GLBackground(float poly, Color color)
        {
            GL.Begin(GL.TRIANGLE_STRIP);
            GL.Color(color);
            for (int i = 0; i < poly; i++)
            {
                float t = Mathf.InverseLerp(0, poly - 1, i); //Normalized value of i
                float angle = t * Mathf.PI * 2;
                float x = Mathf.Cos(angle) * 5;
                float y = Mathf.Sin(angle) * 5;

                GL.Vertex3(x, y, 0);
                GL.Vertex3(0, 0, 0);

            }
            GL.End();
        }
        void GLLines(float count, Color setColor)
        {
            if (activation > 0.5)
            {
                GL.Begin(GL.LINE_STRIP);
                GL.Color(setColor);
                for (int i = 0; i < count * 10; i++)
                {
                    float t = Mathf.InverseLerp(0, count - 1, i); //Normalized value of i
                    float circleAngle = t * Mathf.PI * 2;
                    float waveAngle = t * Mathf.PI * 2 * (valence);
                    float waveAmplitude = Mathf.Sin(waveAngle) * valence * 4;
                    float radius = activation * 2 + waveAmplitude;
                    float x = Mathf.Cos(circleAngle) * radius;
                    float y = Mathf.Sin(circleAngle) * radius;

                    GL.Vertex3(x, y, 0);
                }
                GL.End();


            }
            else
            {
                GL.Begin(GL.LINE_STRIP);
                GL.Color(setColor);
                for (int i = 0; i < count; i++)
                {
                    float t = Mathf.InverseLerp(count, 0, i); //Normalized value of i
                    float angle = t * Mathf.PI * 2;
                    float radius = 5 * t;
                    float x = Mathf.Cos(angle) * radius * t;
                    float y = Mathf.Sin(angle) * radius * t;

                    GL.Vertex3(x, y, 0);


                }
                GL.End();

                GL.Begin(GL.LINE_STRIP);
                GL.Color(setColor);
                for (int i = 0; i < count; i++)
                {
                    float t = Mathf.InverseLerp(count, 0, i); //Normalized value of i
                    float angle = t * Mathf.PI * 2;
                    float radius = 5 * t;
                    float x = Mathf.Cos(angle) * radius * -t;
                    float y = Mathf.Sin(angle) * radius * t;

                    GL.Vertex3(x, y, 0);


                }
                GL.End();
            }
        }
        void GLShapes(float amount, float noise)
        {
            for (int i = 0; i < amount/3; i++)
            {
                //Remember state
                GL.PushMatrix();
                // Transform
                Matrix4x4 transformation = Matrix4x4.Translate(Vector3.right *(valence+1)* i);
                transformation *= Matrix4x4.Rotate(Quaternion.Euler(0, 0, -noise));
                transformation *= Matrix4x4.Scale(Vector3.one * noise/20);
                transformation *= Matrix4x4.Translate(new Vector3(0-amount*2/10, -4, 0));

                GL.MultMatrix(transformation);
                // Draw.
                GL.Begin(GL.QUADS);
                GL.Color(Color.grey);
                GL.Vertex3(0, 0, 0);
                GL.Vertex3(0, activation+1, 0);
                GL.Vertex3(valence+1, activation+1, 0);
                GL.Vertex3(valence+1, 0, 0);
                GL.End();
                //Reaply last transformation state.
                GL.PopMatrix();
            }
        }
        void GLCircle(float amount, float random, Color setcolor)
        {
            GL.Begin(GL.LINE_STRIP);
            GL.Color(setcolor);
            for (int i = 0; i < amount*2; i++)
            {
                float t = Mathf.InverseLerp(0, random, i); //Normalized value of i
                float circleAngle = t * Mathf.PI * amount;
                float waveAngle = t * Mathf.PI * 2;
                float waveAmplitude = Mathf.Sin(waveAngle);
                float radius = 1 + waveAmplitude;
                float x = Mathf.Cos(circleAngle) * radius;
                float y = Mathf.Sin(circleAngle) * radius;

                GL.Vertex3(x, y-2, 0);
            }
            GL.End();
        }
        
    }
}
