using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

/**
 * 씬 로더
 */
public partial class CLoader_Scene : CSingleton<CLoader_Scene>
{
	#region 함수
	/** 씬을 로드한다 */
	public void LoadScene(string a_oName_Scene, bool a_bIsSingle = true)
	{
		SceneManager.LoadScene(a_oName_Scene,
			a_bIsSingle ? LoadSceneMode.Single : LoadSceneMode.Additive);
	}

	/** 씬을 로드한다 */
	public void LoadScene_Async(string a_oName_Scene,
		float a_fDelay, System.Action<AsyncOperation, bool> a_oCallback, bool a_bIsSingle = true)
	{
		StartCoroutine(this.CoLoadScene_Async(a_oName_Scene,
			a_fDelay, a_oCallback, a_bIsSingle));
	}

	/** 씬을 제거한다 */
	public void UnloadScene_Async(string a_oName_Scene,
		float a_fDelay, System.Action<AsyncOperation, bool> a_oCallback)
	{
		StartCoroutine(this.CoUnloadScene_Async(a_oName_Scene, a_fDelay, a_oCallback));
	}
	#endregion // 함수
}

/**
 * 씬 로더 - 코루틴
 */
public partial class CLoader_Scene : CSingleton<CLoader_Scene>
{
	#region 함수
	/** 씬을 로드한다 */
	private IEnumerator CoLoadScene_Async(string a_oName_Scene,
		float a_fDelay, System.Action<AsyncOperation, bool> a_oCallback, bool a_bIsSingle)
	{
		yield return new WaitForSeconds(a_fDelay);

		/*
		 * SceneManager.LoadSceneAsync 메서드란?
		 * - 씬을 비동기로 로드하는 역할을 수행하는 메서드이다. (즉, 해당 메서드를 활용하면 규모가
		 * 큰 씬을 비동기로 로드함으로서 플레이 경험을 좀 더 쾌적하게 만드는 것이 가능하다.)
		 * 
		 * 해당 메서드는 AsyncOperation 객체를 반환하며 해당 객체를 활용하면 씬의 로드 완료 여부를
		 * 검사하는 것이 가능하다. (즉, 해당 객체를 활용하면 비동기 씬의 로딩 상태를 화면 상에
		 * 출력하기 위한 여러 정보를 가져오는 것이 가능하다.)
		 */
		var oOperation_Async = SceneManager.LoadSceneAsync(a_oName_Scene,
			a_bIsSingle ? LoadSceneMode.Single : LoadSceneMode.Additive);

		yield return CManager_Task.Inst.CoWaitOperation_Async(oOperation_Async, a_oCallback);
	}

	/** 씬을 제거한다 */
	private IEnumerator CoUnloadScene_Async(string a_oName_Scene,
		float a_fDelay, System.Action<AsyncOperation, bool> a_oCallback)
	{
		yield return new WaitForSeconds(a_fDelay);

		var oOperation_Async = SceneManager.UnloadSceneAsync(a_oName_Scene,
			UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);

		yield return CManager_Task.Inst.CoWaitOperation_Async(oOperation_Async, a_oCallback);
	}
	#endregion // 함수
}