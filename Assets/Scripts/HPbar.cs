using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HPbar : MonoBehaviour {

    private Slider hpSlider;
    private RectTransform rectTrans;
    private Transform _canvas;

    Transform target;
    float current_value;
    float max_value;

    // Use this for initialization
    void Start () {
        hpSlider = GetComponent<Slider>();
        rectTrans = GetComponent<RectTransform>();
        target = this.gameObject.transform;
    }
    public void SetInit(float _current_value ,float _max_value,Transform _target)
    {
        current_value = _current_value;
        max_value = _max_value;
        target = _target;
        hpSlider.value = current_value / max_value;
    }
    public void SetBarValue(float _current_value)
    {
        current_value = _current_value;
        hpSlider.value = current_value / max_value;
    }
    // Update is called once per frame
    void Update () {
        if (target == null)
            return;
        Vector3 targetpos = target.transform.position;
        targetpos.y += target.GetComponentInChildren<MeshRenderer>().bounds.size.y;
        Vector2 pos = RectTransformUtility.WorldToScreenPoint(Camera.main, targetpos);
        rectTrans.position = pos;
        if(_canvas == null)
            _canvas = GameObject.Find("Canvas").transform;
        this.transform.SetParent(_canvas, false);
    }
}
