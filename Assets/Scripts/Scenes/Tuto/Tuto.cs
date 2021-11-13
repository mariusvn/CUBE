using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tuto : MonoBehaviour
{
    private static bool finishedTuto = false;
    private static int deaths = 0;
    [SerializeField]
    private GameController menu;
    [SerializeField]
    private CameraControl cam;
    [SerializeField]
    private SpawnPoint spStart;
    [SerializeField]
    private SpawnPoint spEnd;
    [SerializeField]
    private Player player;
    [SerializeField]
    private GameObject deadBody;
    [SerializeField]
    private Door lvlEnd;
    [SerializeField]
    private Door endLvlStart;
    [SerializeField]
    private Bridge bridge;

    public void FinishTuto()
    {
         finishedTuto = true;
    }

    public void OnPlayerDeath(int count)
    {
        if (deadBody != null)
        {
            var obj = Instantiate<GameObject>(deadBody, new Vector3(0.0f, 10.0f, 0.0f), Random.rotation);
            obj.GetComponentInChildren<TextMeshPro>().text = count.ToString();
        }

    }

    private IEnumerator Restart() 
    {
         yield return new WaitForSeconds(5);
         print("restart");
         SceneManager.LoadScene(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && deadBody != null && cam != null) {
                var obj = Instantiate<GameObject>(deadBody, other.transform.position, other.transform.rotation);
                if (obj) {
                    if (other.gameObject.TryGetComponent<Player>(out Player p)) {
                        var objRb = obj.GetComponent<Rigidbody>();
                        var otherRb = other.GetComponent<Rigidbody>();
                        objRb.velocity = otherRb.velocity;
                        obj.GetComponentInChildren<TextMeshPro>().text = p.DeathCount.ToString();
                    }
                    GameObject.Destroy(other);
                }
                cam.target = obj.transform;
                StartCoroutine(nameof(Restart));
        }
                finishedTuto = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!finishedTuto && player != null && bridge != null && cam != null && spStart != null)
        {
            menu.mainMenu.SetActive(true);
            lvlEnd.UseDoor();
            var obj = Instantiate<GameObject>(player.gameObject, spStart.SpawnAt, spStart.transform.rotation);
            cam.target = obj.transform;
            obj.GetComponent<Player>().onDeath.AddListener(bridge.DoCloseBridge);
        } else if (finishedTuto && menu != null && endLvlStart != null && player != null && spEnd != null && cam != null) {
            menu.mainMenu.SetActive(false);
            var obj = Instantiate<GameObject>(player.gameObject, spEnd.SpawnAt, spEnd.transform.rotation);
            var p = obj.GetComponent<Player>();
            p.onDeath.AddListener(OnPlayerDeath);
            p.hideId = false;
            p.SetIDText();
            for (int i = 0; i < p.DeathCount; ++i) {
                OnPlayerDeath(i);
            }
            cam.target = obj.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
