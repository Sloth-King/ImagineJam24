using System.Collections;
using UnityEngine;

public class ScaleLerping : MonoBehaviour
{
    public Vector3 minScale;
    public Vector3 maxScale;
    public float scalingDuration;   
    public float scalingSpeed;

    IEnumerator Start(){
        yield return RepeatLerping(minScale, maxScale, scalingDuration);
    }


    IEnumerator RepeatLerping(Vector3 startScale, Vector3 endScale, float time){
        float t = 0.0f;
        float rate = (1f / time ) * scalingSpeed;
        while (t < 1f)
        {
            t += Time.deltaTime * rate;
            transform.localScale = Vector3.Lerp(startScale, endScale, t);
            yield return null;
        }
    }
}
