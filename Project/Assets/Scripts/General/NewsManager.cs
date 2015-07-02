using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using PlayFab.ClientModels;

public class NewsManager : MonoBehaviour
{
     /// <summary>
    /// Text headline reference.
    /// </summary>
    [SerializeField]
    Text newsHeadline;

    /// <summary>
    /// Text body reference.
    /// </summary>
    [SerializeField]
    Text newsBody;

    /// <summary>
    /// Game news list, receved from PlayFab.
    /// </summary>
    List<TitleNewsItem> gameNews;

    /// <summary>
    /// Initialization method. Gets news from PlayFab, and shows a random one.
    /// </summary>
    void OnEnable()
    {
		gameNews = PlayFabManager.instance.gameNews;
		
		if (gameNews != null)
		{
			TitleNewsItem randomNews = gameNews[Random.Range(0, gameNews.Count)];
			
			newsHeadline.text = randomNews.Title;
			newsBody.text = randomNews.Body;
		}
    }
}