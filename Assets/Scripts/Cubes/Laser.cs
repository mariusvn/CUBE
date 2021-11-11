using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public int reflections;
	public float maxLength;
    public string mirrorTag;
    public string targetTag;
    public bool activated;

	private LineRenderer lineRenderer;
    private LineRenderer lineRendererReset;
    private Renderer renderer;
	private Ray ray;
	private RaycastHit hit;
	private Vector3 direction;

	private void Awake()
	{
		lineRenderer = GetComponent<LineRenderer>();
        lineRendererReset = lineRenderer;
        renderer = GetComponent<Renderer>();
        if (activated)
            Activate();
	}

	private void Update()
	{
        if (activated) {
		    ray = new Ray(transform.position, transform.right);

		    lineRenderer.positionCount = 1;
		    lineRenderer.SetPosition(0, transform.position);
		    float remainingLength = maxLength;

		    for (int i = 0; i < reflections; i++)
		    {
			    if(Physics.Raycast(ray.origin, ray.direction, out hit, remainingLength))
			    {
                    // if (hit.transform.tag == targetTag)
					// {
                    //     hit.transform.gameObject.Player.Die();
                    // }
				    lineRenderer.positionCount += 1;
				    lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
				    remainingLength -= Vector3.Distance(ray.origin, hit.point);
				    ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));
				    if (hit.collider.tag != mirrorTag)
					    break;
			    }
			    else
			    {
				    lineRenderer.positionCount += 1;
				    lineRenderer.SetPosition(lineRenderer.positionCount - 1, ray.origin + ray.direction * remainingLength);
			    }
		    }
        }
	}

    public void Activate()
    {
        renderer.material.SetColor("_Color", new Color(200f,0f,0f,255f));
        renderer.material.SetColor("_EmissionColor", new Color(0f,0f,0f,0f));
        activated = true;
    }

    public void Deactivate()
    {
        renderer.material.SetColor("_Color", new Color(79f,0f,0f,255f));
        renderer.material.SetColor("_EmissionColor", new Color(0f,0f,0f,0f));
        activated = false;
        lineRenderer = lineRendererReset;
    }
}
