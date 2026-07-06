using UnityEngine;
 
namespace Adventure {
	public class Breakable : MonoBehaviour
	{
		public void Break() {
			Destroy(gameObject);
		}
	}
}