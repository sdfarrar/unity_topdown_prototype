using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using System;
using UnityEditor;
#endif

[RequireComponent(typeof(BoxCollider2D))]
public class TransitionTrigger : MonoBehaviour {

	public TextureVariable TransitionTexture;
	public TransitionPlateTemplate Template;

	public UnityEvent StepOnEvent;
	public UnityEvent StepOffEvent;

	private void Start(){
		SpriteRenderer renderer = GetComponent<SpriteRenderer>();
		if(renderer!=null){
			renderer.color = Template.SpriteColor;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(Template.TransitionTexture!=null){ TransitionTexture.Value = Template.TransitionTexture; }
		StepOnEvent.Invoke();
	}

	void OnTriggerExit2D(Collider2D other){
		StepOffEvent.Invoke();
	}

#if UNITY_EDITOR
	void OnDrawGizmos()	{
		try{
			DrawText(transform.position, Template.DebugText, Color.black);
		}catch(Exception e){}//ignore. Things explode mid compile sometimes
	}

	static public void DrawText(Vector3 worldPos, string text, Color? color = null) {
             UnityEditor.Handles.BeginGUI();
 
             var restoreColor = GUI.color;
 
             if (color.HasValue) GUI.color = color.Value;
             var view = UnityEditor.SceneView.currentDrawingSceneView;
             Vector3 screenPos = view.camera.WorldToScreenPoint(worldPos);
 
             if (screenPos.y < 0 || screenPos.y > Screen.height || screenPos.x < 0 || screenPos.x > Screen.width || screenPos.z < 0)
             {
                 GUI.color = restoreColor;
                 UnityEditor.Handles.EndGUI();
                 return;
             }
 
             Vector2 size = GUI.skin.label.CalcSize(new GUIContent(text));
             GUI.Label(new Rect(screenPos.x - (size.x / 2), -screenPos.y + view.position.height + 10, size.x, size.y), text);
             GUI.color = restoreColor;
             UnityEditor.Handles.EndGUI();
         }
#endif
}
