using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    Vector3 startposition = new Vector3(840.48f, 0.45f, 259.48f);
    bool stop = false;
    Vector3 force = new Vector3(0.0f, 0.0f, 50.0f);    // 力を設定
    Vector3 back = new Vector3(0.0f, 0.0f, -50.0f);    // 力を設定
    int cooltime = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("w"))
        {
            transform.position += transform.TransformDirection(Vector3.forward) * 50f * Time.deltaTime;
        }
        if (Input.GetKeyDown("s"))
        {
            transform.position += transform.TransformDirection(Vector3.back) * 50f * Time.deltaTime;
        }
        if (Input.GetKeyDown("a"))
        {
            Transform myTransform = this.transform;
            // ローカル座標を基準に、回転を取得
            Vector3 localAngle = myTransform.localEulerAngles;
            localAngle.y -= 90.0f; // ローカル座標を基準に、y軸を軸にした回転を10度に変更
            myTransform.localEulerAngles = localAngle; // 回転角度を設定
        }
        if (Input.GetKeyDown("d"))
        {
            Transform myTransform = this.transform;
            // ローカル座標を基準に、回転を取得
            Vector3 localAngle = myTransform.localEulerAngles;
            localAngle.y += 90.0f; // ローカル座標を基準に、y軸を軸にした回転を10度に変更
            myTransform.localEulerAngles = localAngle; // 回転角度を設定
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Teleporter"))
        {
            transform.position = startposition;
            Debug.Log("テレポーター");
        }
    }
}
