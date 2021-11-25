using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ILogic : MonoBehaviour
{
    [SerializeField] private GameObject ui;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private AudioClip win, lose, drumRoll, wtf;
    [SerializeField] private AudioSource sfx, music;
    private Camera main;
    private float timer;
    public void Flip() => StartCoroutine(IFlip());
    private bool Moving => rb.velocity.magnitude > 0f;

    private void Start()
    {
        main = Camera.main;
        Flip();
    }

    private void Update()
    {
        if (!Moving) timer += Time.deltaTime;
        main.transform.LookAt(rb.transform);
        main.fieldOfView = Mathf.Lerp(main.fieldOfView, Moving? 15f : 30f, Time.deltaTime * 10f);
    }

    private IEnumerator IFlip()
    {
        timer = 0;
        music.Stop();
        Play(drumRoll);
        ui.SetActive(false);
        rb.AddForce(Vector3.up * Random.Range(100f, 750f));
        yield return new WaitForSeconds(Random.Range(0.10f, 0.30f));
        rb.AddTorque(Vector3.forward * Random.Range(250f, 300f));
        yield return new WaitUntil(() => !Moving && timer >= 0.5f);
        sfx.Stop();
        yield return new WaitUntil(() => !Moving && timer >= 1f);

        switch (rb.rotation.eulerAngles.x)
        {
            case 90:
                Play(win);
                break;
            case 270:
                Play(lose);
                break;
            default:
                Play(wtf);
                break;
        }

        music.Play();
        ui.SetActive(true);
    }

    private void Play(AudioClip clip)
    {
        sfx.clip = clip;
        sfx.Play();
    }
}
