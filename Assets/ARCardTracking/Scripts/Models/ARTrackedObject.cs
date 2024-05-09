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
        float scaleFactor;
        public void AddTrackedObject(GameObject objectToInstantiate, Vector3 position, Quaternion rotation, float scaleFactor)
        {
            if(trackedObject != null)
            {
                Destroy(trackedObject);
            }
            trackedObject = Instantiate(objectToInstantiate, position, rotation, transform);
            trackedObject.transform.localScale = Vector3.one * scaleFactor;
        }

        public void UpdateTrackedObject(CardData cardData,ARTrackedImage trackedImage)
        {
            if(cardData.cardName == trackedImage.referenceImage.name)
            {
                if (trackedObject != null)
                {
                    if(!trackedObject.activeInHierarchy)
                        trackedObject.SetActive(true);
                    Vector3 trackedImagePosition = trackedImage.transform.position;
                    Quaternion trackedImageRotation = trackedImage.transform.rotation;
                
                    trackedObject.transform.position = Vector3.Lerp(trackedObject.transform.position, trackedImagePosition, Time.deltaTime * positionLerpSpeed);
                    trackedObject.transform.rotation = Quaternion.Lerp(trackedObject.transform.rotation, trackedImageRotation, Time.deltaTime * rotationLerpSpeed);
                    trackedObject.transform.localScale = Vector3.Lerp(trackedObject.transform.localScale, Vector3.one * scaleFactor, Time.deltaTime * scaleLerpSpeed);
                }
                else
                {
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

