using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{

    private Animator animatorTransition;
    void Start()
    {
        animatorTransition = GetComponent<Animator>();
    }
    
    public void LoadScene(string sceneName){
        StartCoroutine(Transition(sceneName));
    }

    IEnumerator Transition(string sceneName){
        animatorTransition.SetTrigger("end");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
    }

}
