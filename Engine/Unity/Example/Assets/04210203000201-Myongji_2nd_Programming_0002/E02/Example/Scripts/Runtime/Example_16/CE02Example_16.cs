using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace E02Example
{
	/**
	 * Example 16
	 */
	public partial class CE02Example_16 : CManager_Scene
	{
		#region 함수
		/** 초기화 */
		public override void Awake()
		{
			base.Awake();
		}

		/** 플레이 버튼을 눌렀을 경우 */
		public void OnTouchBtn_Play()
		{
			CLoader_Scene.Inst.LoadScene(KDefine.G_N_SCENE_E02_EXAMPLE_17);
		}
		#endregion // 함수
	}
}
