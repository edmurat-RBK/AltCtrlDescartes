using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class returntomenu : MonoBehaviour
{
    public Animator transitionj1;
    public Animator transitionj2;
    // Start is called before the first frame update 
    void Start()
    {
        StartCoroutine(ExitCredits());
    }

    IEnumerator ExitCredits()
    {
        yield return new WaitForSeconds(10);
        transitionj1.SetTrigger("Changescene");
        transitionj2.SetTrigger("Changescene");
        SceneManager.LoadScene(0);
    }
}
