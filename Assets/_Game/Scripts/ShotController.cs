using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    private Camera _camera;
    private bool _goalSearch;
    [SerializeField] private GameObject _vectorHit;
    [SerializeField] GameObject _rocket;

    private void Start()
	{
        _goalSearch = false;
        _camera = GetComponent<Camera>();
        EventController.Instance.OnSecondaryIndexTriggerRightHeld += OnHeld;
        EventController.Instance.OnSecondaryIndexTriggerRightReleased += OnReleased;
    }

	private void OnDisable()
	{
        EventController.Instance.OnSecondaryIndexTriggerRightHeld -= OnHeld;
        EventController.Instance.OnSecondaryIndexTriggerRightReleased -= OnReleased;
    }


	private void OnReleased() => GoalShot();

    private void OnHeld() => _goalSearch = true;

    private void GoalSearch()
    {
        var rayOrigin = new Ray(_camera.transform.position, _camera.transform.forward);
        var didHit = Physics.Raycast(rayOrigin, out RaycastHit hitInfo, Mathf.Infinity);

        if (!didHit) return;

        if (hitInfo.transform.gameObject.tag == "Goal")
        {
            Debug.Log("GoalSearch");
            _vectorHit = hitInfo.transform.gameObject;
            EventController.Instance.OnVibrationController();
        }
    }

    private void GoalShot()
    {
        if (_vectorHit != null)
        {
            Debug.LogError("GoalShot");
            _goalSearch = false;
            GameObject rocket = Instantiate(_rocket, transform.parent.position, Quaternion.identity);
            rocket.transform.SetParent(transform.parent);
            rocket.transform.localPosition = Vector3.zero;
            rocket.transform.localRotation = Quaternion.Euler(transform.parent.localRotation.x - 90, transform.parent.localRotation.y, transform.parent.localRotation.z);
            rocket.transform.parent = null;
            rocket.GetComponent<Rocket>().Fly(_vectorHit);
            _vectorHit = null;
        }
    }

    private void Update()
	{
        if(_goalSearch)
            GoalSearch();
    }
}
