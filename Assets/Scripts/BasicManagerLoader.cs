using System;
using UnityEngine;

public class BasicManagerLoader : MonoBehaviour
{
    public BasicManagerLoader bml;

    private void Start()
    {
        if (bml == null)
        {
            bml = this;
        }
        else
        {
            
        }
    }
}
