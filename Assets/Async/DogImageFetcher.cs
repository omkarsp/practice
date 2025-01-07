using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Threading.Tasks;
using System;
using System.Collections;
using Palmmedia.ReportGenerator.Core.Common;

[Serializable]
public class DogApiResponse
{
    public string message;
    public string status;
}

public class DogImageFetcher : MonoBehaviour
{
    [SerializeField] private RawImage displayImage;
    [SerializeField] private Button fetchButton;
    [SerializeField] private Text statusText;

    private const string DOG_API_URL = "https://dog.ceo/api/breeds/image/random";
    private bool isFetching = false;

    private void Start()
    {
        if (fetchButton != null)
        {
            fetchButton.onClick.AddListener(async () => { await FetchRandomDogImage(); });
        }

        statusText.text = "Press button to fetch a dog image!";
    }

    public async Task FetchRandomDogImage()
    {
        if (isFetching) return;

        try
        {
            isFetching = true;
            statusText.text = "Fetching dog image...";

            string imageUrl = await GetDogURL();

            Texture2D dogTexture = await DownloadImage(imageUrl);

            displayImage.texture = dogTexture ;
            statusText.text = "Fetch successful!";

        }
        catch (Exception e)
        {
            Debug.LogError($"Error fetching dog image: {e.Message}");
            statusText.text = "Error fetching image. Try again!";
        }
    }

    private async Task<string> GetDogURL()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(DOG_API_URL))
        {
            UnityWebRequestAsyncOperation operation = request.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();

            if (request.result != UnityWebRequest.Result.Success)
            {
                throw new Exception($"Failed to get dog image URL: {request.error}");
            }

            var response = JsonUtility.FromJson<DogApiResponse>(request.downloadHandler.text);

            return response.message;
        }
    }

    private async Task<Texture2D> DownloadImage(string url)
    {
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
        {

            var operation = request.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();

            if (request.result != UnityWebRequest.Result.Success)
            {
                throw new Exception($"Failed to download image: {request.error}");
            }

            return DownloadHandlerTexture.GetContent(request);
        }
    }
}
