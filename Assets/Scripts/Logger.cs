using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public static class Logger
{

	private static readonly Dictionary<string, float> LoggerDictionary = new Dictionary<string, float>();

	public static void Write(string key)
	{
		var stackTrace = new StackTrace();
		var callerMethod = stackTrace.GetFrame(1).GetMethod();
		Debug.Assert(callerMethod.ReflectedType != null,
			"callerMethod.ReflectedType is not equal for null");
		var callerName = callerMethod.ReflectedType.Name;
		
		float triggerTime;

		var isAlreadyTriggeredByKey = LoggerDictionary.TryGetValue(key, out triggerTime);
	
		if (isAlreadyTriggeredByKey)
		{
			var endedTime = Time.realtimeSinceStartup;
			
			LoggerDictionary.Remove(key);

			Debug.Log(callerName + " >> " + callerMethod + " >> " + "(" + key + ") " + "has been completed at " + 
			          endedTime + "s, elapsed time " + (endedTime - triggerTime) + "s.");
		}
		else
		{
			triggerTime = Time.realtimeSinceStartup;
			LoggerDictionary.Add(key, Time.realtimeSinceStartup);
			Debug.Log(callerName + " >> " + callerMethod + " >> " + "(" + key + ") " + "has been triggered at "
			          + triggerTime + "s.");
		}
	}
	
}
