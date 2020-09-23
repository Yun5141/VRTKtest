using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ScriptAdder : MonoBehaviourPun
{
    /*
    private void Awake() {
        
        int counter = 0;
        foreach(Transform child in this.GetComponentsInChildren<Transform>())
        {
            Debug.Log("counter"+counter);

            child.gameObject.AddComponent<PhotonView>();

            child.gameObject.AddComponent<PhotonTransformView>();
            PhotonTransformView c = child.gameObject.GetComponent<PhotonTransformView>();
            c.m_SynchronizePosition = true;
            c.m_SynchronizeRotation = true;

            PhotonView v = child.gameObject.GetPhotonView();
            if(v.ObservedComponents == null)
            v.ObservedComponents = new List<Component>();
            v.ObservedComponents.Add(c);
            
            counter++;
            Debug.Log(child.gameObject.name+"--------end add once");
        }
    }
    */
    private void Awake() {
        
        int counter = 0;
        foreach(Transform child in this.GetComponentsInChildren<Transform>())
        {
            Debug.Log("counter"+counter);

            child.gameObject.AddComponent<PhotonView>();

            child.gameObject.AddComponent<PhotonTransformView>();
            PhotonTransformView c = child.gameObject.GetComponent<PhotonTransformView>();
            c.m_SynchronizePosition = true;
            c.m_SynchronizeRotation = true;

            PhotonView v = child.gameObject.GetPhotonView();
            if(v.ObservedComponents == null)
            v.ObservedComponents = new List<Component>();
            v.ObservedComponents.Add(c);
            
            counter++;
            Debug.Log(child.gameObject.name+"--------end add once");

            break;  //only take for the root object
        }

        counter = 0;
        foreach(Transform child in this.GetComponentsInChildren<Transform>())
        {
            Debug.Log("counter"+counter);

            child.gameObject.AddComponent<PhotonView>();

            child.gameObject.AddComponent<PhotonTransformView>();
            PhotonTransformView c = child.gameObject.GetComponent<PhotonTransformView>();
            c.m_SynchronizePosition = true;
            c.m_SynchronizeRotation = true;

            PhotonView v = child.gameObject.GetPhotonView();
            if(v.ObservedComponents == null)
            v.ObservedComponents = new List<Component>();
            v.ObservedComponents.Add(c);
            
            counter++;
            Debug.Log(child.gameObject.name+"--------end add once");

            break;  //only take for the root object
        }

    }

}
