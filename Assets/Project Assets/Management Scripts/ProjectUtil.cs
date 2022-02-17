using UnityEngine;

/// <summary>
/// Holds some static utility functions.
/// </summary>
public static class ProjectUtil
{
	public static T RandomFrom<T>(T[] array) => array[Random.Range(0, array.Length)];
}