using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class UIAbsoluteTouchableLayout : UIAbstractTouchableContainer
{	
	public UIAbsoluteTouchableLayout() : base( UILayoutType.AbsoluteLayout, 0 )
	{
	}
	
	protected override void clipToBounds() {}
}
