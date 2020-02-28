using HomeAssistant;
using UnityEngine;

public class HALight : ActivationBehaviour
{
    public string entityId;
    public GameObject sceneLight;
    
    private LightEntity _lightEntity;

    void Start()
    {
        _lightEntity = new LightEntity(entityId);

        if (_lightEntity != null) // found?
        {
            // grab current state
            _lightEntity.IsOn(on =>
            {
                if(sceneLight != null)
                    sceneLight.SetActive(on);
            });
        }
    }

    public override void OnActivate()
    {
        if (_lightEntity != null)
        {
            _lightEntity.Toggle(on =>
            {
                if (sceneLight != null)
                {
                    sceneLight.SetActive(on);
                }
            });
        }
    }
}
