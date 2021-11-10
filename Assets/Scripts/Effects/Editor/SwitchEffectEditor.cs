using UnityEditor;

[CustomEditor(typeof(SwitchEffect))]
[CanEditMultipleObjects]
public class SwitchEffectEditor: Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        SwitchEffect effect = (SwitchEffect)target;
        EditorGUILayout.LabelField("IsActive: ", effect.isActive + "");
        
    }

    public override bool RequiresConstantRepaint()
    {
        return true;
    }
}