using UnityEngine;

namespace TheWasteland.SceneManaging
{
	public enum Scenes { Proto, MainMenu, Shop, Credits, Gameplay }
	public class SceneGetter : MonoBehaviour
	{
		public Scenes scene;
		public int level = -1;
	}
}