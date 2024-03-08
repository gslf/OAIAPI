using Promezio.OAIAPI.Capabilities.Audio;

namespace Promezio.OAIAPI.Test;

//
// !! WARNING !!
// 
// You need a valid OpenAI API key in your user-secrets to test
// the chat capabitilies. You can find more information in the
// README.md file.
//

[TestClass]
public class AudioTest : Test {

    [TestMethod]
    public async Task TestSpeech() {
        OAIAPI api = new OAIAPI(_apikey);
        bool result = await api.Audio.Speech(
            new Models(),"Hello Testers!", "../../../TestResources/speech.mp3");
        Assert.IsTrue(result);
    }

    [TestMethod]
    public async Task TestSpeechAllParams() {
        OAIAPI api = new OAIAPI(_apikey);
        bool result = await api.Audio.Speech(
            model: new Models(AvailableModels.TTS_1_HD),
            text: "Hello Testers!", 
            outputFile: "../../../TestResources/speech.aac",
            voice: new Voices(AvailableVoices.FABLE),
            responseFormat: new SpeechResponseFormats(AvailableSpeechFormats.AAC),
            speed: 2.5);
        Assert.IsTrue(result);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public async Task TestSpeechInvalidParams() {
        OAIAPI api = new OAIAPI(_apikey);
        bool result = await api.Audio.Speech(
            model: new Models(AvailableModels.TTS_1_HD),
            text: "Hello Testers!",
            outputFile: "../../../TestResources/speech.aac",
            voice: new Voices(AvailableVoices.FABLE),
            responseFormat: new SpeechResponseFormats(AvailableSpeechFormats.AAC),
            speed: 5);
    }

    [TestMethod]
    [ExpectedException(typeof(DirectoryNotFoundException))]
    public async Task TestSpeechInvalidFilePath() {
        OAIAPI api = new OAIAPI(_apikey);
        bool result = await api.Audio.Speech(new Capabilities.Audio.Models(), "Hello Learners!", "../../../TestResources/NotExist/speech.mp3");
    }

    [TestMethod]
    public async Task TestTranscription() {
        OAIAPI api = new OAIAPI(_apikey);
        TranscriptionObject? result = await api.Audio.Transcription(
            audioURL: "../../../TestResources/hello.mp3",
            prompt: "",
            temperature: 0);

        Assert.IsFalse(String.IsNullOrEmpty(result?.Text));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public async Task TestTranscriptionWrongParameter() {
        OAIAPI api = new OAIAPI(_apikey);
        TranscriptionObject? result = await api.Audio.Transcription(
            audioURL: "../../../TestResources/hello.mp3",
            prompt: "",
            temperature: -5);
    }

    [TestMethod]
    [ExpectedException(typeof(DirectoryNotFoundException))]
    public async Task TestTranscriptionWrongFilePath() {
        OAIAPI api = new OAIAPI(_apikey);
        TranscriptionObject? result = await api.Audio.Transcription(
            audioURL: "../../../TestResources/NOTEXIST/hello.mp3",
            prompt: "",
            temperature: 0);
    }

    [TestMethod]
    public async Task TestTranslation() {
        OAIAPI api = new OAIAPI(_apikey);
        string result = await api.Audio.Translation(
            audioURL: "../../../TestResources/hello.mp3",
            prompt: "",
            temperature: 0);

        Assert.IsFalse(String.IsNullOrEmpty(result));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public async Task TestTranslationWrongParameter() {
        OAIAPI api = new OAIAPI(_apikey);
        string result = await api.Audio.Translation(
            audioURL: "../../../TestResources/hello.mp3",
            prompt: "",
            temperature: -5);
    }

    [TestMethod]
    [ExpectedException(typeof(DirectoryNotFoundException))]
    public async Task TestTranslationWrongFilePath() {
        OAIAPI api = new OAIAPI(_apikey);
        string result = await api.Audio.Translation(
            audioURL: "../../../TestResources/NOTEXIST/hello.mp3",
            prompt: "",
            temperature: 0);
    }
}

