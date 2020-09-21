
using UnityEngine;

public class GLTranslationRotationScaleExample : MonoBehaviour
{
    public Material material = null;
    public int rectCount = 16;
    public float rectTranslateXFactor = 1;
    public float rectAngle = -45;
    public float rectScale = 2;
 
    void OnRenderObject()
    {
        material.SetPass(0);
        for (int i = 0; i < rectCount; i++)
        {
            //Remember state
            GL.PushMatrix();
            // Transform
            Matrix4x4 transformation = Matrix4x4.Translate(Vector3.right * i* rectTranslateXFactor);
            transformation *= Matrix4x4.Rotate(Quaternion.Euler(0, 0, rectAngle));
            transformation*=Matrix4x4.Scale(Vector3.one * rectScale);
            transformation *= Matrix4x4.Translate(new Vector3(0 - 0.5f, -1, 0));

            GL.MultMatrix(transformation);
            // Draw.
            GLRect(0.9f, 2);
            //Reaply last transformation state.
            GL.PopMatrix();
        }
    }
   void GLRect(float width, float height)
    {
        //Draw a quad
        GL.Begin(GL.QUADS);
        GL.Vertex3(0, 0, 0);
        GL.Vertex3(0, height, 0);
        GL.Vertex3(width, height, 0);
        GL.Vertex3(width, 0, 0);
        GL.End();
    }
}
