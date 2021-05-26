using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public static class NormalizeOrientation
{
    private static string[] ORIENTATION_NAMES = {"left", "right", "up", "down"};
    public static string Normalize(Vector3 vector) {
        float[] list = {
            Vector3.Angle(vector, Vector3.left),
            Vector3.Angle(vector, Vector3.right),
            Vector3.Angle(vector, Vector3.up),
            Vector3.Angle(vector, Vector3.down)
        };
        float minValue = list.Min();
        int index = list.ToList().IndexOf(minValue);
        return vector == Vector3.zero ? "none" : ORIENTATION_NAMES[index];
    }
}
