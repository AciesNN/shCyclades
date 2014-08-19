class MapEventer {

	protected UIMapPanel parentObject;
	public MapEventer(UIMapPanel parentObject) {
		this.parentObject = parentObject;
	}

	virtual public void OnClickCell(GridPosition cell) {
	}

	virtual public void OnHoverCell(GridPosition cell) {
	}

	virtual public void OnHoverOutCell(GridPosition cell) {
		parentObject.HighlightIsland(false);
	}

	virtual public void OnMapCancel() {
		parentObject.SetEventerType(MapEventerType.DEFAULT);
	}

}