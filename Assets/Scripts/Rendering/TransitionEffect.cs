using System.Collections;
using UnityEngine;

[ExecuteInEditMode]
public class TransitionEffect : MonoBehaviour {

	public Material TransitionMaterial;

	public TextureVariable TransitionTexture;
	public bool ResetTransitionTexture;
	public Texture2D DefaultTransitionTexture;

	public AnimationCurve curve;
	public float transitionTime = 1f;

	public bool FlashBeforeTransition = false;
	[Range(0, 3)]
	public int Flashes = 2;
	public float FlashTime = 0.25f;
	public Color FlashColor = Color.white;

	private float elapsedTime = 0f;
	private Coroutine currentCoroutine;

	private void Awake(){
		if(ResetTransitionTexture){ TransitionTexture.Value = DefaultTransitionTexture; }

		if(TransitionMaterial==null){ return; }
		TransitionMaterial.SetFloat("_Cutoff", 0); // reset cutoff
		if(TransitionTexture==null){ return; }
		TransitionMaterial.SetTexture("_TransitionTex", TransitionTexture.Value);
	}

	void OnRenderImage(RenderTexture src, RenderTexture dest) {
		if(TransitionMaterial==null){ Graphics.Blit(src, dest); return; }
		Graphics.Blit(src, dest, TransitionMaterial);
	}

	public void OnTransitionTextureChanged(){
		TransitionMaterial.SetTexture("_TransitionTex", TransitionTexture.Value);
	}

	public void TransitionForward(){
		StopCoroutine();
		currentCoroutine = StartCoroutine(TransitionIn());
	}

	public void TransitionBackwards(){
		StopCoroutine();
		currentCoroutine = StartCoroutine(TransitionOut());
	}

	public IEnumerator TransitionIn(){
		if(FlashBeforeTransition && Flashes>0){ yield return Flash(); }

		elapsedTime = 0;
		TransitionMaterial.SetColor("_Color", Color.black);
		TransitionMaterial.SetFloat("_Cutoff", curve.Evaluate(0));
		TransitionMaterial.SetFloat("_Fade", 1);
		while(elapsedTime<=transitionTime){
			TransitionMaterial.SetFloat("_Cutoff", TransitionWithCurve(0, 1));
			elapsedTime+=Time.deltaTime;
			yield return null;
		}
		TransitionMaterial.SetFloat("_Cutoff", curve.Evaluate(1));
	}

	public IEnumerator TransitionOut(){
		elapsedTime = 0;
		while(elapsedTime<=transitionTime){
			TransitionMaterial.SetFloat("_Cutoff", TransitionWithCurve(1, 0));
			elapsedTime+=Time.deltaTime;
			yield return null;
		}
		TransitionMaterial.SetFloat("_Cutoff", curve.Evaluate(0));
	}

	private IEnumerator Flash(){
		TransitionMaterial.SetColor("_Color", FlashColor);
		for(int i=0; i<Flashes; ++i){
			// Fade in to FlashColor
			elapsedTime = 0;
			TransitionMaterial.SetFloat("_Cutoff", 1);
			while(elapsedTime<=FlashTime/2){
				TransitionMaterial.SetFloat("_Fade", Fade(0, 1));
				elapsedTime+=Time.deltaTime;
				yield return null;
			}
			TransitionMaterial.SetFloat("_Fade", 1);

			// Fade out from FlashColor
			elapsedTime = 0;
			while(elapsedTime<=FlashTime/2){
				TransitionMaterial.SetFloat("_Fade", Fade(1, 0));
				elapsedTime+=Time.deltaTime;
				yield return null;
			}
			TransitionMaterial.SetFloat("_Fade", 0);
		}
	}

	private float Fade(float start, float end){
		float delta = Mathf.Clamp01(elapsedTime/FlashTime);
		float amount = Mathf.Lerp(start, end, delta);
		return amount;
	}

	private float TransitionWithCurve(float start, float end){
		float delta = Mathf.Clamp01(elapsedTime/transitionTime);
		float curveValue = curve.Evaluate(delta);
		float amount = Mathf.Lerp(start, end, curveValue);
		return amount;
	}

	// Stops the current coroutine prematurely if it exists
	private void StopCoroutine(){
		if(currentCoroutine==null){ return; }
		StopCoroutine(currentCoroutine);
	}

}
