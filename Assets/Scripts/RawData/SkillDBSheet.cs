using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset(AssetPath = "Resources/DB", ExcelName = "SkillDBSheet")]
public class SkillDBSheet : ScriptableObject
{
	public List<SkillData> Entities; // Replace 'EntityType' to an actual type that is serializable.
}
