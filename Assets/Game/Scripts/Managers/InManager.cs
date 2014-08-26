using UnityEngine;
using System.Collections;

public class InManager : Manager<InManager> {

	void Start() {
	}

	public void UpdateData() {
		//TODO тут можно проанализировать - есть ли данные, если есть то подготовлены ли для рассылки и т.д.

		//и потом - рассылка
		SendMessage("");
	}

}
