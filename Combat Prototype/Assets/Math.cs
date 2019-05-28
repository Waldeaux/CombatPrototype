using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Math
{

    public static int ReturnSign(float input)
    {
        input = Mathf.Abs(input) / input;
        return (int)input;
    }
}
