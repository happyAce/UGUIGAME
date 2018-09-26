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
    GameObject hpbar;
    Vector3 offsetpos;
    

    // Use this for initialization
    void Start () {
        offsetpos = new Vector3(0,1.80f,0);
         
    }
    public void SetInit(float _current_value ,float _max_value,Transform _target)
    {
        current_value = _current_value;
        max_value = _max_value;
        target = _target;
        GameObject go = GameObject.Instantiate(Resources.Load<GameObject>("prefabs/hpbar")) as GameObject;

        if (_canvas == null)
            _canvas = GameObject.Find("Canvas").transform;
        go.transform.SetParent(_canvas, false);

        hpbar = go;
        hpSlider = go.GetComponent<Slider>();
        rectTrans = go.GetComponent<RectTransform>();

        hpSlider.value = current_value / max_value;
    }
    public void SetBarValue(float _current_value)
    {
        current_value = _current_value;
        hpSlider.value = current_value / max_value;
    }
    // Update is called once per frame
    Vector3 followPosition;
    void Update ()
    {
        if (target == null)
            return;
        Vector3 targetpos = target.transform.position;

        hpbar.transform.position = targetpos + offsetpos;

        if (current_value == 0)
            hpbar.SetActive(false);
    }
}
