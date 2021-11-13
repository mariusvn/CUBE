using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GoToSceneOnTrigger : MonoBehaviour
{
    public int sceneId = -1;
    [SerializeField]
    private UnityEvent onTrigger;

    private void OnTriggerEnter(Collider other)
    {
        onTrigger.Invoke();
        if (sceneId != -1)
        {
            SceneManager.LoadScene(sceneId);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
