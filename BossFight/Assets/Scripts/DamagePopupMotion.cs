using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class DamagePopupMotion : MonoBehaviour
{
    [SerializeField] private float speed = 2f, timer = 2f;
    Vector3 lastPos;
    private float maxTimer;

    private void Start()
    {
        maxTimer = timer;
    }

    void Update()
    {
        lastPos = this.GetComponent<RectTransform>().localPosition;
        this.GetComponent<RectTransform>().localPosition = new Vector3(lastPos.x, lastPos.y + (speed * Time.deltaTime), lastPos.z);

        timer -= Time.deltaTime;

        this.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, (timer/maxTimer));

        if(timer <= 0.0f)
        {
            timerEnd();
        }
    }

    void timerEnd()
    {
        Destroy(this.gameObject);
    }
}
