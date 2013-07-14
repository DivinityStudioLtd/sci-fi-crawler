using UnityEngine;
using System.Collections;

[AddComponentMenu("Utility/StringUtility")]
/// <summary>
/// String Utility.
/// A static util class who's purpose is too modify strings.
/// </summary>
public class StringUtility : Utility {
	
	static public bool IsString (string s) {
		if (s == null)
			return false;
		if (s.Length == 0)
			return false;
		return true;
	}
	
	static public string prepackDelimiter = " ";
	static public string postpackDelimiter = "`";
	static public void NumberOnly (ref string number) {
		for (int i = 0; i < number.Length; i++)
			if (number[i] != '0'
				&& number[i] != '1'
				&& number[i] != '2'
				&& number[i] != '3'
				&& number[i] != '4'
				&& number[i] != '5'
				&& number[i] != '6'
				&& number[i] != '7'
				&& number[i] != '8'
				&& number[i] != '9') {
					string newNumber;
					newNumber = number.Substring (0, i);
					newNumber += number.Substring (i + 1, number.Length - i - 1);
					number = newNumber;
				}
	}
	
	/// <summary>
	/// Compare the specified string1 and string2.
	/// </summary>
	/// <param name='string1'>
	/// If set to <c>true</c> string1.
	/// </param>
	/// <param name='string2'>
	/// If set to <c>true</c> string2.
	/// </param>
	static public bool Compare (string string1, string string2) { 
		return string.Equals (string1, string2); 
	}
	
	/// <summary>
	/// Replaces a string's spaces with slashes.
	/// </summary>
	/// <returns>
	/// The pack.
	/// </returns>
	/// <param name='s'>
	/// S.
	/// </param>
	static public string NetworkPack (string s) {
		return s.Replace (prepackDelimiter, postpackDelimiter);
	}
	
	/// <summary>
	/// Replaces a string's slashes with spaces.
	/// </summary>
	/// <returns>
	/// The unpack.
	/// </returns>
	/// <param name='s'>
	/// S.
	/// </param>
	static public string NetworkUnpack (string s) {
		return s.Replace (postpackDelimiter, prepackDelimiter);
	}
	
	static public char[] ToCharArray (string s) {
		char[] returnArray = new char[s.Length];
		
		for (int i = 0; i < returnArray.Length; i++) {
			returnArray[i] = s[i];
		}
		
		return returnArray;
	}
	
	static public string ToString (char[] cA) {
		string returnString = "";
		
		foreach (char c in cA) {
			returnString += c;	
			
		}
		
		return returnString;
	}
}
