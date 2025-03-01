﻿using System;

namespace PSTouchExpress.Behaviors
{
	[AttributeUsage(AttributeTargets.All)]
	public class PreserveAttribute : Attribute
	{
		public bool AllMembers;
		public bool Conditional;

		public PreserveAttribute(bool allMembers, bool conditional)
		{
			AllMembers = allMembers;
			Conditional = conditional;
		}

		public PreserveAttribute()
		{
		}
	}
}
