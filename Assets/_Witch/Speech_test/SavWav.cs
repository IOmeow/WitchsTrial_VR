using UnityEngine;
using System.Collections;
using System.IO;
using System;

public static class SavWav {

	const int HEADER_SIZE = 44;

	public static bool Save(string filename, AudioClip clip) {
		if (!filename.ToLower().EndsWith(".wav")) {
			filename += ".wav";
		}

		var filepath = Path.Combine(Application.streamingAssetsPath, filename);

		var data = Convert(clip);

		using (var fileStream = CreateEmpty(filepath)) {
			WriteHeader(fileStream, clip);
			fileStream.Write(data, 0, data.Length);
		}
		
		return true;
	}

	static byte[] Convert(AudioClip clip) {
		var samples = new float[clip.samples];
		clip.GetData(samples, 0);
		var intData = new short[samples.Length];
		var bytesData = new byte[samples.Length * 2];

		const int rescaleFactor = 32767;
		for (int i = 0; i < samples.Length; i++) {
			intData[i] = (short)(samples[i] * rescaleFactor);
			var byteArr = new byte[2];
			byteArr = System.BitConverter.GetBytes(intData[i]);
			byteArr.CopyTo(bytesData, i * 2);
		}
		return bytesData;
	}

	static FileStream CreateEmpty(string filepath) {
		var fileStream = new FileStream(filepath, FileMode.Create);
		byte emptyByte = new byte();

		for (int i = 0; i < HEADER_SIZE; i++) {
			fileStream.WriteByte(emptyByte);
		}

		return fileStream;
	}

	static void WriteHeader(FileStream fileStream, AudioClip clip) {
		var hz = clip.frequency;
		var channels = clip.channels;
		var samples = clip.samples;

		fileStream.Seek(0, SeekOrigin.Begin);

		byte[] riff = System.Text.Encoding.UTF8.GetBytes("RIFF");
		fileStream.Write(riff, 0, 4);

		byte[] chunkSize = System.BitConverter.GetBytes(fileStream.Length - 8);
		fileStream.Write(chunkSize, 0, 4);

		byte[] wave = System.Text.Encoding.UTF8.GetBytes("WAVE");
		fileStream.Write(wave, 0, 4);

		byte[] fmt = System.Text.Encoding.UTF8.GetBytes("fmt ");
		fileStream.Write(fmt, 0, 4);

		byte[] subChunk1 = System.BitConverter.GetBytes(16);
		fileStream.Write(subChunk1, 0, 4);

		UInt16 one = 1;
		byte[] audioFormat = System.BitConverter.GetBytes(one);
		fileStream.Write(audioFormat, 0, 2);

		byte[] numChannels = System.BitConverter.GetBytes(channels);
		fileStream.Write(numChannels, 0, 2);

		byte[] sampleRate = System.BitConverter.GetBytes(hz);
		fileStream.Write(sampleRate, 0, 4);

		byte[] byteRate = System.BitConverter.GetBytes(hz * channels * 2);
		fileStream.Write(byteRate, 0, 4);

		UInt16 blockAlign = (ushort)(channels * 2);
		fileStream.Write(System.BitConverter.GetBytes(blockAlign), 0, 2);

		UInt16 bps = 16;
		byte[] bitsPerSample = System.BitConverter.GetBytes(bps);
		fileStream.Write(bitsPerSample, 0, 2);

		byte[] datastring = System.Text.Encoding.UTF8.GetBytes("data");
		fileStream.Write(datastring, 0, 4);

		byte[] subChunk2 = System.BitConverter.GetBytes(samples * channels * 2);
		fileStream.Write(subChunk2, 0, 4);
	}
}
