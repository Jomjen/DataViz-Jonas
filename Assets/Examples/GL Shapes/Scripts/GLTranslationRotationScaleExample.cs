
using UnityEngine;

public class GLTranslationRotationScaleExample : MonoBehaviour
{
    public Material material = null;
    // Update is called once per frame
    void OnRenderObject()
    {
        material.SetPass(0);

        GLRect();
    }
   void GLRect()
    {
        //Draw a quad
        GL.Begin(GL.QUADS);
        GL.Vertex3(2, 0, 0);
        GL.Vertex3(2, 1, 0);
        GL.Vertex3(3, 1, 0);
        GL.Vertex3(3, 0, 0);
        GL.End();
    }
}
