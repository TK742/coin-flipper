using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ILogic : MonoBehaviour
{
    [SerializeField] private GameObject ui;
    [SerializeField] private Rigidbody rb;
    private Camera main;
    public void Flip() => StartCoroutine(IFlip());
    private bool Moving => rb.velocity.magnitude > 0f;

    private void Start()
    {
        main = Camera.main;
        Flip();
    }

    private void Update()
    {
        main.transform.LookAt(rb.transform);
        main.fieldOfView = Mathf.Lerp(main.fieldOfView, Moving? 15f : 30f, Time.deltaTime * 10f);
    }

    private IEnumerator IFlip()
    {
        ui.SetActive(false);
        rb.AddForce(Vector3.up * Random.Range(250f, 500f));
        yield return new WaitForSeconds(Random.Range(0.10f, 0.30f));
        rb.AddTorque(Vector3.forward * Random.Range(100f, 1000f));
        yield return new WaitUntil(() => !Moving);
        ui.SetActive(true);
    }
}
