// Licensed under the Apache License, Version 2.0. 
// Copyright (c) Alex Lee. All rights reserved.

using System;

namespace SmartQuant.Charting
{
	public class PadRange
	{
		public double Min;
		public double Max;
		protected bool isValid;

		public bool IsValid
		{
			get
			{
				return this.isValid;
			}
		}

		public PadRange(double min, double max)
		{
			Min = min;
			Max = max;
			isValid = max - min > double.Epsilon;
		}
	}
}
