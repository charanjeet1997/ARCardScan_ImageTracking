using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Game.ARCardTracking
{
    
    /// <summary>
    /// This class is responsible for managing the AR Tracked Object.
    /// It is responsible for instantiating, updating, pausing, and stopping the tracked object.
    /// </summary>
    public class ARTrackedObject : MonoBehaviour
    {
        public float positionLerpSpeed = 10f;
        public float rotationLerpSpeed = 10f;
        public float scaleLerpSpeed = 10f;
        
        GameObject trackedObject;
        Vector3 position;
        Quaternion rotation;
        public void AddTrackedObject(GameObject objectToInstantiate, Vector3 position, Quaternion rotation, float scaleFactor)
        {
            if(trackedObject != null)
            {
                Destroy(trackedObject);
            }
            trackedObject = Instantiate(objectToInstantiate, transform);
            trackedObject.transform.localPosition = Vector3.zero;
            trackedObject.transform.localRotation = Quaternion.identity;
           // transform.localScale = Vector3.one * scaleFactor;
        }

        public void UpdateTrackedObject(CardData cardData,ARTrackedImage trackedImage)
        {
            if(cardData.cardName == trackedImage.referenceImage.name)
            {
                Debug.Log("Updating Tracked Object 1");
                if (trackedObject != null)
                {
                    Debug.Log("Updating Tracked Object 2");
                    if(!trackedObject.activeInHierarchy)
                        trackedObject.SetActive(true);
                    Vector3 trackedImagePosition = trackedImage.transform.position;
                    Quaternion trackedImageRotation = trackedImage.transform.rotation;
                    Vector3 trackedImageScale = trackedImage.transform.localScale;
                    
                    Debug.Log("Updating Tracked Object 3");
                    transform.position = Vector3.MoveTowards(transform.position, trackedImagePosition, Time.deltaTime * positionLerpSpeed);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, trackedImageRotation, Time.deltaTime * rotationLerpSpeed);
                    transform.localScale = Vector3.MoveTowards(transform.localScale,  trackedImageScale * cardData.objectScale, Time.deltaTime * scaleLerpSpeed);
                    
                    // transform.position = trackedImagePosition;
                    // transform.rotation = trackedImageRotation;
                   // transform.localScale = trackedImageScale * scaleFactor;
                }
                else
                {
                    Debug.Log("Adding Tracked Object");
                    AddTrackedObject(cardData.objectToShow, trackedImage.transform.position, trackedImage.transform.rotation, cardData.objectScale);
                }
            }
            else
            {
                StopTrackingObject();
            }
           
        }
        
        public void PauseTrackingObject()
        {
            if(trackedObject != null)
                trackedObject.SetActive(false);
        }
        
        public void StopTrackingObject()
        {
            if(trackedObject != null)
                Destroy(trackedObject);
        }
    }
}

