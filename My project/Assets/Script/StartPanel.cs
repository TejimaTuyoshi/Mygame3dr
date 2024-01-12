using UnityEngine;

public class StartPanel : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Panel()
    {
        Time.timeScale = 1.0f;
    }
}
