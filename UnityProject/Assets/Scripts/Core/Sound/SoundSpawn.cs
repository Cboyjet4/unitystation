﻿using System;
using UnityEngine;

public class SoundSpawn : MonoBehaviour
{
	public AudioSource audioSource;
	//We need to handle this manually to prevent multiple requests grabbing sound pool items in the same frame
	public bool isPlaying = false;
	private bool monitor = false;

	public void PlayOneShot()
	{
		if (audioSource == null) return;
		audioSource.PlayOneShot(audioSource.clip);
		WaitForPlayToFinish();
	}

	public void PlayNormally()
	{
		if (audioSource == null) return;
		audioSource.Play();
		WaitForPlayToFinish();
	}

	void WaitForPlayToFinish()
	{
		monitor = true;
	}

	private void OnEnable()
	{
		UpdateManager.Add(UpdateMe, 0.2f);
	}

	private void OnDisable()
	{
		UpdateManager.Remove(CallbackType.PERIODIC_UPDATE, UpdateMe);
	}

	void UpdateMe()
	{
		if (!monitor || audioSource == null) return;

		if (!audioSource.isPlaying)
		{
			isPlaying = false;
			monitor = false;
		}
	}
}
