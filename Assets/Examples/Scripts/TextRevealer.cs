using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextRevealer : MonoBehaviour
{

    public GameObject textObject = null;

    // Start is called before the first frame update
    void Start()
    {
        textObject.SetActive(false);
    }

    // Update is called once per frame
    void OnMouseEnter()
    {
        textObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        textObject.SetActive(false);
    }
}
