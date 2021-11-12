using System.Collections;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _pans;
    [SerializeField]
    private Renderer _sidePanel;
    private bool _isOpen = false;
    private bool _finished = false;

    private IEnumerator OpenBridge()
    {
        var start = Time.time;
        var span = 0.2f;
        while (Time.time - start < span)
        {
            for (int i = 0; i < _pans.Length; ++i)
            {
                var rot = _pans[i].transform.rotation.eulerAngles;
                _pans[i].transform.rotation = Quaternion.Euler(rot.x, rot.y, -90.0f * (Time.time - start) / span);
            }
            yield return null;
        }
        _isOpen = true;
        for (int i = 0; i < _pans.Length; ++i)
        {
            var rot = _pans[i].transform.rotation.eulerAngles;

            _pans[i].transform.rotation = Quaternion.Euler(rot.x, rot.y, -91.0f);
        }
        yield return null;
    }

    private IEnumerator CloseBridge()
    {
        var start = Time.time;
        var span = 0.2f;
        if (_isOpen)
        {
            while (Time.time - start < span)
            {
                for (int i = 0; i < _pans.Length; ++i)
                {
                    var rot = _pans[i].transform.rotation.eulerAngles;
                    _pans[i].transform.rotation = Quaternion.Euler(rot.x, rot.y, 90.0f - 90.0f * (Time.time - start) / span);
                }
                yield return null;
            }
            for (int i = 0; i < _pans.Length; ++i)
            {
                var rot = _pans[i].transform.rotation.eulerAngles;
                _pans[i].transform.rotation = Quaternion.Euler(rot.x, rot.y, 0.0f);
            }
            _finished = true;
        }
        yield return null;
    }

    public void DoCloseBridge(int deathCount)
    {
        _ = StartCoroutine(nameof(CloseBridge));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_finished && other.CompareTag("Player"))
        {
            _ = StartCoroutine(nameof(OpenBridge));
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
