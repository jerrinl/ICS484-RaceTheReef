#pragma strict

private var normalColor : Color;
private var underWaterColor : Color;

function Start ()
{
	normalColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
	underWaterColor = new Color(0.22f, 0.65f, 0.77f, 0.5f);
}

function Update () 
{
	RenderSettings.fog = true;
	RenderSettings.fogColor = underWaterColor;
	RenderSettings.fogDensity = 0.05f;
}