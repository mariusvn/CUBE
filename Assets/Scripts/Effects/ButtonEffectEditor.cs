using UnityEditor;

[CustomEditor(typeof(ButtonEffect))]
[CanEditMultipleObjects]
public class ButtonEffectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        ButtonEffect effect = (ButtonEffect)target;
        EditorGUILayout.LabelField("IsActive: ", effect.IsActive + "");
    }

    public override bool RequiresConstantRepaint()
    {
        return true;
    }
}
