using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TravelMonkey.Services
{
    public class FaceService
    {
        private readonly IFaceClient _faceClient = new FaceClient(new ApiKeyServiceClientCredentials(ApiKeys.FaceApiKey))
        {
            Endpoint = ApiKeys.FaceEndpoint
        };

        public async Task<Emotion> DetectFaceEmotion(Stream photoStream)
        {
            try
            {
                var result = await _faceClient.Face.DetectWithStreamAsync(photoStream, returnFaceAttributes: new List<FaceAttributeType> { FaceAttributeType.Emotion }, recognitionModel: RecognitionModel.Recognition02);

                return result?.FirstOrDefault()?.FaceAttributes?.Emotion ?? new Emotion();
            }
            catch
            {
                return new Emotion();
            }
        }
    }
}