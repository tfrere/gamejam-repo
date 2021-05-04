using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.U2D;
using UnityEngine.U2D;

#if UNITY_EDITOR
public static class UpdateSpriteShapeMask
{
    [MenuItem("CONTEXT/SpriteShapeController/Update Sprite Mask")]
    static void UpdateShape(MenuCommand command)
    {
        SpriteShapeController mask = (SpriteShapeController)command.context;
        SpriteShapeController child = mask.transform.GetChild(0).GetComponent<SpriteShapeController>();
        EditorUtility.CopySerialized(mask, child);

        child.spriteShape = Resources.Load<SpriteShape>("BloodSplat");
    }



}
#endif