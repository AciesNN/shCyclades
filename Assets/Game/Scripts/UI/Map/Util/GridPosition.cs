using UnityEngine;
using System.Collections;

public struct GridPosition {

	public int x;
	public int y;

	public long long_x {
		get {return (long)x;}
	}
	public long long_y {
		get {return (long)y;}
	}

	public GridPosition(int x, int y) {
		this.x = x;
		this.y = y;
	}

	public GridPosition(long x, long y) {
		this.x = (int)x;
		this.y = (int)y;
	}

	public GridPosition(float x, float y) {
		this.x = (int)System.Math.Floor(x);
		this.y = (int)System.Math.Floor(y);
	}

	public static GridPosition operator +(GridPosition g1, GridPosition g2)	{
    		return new GridPosition(g1.x + g2.x, g1.y + g2.y);
   	}
	
	public static GridPosition operator -(GridPosition g1, GridPosition g2) {
    		return new GridPosition(g1.x - g2.x, g1.y - g2.y);
   	}
		
	public static bool operator ==(GridPosition g1, GridPosition g2) {
   		if (g1.x != g2.x) return false; 
		if (g1.y != g2.y) return false;
		return true;
   	}
	
	public static bool operator !=(GridPosition g1, GridPosition g2) {
   		return !(g1 == g2);
   	}
	
 	public override bool Equals(object obj) {
		if (obj != null && obj is GridPosition) return this == (GridPosition)obj;
		return false;
   	}
	
	override public int GetHashCode() {
		return x << 16 + y;	
	}

	public override string ToString() {
		return "" + x + ":" + y;
	}
}