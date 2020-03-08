A port of the [HADotNet](https://github.com/qJake/HADotNet/) library for [Unity](https://unity3d.com/).

HADotNet is a simple, straighforward .NET Standard library for the [Home Assistant](https://github.com/home-assistant/home-assistant) API.

**Dependencies**

This library depends on JSON.Net, a modified version can be installed from the [Asset store](https://assetstore.unity.com/packages/tools/input-management/json-net-for-unity-11347).

**Usage**

See the scripts folder for a sample usage to turn on/off a light.

1. Attach HomeAssistant to an Empty Gameobject in your scene
2. Configure the url & api key
3. Attach a HALight to one of your scene objects, be sure to also have a collider component configured
4. Provide the HA device id of your light entity & configure the interaction
5. Turn on/off your light
