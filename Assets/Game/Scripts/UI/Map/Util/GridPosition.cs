using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	public GridPosition(List<object> coords) {
		this.x = (int)(long)coords[0];
		this.y = (int)(long)coords[1];
	}

	public GridPosition(List<long> coords) {
		this.x = (int)coords[0];
		this.y = (int)coords[1];
	}

	public GridPosition(List<int> coords) {
		this.x = coords[0];
		this.y = coords[1];
	}

	public GridPosition(float x, float y) {
		this.x = (int)System.Math.Floor(x);
		this.y = (int)System.Math.Floor(y);
	}

	public bool IsLessThanZero() {
		return x < 0 || y < 0;
	}

	public static GridPosition LessThanZero() {
		return new GridPosition(-1, -1);
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